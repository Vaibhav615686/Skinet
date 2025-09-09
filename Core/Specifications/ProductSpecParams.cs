using System;

namespace Core.Specifications;

public class ProductSpecParams
{
    public int MaxpageSize = 50;

    public int _pageIndex;
    public int PageIndex
    {
        get => _pageIndex;
        set { _pageIndex = value; }
    }

    public int _pageSize;
    public int PageSize
    {
        get => _pageSize;
        set { _pageSize = value > MaxpageSize ? MaxpageSize : value; }
    }

    private string _search="";
    public string Search
    {
        get => _search??"";
        set { _search = value.ToLower(); }
    }
    

    public bool IsPaginationEnabled = false;

    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands;
        set
        {
            _brands = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get => _types;
        set
        {
            _types = value.SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    
    public string? Sort { get; set; }
    
}
