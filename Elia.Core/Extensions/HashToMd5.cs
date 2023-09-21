using System.Security.Cryptography;
using System.Text;

namespace Elia.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class HashToMd5
{
    public static string Hash(this string input)
    {
        byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder stringBuilder = new StringBuilder();
        int index = 0;
        while (index < hash.Length)
        {
            stringBuilder.Append(hash[index].ToString("x2"));
            checked { ++index; }
        }
        return stringBuilder.ToString();
    }

    public static bool checkMd5Hash(this string input, string hash)
    {
        MD5.Create();
        return StringComparer.OrdinalIgnoreCase.Compare(input.Hash(), hash) == 0;
    }
}