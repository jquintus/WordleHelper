namespace WordleSolver.Models
{
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
}