using System;

namespace UtmBuilder.Mobile.Exceptions
{
    public class InvalidUtmException  : Exception
    {
        public InvalidUtmException(
            string message = "Invalid UTM parameters") : base(message)
        {

        }

        public static void ThrowIfNull(
            string? item, 
            string message = "Invalid UTM parameters")
        {
            if (String.IsNullOrEmpty(item))
                throw new InvalidUtmException(message);
        }
    }
}
