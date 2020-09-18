using System;
using System.Collections.Generic;
using System.Linq;

namespace string_calculator
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            if (input == "")
            {
                return 0;
            }

            string delimiter=",";
            if (input.Contains("//"))
            {
                int indexOfSeparateLine = input.LastIndexOf("/");
                int indexOfLineBreak = input.IndexOf(@"\");
                int substringLength = indexOfLineBreak - indexOfSeparateLine-1;
                delimiter = input.Substring(indexOfSeparateLine+1, substringLength);
            }
            int indexOfN = input.IndexOf("n");
            string inputString = input.Substring(indexOfN + 1);

            List<string> inputList = new List<string>();
            inputList.Add(inputString);
            

            var commaSeparateList = SeparateNumberStringToCreateANumberList(inputList, delimiter);
            var lineBreakSeparateList = SeparateNumberStringToCreateANumberList(commaSeparateList, "\n");
            
            List<int> newNumberList = new List<int>();
            foreach (var numberWord in lineBreakSeparateList)
            {
                Console.WriteLine(numberWord);
                newNumberList.Add(int.Parse(numberWord));
            }

            return newNumberList.Sum();
        }

        public List<string> SeparateNumberStringToCreateANumberList(List<string> inputString, string delimiter)
        {
            List<string> newStringList = new List<string>();
            foreach (var item in inputString)
            {
                if (item.Contains(delimiter))
                {
                    var newArray = item.Split(delimiter, StringSplitOptions.None);
                
                    foreach (var word in newArray)
                    {
                        newStringList.Add(word);
                    }
                }
                else
                {
                    newStringList.Add(item);
                }
            }
            return newStringList;
        }
    }
}

