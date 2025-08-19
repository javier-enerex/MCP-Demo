using System.ComponentModel;

public class PagedResult<T>
{
    [Description("OpportunitiyItem list")]
    public IEnumerable<T> Items { get; set; }
    [Description("next cursor value, if empty no more result, if not empty needs pagination")]
    public string NextCursor { get; set; }
    [Description("Total results")]
    public int Total { get; set; }
}