using System.Diagnostics.CodeAnalysis;

namespace BlazorApp1
{
	public class WordLetterComparer : IEqualityComparer<WordleLetter>
	{
		public bool Equals(WordleLetter? x, WordleLetter? y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (x == null && y == null) return true;
			if (x == null || y == null) return false;

			return x.Letter.Equals(y.Letter);
		}

		public int GetHashCode([DisallowNull] WordleLetter obj)
		{
			return obj.Letter.GetHashCode();
		}
	}
}