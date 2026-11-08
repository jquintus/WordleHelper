using WordleHelper.Models;
using WordleHelper.Services;

namespace WordleHelper.Tests
{
	public class WordlizerTests
	{
		[Fact]
		public void GeneratePermutations_YellowLettersAcrossGuesses_ReturnsAllPermutations()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>
			{
				new WordleGuess(new List<WordleLetter>
				{
					new('a', 0, LetterState.WrongPosition),
					new('b', 1, LetterState.WrongPosition),
				}),
				new WordleGuess(new List<WordleLetter>
				{
					new('a', 1, LetterState.WrongPosition),
					new('b', 2, LetterState.WrongPosition),
				}),
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
		public void GeneratePermutations_OneYellowLetter_ReturnsAllPermutations()
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
			var actual = wl.GeneratePermutations(new[] { new WordleGuess(input) }).ToList();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_SameYellowLetterAcrossGuesses_RequiresOneCopy()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>
			{
				new WordleGuess(new[] { new WordleLetter('a', 0, LetterState.WrongPosition) }),
				new WordleGuess(new[] { new WordleLetter('a', 1, LetterState.WrongPosition) }),
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
		public void GeneratePermutations_TwoYellowEsInOneGuess_RequiresTwoCopies()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('e', 0, LetterState.WrongPosition),
				new WordleLetter('e', 1, LetterState.WrongPosition),
			};
			var expected = new List<string> { "---ee", "--e-e", "--ee-" };

			// Act
			var actual = wl.GeneratePermutations(new[] { new WordleGuess(input) }).ToList();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_YellowAndGreenEInOneGuess_RequiresTwoCopies()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleLetter>
			{
				new WordleLetter('e', 0, LetterState.WrongPosition),
				new WordleLetter('e', 2, LetterState.CorrectPosition),
			};
			var expected = new List<string> { "--e-e", "--ee-", "-ee--" };

			// Act
			var actual = wl.GeneratePermutations(new[] { new WordleGuess(input) }).ToList();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_YellowAndGreenEAcrossGuesses_RequiresOneCopy()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>
			{
				new WordleGuess(new[] { new WordleLetter('e', 0, LetterState.WrongPosition) }),
				new WordleGuess(new[] { new WordleLetter('e', 2, LetterState.CorrectPosition) }),
			};

			// Act
			var actual = wl.GeneratePermutations(input).ToList();

			// Assert
			Assert.Equal(new List<string> { "--e--" }, actual);
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

			// Act
			var exception = Record.Exception(() => wl.GeneratePermutations(new[] { new WordleGuess(input) }));

			// Assert
			Assert.IsType<ArgumentException>(exception);
		}

		[Fact]
		public void GeneratePermutations_EmptyInput_ReturnsFiveDashes()
		{
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>();
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
			var actual = wl.GeneratePermutations(new[] { new WordleGuess(input) });

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void GeneratePermutations_GreenLettersAndTwoYellowLetters_ReturnsMatchingPermutations()
		{
			// The word was "TREND"
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>
			{
				new WordleGuess(new[]
				{
					new WordleLetter('r', 1, LetterState.CorrectPosition),
					new WordleLetter('n', 3, LetterState.CorrectPosition),
					new WordleLetter('e', 4, LetterState.WrongPosition),
				}),
				new WordleGuess(new[] { new WordleLetter('t', 4, LetterState.WrongPosition) }),
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
		public void GeneratePermutations_TwoGuessesWithSharedGreens_ReturnsMatchingPermutations()
		{
			// The word was "TREND"
			// Assemble
			var wl = new Wordlizer();
			var input = new List<WordleGuess>
			{
				new WordleGuess(new[]
				{
					new WordleLetter('c', 0, LetterState.CorrectPosition),
					new WordleLetter('r', 1, LetterState.WrongPosition),
					new WordleLetter('a', 2, LetterState.NotPresent),
					new WordleLetter('n', 3, LetterState.NotPresent),
					new WordleLetter('e', 4, LetterState.WrongPosition),
				}),
				new WordleGuess(new[]
				{
					new WordleLetter('c', 0, LetterState.CorrectPosition),
					new WordleLetter('h', 1, LetterState.NotPresent),
					new WordleLetter('a', 2, LetterState.NotPresent),
					new WordleLetter('i', 3, LetterState.NotPresent),
					new WordleLetter('r', 4, LetterState.CorrectPosition),
				}),
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
