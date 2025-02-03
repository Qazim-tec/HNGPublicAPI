using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace HngAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberClassificationController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet("classify-number")]
        public async Task<IActionResult> ClassifyNumber([FromQuery] string number)
        {
            // Check if the input is a valid integer
            if (!int.TryParse(number, out int validNumber))
            {
                return BadRequest(new { number = number, error = true });
            }

            // Initialize the properties list to store classification results
            var properties = new List<string>();

            // Check if the number is an Armstrong number
            if (IsArmstrong(validNumber)) properties.Add("armstrong");

            // Check if the number is odd or even
            properties.Add(validNumber % 2 == 0 ? "even" : "odd");

            // Check if the number is prime and perfect
            bool isPrime = IsPrime(validNumber);
            bool isPerfect = IsPerfect(validNumber);

            // Calculate the sum of the digits of the number
            var digitSum = validNumber.ToString().Sum(c => c - '0');

            // Get the fun fact about the number
            var funFact = await GetFunFact(validNumber);

            // Return the response in the required JSON format
            return Ok(new
            {
                number = validNumber,
                is_prime = isPrime,
                is_perfect = isPerfect,
                properties,
                digit_sum = digitSum,
                fun_fact = funFact
            });
        }

        // Helper method to check if a number is prime
        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        // Helper method to check if a number is perfect
        private bool IsPerfect(int number)
        {
            int sum = 0;
            for (int i = 1; i <= number / 2; i++)
            {
                if (number % i == 0) sum += i;
            }
            return sum == number;
        }

        // Helper method to check if a number is an Armstrong number
        private bool IsArmstrong(int number)
        {
            int sum = 0, temp = number, digits = number.ToString().Length;
            while (temp != 0)
            {
                int remainder = temp % 10;
                sum += (int)Math.Pow(remainder, digits);
                temp /= 10;
            }
            return sum == number;
        }

        // Helper method to get a fun fact from the Numbers API
        private async Task<string> GetFunFact(int number)
        {
            try
            {
                var response = await client.GetStringAsync($"http://numbersapi.com/{number}?json");
                var jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(response);
                return jsonResponse["text"].ToString();
            }
            catch (Exception)
            {
                return "No fun fact available.";
            }
        }
    }
}