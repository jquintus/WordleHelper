namespace BlazorApp1
{
    public class InputModel
    {
        private readonly Wordlizer _service;

        public string Word { get; set; }
        public List<string> Guesses { get; private set; }
        public List<string> Suggestions { get; private set; }

        public InputModel(Wordlizer service)
        {
            _service = service;
            Word = string.Empty;
            Guesses = new List<string>();
            Suggestions = new List<string>();
        }

        public void Reset()
        {
            Guesses = new List<string>();
            Suggestions = new List<string>();
        }

        public void AddGuess(string guess)
        {
            if (string.IsNullOrEmpty(guess)) return;
            Guesses.Add(guess);
            Suggestions = _service.GeneratePermutations(guess).ToList();
        }
    }
}
