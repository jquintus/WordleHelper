namespace WordleHelper
{
	public static class Extentions
	{
		public static string JoinToString(this IEnumerable<string> source, string separator = "")
		{
			return string.Join(separator, source);
		}

		public static string JoinToString(this IEnumerable<char> source, string separator = "")
		{
			return string.Join(separator, source);
		}

		public static bool None<TSource>(this IEnumerable<TSource> source)
		{
			return !source.Any();
		}

		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate);
		}

		public static IEnumerable<TSource> Order<TSource>(this IEnumerable<TSource> source)
		{
			return source.OrderBy(x => x);
		}

		public static IEnumerable<string> Permutate(this string source) =>
		 source
			.AsEnumerable()
			.Permutate()
			.Select(a => new string(a));

		public static IEnumerable<T[]> Permutate<T>(this IEnumerable<T> source)
		{
			return permutate(source, Enumerable.Empty<T>());
			IEnumerable<T[]> permutate(IEnumerable<T> reminder, IEnumerable<T> prefix) =>
				!reminder.Any() ? new[] { prefix.ToArray() } :
				reminder.SelectMany((c, i) => permutate(
					reminder.Take(i).Concat(reminder.Skip(i + 1)).ToArray(),
					prefix.Append(c)));
		}
	}

}
