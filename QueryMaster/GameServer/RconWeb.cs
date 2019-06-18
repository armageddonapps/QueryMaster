using System;
using System.Threading.Tasks;
using WebSocketSharp;

namespace QueryMaster.GameServer
{
    class RconWeb : Rcon
    {
        private const string NotSupportedMessage = "Method is not supported in WebRcon. Use SendCommand for execute some command.";

        private readonly WebSocket WebSocket;

        private readonly object LockObject = new object();

        private RconWeb(string connectionString)
        {
            WebSocket = new WebSocket(connectionString);
            WebSocket.Connect();
        }

        internal static Rcon Authorize(ConnectionInfo conInfo, string msg)
        {
            var rcon = new RconWeb($"ws://{conInfo.EndPoint.Address}:{conInfo.EndPoint.Port}/{msg}");

            if (rcon.WebSocket.Ping())
            {
                return rcon;
            }

            rcon.Dispose();

            return null;
        }

        public override string SendCommand(string cmd, bool isMultiPacketResponse = false)
        {
            lock (LockObject)
            {
                return SendCommandAsync(cmd).Result;
            }
        }

        [Obsolete]
        public override void AddlogAddress(string ip, ushort port)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        [Obsolete]
        public override void RemovelogAddress(string ip, ushort port)
        {
            throw new NotSupportedException(NotSupportedMessage);
        }

        private async Task<string> SendCommandAsync(string message)
        {
            var task = new TaskCompletionSource<string>();
            void EventHandler(object sender, MessageEventArgs args) => task.TrySetResult(args.Data);

            try
            {
                WebSocket.OnMessage += EventHandler;

                WebSocket.Send(message);

                return await task.Task;
            }
            finally
            {
                WebSocket.OnMessage -= EventHandler;
            }
        }
    }
}
