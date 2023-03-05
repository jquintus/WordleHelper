using WordleHelper.Models;

namespace WordleHelper.Services
{
	public record WordleLetter(char Letter, int Position, LetterState State);
}
