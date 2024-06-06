using Microsoft.AspNetCore.Mvc;

namespace veeb.Controllers
{
    [Route("[controller]")]
[ApiController]
public class NordpoolController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public NordpoolController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetNordpoolPrices()
    {
        var response = await _httpClient.GetAsync("https://dashboard.elering.ee/api/nps/price");
        var responseBody = await response.Content.ReadAsStringAsync();
        return Content(responseBody, "application/json");
    }
}
}