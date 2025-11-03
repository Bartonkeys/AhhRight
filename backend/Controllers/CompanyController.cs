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

    [HttpPost("startups/feed")]
    public async Task<ActionResult<StartupFeedResponse>> GetStartupFeed([FromBody] StartupSearchRequest request)
    {
        var results = await _companiesHouseService.GetNewlyRegisteredCompanies(request);
        return Ok(results);
    }

    [HttpGet("startups/daily")]
    public async Task<ActionResult<StartupFeedResponse>> GetDailyStartups(
        [FromQuery] string? location = null,
        [FromQuery] string? sicCodes = null,
        [FromQuery] int size = 50)
    {
        var request = new StartupSearchRequest
        {
            IncorporatedFrom = DateTime.Today,
            IncorporatedTo = DateTime.Today,
            Location = location,
            SicCodes = string.IsNullOrEmpty(sicCodes) ? null : sicCodes.Split(',').ToList(),
            CompanyStatus = "active",
            Size = size
        };

        var results = await _companiesHouseService.GetNewlyRegisteredCompanies(request);
        return Ok(results);
    }

    [HttpPost("startups/trends")]
    public async Task<ActionResult<TrendAnalysisResponse>> GetStartupTrends([FromBody] TrendAnalysisRequest request)
    {
        if (request.StartDate >= request.EndDate)
        {
            return BadRequest("Start date must be before end date");
        }

        var results = await _companiesHouseService.GetStartupTrends(request);
        return Ok(results);
    }
}
