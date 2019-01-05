using FizzBuzz;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class FizzBuzz
    {
        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "")]
        [TestCase(2, "Fizz")]
        [TestCase(3, "Buzz")]
        [TestCase(4, "Fizz")]
        [TestCase(5, "")]
        [TestCase(6, "FizzBuzz")]
        [TestCase(999, "Buzz")]
        [TestCase(1000, "Fizz")]

        public void FizzBuzz_ReturnsExpectedResult(int number, string expectedResult)
        {
            Assert.That(Methods.FizzBuzz(number), Is.EqualTo(expectedResult));
        }

        [TestCase(-1)]
        [TestCase(1001)]

        public void FizzBuzz_OutOfRange (int number)
        {
            Assert.Throws<ArgumentOutOfRangeException>(delegate { Methods.FizzBuzz(number); });
        }
    }
}