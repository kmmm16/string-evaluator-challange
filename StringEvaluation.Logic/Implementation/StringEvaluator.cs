using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using StringEvaluation.Logic.Core;

namespace StringEvaluation.Logic.Implementation
{
    public class StringEvaluator : IStringEvaluator
    {
        private readonly IStringEvaluationValidator _stringEvaluationValidator;
        private List<double> _numbers = new List<double>();
        private List<string> _operators = new List<string>();
        public StringEvaluator(IStringEvaluationValidator stringEvaluationValidator)
        {
            this._stringEvaluationValidator = stringEvaluationValidator;
        }
        public string GetResult(string input)
        {
            Initiailize();

            if(!_stringEvaluationValidator.Validate(input))
                return "String not valid";           

            foreach (Match m in Regex.Matches(input, @"(\d+)([-+/*])?"))
            {
                if(!string.IsNullOrEmpty(m.Groups[2].ToString()))
                {
                    _numbers.Add(Convert.ToDouble(m.Groups[1].ToString()));
                    _operators.Add(m.Groups[2].ToString());
                    continue;
                }

                _numbers.Add(Convert.ToDouble(m.Groups[1].ToString()));
            }

            Evaluate();

            return _numbers[0].ToString(CultureInfo.InvariantCulture);
        }
        
        
        private void Evaluate()
        {
            for(int i = 0; i < this._operators.Count; i++)
            {
                string op = this._operators[i];
                if(op.Equals("*") || op.Equals("/"))
                {
                    PartEvaluate(i, op);
                    i--;
                }
            }

            for(int i = 0; i < this._operators.Count; i++)
            {
                string op = this._operators[i];
                if(op.Equals("+") || op.Equals("-"))
                {
                    PartEvaluate(i, op);
                    i--;
                }
            }
        }
        
        private void PartEvaluate(int operatorIndex, string operatorSymbol)
        {
            double firstNumber = this._numbers[operatorIndex];
            double secondNumber = this._numbers[operatorIndex + 1];

            double operationResult;

            if (operatorSymbol.Equals("*"))
                operationResult = firstNumber * secondNumber;
            else if (operatorSymbol.Equals("/"))
                operationResult = firstNumber / secondNumber;
            else if (operatorSymbol.Equals("-"))
                operationResult = firstNumber - secondNumber; 
            else
                operationResult = firstNumber + secondNumber;

            this._operators.RemoveAt(operatorIndex);
            this._numbers[operatorIndex] = operationResult;
            this._numbers.RemoveAt(operatorIndex + 1);
        }

        private void Initiailize()
        {
            this._operators = new List<string>();
            this._numbers = new List<double>();
        }
    }
}
