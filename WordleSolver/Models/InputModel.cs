namespace WordleSolver.Models
{
	public class InputModel
	{
		public List<JoshModel> Guesses { get; set; }
		public List<string> Suggestions { get; set; }
		public string Word { get; set; }

		public InputModel(object obj)
		{
			Guesses = new List<JoshModel>();
			Suggestions = new List<string>();
		}

		public void AddGuess(string word)
		{
		}

		public void GenerateSuggestions()
		{ }

		public void Reset()
		{ }
	}

	public class JoshModel
	{
		public List<LetterModel> Letters { get; set; } = new List<LetterModel>();
	}
	public class LetterModel { 
		public void ToggleState()
		{

		}
		public string Color { get; set; } = "red";

    }
}