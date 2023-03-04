using BlazorApp1.Models;
using System.Linq;

namespace BlazorApp1
{
	public record WordleLetter(char Letter, int Position, LetterState State);

	public class Wordlizer
	{
		public IEnumerable<string> GeneratePermutations(IEnumerable<WordleLetter> letters)
		{
			if (letters == null) return Enumerable.Empty<string>();

			var certainLetters = GetCertainLetters(letters);

			var uncertainLetters = letters
				.Where(l => l.State == LetterState.PreseentAndInWrongPosition)
				.Select(l => l.Letter);

			Permutate(uncertainLetters)
				.Select(permutation => new string(permutation))
				.Select(permutation => Expand(permutation, certainLetters))
				.Distinct();
			return Enumerable.Empty<string>();
		}

		private static List<WordleLetter> GetCertainLetters(IEnumerable<WordleLetter> letters)
		{
			var certainLetters = letters
							.Distinct()
							.Where(l => l.State == LetterState.PresentAndInCorrectPosition)
							.OrderBy(l => l.Position)
							.ToList();

			var invalidLetters = certainLetters.GroupBy(l => l.Position)
				.Where(group => group.Count() > 1)
				.Select(l => l.Key + 1)
				.Select(idx => idx.ToString());

			if (invalidLetters.Any())
			{
				throw new ArgumentException($"You have marked letters as green that should not be green at positions {invalidLetters}");
			}

			return certainLetters;
		}

		private static IEnumerable<char[]> Permutate(IEnumerable<char> source)
		{
			return permutate(source, Enumerable.Empty<char>());

			IEnumerable<char[]> permutate(IEnumerable<char> reminder, IEnumerable<char> prefix) =>
				!reminder.Any() ? new[] { prefix.ToArray() } :
				reminder.SelectMany((c, i) => permutate(
					reminder.Take(i).Concat(reminder.Skip(i + 1)).ToArray(),
					prefix.Append(c)));
		}

		private object Expand(string permutation, List<WordleLetter> certainLetters)
		{
			throw new NotImplementedException();
		}
	}
}