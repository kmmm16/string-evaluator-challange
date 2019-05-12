using System.Text.RegularExpressions;
using StringEvaluation.Logic.Core;

namespace StringEvaluation.Logic.Implementation
{
    public class StringEvaluationValidator : IStringEvaluationValidator
    {
        private const string _validationPattern = @"^((\d+)[*/+-]?)+$";
        public bool Validate(string input)
        {
            return Regex.IsMatch(input, _validationPattern);
        }
    }
}