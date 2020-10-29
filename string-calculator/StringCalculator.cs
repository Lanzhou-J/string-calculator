using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace string_calculator
{
    public class StringCalculator
    {
        private const string CustomDelimiterPattern = @"^\/\/(\D+?)(\n)"; //"//;\n1;2"
        private const string DelimiterPatternOfAnyLength = @"^\/\/(\[)(\D+?)(\])(\n)";//"//[***]\n1***2***3"
        private const string SingleNumberPattern = @"^(\d+)$";
        private const string NegativeValuePattern = @"-(\d+)";
        private const string ErrorMessage = "Negatives not allowed:";
        private const string LineBreak = "\n";
        
        public static int Add(string input)
        {
            var delimiter = ",";
            var stringNumbers = input;
            CheckNegativeNumbers(stringNumbers);
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
            
            var sum = SplitMultipleStringNumbersToCalculateSum(delimiter, stringNumbers);
            return sum;
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
            return Regex.IsMatch(input, DelimiterPatternOfAnyLength);
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



        private static int SplitMultipleStringNumbersToCalculateSum(string delimiter, string stringNumber)
        {
            
            var multipleNumberPattern = GetMultipleNumberPattern(delimiter);
            
            if (Regex.IsMatch(stringNumber, SingleNumberPattern))
            {
                return int.Parse(stringNumber);
            }
            else if(Regex.IsMatch(stringNumber, multipleNumberPattern))
            {
                var stringNumberArray = stringNumber.Split(new[] {delimiter , LineBreak }, StringSplitOptions.None);
                var sum = 0;
                foreach (var item in stringNumberArray)
                {
                    var number = int.Parse(item);
                    if (number<1000)
                    {
                        sum += number;
                    }
                }

                return sum;
            }

            return 0;
        }

        private static string GetMultipleNumberPattern(string delimiter)
        {
            var newDelimiter = delimiter;

            if (delimiter.Contains("*"))
            {
                newDelimiter = delimiter.Replace("*", @"\*");
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

