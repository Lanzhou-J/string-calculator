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
        public int Add(string input)
        {
            string delimiter;
            var stringNumbers = input;
            string customDelimiterMarker;
            
            if (Regex.IsMatch(input, DelimiterPatternOfAnyLength))
            {
                var delimiterAndStringNumbers = input.Split(new[] {'\n'});
                customDelimiterMarker = delimiterAndStringNumbers[0];
                stringNumbers = delimiterAndStringNumbers[1];
                var delimiterWithSquareBrackets = customDelimiterMarker.Substring(2);
                delimiter = delimiterWithSquareBrackets.Substring(1, delimiterWithSquareBrackets.Length - 2);
            }
            else if (Regex.IsMatch(input, CustomDelimiterPattern)) 
            {
                var newArray = input.Split(new[] {'\n'});
                customDelimiterMarker = newArray[0];
                stringNumbers = newArray[1];
                delimiter = customDelimiterMarker.Substring(2);
            }
            else
            {
                delimiter = ",";
            }

            return SplitMultipleNumberStringToCalculateSum(delimiter, stringNumbers);
        }

        private static int SplitMultipleNumberStringToCalculateSum(string delimiter, string input)
        {
            const string lineBreak = "\n";
            var newDelimiter = delimiter;

            if (delimiter.Contains("*"))
            {
               newDelimiter = delimiter.Replace("*", @"\*");
            }

            var delimiterPattern = $@"{newDelimiter}|\n";
            const string singleNumberPattern = @"^(\d+)$";
            var multipleNumberPattern = $@"^(((\d+)({delimiterPattern}))+){{0,1}}(\d+)$";
            const string negativeValuePattern = @"-(\d+)";
            
            const string errorMessage = "Negatives not allowed:";
            var negativeNumberList = new List<Match>();
            foreach (Match match in Regex.Matches(input, negativeValuePattern))
            {
                negativeNumberList.Add(match);
            }

            if (negativeNumberList.Count >= 1)
            {
                var joined = string.Join(", ", negativeNumberList);
                throw new Exception(errorMessage+" "+joined);
            }

            if (Regex.IsMatch(input, singleNumberPattern))
            {
                return int.Parse(input);
            }
            else if(Regex.IsMatch(input, multipleNumberPattern))
            {
                var newArray = input.Split(new[] {delimiter , lineBreak }, StringSplitOptions.None);
                var newNumberList = new List<int>();
                foreach (var word in newArray)
                {
                    var wordNumber = int.Parse(word);
                    if (wordNumber<1000)
                    {
                        newNumberList.Add(wordNumber);
                    }
                }

                return newNumberList.Sum();
            }

            return 0;
        }
    }
}

