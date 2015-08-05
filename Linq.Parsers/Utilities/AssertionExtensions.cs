using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public static class AssertionExtensions
    {
        #region AssertNotNull

        public static void AssertNotNull<T>(this T instance, string message = "Expected a non-null object reference.")
            where T : class
        {
            if (instance == null)
                throw new NullReferenceException(message);
        }

        #endregion //AssertNotNull

        #region AssertNotNegative

        public static void AssertNotNegative(this int value, string message = "The specified value cannot be less than zero.")
        {
            if (value < 0)
                throw new InvalidOperationException(message);
        }

        #endregion //AssertNotNegative
    }
}
