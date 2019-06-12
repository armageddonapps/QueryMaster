
#region License
/*
Copyright (c) 2015 Betson Roy

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace QueryMaster.MasterServer
{
    /// <summary>
    /// Invoked when addressess are received from master server.
    /// </summary>
    /// <param name="batchInfo">Server endpoints</param>
    public delegate void BatchReceivedCallback(BatchInfo batchInfo);
    /// <summary>
    /// Represents Master Server.Provides method(s) to query master server.
    /// </summary>
    public class Server : QueryMasterBase
    {
        private static readonly int BufferSize = 1400;
        private IPEndPoint SeedEndpoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0), lastEndPoint = null;        
        private BatchReceivedCallback Callback;
        private ErrorCallback ErrorCallback;
        private AttemptCallback AttemptCallback;
        private IpFilter filter;
        private Socket Socket = null;
        private int BatchCount = 0;
        private CancellationTokenSource cts;
        private ConnectionInfo ConInfo;
        private Region region;
        private List<Task> TaskList = new List<Task>();
        /// <summary>
        /// Get region.
        /// </summary>
        public Region Region { get { return region; } }

        internal Server(ConnectionInfo conInfo,AttemptCallback attemptCallback)
        {
            ConInfo = conInfo;
            AttemptCallback = attemptCallback;
        }

        /// <summary>
        /// Starts receiving socket addresses of servers.
        /// </summary>
        /// <param name="region">The region of the world that you wish to find servers in.</param>
        /// <param name="callback">Called when a batch of Socket addresses are received.</param>
        /// <param name="filter">Used to set filter on the type of server required.</param>
        /// <param name="batchCount">Number of batches to fetch.-1 would return all addressess.(1 batch = 1 udppacket = 231 addressess).</param>
        /// <param name="errorCallback">Invoked in case of error.</param>
        public void GetAddresses(Region region, BatchReceivedCallback callback, IpFilter filter = null, int batchCount = 1, ErrorCallback errorCallback = null)
        {
            ThrowIfDisposed();
            StopReceiving();
            this.region = region;
            Callback = callback;
            ErrorCallback = errorCallback;
            BatchCount = batchCount == -1 ? int.MaxValue : batchCount;
            this.filter = filter;
            lastEndPoint = null;
            Initialize();
            TaskList.First().Start();
        }

        /// <summary>
        /// Provides next batch of addressess.
        /// </summary>
        /// <param name="batchCount">Number of batches to fetch.-1 would return all addressess.(1 batch = 1 udppacket = 231 addressess).</param>
        /// <param name="refresh">Whether to clear internal state and obtain addresses from start.</param>
        public void GetNextBatch(int batchCount=1,bool refresh=false)
        {
            ThrowIfDisposed();
            TaskList.Add(TaskList.Last().ContinueWith(x =>
             {
                 if (IsDisposed)
                     return;
                 if (Callback == null)
                     throw new InvalidOperationException("Call GetAddresses before calling this method.");
                 if (cts.IsCancellationRequested)
                     return;
                 if (refresh)
                     lastEndPoint = null;
                 else
                     if (lastEndPoint.Equals(SeedEndpoint))
                     {
                         if(cts !=null)
                            cts.Cancel();
                        throw new MasterServerException("Already received all the addresses.");
                     }
                 BatchCount = batchCount == -1 ? int.MaxValue : batchCount; 
                 StartReceiving();
             }));
        }

        private void Initialize()
        {
            Socket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
            Socket.SendTimeout = ConInfo.SendTimeout;
            Socket.ReceiveTimeout = ConInfo.ReceiveTimeout;
            Socket.Connect(ConInfo.EndPoint);
            if (cts != null)
                cts.Dispose();
            cts = new CancellationTokenSource();
            TaskList.Clear();
            TaskList.Add(new Task(StartReceiving, cts.Token));
        }

        private void StartReceiving()
        {
            byte[] msg = null, recvBytes = null;
            bool hasRecvMsg = true, IsNewMsg = true;
            int recv = 0, attemptCounter = 0, attempts = ConInfo.Retries + 1, batchCounter = 0;
            IPEndPoint endPoint;
            List<IPEndPoint> endPoints = null;
            bool isLastBatch = false;
            if (lastEndPoint == null)
                endPoint = SeedEndpoint;
            else
                endPoint = lastEndPoint;

            try
            {
                while (batchCounter < BatchCount)
                {
                    hasRecvMsg = false;
                    if (IsNewMsg)
                    {
                        msg = MasterUtil.BuildPacket(endPoint.ToString(), region , filter);
                        recvBytes = new byte[BufferSize];
                        IsNewMsg = false;
                    }
                    try
                    {                       
                        attemptCounter++;
                        if (AttemptCallback != null)
                            ThreadPool.QueueUserWorkItem(x => AttemptCallback(attemptCounter));
                        Socket.Send(msg);
                        recv = Socket.Receive(recvBytes);
                        recvBytes = recvBytes.Take(recv).ToArray();
                        hasRecvMsg = true;
                    }
                    catch (ObjectDisposedException)
                    {
                        return;
                    }
                    catch (SocketException ex)
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        if (ex.SocketErrorCode == SocketError.TimedOut)
                        {
                            hasRecvMsg = false;
                            if (attemptCounter >= attempts)
                                throw;
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (hasRecvMsg)
                    {
                        attemptCounter = 0;
                        batchCounter++;
                        endPoints = MasterUtil.ProcessPacket(recvBytes);
                        endPoint = endPoints.Last();
                        IsNewMsg = true;
                        lastEndPoint = endPoint;
                        if (endPoints.Last().Equals(SeedEndpoint))
                        {
                            endPoints.RemoveAt(endPoints.Count - 1);
                            isLastBatch = true;
                        }
                        Callback(new BatchInfo 
                        {
                            Region = Region, 
                            Source = ConInfo.EndPoint,
                            ReceivedEndpoints = new QueryMasterCollection<IPEndPoint>(endPoints),
                            IsLastBatch=isLastBatch
                        });
                        if (isLastBatch)
                        {
                            cts.Cancel();
                            break;
                        }
                        
                    }
                    cts.Token.ThrowIfCancellationRequested();
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                if (ErrorCallback != null)
                    ErrorCallback(ex);
            }

        }

        private void StopReceiving()
        {
            if (TaskList.Count != 0)
            {
                if(cts !=null)
                    cts.Cancel();
                if (Socket != null)
                    Socket.Dispose();
                Task.WaitAll(TaskList.ToArray());
                TaskList.Clear();
                cts = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    StopReceiving();
                    if (cts != null)
                    {
                        cts.Dispose();
                        cts = null;
                    }
                    TaskList.Clear();
                    Callback = null;
                    ErrorCallback = null;
                    AttemptCallback = null;
                }
                base.Dispose(disposing);
                IsDisposed = true;
            }
        }

    }
}
