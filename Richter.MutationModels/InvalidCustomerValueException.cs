using System;
using System.Runtime.Serialization;

namespace Richter.MutationModels
{
    [Serializable]
    public class InvalidCustomerValueException : Exception
    {
        public InvalidCustomerValueException()
        {
        }

        public InvalidCustomerValueException(string message) : base(message)
        {
        }

        public InvalidCustomerValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCustomerValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}