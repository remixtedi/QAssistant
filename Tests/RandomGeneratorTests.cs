using System.Linq;
using NUnit.Framework;
using QAssistant.Helpers;

namespace Tests
{
    internal class RandomGeneratorTests
    {
        private readonly RandomGenerator _generator;

        public RandomGeneratorTests()
        {
            _generator = new RandomGenerator();
        }

        [Test]
        public void RandomSymbols()
        {
            Assert.True(_generator.RandomSymbols(10).Any(char.IsSymbol));
        }

        [Test]
        public void GenerateOnly1RandomSymbol()
        {
            Assert.True(_generator.RandomSymbols(1).Length == 1);
        }

        [Test]
        public void ValueIsNotEmpty()
        {
            Assert.IsNotEmpty(_generator.RandomString());
        }

        [Test]
        public void LengthIsCorrect()
        {
            Assert.AreEqual(30, _generator.RandomString(30).Length);
        }

        [Test]
        public void ValueIsUppercase()
        {
            Assert.IsTrue(_generator.RandomString(30, false).Any(char.IsUpper));
        }

        [Test]
        public void ValueIsLowercase()
        {
            Assert.IsTrue(_generator.RandomString(30, true).Any(char.IsLower));
        }

        [Test]
        public void VRandomStringReturnsOnlyAlphabeticalCharacters()
        {
            Assert.IsFalse(_generator.RandomString(30).Any(char.IsNumber));
        }

        [Test]
        public void RandomNumberToStrinsIsNumeric()
        {
            Assert.True(_generator.RandomNumber().ToString().Any(char.IsNumber));
        }

        [Test]
        public void RandomNumberReturnsIntType()
        {
            Assert.AreEqual(_generator.RandomNumber().GetType(), typeof(int));
        }

        [Test]
        public void RandomNumberReturnsMin0Max999()
        {
            var value = _generator.RandomNumber();
            Assert.IsTrue(value >= 0 && value <= 999);
        }

        [Test]
        public void RandomNumberReturnsMin10Max20()
        {
            var value = _generator.RandomNumber(10, 20);
            Assert.IsTrue(value >= 10 && value <= 20);
        }

        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters()
        {
            Assert.True(_generator.RandomDigitsAndLetters(10, true).Any(char.IsLetterOrDigit));
        }

        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters2()
        {
            Assert.True(_generator.RandomDigitsAndLetters(10).Any(char.IsLetterOrDigit));
        }

        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters3()
        {
            Assert.True(_generator.RandomDigitsAndLetters(true).Any(c => char.IsLetterOrDigit(c) && char.IsLower(c)));
        }

        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters4()
        {
            Assert.True(_generator.RandomDigitsAndLetters(10).Length == 10);
        }

        [Test]
        public void GeneratesNumbersAndAlphabeticalCharacters5()
        {
            Assert.IsNotEmpty(_generator.RandomDigitsAndLetters());
        }
    }
}
