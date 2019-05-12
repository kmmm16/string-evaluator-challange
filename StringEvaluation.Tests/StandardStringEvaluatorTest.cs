using System;
using System.Globalization;
using NUnit.Framework;
using StringEvaluation.Logic.Core;
using StringEvaluation.Logic.Implementation;

namespace Tests
{
    public abstract class StandardStringEvaluatorTest
    {
        protected IStringEvaluator _stringEvaluator;


        [SetUp]
        public void Setup()
        {
            this._stringEvaluator = CreateStringEvaluator();
        }
        protected abstract IStringEvaluator CreateStringEvaluator();

        [Test]
        [TestCase("2+2+2*2*2+4+4+4*4*4*4", "276")]
        [TestCase("8*6-12+65/5", "49")]
        [TestCase("1+1+1+1+1", "5")]
        [TestCase("1*1*1*1*0", "0")]
        [TestCase("1*1*1*1*1", "1")]
        [TestCase("1-69", "-68")]
        [TestCase("4+5*2", "14")]
        [TestCase("4+5/2", "6.5")]
        [TestCase("4+5/2-1", "5.5")]
        [TestCase("1", "1")]
        public void Calculate_When_InputFormatIsValid(string input, string expectedResult)
        {
            var result = _stringEvaluator.GetResult(input);
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [TestCase("random string", "String not valid")]
        [TestCase("random 1+6 string", "String not valid")]
        [TestCase("4//2", "String not valid")]
        [TestCase("4.69-1", "String not valid")]
        [TestCase("1 ", "String not valid")]
        [TestCase("1 * 7 ", "String not valid")]
        public void ReturnNotValidCommunicate_When_InputFormatIsNotValid(string input, string expectedResult)
        {
            var result = _stringEvaluator.GetResult(input);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("1/0")]
        [TestCase("1+4+1/0")]
        public void ReturnsInfinity_When_InInputUserPlaceDivisionByZero(string input)
        {
            var result = _stringEvaluator.GetResult(input);

            Assert.That(result, Is.EqualTo(double.PositiveInfinity.ToString(CultureInfo.InvariantCulture)));
        }
    }


}