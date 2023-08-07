using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PLANEY.Models
{
    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> MakeApiCall()
        {
            // Your access token from the Duffel API
            string accessToken = "duffel_test_ymnhCGBYCd8K0h23yJnuzRM3cZ45QZ3zVAMp1IgUqMk"; // Replace with your actual access token

            // API endpoint URL
            string apiUrl = "https://api.duffel.com/air/orders";

            // Set request headers
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            httpClient.DefaultRequestHeaders.Add("Duffel-Version", "v1");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            // Send the GET request and get the response
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            // Check if the response was successful
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Failed to fetch data from the external API.";
            }
        }
    }
}


