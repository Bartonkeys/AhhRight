using Microsoft.AspNetCore.Mvc;
using AhhRightApi.Models;
using AhhRightApi.Services;

namespace AhhRightApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompaniesHouseService _companiesHouseService;

    public CompanyController(ICompaniesHouseService companiesHouseService)
    {
        _companiesHouseService = companiesHouseService;
    }

    [HttpPost("search")]
    public async Task<ActionResult<List<CompanySearchResponse>>> SearchCompanies([FromBody] CompanySearchRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.BusinessName) || string.IsNullOrWhiteSpace(request.City))
        {
            return BadRequest("Business name and city are required");
        }

        var results = await _companiesHouseService.SearchCompaniesByNameAndCity(request.BusinessName, request.City);
        return Ok(results);
    }
}
