namespace TextToNumericConverter.Test
{
    public class NumericConverterTest
    {
        [Fact]
        public void Convert_StringNumbers_In_Text_To_Numeric()
        {
            // Arrange
            string text = "He paid one thousand twenty five for thirty million one hundred twenty three such cars.";
            string expected = "He paid 1025 for 30000123 such cars.";

            // Act
            string result = NumericConverter.Convert(text);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_To_Number_And_Join_Text_Using_Given_Indexes()
        {
            // Arrange
            string[] wordArray = new string[]
            {
                "He", "paid", "one", "thousand", "twenty", "five",
                "for", "thirty", "million", "one",
                "hundred", "twenty", "three", "such", "cars."
            };

            int[] indexesOfNumStringsInWordArray = new int[]
            {
                2, 3, 4, 5,7, 8,9, 10, 11, 12
            };

            string expected = "He paid 1025 for 30000123 such cars.";

            // Act
            string result = NumericConverter.ConvertToNumberOutput(indexesOfNumStringsInWordArray, wordArray);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Convert_StringNumber_As_Numeric()
        {
            // Arrange
            string num = "one thousand twenty five";

            // Act
            long result = NumericConverter.ConvertToNumber(num);

            // Assert
            Assert.Equal(1025, result);
        }

        [Fact]
        public void Find_NumberString_Items_In_WordArray()
        {
            // Arrange
            string[] nums = new string[]
            {
                "He", "paid", "one", "thousand", "twenty", "five",
                "for", "thirty", "million", "one",
                "hundred", "twenty", "three", "such", "cars."
            };

            int[] expectedResult = new int[]
            {
                2, 3, 4, 5,7, 8,9, 10, 11, 12
            };

            // Act
            int[] result = NumericConverter.FindNumberStringsInWordArray(nums);

            // Assert
            Assert.True(expectedResult.SequenceEqual(result));
        }

        [Fact]
        public void Check_Number_Mapping_Contains_Required_Mappings()
        {
            // Arrange
            var expectedResult = new Dictionary<string, long>()
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

            // Act
            var result = NumericConverter.numberMapping;

            // Assert
            Assert.True(expectedResult.SequenceEqual(result));
        }

        [Fact]
        public void Input_String_And_Get_Mapped_NumericValue_As_Array()
        {
            // Arrange
            string input = "thirty million one hundred twenty three";

            long[] expectedResult = new long[]
            {
                30, 1000000, 1, 100, 20, 3
            };

            // Act
            var result = NumericConverter.GetMappedNumericValuesAsArray(input);

            // Assert
            Assert.True(expectedResult.SequenceEqual(result));
        }
    }
}