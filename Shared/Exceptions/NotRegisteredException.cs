using System.Globalization;

namespace Shared.Exceptions
{

    public class NotRegisteredException : Exception
    {
        public NotRegisteredException() : base() { }

        public NotRegisteredException(string message) : base(message) { }

        public NotRegisteredException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
