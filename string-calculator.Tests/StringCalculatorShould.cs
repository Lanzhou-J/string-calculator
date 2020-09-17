using System;
using Xunit;

namespace string_calculator.Tests
{
    public class StringCalculatorShould
    {
        [Fact]
        public void ReturnZeroFromEmptyString()
        {
            var calculator = new StringCalculator();
            Assert.Equal(0,calculator.Add(""));
        }
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        public void ReturnNumberCorrespondingToInputString(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,calculator.Add(input));
        }
        
        
        // Add("1,2") > Returns 3
        // Add("3,5") > Returns 8
        
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        public void ReturnSumBasedOnTwoInputStrings(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,calculator.Add(input));
        }
        
    }
}