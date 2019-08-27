using System;

namespace DT.Core.Exceptions
{
    public class ExistsException : Exception
    {
        public ExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) exists in system.")
        {
        }
    }
}
