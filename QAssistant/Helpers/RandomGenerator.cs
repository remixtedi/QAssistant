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

        /// <summary>
        ///     Selects a random symbols from the <see cref="_symbols" /> list.
        /// </summary>
        /// <param name="length">Length of value</param>
        /// <returns>Randomly selected symbols as <see cref="string" /> type value.</returns>
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

        /// <summary>
        ///     Generates a random integer from range of <see cref="DefaultRandomNumberMin" /> and
        ///     <see cref="DefaultRandomNumberMax" />
        /// </summary>
        /// <returns>Randomly generated <see cref="int" /> type value.</returns>
        public int RandomNumber()
        {
            return _random.Next(DefaultRandomNumberMin, DefaultRandomNumberMax);
        }

        /// <summary>
        ///     Generates a random integer from range of min and max.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Randomly generated <see cref="int" /> type value using the passed range.</returns>
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        ///     Selects a random letter, digit or symbol character.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value.</returns>
        public string RandomString(int length, bool lowerCase)
        {
            var builder = new StringBuilder(length);

            for (var i = 0; i < length; i++)
                if (_random.Next(3) == 1)
                    builder.Append(RandomSymbols(DefaultSymbolLength));
                else if (_random.Next(3) == 2)
                    builder.Append(RandomLetters(DefaultLetterLength, lowerCase));
                else
                    builder.Append(RandomNumber(0, 9));

            return builder.ToString();
        }

        /// <summary>
        ///     Selects a random uppercase letter, digit or symbol character.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <returns>Randomly generated <see cref="string" /> type value.</returns>
        public string RandomString(int length)
        {
            return RandomString(length, false);
        }

        /// <summary>
        ///     Selects a random letter, digit or symbol character with length of <see cref="DefaultLength" />.
        /// </summary>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value.</returns>
        public string RandomString(bool lowerCase)
        {
            return RandomString(DefaultLength, lowerCase);
        }

        /// <summary>
        ///     Selects a random uppercase letter, digit or symbol character with length of <see cref="DefaultLength" />.
        /// </summary>
        /// <returns>Randomly generated <see cref="string" /> type value.</returns>
        public string RandomString()
        {
            return RandomString(DefaultLength, false);
        }

        /// <summary>
        ///     Selects a random letter.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value of letters.</returns>
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

        /// <summary>
        ///     Selects a random uppercase letter.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <returns>Randomly generated <see cref="string" /> type value of letters.</returns>
        public string RandomLetters(int length)
        {
            return RandomLetters(length, false);
        }

        /// <summary>
        ///     Selects a random letter.
        /// </summary>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value of letters.</returns>
        public string RandomLetters(bool lowerCase)
        {
            return RandomLetters(DefaultLettersLength, lowerCase);
        }

        /// <summary>
        ///     Selects a random letter.
        /// </summary>
        /// <returns>Randomly generated <see cref="string" /> value.</returns>
        public string RandomLetter()
        {
            return RandomLetters(DefaultLetterLength, false);
        }

        /// <summary>
        ///     Selects a random letters and digits with passed length.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value of letters and numbers.</returns>
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

        /// <summary>
        ///     Selects a random uppercase letters and digits with passed length.
        /// </summary>
        /// <param name="length">Length of value.</param>
        /// <returns>Randomly generated <see cref="string" /> type value of uppercase letters and digits.</returns>
        public string RandomDigitsAndLetters(int length)
        {
            return RandomDigitsAndLetters(length, false);
        }

        /// <summary>
        ///     Selects a random letters and digits with length of <see cref="DefaultLength" />.
        /// </summary>
        /// <param name="lowerCase">
        ///     <see langword="true" /> if you want to generate lower case string, otherwise
        ///     <see langword="false" />
        /// </param>
        /// <returns>Randomly generated <see cref="string" /> type value of uppercase letters and digits.</returns>
        public string RandomDigitsAndLetters(bool lowerCase)
        {
            return RandomDigitsAndLetters(DefaultLength, lowerCase);
        }

        /// <summary>
        ///     Selects a random uppercase letters and digits with length of <see cref="DefaultLength" />.
        /// </summary>
        /// <returns>Randomly generated <see cref="string" /> type value of uppercase letters and digits.</returns>
        public string RandomDigitsAndLetters()
        {
            return RandomDigitsAndLetters(DefaultLength, false);
        }
    }
}