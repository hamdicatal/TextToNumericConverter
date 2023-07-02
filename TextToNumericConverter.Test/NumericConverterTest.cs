namespace TextToNumericConverter.Test
{
    public class NumericConverterTest
    {
        [Fact]
        public void Convert_StringNumber_As_Numeric()
        {
            // Arrange
            string num = "one thousand twenty five";

            // Act
            long result = NumericConverter.ConvertToNumber(num);

            // Assert
            Assert.Equal<long>(1025, result);
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
            int[] result = NumericConverter.FindNumberStringInWordArray(nums);

            // Assert
            Assert.True(expectedResult.SequenceEqual(result));
        }
    }
}