using System;
using System.Text;

namespace QAssistant.Helpers
{
    public class RandomGenerator
    {
        private const int DefaultLength = 10;
        private const int DefaultRandomNumberMin = 0;
        private const int DefaultRandomNumberMax = 999;
        private readonly Random _random = new Random();


        public string RandomSymbols(int length)
        {
            var builder = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                var @char = (char) _random.Next(33, 47);
                builder.Append(@char);
            }

            return builder.ToString();
        }

        public int RandomNumber()
        {
            return _random.Next(DefaultRandomNumberMin, DefaultRandomNumberMax);
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int length, bool lowerCase)
        {
            var builder = new StringBuilder(length);
            var offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < length; i++)
            {
                var @char = (char) _random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomString(int length)
        {
            return RandomString(length, false);
        }

        public string RandomString(bool lowerCase)
        {
            return RandomString(DefaultLength, lowerCase);
        }

        public string RandomString()
        {
            return RandomString(DefaultLength, false);
        }
    }
}