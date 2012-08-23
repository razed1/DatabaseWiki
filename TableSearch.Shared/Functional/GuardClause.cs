using System;

namespace TableSearch.Shared.Functional
{
    public class GuardClause
    {
        public static void IfFalseThenThrowArgumentException(Func<bool> method, string errorMessage)
        {
            if (!method())
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void IfIsLessThanZeroThrowArgumentException(int valueToCheck, string errorMessage)
        {
            if (valueToCheck < 0)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void IfIsNullOrEmptyThrowArgumentException(string stringToCheck, string errorMessage)
        {
            if (string.IsNullOrEmpty(stringToCheck))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void IfNullThrowArgumentException<T>(T itemToCheck, string errorMessage) where T : class
        {
            if (itemToCheck == null)
            {
                throw new ArgumentException(errorMessage);
            }
        }


    }
}
