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
            char delimiter = ',';
            string changeDelimiterPattern = @"^\/\/(.+)?(\n|\r|\n\r)";
            if (Regex.IsMatch(input, changeDelimiterPattern))
            {
            }

            return SplitMultipleNumberStringToCalculateSum(delimiter, input);
        }

        public int SplitMultipleNumberStringToCalculateSum(char delimiter, string input)
        {
            char lineBreak = '\n';
            
            string delimiterPattern = $@"{delimiter}|\r|\n|\r\n";
            string singleNumberPattern = @"^(\d+)$";
            string multipleNumberPattern = $@"^(((\d+)({delimiterPattern}))+){{0,1}}(\d+)$";
            if (Regex.IsMatch(input, singleNumberPattern))
            {
                return int.Parse(input);
            }else if(Regex.IsMatch(input, multipleNumberPattern))
            {
                var newArray = input.Split(new Char [] {delimiter , lineBreak }, StringSplitOptions.None);
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

