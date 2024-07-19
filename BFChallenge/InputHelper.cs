namespace BFChallenge;

public static class InputHelper
{
    public static decimal? GetDecimalInput(string prompt)
    {
        Console.WriteLine(prompt);
        var amountFromUser = Console.ReadLine();
        var success = decimal.TryParse(amountFromUser, out var parseResult);
        if (success)
            return parseResult;

        return null;
    }

    public static int? GetIntegerInput(string prompt)
    {
        Console.WriteLine(prompt);
        var amountFromUser = Console.ReadLine();
        var success = int.TryParse(amountFromUser, out var parseResult);
        if (success)
            return parseResult;

        return null;
    }
}
