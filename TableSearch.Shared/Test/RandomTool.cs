using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableSearch.Shared.Test
{
    public class RandomTool
    {
        #region Constructors

        static RandomTool()
        {
            RandomGenerator = new System.Random();
            FirstNameList = new List<string>
								{
									"Adam",
									"Bob",
									"Charlie",
									"Chuck",
									"Dana",
									"Dirk",
									"Ed",
									"Falcon",
									"Flex",
									"Flora",
									"Godfrey",
									"Hanna",
									"Harry",
									"Ingrid",
									"Max",
									"Tommy",
								};

            LastNameList = new List<string>
								{
									"Armstrong",
									"Allan",
									"Bigs",
									"Butler",
									"Croftshire",
									"Cutler",
									"Dance",
									"Edwards",
									"Finley",
									"Goodspeed",
									"Hall",
									"Ingots",
									"Strong",
								};
        }

        #endregion

        #region Fields

        private const int DefaultStringLength = 10;

        private static readonly IList<string> FirstNameList;
        private static readonly IList<string> LastNameList;
        private static readonly System.Random RandomGenerator;

        #endregion

        #region Support Methods

        private static string ShowHyphenIfNeeded(bool needed)
        {
            return needed ? "-" : string.Empty;
        }

        #endregion

        #region Methods

        public static char CreateAChar()
        {
            return (char)('A' + RandomGenerator.Next(0, 25));
        }

        public static decimal RandomCurrency()
        {
            return (decimal)(RandomInt32(1, 1000) + RandomInt32(0, 99) / 100.0);
        }

        public static DateTime RandomDate()
        {
            return new DateTime(RandomInt32(1970, 2000), RandomInt32(1, 12), RandomInt32(1, 28));
        }

        public static decimal RandomDecimal()
        {
            return RandomGenerator.Next();
        }

        public static String RandomEmail()
        {
            return RandomString(5) + "@" + RandomString(5) + ".com";
        }

        public static E RandomEnumeration<E>()
        {
            var enumerationToCheck = Activator.CreateInstance<E>();

            if (enumerationToCheck as Enum == null)
            {
                throw new InvalidOperationException();
            }

            var names = Enum.GetNames(typeof(E));

            if (names.Length > 0)
            {
                var indexToUse = RandomInt32(0, names.Length);
                enumerationToCheck = (E)Enum.Parse(typeof(E), names[indexToUse]);
            }

            return enumerationToCheck;
        }

        public static Int16 RandomInt16()
        {
            return (Int16)RandomGenerator.Next(Int16.MinValue, Int16.MaxValue);
        }

        public static Int32 RandomInt32()
        {
            return RandomGenerator.Next();
        }

        public static Int32 RandomInt32(Int32 minimum, Int32 maximum)
        {
            return RandomGenerator.Next(minimum, maximum);
        }

        public static Int64 RandomInt64()
        {
            return (Int64)RandomGenerator.Next(Int32.MinValue, Int32.MaxValue);
        }

        public static Int32 RandomNegativeInt32()
        {
            return RandomInt32(-999999, -1);
        }

        public static string RandomName()
        {
            var randomFirstNameIndex = RandomInt32(0, FirstNameList.Count - 1);
            var randomLastNameIndex = RandomInt32(0, LastNameList.Count - 1);

            return FirstNameList[randomFirstNameIndex] + " " + LastNameList[randomLastNameIndex];
        }

        public static string RandomParagraph(int length)
        {
            var paragraph = new StringBuilder();

            do
            {
                paragraph.Append(RandomString(RandomInt32(0, 20)) + " ");

            } while (paragraph.Length < length);

            if (paragraph.Length > length)
            {
                paragraph = paragraph.Remove(length, paragraph.Length - length - 1);
            }
            return paragraph.ToString();
        }

        public static string RandomSocialSecurityNumber(bool includeHyphens)
        {
            return
                (new StringBuilder())
                    .Append(RandomInt32(100, 999))
                    .Append(ShowHyphenIfNeeded(includeHyphens))
                    .Append(RandomInt32(10, 99))
                    .Append(ShowHyphenIfNeeded(includeHyphens))
                    .Append(RandomInt32(1000, 9999))
                    .ToString();
        }

        public static String RandomString()
        {
            return RandomString(DefaultStringLength);
        }

        public static String RandomString(Int32 length)
        {
            return new string(Enumerable.Range(0, length).Select(i => (char)('A' + RandomGenerator.Next(0, 25))).ToArray());
        }

        public static bool RandomBoolean()
        {
            return (RandomGenerator.NextDouble() > 0.5);
        }

        public static string RandomStringInCollection(IEnumerable<string> strings)
        {
            return (string)RandomObjectInCollection(strings);
        }

        private static object RandomObjectInCollection(IEnumerable<object> objects)
        {
            return objects.OrderBy(o => RandomString()).First();
        }

        #endregion

    }
}