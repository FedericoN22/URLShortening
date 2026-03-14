using System.Security.Cryptography;
namespace URL_Shortening.Services;

public class ShortCode
{
    public string GenerateShortCode(string url)
    {
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(url));
        var shortCode = Convert.ToBase64String(hash)
        .Replace("/", "_")
        .Replace("+", "-")
        .Substring(0, 6);

        return shortCode;
    }
}
