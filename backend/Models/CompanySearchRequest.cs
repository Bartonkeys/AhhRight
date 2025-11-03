namespace AhhRightApi.Models;

public class CompanySearchRequest
{
    public string City { get; set; } = string.Empty;
    public string BusinessName { get; set; } = string.Empty;
}

public class StartupSearchRequest
{
    public DateTime? IncorporatedFrom { get; set; }
    public DateTime? IncorporatedTo { get; set; }
    public string? Location { get; set; }
    public List<string>? SicCodes { get; set; }
    public string? CompanyStatus { get; set; }
    public int Size { get; set; } = 50;
    public int StartIndex { get; set; } = 0;
}

public class TrendAnalysisRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public List<string>? SicCodes { get; set; }
    public string GroupBy { get; set; } = "day"; // day, week, month
}
