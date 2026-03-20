namespace URL_Shortening.Services;

public static class ShortCode
{
    private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string GenerateShortCode(int id)
    {
        return EncodeToBase62(id);
    }

    private static string EncodeToBase62(int number)
    {
        if (number == 0) return "0";

        var result = new char[11];
        var index = result.Length;

        while (number > 0)
        {
            result[--index] = Chars[(int)(number % 62)];
            number /= 62;
        }

        return new string(result, index, result.Length - index);
    }
}
