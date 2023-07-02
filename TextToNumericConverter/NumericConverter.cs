namespace TextToNumericConverter
{
    public static class NumericConverter
    {
        /// <summary>
        /// Assumed our input up to quadrillion level
        /// </summary>
        public static Dictionary<string, long> numberMapping = new Dictionary<string, long>()
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
            { "trillion", 1000000000000 },
            { "quadrillion",1000000000000000}
        };

        /// <summary>
        /// Converts all numeric values written as text to numeric.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Convert(string input)
        {
            // Split text and find the indexes.
            string[] wordArray = input.Split(' ');
            int[] numIndexes = FindNumberStringsInWordArray(wordArray);

            // Convert to number and join text.
            return ConvertToNumberOutput(numIndexes, wordArray);
        }

        /// <summary>
        /// Gets a number string indexes and word array splitted from input text.
        /// Converts string numbers to numeric with using given indexes in word array.
        /// </summary>
        /// <param name="numIndexes"></param>
        /// <param name="wordArray"></param>
        /// <returns></returns>
        public static string ConvertToNumberOutput(int[] numIndexes, string[] wordArray)
        {
            string output = string.Empty;
            output += string.Join(' ', wordArray, 0, numIndexes[0]);

            List<int> tempFrom = new List<int>();
            List<int> tempCount = new List<int>();
            int count = 1;

            for (int i = 0; i < numIndexes.Length; i++)
            {
                if (i < numIndexes.Length - 1 && numIndexes[i + 1] == (numIndexes[i] + 1))
                {
                    count++;
                }
                else
                {
                    tempFrom.Add(numIndexes[i] - count + 1);
                    tempCount.Add(count);
                    count = 1;
                }
            }

            int[] joinCount = tempCount.ToArray();
            int[] joinFrom = tempFrom.ToArray();

            for (int i = 0; i < joinFrom.Length; i++)
            {
                output += " " + ConvertToNumber(string.Join(' ', wordArray, joinFrom[i], joinCount[i])).ToString();

                if (i < joinFrom.Length - 1 && joinFrom[i] + joinCount[i] < joinFrom[i + 1])
                    output += " " + string.Join(' ', wordArray, joinFrom[i] + joinCount[i], joinFrom[i + 1] - (joinFrom[i] + joinCount[i]));

            }

            int tmp1 = joinFrom[joinFrom.Length - 1] + joinCount[joinCount.Length - 1];
            int tmp2 = wordArray.Length - tmp1;
            output += " " + string.Join(' ', wordArray, tmp1, tmp2);

            return output.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Finds an indexes of numeric strings in text.
        /// </summary>
        /// <param name="input"></param>
        public static int[] FindNumberStringsInWordArray(string[] wordArray)
        {
            int temp = 0, tempJ = 0;
            List<int> numIndexes = new List<int>();

            for (int i = 0; i < wordArray.Length; i++)
            {
                if (numberMapping.ContainsKey(wordArray[i]))
                {
                    numIndexes.Add(i);
                    temp++;

                    for (int j = i + 1; j < wordArray.Length - 1; j++) // o indexden sonra mappingde olmayan değeri bul
                    {
                        if (numberMapping.ContainsKey(wordArray[j]))
                        {
                            numIndexes.Add(j);
                            temp++;
                        }

                        tempJ = j;
                    }

                    i = tempJ + 1;
                }
            }

            return numIndexes.ToArray();
        }

        /// <summary>
        /// Converts string spelled number as numeric.
        /// Assumed to all numbers written like "one thousand, one hundred" not like "a thousand, a hundred".
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static long ConvertToNumber(string numberString)
        {
            var numbers = numberString.Split(' ')
                .Select(x => x.ToLowerInvariant())
                .Where(y => numberMapping.ContainsKey(y))
                .Select(z => numberMapping[z]);

            long temp = 0, total = 0L;

            foreach (long n in numbers)
            {
                if (n >= 1000)
                {
                    total += temp * n;
                    temp = 0;
                }
                else if (n >= 100)
                {
                    temp *= n;
                }
                else temp += n;
            }
            return (total + temp);
        }

        /// <summary>
        /// Gets an only one number string parameter and convert it to numeric value with using number mapping.
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static IEnumerable<long> GetMappedNumericValuesAsArray(string numberString)
        {
           return numberString.Split(' ')
                .Select(x => x.ToLowerInvariant())
                .Where(y => numberMapping.ContainsKey(y))
                .Select(z => numberMapping[z]);
        }
    }
}
