namespace BlazorApp1.Models
{
	public class InputModel
	{
		private readonly Wordlizer _service;

		public List<GuessModel> Guesses { get; private set; }
		public List<string> Suggestions { get; private set; }
		public string Word { get; set; }

		public InputModel(Wordlizer service)
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
				.ToList();
		}

		public void Reset()
		{
			Guesses = new List<GuessModel>();
			Suggestions = new List<string>();
		}
	}
}