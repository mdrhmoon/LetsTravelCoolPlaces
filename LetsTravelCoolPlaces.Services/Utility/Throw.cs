namespace LetsTravelCoolPlaces.Services.Utility;

public static class Throw
{
    public static void Exception(string message) => throw new Exception(message);
    public static int IfNull(object? obj, string message) => (obj is null) ? throw new Exception(message) : 0;
}