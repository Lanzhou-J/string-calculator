using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace string_calculator
{
    public class StringCalculator
    {
        private const string CustomDelimiterPattern = @"^\/\/(\D+?)(\n)"; //Input: "//;\n1;2"
        private const string CustomDelimiterPatternOfAnyLength = @"^\/\/(\[)(\D+?)(\])(\n)";//Input: "//[***]\n1***2***3"
        private const string SingleNumberPattern = @"^(\d+)$";
        private const string NegativeValuePattern = @"-(\d+)";
        private const string ErrorMessage = "Negatives not allowed:";
        private const string LineBreak = "\n";
        
        public static int Add(string input)
        {
            CheckNegativeNumbers(input);
            var delimiter = ",";
            var stringNumbers = input;
            var sum = 0;
            
            // Input: "//[***]\n1***2***3"
            if (HasDelimiterPatternOfAnyLength(input))
            {
                delimiter = GetDelimiterForDelimiterPatternOfAnyLength(input); // ***
                stringNumbers = GetStringNumbersForDelimiterPatternOfAnyLength(input);// 1***2***3
            }
            else if (HasCustomDelimiterPattern(input))
            {
                var delimiterAndStringNumbers = GetDelimiterAndStringNumbers(input);
                delimiter = GetDelimiterForCustomDelimiterPattern(delimiterAndStringNumbers);
                stringNumbers = GetStringNumbersForCustomDelimiterPattern(delimiterAndStringNumbers);
            }
            
            if (MatchesSingleNumberPattern(stringNumbers))
            {
                sum = GetSingleNumberValue(stringNumbers);
                return sum;
            }
            
            if(MatchesMultipleNumberPattern(stringNumbers, delimiter))
            {
                sum = GetSumForMultipleStringNumbers(stringNumbers, delimiter);
                return sum;
            }
            return sum;
        }

        private static int GetSumForMultipleStringNumbers(string stringNumbers, string delimiter)
        {
            var sum = 0;
            var stringNumberArray = stringNumbers.Split(new[] {delimiter, LineBreak}, StringSplitOptions.None);
            foreach (var item in stringNumberArray)
            {
                var number = int.Parse(item);
                if (number < 1000)
                {
                    sum += number;
                }
            }
            return sum;
        }

        private static bool MatchesMultipleNumberPattern(string stringNumbers, string delimiter)
        {
            var multipleNumberPattern = GetMultipleNumberPattern(delimiter);
            return Regex.IsMatch(stringNumbers, multipleNumberPattern);
        }

        private static int GetSingleNumberValue(string stringNumbers)
        {
            return int.Parse(stringNumbers);
        }

        private static string[] GetDelimiterAndStringNumbers(string input)
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

        private static bool HasCustomDelimiterPattern(string input)
        {
            return Regex.IsMatch(input, CustomDelimiterPattern);
        }

        private static bool HasDelimiterPatternOfAnyLength(string input)
        {
            return Regex.IsMatch(input, CustomDelimiterPatternOfAnyLength);
        }

        private static string GetDelimiterForDelimiterPatternOfAnyLength(string input)
        {
            
            var customDelimiterWithMarker = GetDelimiterAndStringNumbers(input)[0];
            var delimiterWithSquareBrackets = customDelimiterWithMarker.Substring(2);
            var delimiter = delimiterWithSquareBrackets.Substring(1, delimiterWithSquareBrackets.Length - 2);
            return delimiter;
        }
        
        private static string GetStringNumbersForDelimiterPatternOfAnyLength(string input)
        {
            var stringNumbers = GetDelimiterAndStringNumbers(input)[1];
            return stringNumbers;
        }

        private static bool MatchesSingleNumberPattern(string stringNumber)
        {
            return Regex.IsMatch(stringNumber, SingleNumberPattern);
        }

        private static string GetMultipleNumberPattern(string delimiter)
        {
            var newDelimiter = delimiter;

            if (delimiter.Contains("*"))
            {
                newDelimiter = delimiter.Replace("*", @"\*");
            }

            if (delimiter.Contains("$"))
            {
                newDelimiter = delimiter.Replace("$", @"\$");
            }

            var delimiterPattern = $@"{newDelimiter}|\n";

            var multipleNumberPattern = $@"^(((\d+)({delimiterPattern}))+){{0,1}}(\d+)$";
            return multipleNumberPattern;
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

