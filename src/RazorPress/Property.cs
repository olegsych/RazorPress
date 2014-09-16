using System;

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
    }
}
