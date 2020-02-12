using System;

namespace JML.Utility.Exceptions
{
    public static class ThrowUtility
    {
        public static void NotNull<T>(this T obj) where T : class
        {
            if (obj == null)
            {
                throw new Exception("Value can't be null.");
            }
        }
    }
}
