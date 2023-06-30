using System;
using System.Text.RegularExpressions;

namespace TextToNumericConverter
{
    public static class NumericConverter
    {
        /// <summary>
        /// Assumed our input up to quadrillion level
        /// </summary>
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
            { "trillion", 1000000000000 },
            { "quadrillion",1000000000000000}
        };


        public static string Convert(string input)
        {
            string output = "";

            // metni parçala ve sayıların indexlerini bul
            var(indexes, wordArray) = FindNumberStringInText(input);

            // indexlerdeki sayıları birleştir

            output += string.Join(' ', wordArray, 0, indexes[0]);

            string tempFrom = "", tempCount = "";
            int count = 1;

            for (int i = 0; i < indexes.Length; i++)
            {
                if (i < indexes.Length - 1 && indexes[i + 1] == (indexes[i] + 1))
                {
                    count++;
                }
                else
                {
                    tempFrom = tempFrom + (indexes[i]-count+1) + ",";
                    tempCount = tempCount + count + ",";
                    count = 1;
                }   
            }

            int[] joinCount = Array.ConvertAll(tempCount.Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int[] joinFrom = Array.ConvertAll(tempFrom.Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);

            for (int i = 0; i < joinFrom.Length; i++)
            {
                output += " " + ConvertToNumber(string.Join(' ', wordArray, joinFrom[i], joinCount[i])).ToString();

                if (i < joinFrom.Length - 1 && joinFrom[i] + joinCount[i] < joinFrom[i+1])
                    output += " " + string.Join(' ', wordArray, joinFrom[i] + joinCount[i], joinFrom[i + 1] - (joinFrom[i] + joinCount[i]));
                    
            }


            int tmp1 = joinFrom[joinFrom.Length - 1] + joinCount[joinCount.Length - 1];
            int tmp2 = wordArray.Length - tmp1;
            output += " " + string.Join(' ', wordArray, tmp1, tmp2);

            return output;
        }

        /// <summary>
        /// Finds an indexes of numeric strings in text.
        /// Returns indexes seperated with comma as string.
        /// </summary>
        /// <param name="input"></param>
        private static (int[] indexes, string[] wordArray) FindNumberStringInText(string input)
        {
            string[] words = input.Split(' ');
            int temp = 0, tempJ = 0;
            List<int> numIndexes = new List<int>();

            for (int i = 0; i < words.Length; i++)
            {
                if (numberMapping.ContainsKey(words[i]))
                {
                    numIndexes.Add(i);
                    temp++;

                    for (int j = i + 1; j < words.Length - 1; j++) // o indexden sonra mappingde olmayan değeri bul
                    {
                        if (numberMapping.ContainsKey(words[j]))
                        {
                            numIndexes.Add(j);
                            temp++;
                        }

                        tempJ = j;
                    }

                    i = tempJ + 1;
                }
            }

            return (indexes: numIndexes.ToArray(), wordArray: words);
        }

        /// <summary>
        /// Converts string spelled number as numeric.
        /// Assumed to all numbers written like "one thousand, one hundred" not like "a thousand, a hundred".
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        private static long ConvertToNumber(string numberString)
        {
            #region With RegEx
            //var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
            //        .Select(m => m.Value.ToLowerInvariant())
            //        .Where(v => numberMappings.ContainsKey(v))
            //        .Select(v => numberMappings[v]);
            #endregion

            var numbers = numberString.Split(' ')
                .Select(x => x.ToLowerInvariant())
                .Where(y => numberMapping.ContainsKey(y))
                .Select(z => numberMapping[z]);

            // example: thirty million one hundred twenty three

            long acc = 0, total = 0L;

            foreach (var n in numbers)
            {
                if (n >= 1000)
                {
                    total += acc * n;
                    acc = 0;
                }
                else if (n >= 100)
                {
                    acc *= n;
                }
                else acc += n;
            }
            return (total + acc);
        }
    }
}
