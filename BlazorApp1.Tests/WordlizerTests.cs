using BlazorApp1.Models;

namespace BlazorApp1.Tests
{
	public class WordlizerTests
	{
		[Fact]
		public void GeneratePermumations_MultipleUncertainLetters_ReturnsAllPermutations()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('a', 0, LetterState.WrongPosition),
				new WordleLetter('a', 1, LetterState.WrongPosition),
				new WordleLetter('b', 1, LetterState.WrongPosition),
				new WordleLetter('b', 2, LetterState.WrongPosition),
			};

			var expected = new List<string>
			{
				"---ab",
				"---ba",
				"--a-b",
				"--ab-",
				"b---a",
				"b--a-",
				"b-a--",
			};

			// Act
			var actual = wl.GeneratePermutations(input).ToList();

			// Assert
			// We have to order the lists to make sure they can be compared
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermumations_OnlyOneUncertainLetters_ReturnsAllPermutations()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('a', 0, LetterState.WrongPosition),
			};

			var expected = new List<string>
			{
				"----a",
				"---a-",
				"--a--",
				"-a---",
			};

			// Act
			var actual = wl.GeneratePermutations(input).ToList();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermumations_RepeatSameUncertainLetters_DoesNotDuplicateTheLetter()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('a', 0, LetterState.WrongPosition),
				new WordleLetter('a', 1, LetterState.WrongPosition),
			};

			var expected = new List<string>
			{
				"----a",
				"---a-",
				"--a--",
			};

			// Act
			var actual = wl.GeneratePermutations(input).ToList();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_CertainAboutDifferentLettersInSamePosition_Throws()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('c', 1, LetterState.CorrectPosition),
				new WordleLetter('d', 1, LetterState.CorrectPosition),
			};

			// Act / Assert
			Assert.Throws<ArgumentException>(() => wl.GeneratePermutations(input));
		}
	}
}