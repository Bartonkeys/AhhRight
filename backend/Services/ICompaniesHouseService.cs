using AhhRightApi.Models;

namespace AhhRightApi.Services;

public interface ICompaniesHouseService
{
    Task<List<CompanySearchResponse>> SearchCompaniesByNameAndCity(string businessName, string city);
}
