namespace TextToNumericConverter
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please input some text to convert:");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    input = "He paid one thousand twenty five for thirty million one hundred twenty three such cars.";

                string output = NumericConverter.Convert(input);

                Console.WriteLine("Input: " + input);
                Console.WriteLine("Output: " + output);
                Console.WriteLine("_____________________________________");
            }
        }
    }
}