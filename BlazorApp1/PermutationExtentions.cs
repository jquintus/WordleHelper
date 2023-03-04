namespace BlazorApp1
{
	public static class Extentions
	{
		public static string JoinToString(this IEnumerable<string> items, string separator = ", '")
		{
			return string.Join(separator, items);
		}
	}
}
