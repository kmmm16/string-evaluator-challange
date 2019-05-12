using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using StringEvaluation.Logic.Core;
using StringEvaluation.Logic.Implementation;

namespace StringEvaluation.UI
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();

            var validator = _serviceProvider.GetService<IStringEvaluator>();
            
            if(validator is StringEvaluator)
                Console.WriteLine("You are using now String Evaluator by Krzysztof Majchrowicz");
            else
                Console.WriteLine("You are using now String Evaluator from .NET Core framework");

            string input;

            do
            {
                Console.WriteLine("Enter input (empty input exits program):");
                input = Console.ReadLine();

                if(!string.IsNullOrEmpty(input))
                    Console.WriteLine(validator.GetResult(input));
            }
            while (!string.IsNullOrEmpty(input));

            Console.WriteLine("Thank you, goodbye ;).");

            DisposeServices();
        }
        
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IStringEvaluator, StringEvaluator>();
            collection.AddSingleton<IStringEvaluationValidator, StringEvaluationValidator>();

            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
         if(_serviceProvider == null)
         {
             return;
         }
         if (_serviceProvider is IDisposable)
         {
             ((IDisposable)_serviceProvider).Dispose();
         }
        }
    }
}
