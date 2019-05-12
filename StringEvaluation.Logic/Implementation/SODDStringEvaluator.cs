using System;
using System.Data;
using System.Globalization;
using StringEvaluation.Logic.Core;

namespace StringEvaluation.Logic.Implementation
{
    public class SODDStringEvaluator : IStringEvaluator
    {
        private readonly IStringEvaluationValidator _stringEvaluationValidator;

        public SODDStringEvaluator(IStringEvaluationValidator stringEvaluationValidator)
        {
            this._stringEvaluationValidator = stringEvaluationValidator;
        }
        public string GetResult(string input)
        {
            if(!_stringEvaluationValidator.Validate(input))
                return "String not valid";

            var output = new DataTable().Compute(input, null);
            double doubleOutput = Convert.ToDouble(output);
            
            return doubleOutput.ToString(CultureInfo.InvariantCulture);
        }
    }
}