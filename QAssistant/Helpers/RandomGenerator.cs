using System;
using System.Text;

namespace QAssistant.Helpers
{
    public class RandomGenerator
    {
        private const int DefaultLength = 10;
        private const int DefaultLetterLength = 1;
        private const int DefaultLettersLength = 10;
        private const int DefaultSymbolLength = 1;
        private const int DefaultRandomNumberMin = 0;
        private const int DefaultRandomNumberMax = 999;
        private readonly Random _random = new Random();

        private readonly char[] _symbols =
        {
            '!', '"', '#', '$', '%', '\'', '(', ')', '*', '+', ',', '.', '-', '_', '.', '/', '\\', ':', ';', '<',
            '>', '=', '?', '@', '^', '&', '[', ']', '{', '}', '`', '~'
        };

        public string RandomSymbols(int length)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = _symbols[_random.Next(0, _symbols.Length)];
                builder.Append(c);
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
            // var offset = lowerCase ? 'a' : 'A';
            // const int lettersOffset = 26;

            for (var i = 0; i < length; i++)
                if (_random.Next(3) == 1)
                    builder.Append(RandomSymbols(DefaultSymbolLength));
                else if (_random.Next(3) == 2)
                    builder.Append(RandomLetters(DefaultLetterLength, lowerCase));
                else
                    builder.Append(RandomNumber(0, 9));

            // var @char = (char) _random.Next(offset, offset + lettersOffset);
            // builder.Append(@char);

            return builder.ToString();
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

        public string RandomLetters(int length, bool lowerCase)
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

        public string RandomLetters(int length)
        {
            return RandomLetters(length, false);
        }

        public string RandomLetters(bool lowerCase)
        {
            return RandomLetters(DefaultLettersLength, lowerCase);
        }

        public string RandomLetter()
        {
            return RandomLetters(DefaultLetterLength, false);
        }

        public string RandomDigitsAndLetters(int length, bool lowerCase)
        {
            var builder = new StringBuilder(length);
            for (var i = 0; i < length; i++)
                if (_random.Next(2) == 1)
                    builder.Append(RandomLetter());
                else
                    builder.Append(RandomNumber(0, 9));

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomDigitsAndLetters(int length)
        {
            return RandomDigitsAndLetters(length, false);
        }

        public string RandomDigitsAndLetters(bool lowerCase)
        {
            return RandomDigitsAndLetters(DefaultLength, lowerCase);
        }

        public string RandomDigitsAndLetters()
        {
            return RandomDigitsAndLetters(DefaultLength, false);
        }
    }
}