using System.Text;
using Microsoft.AspNetCore.Mvc;
using PLANEY.Models;
using Newtonsoft.Json;
namespace PLANEY.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlaneController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PlaneController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
          
            return View();
        }

        [HttpPost("MakeApiCall")]
        public async Task<IActionResult> MakeApiCall([FromBody] ApiRequestModel requestModel)
        {
            // Your access token from the Duffel API
            string accessToken = "duffel_test_ymnhCGBYCd8K0h23yJnuzRM3cZ45QZ3zVAMp1IgUqMk"; // Replace with your actual access token

            // Log a message to the console to indicate that the method is running
            Console.WriteLine("MakeApiCall method is running.");

            // API endpoint URL
            string apiUrl = "https://api.duffel.com/air/offer_requests";

            // Build the API request payload using the user input
            var requestBody = new
            {
                data = new
                {
                    slices = requestModel.Slices.Select(slice => new
                    {
                        origin = slice.Origin,
                        destination = slice.Destination,
                        departure_time = new
                        {
                            to = "17:00",
                            from = "09:45"
                        },
                        departure_date = "2023-09-24",
                        arrival_time = new
                        {
                            to = "17:00",
                            from = "09:45"
                        }
                    }).ToArray(),
                    passengers = new[]
                    {
                new
                {
                    family_name = "Earhart",
                    given_name = "Amelia",
                    type = "adult"
                }
            },
                    max_connections = 0,
                    cabin_class = "economy"
                }
            };

            // Serialize the request payload to JSON
            string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

            // Create the HttpContent from the request body
            HttpContent httpContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // Set request headers
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v1");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            // Send the POST request and get the response
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + apiResponse);
                return Ok(apiResponse); // Return the API response as JSON
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Message: " + errorMessage);
                return BadRequest(errorMessage);
            }
        }
    }
}