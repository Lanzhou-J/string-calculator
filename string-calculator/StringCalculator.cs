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
            string singleNumberPattern = @"^(\d+)$";
            string multipleNumberPattern = @"^((\d+,)+){0,1}(\d+)$";
            if (Regex.IsMatch(input, singleNumberPattern))
            {
                return int.Parse(input);
            }else if(Regex.IsMatch(input, multipleNumberPattern))
            {
                var newArray = input.Split(",", StringSplitOptions.None);
                var newNumberList = new List<int>();
                foreach (var word in newArray)
                {
                    newNumberList.Add(int.Parse(word));
                }

                return newNumberList.Sum();
            }

            return 0;
        }


        // public List<string> SeparateNumberStringToCreateANumberList(List<string> inputString, string delimiter)
        // {
        //     List<string> newStringList = new List<string>();
        //     foreach (var item in inputString)
        //     {
        //         if (item.Contains(delimiter))
        //         {
        //             var newArray = item.Split(delimiter, StringSplitOptions.None);
        //         
        //             foreach (var word in newArray)
        //             {
        //                 newStringList.Add(word);
        //             }
        //         }
        //         else
        //         {
        //             newStringList.Add(item);
        //         }
        //     }
        //     return newStringList;
        // }
    }
}

