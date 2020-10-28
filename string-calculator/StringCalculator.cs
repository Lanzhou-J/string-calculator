using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace string_calculator
{
    public class StringCalculator
    {
        private const string CustomDelimiterPattern = @"^\/\/(\D+?)(\n)";
        private const string DelimiterPatternOfAnyLength = @"^\/\/(\[)(\D+?)(\])(\n)";
        private const string SingleNumberPattern = @"^(\d+)$";
        private const string NegativeValuePattern = @"-(\d+)";
        private const string ErrorMessage = "Negatives not allowed:";
        private const string LineBreak = "\n";
        
        public static int Add(string input)
        {
            string delimiter;
            var stringNumbers = input;

            // Input: "//[***]\n1***2***3"
            if (InputMatchesDelimiterPatternOfAnyLengthPattern(input))
            {
                delimiter = GetDelimiterForDelimiterPatternOfAnyLength(input); // ***
                stringNumbers = GetStringNumbersForDelimiterPatternOfAnyLength(input);// 1***2***3
            }
            else if (InputMatchesCustomDelimiterPattern(input))
            {
                var delimiterAndStringNumbers = GetDelimiterAndStringNumbersForCustomDelimiterPattern(input);
                delimiter = GetDelimiterForCustomDelimiterPattern(delimiterAndStringNumbers);
                stringNumbers = GetStringNumbersForCustomDelimiterPattern(delimiterAndStringNumbers);
            }
            else
            {
                delimiter = ",";
            }
            var sum = SplitMultipleStringNumbersToCalculateSum(delimiter, stringNumbers);
            return sum;
        }

        private static string[] GetDelimiterAndStringNumbersForCustomDelimiterPattern(string input)
        {
           return input.Split(new[] {'\n'});
        }

        private static string GetDelimiterForCustomDelimiterPattern( string[] delimiterAndStringNumbers)
        {
            return delimiterAndStringNumbers[0].Substring(2);
        }
        
        private static string GetStringNumbersForCustomDelimiterPattern( string[] delimiterAndStringNumbers)
        {
            return delimiterAndStringNumbers[1];
        }

        private static bool InputMatchesCustomDelimiterPattern(string input)
        {
            return Regex.IsMatch(input, CustomDelimiterPattern);
        }

        private static bool InputMatchesDelimiterPatternOfAnyLengthPattern(string input)
        {
            return Regex.IsMatch(input, DelimiterPatternOfAnyLength);
        }

        private static string GetDelimiterForDelimiterPatternOfAnyLength(string input)
        {
            
            var customDelimiterMarker = GetDelimiterAndStringNumbers(input)[0];
            var delimiterWithSquareBrackets = customDelimiterMarker.Substring(2);
            var delimiter = delimiterWithSquareBrackets.Substring(1, delimiterWithSquareBrackets.Length - 2);
            return delimiter;
        }
        
        private static string GetStringNumbersForDelimiterPatternOfAnyLength(string input)
        {
            
            var stringNumbers = GetDelimiterAndStringNumbers(input)[1];
            return stringNumbers;
        }

        private static string[] GetDelimiterAndStringNumbers(string input)
        {
            return input.Split(new[] {'\n'});
        }

        private static int SplitMultipleStringNumbersToCalculateSum(string delimiter, string stringNumber)
        {
            
            var newDelimiter = delimiter;

            if (delimiter.Contains("*"))
            {
               newDelimiter = delimiter.Replace("*", @"\*");
            }

            var delimiterPattern = $@"{newDelimiter}|\n";
            
            var multipleNumberPattern = $@"^(((\d+)({delimiterPattern}))+){{0,1}}(\d+)$";
            
            CheckNegativeNumbers(stringNumber);

            if (Regex.IsMatch(stringNumber, SingleNumberPattern))
            {
                return int.Parse(stringNumber);
            }
            else if(Regex.IsMatch(stringNumber, multipleNumberPattern))
            {
                var stringNumberArray = stringNumber.Split(new[] {delimiter , LineBreak }, StringSplitOptions.None);
                var numbers = new List<int>();
                foreach (var item in stringNumberArray)
                {
                    var number = int.Parse(item);
                    if (number<1000)
                    {
                        numbers.Add(number);
                    }
                }

                return numbers.Sum();
            }

            return 0;
        }

        private static void CheckNegativeNumbers(string stringNumber)
        {
            var negativeNumberList = new List<Match>();
            foreach (Match match in Regex.Matches(stringNumber, NegativeValuePattern))
            {
                negativeNumberList.Add(match);
            }

            if (negativeNumberList.Count < 1) return;
            var joined = string.Join(", ", negativeNumberList);
            throw new Exception(ErrorMessage + " " + joined);
        }
    }
}

