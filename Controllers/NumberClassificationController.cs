using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HngAPI.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class NumberClassificationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NumberClassificationController> _logger;

        public NumberClassificationController(
            IHttpClientFactory httpClientFactory,
            ILogger<NumberClassificationController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("NumbersApi");
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ClassifyNumber([FromQuery] string number)
        {
            if (!int.TryParse(number, out int num))
            {
                return BadRequest(new { number = number, error = true });
            }

            try
            {
                var isPrime = NumberHelper.IsPrime(num);
                var isPerfect = NumberHelper.IsPerfect(num);
                var digitSum = NumberHelper.CalculateDigitSum(num);
                var isArmstrong = NumberHelper.IsArmstrongNumber(num);
                var parity = num % 2 == 0 ? "even" : "odd";

                var properties = new List<string>();
                if (isArmstrong) properties.Add("armstrong");
                properties.Add(parity);

                var funFact = await GetFunFact(num);

                return Ok(new
                {
                    number = num,
                    is_prime = isPrime,
                    is_perfect = isPerfect,
                    properties,
                    digit_sum = digitSum,
                    fun_fact = funFact
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing number classification");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        private async Task<string> GetFunFact(int number)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{number}/math?json");
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadFromJsonAsync<NumbersApiResponse>();
                return jsonResponse?.Text ?? "No fun fact available";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching fun fact");
                return "Fun fact unavailable at this time";
            }
        }
    }

    public class NumbersApiResponse
    {
        public string Text { get; set; } = string.Empty;
    }
}
