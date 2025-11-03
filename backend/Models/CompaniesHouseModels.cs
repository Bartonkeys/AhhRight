using System.Text.Json.Serialization;

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

public class AdvancedSearchResult
{
    [JsonPropertyName("etag")]
    public string Etag { get; set; } = string.Empty;
    
    [JsonPropertyName("hits")]
    public string Hits { get; set; } = string.Empty;
    
    [JsonPropertyName("items")]
    public List<AdvancedCompanyItem> Items { get; set; } = new();
    
    [JsonPropertyName("kind")]
    public string Kind { get; set; } = string.Empty;
}

public class AdvancedCompanyItem
{
    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; } = string.Empty;
    
    [JsonPropertyName("company_number")]
    public string CompanyNumber { get; set; } = string.Empty;
    
    [JsonPropertyName("company_status")]
    public string CompanyStatus { get; set; } = string.Empty;
    
    [JsonPropertyName("registered_office_address")]
    public RegisteredOfficeAddress? RegisteredOfficeAddress { get; set; }
}

public class RegisteredOfficeAddress
{
    [JsonPropertyName("address_line_1")]
    public string AddressLine1 { get; set; } = string.Empty;
    
    [JsonPropertyName("address_line_2")]
    public string AddressLine2 { get; set; } = string.Empty;
    
    [JsonPropertyName("locality")]
    public string Locality { get; set; } = string.Empty;
    
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty;
    
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;
    
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
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
