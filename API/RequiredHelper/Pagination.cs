using System;

namespace API.RequiredHelper;

public class Pagination<T>(int PageIndex, int PageSize, int Count, IEnumerable<T> Items)
{
    public int PageIndex { get; set; } = PageIndex;
    public int PageSize { get; set; } = PageSize;

    public int Count { get; set; } = Count;

    public IEnumerable<T> Items { get; set; } = Items;
}
