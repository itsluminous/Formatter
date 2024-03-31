namespace ParserService;

public class Mapper
{
    private static readonly Dictionary<string, int> NumberMappings = new Dictionary<string, int>
    {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
        // Add more mappings as needed
    };

    public static string ReplaceNums(string message)
    {
        // Split the message into words
        var words = message.Split(' ');

        // Replace words representing numbers with their numeric values
        var replacedWords = words.Select(word =>
        {
            // Check if the word represents a number
            if (NumberMappings.ContainsKey(word.ToLower()))
            {
                // If yes, replace it with its numeric value
                return NumberMappings[word.ToLower()].ToString();
            }
            else
            {
                // Otherwise, keep the original word
                return word;
            }
        });

        // Join the replaced words back into a single string
        return string.Join(' ', replacedWords);
    }
}