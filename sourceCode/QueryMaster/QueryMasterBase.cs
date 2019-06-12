
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
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace QueryMaster
{
    /// <summary>
    /// Provides clean up code.
    /// </summary>
   public class QueryMasterBase:IDisposable
    {
       /// <summary>
       /// To check whether dispose method was called before.
       /// </summary>
       protected bool IsDisposed { get; set; }
       /// <summary>
       /// Disposes all the resources used by this instance.
       /// </summary>
       /// <param name="disposing"></param>
       protected virtual void Dispose(bool disposing)
       {

       }
       /// <summary>
       /// Throw <see cref="ObjectDisposedException"/> if this instance is already disposed.
       /// </summary>
       protected void ThrowIfDisposed()
       {
           if (IsDisposed) throw new ObjectDisposedException(GetType().FullName);
       }

       internal T Invoke<T>(Func<T> method, int attempts, AttemptCallback attemptcallback,bool throwExceptions) where T : class
       {
           int AttemptCounter = 0;
           while (true)
           {
                try
                {
                    AttemptCounter++;
                    if (attemptcallback != null)
                    {
                        ThreadPool.QueueUserWorkItem(x => attemptcallback(AttemptCounter));
                    }
                    T reply = method();
                    return reply;
                }
                catch (Exception)
                {
                    if (AttemptCounter >= attempts)
                        if (throwExceptions)
                            throw;
                        else
                            return null;
                }

            }
       }

       /// <summary>
       /// Disposes all the resources used by this instance.
       /// </summary>
       public void Dispose()
       {
           if (IsDisposed)
               return;
           Dispose(true);
           GC.SuppressFinalize(this);
       }

       ~QueryMasterBase()
       {
           Dispose(false);
       }
    }
}
