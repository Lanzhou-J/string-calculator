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
            if (Regex.IsMatch(input, singleNumberPattern))
            {
                return int.Parse(input);
            }else if(Regex.IsMatch(input, multipleNumberPattern))
            {
                var newArray = input.Split(new String [] {delimiter , lineBreak }, StringSplitOptions.None);
                var newNumberList = new List<int>();
                foreach (var word in newArray)
                {
                    newNumberList.Add(int.Parse(word));
                }

                return newNumberList.Sum();
            }

            return 0;
        }
    }
}

