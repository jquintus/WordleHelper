namespace BlazorApp1.Models
{
    public record LetterModel(string Letter, int Position, LetterState State)
    {
        public LetterState State { get; private set; } = State;

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
