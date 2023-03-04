namespace BlazorApp1
{
    public static class PermutationExtentions
	{
		public static string OrDefault(this string source) => source ?? string.Empty;

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
