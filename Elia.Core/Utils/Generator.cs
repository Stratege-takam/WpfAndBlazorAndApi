namespace Elia.Core.Utils;

/// <summary>
/// 
/// </summary>
public  class Generator
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GenerateDigit()
    {
        System.Threading.Thread.Sleep(10);
        Random generator = new Random();
        String r = generator.Next(0, 1000000000).ToString("D9");

        return r;
    }
}