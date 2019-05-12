
using NUnit.Framework;
using StringEvaluation.Logic.Core;
using StringEvaluation.Logic.Implementation;
using Tests;

namespace StringEvaluation.Tests
{
    [TestFixture]
    public class StringEvaluationTest : StandardStringEvaluatorTest
    {

        protected override IStringEvaluator CreateStringEvaluator()
        {
            return new StringEvaluator(new StringEvaluationValidator());
        }
    }
}