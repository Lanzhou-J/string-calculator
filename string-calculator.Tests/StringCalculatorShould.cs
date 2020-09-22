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
        [InlineData("1,2,3", 6)]
        [InlineData("3,5,3,9", 20)]
        [InlineData("1,2\n3", 6)]
        [InlineData("3\n5\n3,9", 20)]
        public void ReturnSumBasedOnNInputStringsWithEitherCommaOrLineBreakDelimiter(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,calculator.Add(input));
        }


        
        //Add("//;\n1;2") > Returns 3  
        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void ReturnCorrectSumWhileSupportingDifferentDelimiters(string input, int expectedResult)
        {
            var calculator = new StringCalculator();
            Assert.Equal(expectedResult,calculator.Add(input));
        }
        
        [Fact]
        public void ReturnCorrectMessage_WhenAddingANegativeNumber()
        {
            var calculator = new StringCalculator();
 
            var ex = Assert.Throws<Exception>(() => calculator.Add("-1,2,-3"));
 
            Assert.Equal("Negatives not allowed: -1, -3", ex.Message);
        }
        
        [Fact]
        public void NumbersGreaterOrEqualToAThousandShouldBeIgnored()
        {
            var calculator = new StringCalculator();
 
            var result = calculator.Add("1000,1001,2");
 
            Assert.Equal(2, result);
        }
    }
}