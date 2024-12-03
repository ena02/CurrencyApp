using CurrencyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly CurrencyRateService _currencyRateService;
        private readonly IConfiguration _configuration;

        public CurrencyRateController(CurrencyRateService currencyRateService, IConfiguration configuration)
        {
            _currencyRateService = currencyRateService;
            _configuration = configuration;
        }

        [HttpGet("rate_usd_kzt")]
        public async Task<IActionResult> GetUsdToKztRate()
        {
            var url = _configuration["CurrencyRateConfig:RateUrl"];
            var xPath = _configuration["CurrencyRateConfig:UsdXPath"];

            try
            {
                var rate = await _currencyRateService.GetUsdToKztRateAsync(url, xPath);
                return Ok(rate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при получении курса: {ex.Message}");
            }
        }
    }
}
