using WordleSolver.Models;
using WordleSolver.Services;

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

		[Fact]
		public void GeneratePermutations_EmptyInput_ReturnsFiveDashes()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>();
			var expected = new List<string> {
				"-----",
			};

			// Act
			var actual = wl.GeneratePermutations(input);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_OnlyCertainInput_ReturnsOnePermutation()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('c', 0, LetterState.CorrectPosition),
				new WordleLetter('r', 1, LetterState.CorrectPosition),
				new WordleLetter('a', 2, LetterState.CorrectPosition),
				new WordleLetter('n', 3, LetterState.CorrectPosition),
			};
			var expected = new List<string> {
				"cran-",
			};

			// Act
			var actual = wl.GeneratePermutations(input);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_RealWorldTest1_ReturnsOnePermutation()
		{
			// The word was "TREND"
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('r', 1, LetterState.CorrectPosition),
				new WordleLetter('n', 3, LetterState.CorrectPosition),
				new WordleLetter('e', 4, LetterState.WrongPosition),
				new WordleLetter('t', 4, LetterState.WrongPosition),
			};
			var expected = new List<string> {
				"ertn-",
				"tren-",
			};

			// Act
			var actual = wl.GeneratePermutations(input);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_RealWorldTest2_ReturnsOnePermutation()
		{
			// The word was "TREND"
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('c', 0, LetterState.CorrectPosition),
				new WordleLetter('r', 1, LetterState.WrongPosition),
				new WordleLetter('a', 2, LetterState.NotPresent),
				new WordleLetter('n', 3, LetterState.NotPresent),
				new WordleLetter('e', 4, LetterState.WrongPosition),

				new WordleLetter('c', 0, LetterState.CorrectPosition),
				new WordleLetter('h', 1, LetterState.NotPresent),
				new WordleLetter('a', 2, LetterState.NotPresent),
				new WordleLetter('i', 3, LetterState.NotPresent),
				new WordleLetter('r', 4, LetterState.CorrectPosition),
			};
			var expected = new List<string> {
				"c--er",
				"c-e-r",
				"ce--r",
			};

			// Act
			var actual = wl.GeneratePermutations(input);

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}