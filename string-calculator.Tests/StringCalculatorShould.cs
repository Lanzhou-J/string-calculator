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
            Assert.Equal(0,StringCalculator.Add(""));
        }
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        public void ReturnNumberCorrespondingToInputString(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,StringCalculator.Add(input));
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,5", 8)]
        [InlineData("1,2,3", 6)]
        [InlineData("3,5,3,9", 20)]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void ReturnSumBasedOnNInputStringsWithEitherCommaOrLineBreakDelimiter(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,StringCalculator.Add(input));
        }
        
        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void ReturnCorrectSumWhileSupportingDifferentDelimiters(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,StringCalculator.Add(input));
        }
        
        [Fact]
        public void ReturnCorrectMessage_WhenAddingANegativeNumber()
        {
            var calculator = new StringCalculator();
 
            var ex = Assert.Throws<Exception>(() => StringCalculator.Add("-1,2,-3"));
 
            Assert.Equal("Negatives not allowed: -1, -3", ex.Message);
        }
        
        [Fact]
        public void NumbersGreaterOrEqualToAThousandShouldBeIgnored()
        {
            var calculator = new StringCalculator();
 
            var result = StringCalculator.Add("1000,1001,2");
 
            Assert.Equal(2, result);
        }
        
        [Fact]
        public void ReturnCorrectSum_WhenDelimitersAreInSquareBrackets()
        {
            var calculator = new StringCalculator();
 
            var result = StringCalculator.Add("//[,,]\n1,,2,,3");
 
            Assert.Equal(6, result);
        }
        
        [Fact]
        public void ReturnCorrectSum_WhenDelimitersContainStarSign()
        {
            var calculator = new StringCalculator();
 
            var result = StringCalculator.Add("//[***]\n1***2***3");
 
            Assert.Equal(6, result);
        }
        
        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        [InlineData("//[,,,]\n1,,,2,,,3", 6)]
        [InlineData("//[@@@]\n1@@@2@@@3", 6)]
        [InlineData("//[###]\n1###2###3", 6)]
        // [InlineData("//[$$$$$]\n1$$$$$2$$$$$3", 6)]
        
        public void ReturnCorrectSum_WhenDelimitersContainMultipleSymbols(string input, int expectedOutput)
        {
            var calculator = new StringCalculator();
 
            var result = StringCalculator.Add(input);
 
            Assert.Equal(expectedOutput, result);
        }
        
        // [Fact]
        // public void ReturnCorrectSum_WhenThereAreMultipleDelimiters()
        // {
        //     var calculator = new StringCalculator();
        //
        //     var result = calculator.Add("//[*][%]\n1*2%3");
        //
        //     Assert.Equal(6, result);
        // }
    }
}