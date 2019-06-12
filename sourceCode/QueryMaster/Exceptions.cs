
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
using System.Runtime.Serialization;

namespace QueryMaster
{
    /// <summary>
    /// The exception that is thrown by the QueryMaster library.
    /// </summary>
    [Serializable]
    public class QueryMasterException : Exception
    {
        public QueryMasterException() : base() { }
        public QueryMasterException(string message) : base(message) { }
        public QueryMasterException(string message, Exception innerException) : base(message, innerException) { }
        protected QueryMasterException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    /// <summary>
    /// The exception that is thrown when an invalid message header is received.
    /// </summary>
    [Serializable]
    public class InvalidHeaderException : QueryMasterException
    {
        public InvalidHeaderException() : base() { }
        public InvalidHeaderException(string message) : base(message) { }
        public InvalidHeaderException(string message, Exception innerException) : base(message, innerException) { }
        protected InvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// The exception that is thrown when an invalid packet is received.
    /// </summary>
    [Serializable]
    public class InvalidPacketException : QueryMasterException
    {
        public InvalidPacketException() : base() { }
        public InvalidPacketException(string message) : base(message) { }
        public InvalidPacketException(string message, Exception innerException) : base(message, innerException) { }
        protected InvalidPacketException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// The exception that is thrown when there is an error while parsing received packets.
    /// </summary>
    [Serializable]
    public class ParseException : QueryMasterException
    {
        public ParseException() : base() { }
        public ParseException(string message) : base(message) { }
        public ParseException(string message, Exception innerException) : base(message, innerException) { }
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Base for all GameServer Exception.
    /// </summary>
    [Serializable]
    public class GameServerException : QueryMasterException
    {
        public GameServerException() : base() { }
        public GameServerException(string message) : base(message) { }
        public GameServerException(string message, Exception innerException) : base(message, innerException) { }
        protected GameServerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Base for all MasterServer Exception.
    /// </summary>
    [Serializable]
    public class MasterServerException : QueryMasterException
    {
        public MasterServerException() : base() { }
        public MasterServerException(string message) : base(message) { }
        public MasterServerException(string message, Exception innerException) : base(message, innerException) { }
        protected MasterServerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    /// <summary>
    /// Base for all Steam Exception.
    /// </summary>
    [Serializable]
    public class SteamException : QueryMasterException
    {
        public SteamException() : base() { }
        public SteamException(string message) : base(message) { }
        public SteamException(string message, Exception innerException) : base(message, innerException) { }
        protected SteamException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }



}
