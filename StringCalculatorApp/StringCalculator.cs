namespace StringCalculatorApp
{
    public class StringCalculator
    {
        #region "constants"
        private const string Delimiter_Comma = ",";
        private const string Delimiter_ForwardSlashes = "//";
        private const string Delimiter_NewLine = "\n";
        private const string Delimiter_Seperator = "][";      
        private const string Delimiter_Bracket = "[";
        private const int Delimiter_Start_Position = 2;
        private const int MaxNumberAllowedToCalculate = 1000;
        #endregion

        public int Add(string numbers)
        {
            if (numbers.IsEmpty()) 
            {  
                return 0; 
            }

            var delimitersList = BuildDelimitersList(numbers);

            if (numbers.StartsWith(Delimiter_ForwardSlashes))
            {
                var startIndex = 
                    numbers.IndexOf(Delimiter_NewLine) + 1;
                numbers = numbers[startIndex..];
            }

                       
            var numbersList = 
                FilterMaxAllowedNumbers(numbers.Split(delimitersList, StringSplitOptions.RemoveEmptyEntries));

              
            var negativeNumbers = GetNegativeNumbers(numbersList);
            if (negativeNumbers.Any())
            {               
                var exceptionMessage
                    = $"Negatives Not Allowed - {string.Join(",", negativeNumbers)}";
                throw new ArgumentException(exceptionMessage);
            }

            return numbersList.Sum();
        }

        private IEnumerable<int> FilterMaxAllowedNumbers(string[] numbers)
        {
            return numbers                   
                    .Where(stringNumber => Convert.ToInt32(stringNumber) <= MaxNumberAllowedToCalculate)
                    .Select(stringNumber => Convert.ToInt32(stringNumber));                    
        }


        private string[] BuildDelimitersList(string numbers)
        {
            return numbers.StartsWith(Delimiter_ForwardSlashes) ?
                    GetCustomisedDelimitersList(numbers) :
                    new string[] { Delimiter_Comma, Delimiter_NewLine };
        }

        private string[] GetCustomisedDelimitersList(string numbers)
        {
            const int DelimiterStartingValue = 1;
            var delimiterLength = numbers.IndexOf(Delimiter_NewLine) - Delimiter_Start_Position;

            var delimiter = numbers.Substring(Delimiter_Start_Position, delimiterLength);

            if (delimiter.Contains(Delimiter_Bracket))
            {
                delimiter = delimiter[DelimiterStartingValue..^1];

                return delimiter.Split(new string[] { Delimiter_Seperator }, StringSplitOptions.RemoveEmptyEntries);
            }

            delimiter = numbers.Substring(Delimiter_Start_Position, DelimiterStartingValue);

            return new string[] { delimiter };
        }

        private IEnumerable<int> GetNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers
                    .Where(number => number < 0)
                    .Select(number => number);                   
        }
              
    }
}
