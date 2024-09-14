using StringCalculatorApp;

namespace StringCalculatorAppTests
{
    public class StringCalculatorTests : IClassFixture<StringCalculatorTestFixture>
    {        
        private readonly StringCalculator _stringCalculator;

        public StringCalculatorTests(StringCalculatorTestFixture fixture)
        {
            _stringCalculator = fixture.Calculator;
        }

        [Fact]
        public void Given_Add_Is_Called_Then_Returns_Value_Of_Type_Int()
        {
            var actualResult  = _stringCalculator.Add("");
            Assert.IsType<int>(actualResult);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("   ", 0)] //This will also covers string with white spaces
        public void Given_Add_Is_Called_When_Empty_String_Is_Passed_Then_Returns_Zero(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);            
            Assert.Equal(expectedResult, actualResult);         
        }

        [Theory]
        [InlineData("1",1)]
        [InlineData("2", 2)] 
        public void Given_Add_Is_Called_When_Single_Number_Is_Passed_Then_Returns_That_Number_Only(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);            
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("6,4", 10)]
        public void Given_Add_Is_Called_When_Two_Numbers_Are_Passed_Containing_Comma_Then_Returns_Sum_Of_them(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);           
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("6,4,10,10,5,10", 45)]
        public void Given_Add_Is_Called_When_Unlimited_Numbers_Are_Passed_Containing_Comma_Then_Returns_Sum_Of_them(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("1\n2", 3)]        
        public void Given_Add_Is_Called_When_Numbers_Are_Passed_Containing_NewLine_Then_Returns_Sum_Of_them(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        public void Given_Add_Is_Called_When_Numbers_Are_Passed_Containing_NewLine_And_Comma_Then_Returns_Sum_Of_them(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("//;\n8", 8)]
        public void Given_Add_Is_Called_When_Single_Number_Is_Passed_Containing_Custom_Delimeter_Then_Returns_That_Number_Only(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        public void Given_Add_Is_Called_When_Multiple_Numbers_Are_Passed_Containing_Custom_Delimeter_Then_Returns_Sum_Of_Them(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        
        [Theory]
        [InlineData("-1")]
        public void Given_Add_Is_Called_When_Negative_Number_Is_Passed_Throws_An_Exception(string value)
        {
            void action() => _stringCalculator.Add(value);
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData("//|\n-10", "Negatives Not Allowed")]
        public void Given_Add_Is_Called_When_Negative_Number_Is_Passed_Then_Throws_An_Exception_With_Message(string value, string expectedMessage)
        {
            void action() => _stringCalculator.Add(value);
            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Contains(expectedMessage, ex.Message);
        }

        [Theory]
        [InlineData("//|\n-2|-3", "-2,-3")]
        public void Given_Add_Is_Called_When_Negative_Numbers_Are_Passed_Then_Throws_An_Exception_With_Message_Containing_Naegative_Numbers(string value, string expectedResult)
        {
            void action() => _stringCalculator.Add(value);
            var ex = Assert.Throws<ArgumentException>(action);
            var negativeNumbers = ex.Message.Split('-');
            Assert.Contains(expectedResult, ex.Message);
        }

        [Theory]
        [InlineData("//|\n1001|6|4", 10)]
        public void Given_Add_Is_Called_When_Number_Is_Passed_Containing_A_Number_Larger_Than_Thousand_Then_Exclude_This_Number_In_The_Sum(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("//[***]\n1***2***3", 6)]
        public void Given_Add_Is_Called_When_Numbers_Are_Passed_Containing_Various_Multi_Characters_Delimeters_Then_Return_Sum_of_All_The_Numbers(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("//[*][%]\n1*2%3", 6)]
        public void Given_Add_Is_Called_When_Numbers_Are_Passed_Containing_Multi_Single_Length_Delimeters_Then_Return_Sum_of_All_The_Numbers(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("//[**][%%%%]\n100**15%%%%10**5", 130)]
        public void Given_Add_Is_Called_When_Numbers_Are_Passed_Containing_Multiple_Various_Length_Delimeters_Then_Return_Sum_of_All_The_Numbers(string value, int expectedResult)
        {
            var actualResult = _stringCalculator.Add(value);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}