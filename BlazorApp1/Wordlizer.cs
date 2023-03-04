namespace BlazorApp1
{
    public class Wordlizer
    {
        public IEnumerable<string> GeneratePermutations(string input)
        {
            if (input == null) return Enumerable.Empty<string>();

            if (input.Length < 5)
            {
                input = input + new string('-', 5 - input.Length);
            }
            return input
                .OrDefault()
                .Permutate()
                .Distinct();
        }

    }
}
