namespace TextToNumericConverter
{
    public class Program
    {
        static void Main(string[] args)
        {
            string input = "He paid one thousand twenty five for thirty million one hundred twenty three such cars.";
            //input = "one thousand twenty five for thirty million one hundred twenty three such cars.";
            string output = NumericConverter.Convert(input);

            Console.WriteLine("Input: " + input);
            Console.WriteLine("Output: " + output);
            Console.ReadKey();

            // TODO: refactor and write unit tests. DON'T FORGET!!!
        }
    }
}