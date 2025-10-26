namespace AhhRightApi.Models;

public class CompanySearchResponse
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public List<string> PreviousNames { get; set; } = new();
}
