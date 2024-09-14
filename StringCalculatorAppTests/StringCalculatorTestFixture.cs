using StringCalculatorApp;

namespace StringCalculatorAppTests
{
    public class StringCalculatorTestFixture : IDisposable
    { 
        public readonly StringCalculator Calculator;
        public StringCalculatorTestFixture()
        {
            Calculator = new StringCalculator();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
