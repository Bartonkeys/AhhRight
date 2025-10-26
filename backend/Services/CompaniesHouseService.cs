using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AhhRightApi.Models;

namespace AhhRightApi.Services;

public class CompaniesHouseService : ICompaniesHouseService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _apiKey;

    public CompaniesHouseService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _apiKey = (_configuration["CompaniesHouse:ApiKey"] ?? "").Trim();
        
        if (string.IsNullOrEmpty(_apiKey))
        {
            Console.WriteLine("WARNING: CompaniesHouse API key is not configured!");
        }
        else
        {
            Console.WriteLine($"CompaniesHouse API key loaded (length: {_apiKey.Length})");
        }
        
        _httpClient.BaseAddress = new Uri("https://api.company-information.service.gov.uk");
        
        var authValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_apiKey}:"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authValue);
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("AhhRight/1.0");
    }

    public async Task<List<CompanySearchResponse>> SearchCompaniesByNameAndCity(string businessName, string city)
    {
        var results = new List<CompanySearchResponse>();

        try
        {
            var searchQuery = $"{businessName} {city}";
            var url = $"/search/companies?q={Uri.EscapeDataString(searchQuery)}";
            var response = await _httpClient.GetAsync(url);
            
            Console.WriteLine($"CH {url} -> {(int)response.StatusCode} {response.ReasonPhrase}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Companies House API error: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error details: {errorContent}");
                return results;
            }

            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<CompaniesHouseSearchResult>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (searchResult?.Items == null || !searchResult.Items.Any())
            {
                return results;
            }

            foreach (var company in searchResult.Items.Where(c => 
                c.Address?.Locality?.Contains(city, StringComparison.OrdinalIgnoreCase) == true))
            {
                var companyProfile = await GetCompanyProfile(company.CompanyNumber);
                
                if (companyProfile != null)
                {
                    results.Add(new CompanySearchResponse
                    {
                        CompanyName = companyProfile.CompanyName,
                        CompanyNumber = companyProfile.CompanyNumber,
                        PreviousNames = companyProfile.PreviousCompanyNames
                            .Select(p => $"{p.Name} (until {p.CeasedOn})")
                            .ToList()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching companies: {ex.Message}");
        }

        return results;
    }

    private async Task<CompanyProfile?> GetCompanyProfile(string companyNumber)
    {
        try
        {
            var url = $"/company/{companyNumber}";
            var response = await _httpClient.GetAsync(url);
            
            Console.WriteLine($"CH {url} -> {(int)response.StatusCode} {response.ReasonPhrase}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Companies House API error getting profile: {response.StatusCode}");
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error details: {errorContent}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var profile = JsonSerializer.Deserialize<CompanyProfile>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return profile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting company profile: {ex.Message}");
            return null;
        }
    }
}
