namespace AhhRightApi.Models;

public class CompaniesHouseSearchResult
{
    public int ItemsPerPage { get; set; }
    public int StartIndex { get; set; }
    public int TotalResults { get; set; }
    public List<CompanyItem> Items { get; set; } = new();
}

public class CompanyItem
{
    public string CompanyNumber { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string CompanyStatus { get; set; } = string.Empty;
    public string CompanyType { get; set; } = string.Empty;
    public Address? Address { get; set; }
}

public class Address
{
    public string Locality { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
}

public class CompanyProfile
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyNumber { get; set; } = string.Empty;
    public List<PreviousCompanyName> PreviousCompanyNames { get; set; } = new();
}

public class PreviousCompanyName
{
    public string Name { get; set; } = string.Empty;
    public string EffectiveFrom { get; set; } = string.Empty;
    public string CeasedOn { get; set; } = string.Empty;
}
