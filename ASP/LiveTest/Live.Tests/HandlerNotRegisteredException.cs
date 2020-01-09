using System;

namespace Live.Tests
{
    public class HandlerNotRegisteredException : Exception
    {
        public HandlerNotRegisteredException() : base()
        {

        }

        public HandlerNotRegisteredException(string message) : base(message)
        {

        }

        public HandlerNotRegisteredException(string message, Exception innerException)
        : base(message, innerException)
        {

        }

        public HandlerNotRegisteredException(Type eventType)
        : base(eventType.ToString())
        {

        }
    }
}