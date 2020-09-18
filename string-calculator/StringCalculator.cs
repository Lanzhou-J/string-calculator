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
            
            List<string> inputList = new List<string>();
            inputList.Add(input);

            var commaSeparateList = SeparateNumberStringToCreateANumberList(inputList, ",");
            var lineBreakSeparateList = SeparateNumberStringToCreateANumberList(commaSeparateList, "\n");
            
            List<int> newNumberList = new List<int>();
            foreach (var numberWord in lineBreakSeparateList)
            {
                Console.WriteLine(numberWord);
                newNumberList.Add(int.Parse(numberWord));
            }

            return newNumberList.Sum();
        }

        public List<string> SeparateNumberStringToCreateANumberList(List<string> inputString, string separator)
        {
            List<string> newStringList = new List<string>();
            foreach (var item in inputString)
            {
                if (item.Contains(separator))
                {
                    var newArray = item.Split(separator, StringSplitOptions.None);
                
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

