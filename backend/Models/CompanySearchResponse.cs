namespace AhhRightApi.Models;

public class CompanySearchResponse
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public List<string> PreviousNames { get; set; } = new();
}

public class StartupSearchResponse
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public string CompanyStatus { get; set; } = string.Empty;
    public DateTime? DateOfCreation { get; set; }
    public string? Location { get; set; }
    public List<string> SicCodes { get; set; } = new();
    public string? CompanyType { get; set; }
}

public class StartupFeedResponse
{
    public int TotalResults { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public List<StartupSearchResponse> Companies { get; set; } = new();
}

public class TrendDataPoint
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
    public string? Location { get; set; }
    public string? SicCode { get; set; }
}

public class TrendAnalysisResponse
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalCompanies { get; set; }
    public List<TrendDataPoint> TrendData { get; set; } = new();
    public Dictionary<string, int> LocationBreakdown { get; set; } = new();
    public Dictionary<string, int> IndustryBreakdown { get; set; } = new();
}
