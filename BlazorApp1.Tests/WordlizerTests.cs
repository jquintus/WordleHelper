namespace BlazorApp1.Tests
{
	public class WordlizerTests
	{
		[Fact]
		public void GeneratePermutations_EmptyLetters_ReturnsEmpty()
		{
			// Assemble
			var wl = new Wordlizer();

			// Act
			var result = wl.GeneratePermutations(Enumerable.Empty<WordleLetter>());

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void GeneratePermutations_CertainAboutDifferentLettersInSamePosition_Throws()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('a', 1, Models.LetterState.PresentAndInCorrectPosition),
				new WordleLetter('b', 1, Models.LetterState.PresentAndInCorrectPosition),
			};

			// Act / Assert
			Assert.Throws<ArgumentException>(() => wl.GeneratePermutations(input));
		}
	}
}