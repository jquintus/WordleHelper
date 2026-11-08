namespace WordleHelper.Services
{
	public interface IWordlizer
	{
		IEnumerable<string> GeneratePermutations(IEnumerable<WordleGuess> guesses, int length = 5);
	}
}
