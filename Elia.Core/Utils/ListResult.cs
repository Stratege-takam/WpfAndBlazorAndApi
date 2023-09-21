namespace Elia.Core.Utils;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class ListResult<T>
{
    /// <summary>
    /// Get and set count
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Get and set results
    /// </summary>
    public IEnumerable<T> Results { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="count"></param>
    /// <param name="results"></param>
    public ListResult(IEnumerable<T> results, int count)
    {
        Count = count;
        Results = results;
    }
}