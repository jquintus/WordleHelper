﻿using System.ComponentModel.DataAnnotations;
using System.Text;
using WordleHelper.Services;

namespace WordleHelper.Models
{
	public class InputModel
	{
		private readonly IWordlizer _service;

		public List<GuessModel> Guesses { get; private set; }
		public List<string> Suggestions { get; private set; }

		[Required]
		[StringLength(5, MinimumLength = 5, ErrorMessage = "Guesses must be 5 letters long.")]
		public string Word { get; set; }

		public InputModel(IWordlizer service)
		{
			_service = service;
			Word = string.Empty;
			Guesses = new List<GuessModel>();
			Suggestions = new List<string>();
		}

		public void AddGuess(string guess)
		{
			if (string.IsNullOrEmpty(guess)) return;
			Guesses.Add(new(guess.ToUpper()));
		}

		public void GenerateSuggestions()
		{
			var letters = Guesses
				.SelectMany(g => g.Letters)
				.Select(l => new WordleLetter(l.Letter.First(), l.Position, l.State));

			Suggestions = _service
				.GeneratePermutations(letters)
				.Select(s => AddSpaces(s))
				.ToList();
		}

		public void Reset()
		{
			Guesses = new List<GuessModel>();
			Suggestions = new List<string>();
		}

		private static string AddSpaces(string s)
		{
			StringBuilder sb = new StringBuilder(s.Length * 2);

			foreach (var c in s)
			{
				sb.Append(c);
				sb.Append(' ');
			}

			sb.Length--;
			return sb.ToString();
		}
	}
}