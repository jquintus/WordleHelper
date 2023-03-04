namespace BlazorApp1.Models
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
}