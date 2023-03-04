﻿using System.Diagnostics.CodeAnalysis;
using WordleSolver.Models;

namespace WordleSolver
{
	public record WordleLetter(char Letter, int Position, LetterState State);

	public class WordLetterComparer : IEqualityComparer<WordleLetter>
	{
		public bool Equals(WordleLetter? x, WordleLetter? y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;

			return x.Letter.Equals(y.Letter);
		}

		public int GetHashCode([DisallowNull] WordleLetter obj)
		{
			return obj.Letter.GetHashCode();
		}
	}

	public class Wordlizer
	{
		public IEnumerable<string> GeneratePermutations(IEnumerable<WordleLetter> letters, int length = 5)
		{
			if (letters == null) return Enumerable.Empty<string>();

			var certainLetters = GetCertainLetters(letters);

			var uncertainLetters = letters
				.Where(l => l.State == LetterState.WrongPosition)
				.Where(l => !certainLetters.Contains(l, new WordLetterComparer()))
				.ToList();

			var result = uncertainLetters
				.Select(l => l.Letter)
				.Distinct()
				.JoinToString()
				.PadRight(length, '-')
				.Permutate()
				.Distinct()
				.Select(permutation => Expand(permutation, certainLetters))
				.Where(permutation => IsValidForUncertainLetters(permutation, uncertainLetters))
				.Distinct()
				.Order();

			return result;
		}

		private static string Expand(string permutation, List<WordleLetter> certainLetters)
		{
			char[] expanded = new char[permutation.Length];
			for (int i = 0; i < permutation.Length; i++)
			{
				expanded[i] = certainLetters
					.Where(l => l.Position == i)
					.Select(l => l.Letter)
					.FirstOrDefault(permutation[i]);
			}

			return new string(expanded);
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

		private static bool IsValidForUncertainLetters(string permutation, List<WordleLetter> uncertainLetters)
		{
			bool areLettersInWrongSpots = uncertainLetters.None(ul => permutation[ul.Position] == ul.Letter);
			bool areAllLettersPresent = uncertainLetters.All(ul => permutation.Contains(ul.Letter));

			return areLettersInWrongSpots && areAllLettersPresent;
		}
	}
}