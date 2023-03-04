namespace BlazorApp1.Models
{
	public class LetterModel
	{

		public LetterModel(string letter, int position, LetterState state = LetterState.NotPresent)
		{
			Letter = letter;
			Position = position;
			State = state;
		}

		public string Letter { get; }
		public int Position { get; }
		public LetterState State { get; private set; }

		public void ToggleState()
		{
			State = State switch
			{
				LetterState.NotPresent => LetterState.PreseentAndInWrongPosition,
				LetterState.PreseentAndInWrongPosition => LetterState.PresentAndInCorrectPosition,
				LetterState.PresentAndInCorrectPosition => LetterState.NotPresent,

				_ => LetterState.NotPresent,
			};
		}

		public override string ToString() => Letter;

	}
}
