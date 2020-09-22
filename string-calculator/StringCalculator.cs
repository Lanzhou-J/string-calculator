using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace string_calculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            string delimiter;
            string changeDelimiterPattern = @"^\/\/(.+?)(\n)";
            if (Regex.IsMatch(input, changeDelimiterPattern))
            {
                var newArray = input.Split(new[] {'\n'});
                var line1 = newArray[0];
                string restOfInput = newArray[1];
                delimiter = line1.Substring(2);
                input = restOfInput;
            }
            else
            {
                delimiter = ",";
            }

            return SplitMultipleNumberStringToCalculateSum(delimiter, input);
        }

        public int SplitMultipleNumberStringToCalculateSum(string delimiter, string input)
        {
            string lineBreak = "\n";

            string delimiterPattern = $@"{delimiter}|\n";
            string singleNumberPattern = @"^(\d+)$";
            string multipleNumberPattern = $@"^(((\d+)({delimiterPattern}))+){{0,1}}(\d+)$";
            string NegativeValuePattern = @"-(\d+)";
            
            string errorMessage = "Negatives not allowed:";
            List<Match> negativeNumberList = new List<Match>();
            foreach (Match match in Regex.Matches(input, NegativeValuePattern))
            {
                negativeNumberList.Add(match);
            }

            if (negativeNumberList.Count >= 1)
            {
                string joined = string.Join(", ", negativeNumberList);
                throw new Exception(errorMessage+" "+joined);
            }

            if (Regex.IsMatch(input, singleNumberPattern))
            {
                return int.Parse(input);
            }else if(Regex.IsMatch(input, multipleNumberPattern))
            {
                var newArray = input.Split(new String [] {delimiter , lineBreak }, StringSplitOptions.None);
                var newNumberList = new List<int>();
                int wordNumber;
                foreach (var word in newArray)
                {
                    wordNumber = int.Parse(word);
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

