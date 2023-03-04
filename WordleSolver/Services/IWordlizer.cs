namespace WordleSolver.Services
{
	public interface IWordlizer
	{
		IEnumerable<string> GeneratePermutations(IEnumerable<WordleLetter> letters, int length = 5);
	}
}