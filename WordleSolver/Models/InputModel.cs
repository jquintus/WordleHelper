namespace WordleSolver.Models
{
	public class GuessModel
	{
		private readonly string _guess;
		public List<LetterModel> Letters { get; }

		public GuessModel(string guess)
		{
			_guess = guess;
			Letters = guess
				.Select((l, idx) => new LetterModel(l.ToString(), idx, LetterState.NotPresent))
				.ToList();
		}

		public override string ToString() => _guess;
	}

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


	public class LetterModel
	{
		public string Color => State switch
		{
			LetterState.NotPresent => "#787c7e",
			LetterState.WrongPosition => "#c9b458",
			LetterState.CorrectPosition => "#6aaa64",
			_ => "#787c7e",
		};

		public string Letter { get; }

		public int Position { get; }

		public LetterState State { get; private set; }

		public LetterModel(string letter, int position, LetterState state = LetterState.NotPresent)
		{
			Letter = letter;
			Position = position;
			State = state;
		}

		public void ToggleState()
		{
			State = State switch
			{
				LetterState.NotPresent => LetterState.WrongPosition,
				LetterState.WrongPosition => LetterState.CorrectPosition,
				LetterState.CorrectPosition => LetterState.NotPresent,

				_ => LetterState.NotPresent,
			};
		}

		public override string ToString() => Letter;
	}
	public enum LetterState
	{
		NotPresent,
		WrongPosition,
		CorrectPosition,
	}
}