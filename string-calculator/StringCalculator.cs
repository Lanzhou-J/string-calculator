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
            else if (input.Contains(","))
            {
                var newArray = input.Split(',', StringSplitOptions.None);
                 List<int> numberArray = new List<int>();
                foreach (var item in newArray)
                {
                    numberArray.Add(int.Parse(item));
                }

                return numberArray.Sum();
            }
            else
            {
                return int.Parse(input);
            }


        }
    }
}