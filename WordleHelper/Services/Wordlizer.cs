using WordleHelper.Models;

namespace WordleHelper.Services
{
	public class Wordlizer : IWordlizer
	{
		public IEnumerable<string> GeneratePermutations(IEnumerable<WordleGuess> guesses, int length = 5)
		{
			if (guesses == null) return Enumerable.Empty<string>();

			var allGuesses = guesses.ToList();
			var allLetters = allGuesses.SelectMany(g => g.Letters).ToList();
			var certainLetters = GetCertainLetters(allLetters);

			var uncertainLetters = allLetters
				.Where(l => l.State == LetterState.WrongPosition)
				.ToList();

			var minimumCounts = allGuesses
				.Select(g => CountPresentLetters(g))
				.SelectMany(x => x)
				.GroupBy(c => c.Letter)
				.ToDictionary(g => g.Key, g => g.Max(c => c.Count));

			var freePositions = Enumerable.Range(0, length)
				.Except(certainLetters.Select(l => l.Position))
				.ToArray();

			var requiredLetters = minimumCounts
				.SelectMany(c => Enumerable.Repeat(
					c.Key,
					Math.Max(0, c.Value - certainLetters.Count(l => l.Letter == c.Key))))
				.ToArray();

			if (requiredLetters.Length > freePositions.Length) return Enumerable.Empty<string>();

			var result = new string(requiredLetters)
				.PadRight(freePositions.Length, '-')
				.Permutate()
				.Distinct()
				.Select(p => Expand(p, certainLetters, freePositions, length))
				.Where(p => IsValidForUncertainLetters(p, uncertainLetters))
				.Order();

			return result;
		}

		private static IEnumerable<(char Letter, int Count)> CountPresentLetters(WordleGuess guess)
		{
			return guess.Letters
				.Where(l => l.State != LetterState.NotPresent)
				.GroupBy(l => l.Letter)
				.Select(g => (g.Key, g.Count()));
		}

		private static string Expand(string permutation, List<WordleLetter> certainLetters, int[] freePositions, int length)
		{
			char[] expanded = new char[length];
			foreach (var letter in certainLetters)
			{
				expanded[letter.Position] = letter.Letter;
			}

			for (int i = 0; i < freePositions.Length; i++)
			{
				expanded[freePositions[i]] = permutation[i];
			}

			return new string(expanded);
		}

		private static List<WordleLetter> GetCertainLetters(IEnumerable<WordleLetter> letters)
		{
			var certainLetters = letters
							.Where(l => l.State == LetterState.CorrectPosition)
							.DistinctBy(l => (l.Letter, l.Position))
							.OrderBy(l => l.Position)
							.ToList();

			var invalidLetters = certainLetters.GroupBy(l => l.Position)
				.Where(g => g.Count() > 1)
				.Select(g => g.Key + 1)
				.Select(i => i.ToString())
				.JoinToString(", ");

			if (invalidLetters.Any())
			{
				throw new ArgumentException($"You have marked letters as green that should not be green at positions {invalidLetters}");
			}

			return certainLetters;
		}

		private static bool IsValidForUncertainLetters(string permutation, List<WordleLetter> uncertainLetters)
		{
			bool areLettersInWrongSpots = uncertainLetters.None(l => permutation[l.Position] == l.Letter);
			bool areAllLettersPresent = uncertainLetters.All(l => permutation.Contains(l.Letter));

			return areLettersInWrongSpots && areAllLettersPresent;
		}
	}
}
