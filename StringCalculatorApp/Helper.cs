namespace StringCalculatorApp
{
    internal static class Helper
    {
        public static bool IsEmpty(this string numbers)
        {
            return string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers);
        }
    }
}
