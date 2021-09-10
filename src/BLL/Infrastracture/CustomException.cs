using System;

namespace BLL.Infrastracture
{
    public class CustomException : Exception
    {
        public string Property { get; protected set; }
        public CustomException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
