namespace TextToNumericConverter
{
    public class Program
    {
        private static Dictionary<string, long> numberMapping = new Dictionary<string, long>()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "ten", 10 },
            { "eleven", 11 },
            { "twelve", 12 },
            { "thirteen", 13 },
            { "fourteen", 14 },
            { "fifteen", 15 },
            { "sixteen", 16 },
            { "seventeen", 17 },
            { "eighteen", 18 },
            { "nineteen", 19 },
            { "twenty", 20 },
            { "thirty", 30 },
            { "forty", 40 },
            { "fifty", 50 },
            { "sixty", 60 },
            { "seventy", 70 },
            { "eighty", 80 },
            { "ninety", 90 },
            { "hundred", 100 },
            { "thousand", 1000 },
            { "million", 1000000 },
            { "billion", 1000000000 },
            { "trillion", 1000000000000 }
        };



        static void Main(string[] args)
        {
            string input = "He paid one thousand twenty five for thirty million one hundred twenty three such cars.";

            string[] words = input.Split(' ');
            int[] numIndexArr = new int[words.Length];
            int temp = 0, tempJ = 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (numberMapping.ContainsKey(words[i]))
                {
                    numIndexArr[temp] = i; // mappingde olan numaranın indexini tut
                    temp++;

                    for (int j = i + 1; j < words.Length - 1; j++) // o indexden sonra mappingde olmayan değeri bul
                    {
                        if (numberMapping.ContainsKey(words[j]))
                        {
                            numIndexArr[temp] = j;
                            temp++;
                        }

                        tempJ = j;
                    }

                    i = tempJ + 1;
                }
            }



            Console.WriteLine("Hello, World!");
        }
    }
}