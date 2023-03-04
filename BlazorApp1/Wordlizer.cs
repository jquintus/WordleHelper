using BlazorApp1.Models;
using System.Linq;

namespace BlazorApp1
{
	public record WordleLetter(char Letter, int Position, LetterState State);

	public class Wordlizer
	{
		public IEnumerable<string> GeneratePermutations(IEnumerable<WordleLetter> letters, int length = 5)
		{
			if (letters == null) return Enumerable.Empty<string>();

			var certainLetters = GetCertainLetters(letters);

			var uncertainLetters = letters
				.Where(l => l.State == LetterState.WrongPosition)
				.ToList();

			var result = uncertainLetters
				.Select(l => l.Letter)
				.Distinct()
				.JoinToString()
				.PadRight(length, '-')
				.Permutate()
				.Select(permutation => Expand(permutation, certainLetters))
				.Where(permutation => IsValidForCertainLetters(permutation, certainLetters))
				.Where(permutation => IsValidForUncertainLetters(permutation, uncertainLetters))
				.Distinct()
				.Order();

			return result;
		}

		private static bool IsValidForUncertainLetters(string permutation, List<WordleLetter> uncertainLetters)
		{
			return uncertainLetters.None(ul => permutation[ul.Position] == ul.Letter);
		}

		private static bool IsValidForCertainLetters(string permutation, List<WordleLetter> certainLetters)
		{
			return true;
		}

		private static List<WordleLetter> GetCertainLetters(IEnumerable<WordleLetter> letters)
		{
			var certainLetters = letters
							.Distinct()
							.Where(l => l.State == LetterState.CorrectPosition)
							.OrderBy(l => l.Position)
							.ToList();

			var invalidLetters = certainLetters.GroupBy(l => l.Position)
				.Where(group => group.Count() > 1)
				.Select(l => l.Key + 1)
				.Select(idx => idx.ToString())
				.JoinToString(", ");

			if (invalidLetters.Any())
			{
				throw new ArgumentException($"You have marked letters as green that should not be green at positions {invalidLetters}");
			}

			return certainLetters;
		}

		private string Expand(string permutation, List<WordleLetter> certainLetters)
		{
			return permutation;
		}
	}
}