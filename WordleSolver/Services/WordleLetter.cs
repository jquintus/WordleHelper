using WordleSolver.Models;

namespace WordleSolver.Services
{
	public record WordleLetter(char Letter, int Position, LetterState State);
}
