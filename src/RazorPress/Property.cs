using System;
using System.Linq.Expressions;

namespace RazorPress
{
    internal class Property
    {
        public static void Set<T>(ref T field, T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            field = value;
        }

        public static void Require<T>(T value, string propertyName) where T : class
        {
            if (value == null)
            {
                throw new InvalidOperationException(propertyName + " must be initialized.");
            }
        }
    }
}
