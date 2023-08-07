using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace chessAI.Controllers
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

        [HttpGet]
        public async Task<IActionResult> MakeApiCall()
        {
            // Your access token from the Duffel API
            string accessToken = "duffel_test_ymnhCGBYCd8K0h23yJnuzRM3cZ45QZ3zVAMp1IgUqMk"; // Replace with your actual access token

            // Log a message to the console to indicate that the method is running
            Console.WriteLine("MakeApiCall method is runningss.");


            // API endpoint URL
            string apiUrl = "https://api.duffel.com/air/offer_requests";

                    // API request payload
            string requestBody = @"
             {
           ""data"": {
           ""slices"": [
      {
        ""origin"": ""LHR"",
        ""destination"": ""JFK"",
        ""departure_time"": {
          ""to"": ""17:00"",
          ""from"": ""09:45""
        },
        ""departure_date"": ""2023-09-24"",
        ""arrival_time"": {
          ""to"": ""17:00"",
          ""from"": ""09:45""
        }
      }
    ],
    
    ""passengers"": [
      {
        ""family_name"": ""Earhart"",
        ""given_name"": ""Amelia"",
        ""type"": ""adult""
      }
     
    ],
    ""max_connections"": 0,
    ""cabin_class"": ""economy""
  }
}
            ";

            Console.WriteLine(requestBody);
            // Create the HttpContent from the request body
            HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Set request headers
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           // httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v1");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            // Send the POST request and get the response
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);

            Console.WriteLine(response);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("hobbies");
                return Ok(apiResponse); // Return the API response as JSON
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorMessage);
                return BadRequest(errorMessage);
            }
            
            /*
            // Your access token from the Duffel API
            string accessToken = "duffel_test_ymnhCGBYCd8K0h23yJnuzRM3cZ45QZ3zVAMp1IgUqMk"; // Replace with your actual access token

            // Log a message to the console to indicate that the method is running
            Console.WriteLine("MakeApiCall method is running.");

            // API endpoint URL with query parameters (if any)
            string apiUrl = "https://api.duffel.com/air/orders";

            // Set request headers
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           // httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v1");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            // Send the GET request and get the response
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                return Ok(apiResponse); // Return the API response as JSON
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorMessage);
                return BadRequest(errorMessage);
            }
            */

            /*
            var sampleData = new
            {
                Name = "John Doe",
                Age = 30,
                Email = "john@example.com"
            };

            Console.WriteLine(sampleData);

            return Ok(sampleData);

            */
            

        }

        [HttpPost]
        public async Task<IActionResult> TriggerApiCall()
        {
            // Call the MakeApiCall method to make the API call
            var result = await MakeApiCall();

            // Return the result (success or error message) to the frontend
            return Ok(result);
        }
    }
}