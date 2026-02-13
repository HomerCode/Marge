using System.Security.Cryptography;
using System.Text;

namespace Marge.Hasher;

public static class Utility
{
    public static string Convert(string str)
    {
        using var hasher = SHA256.Create();

        var hashBytes = hasher.ComputeHash(
            Encoding.UTF8.GetBytes(str)
        );

        var hashHex = BitConverter.ToString(hashBytes)
            .Replace("-", "")
            .ToLower();

        return hashHex;
    }
}
