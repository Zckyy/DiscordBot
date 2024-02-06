using Newtonsoft.Json;
using RestSharp;

namespace DiscordBotTemplateNet8.Helper
{
    public class WebHelper
    {
        public static async Task<Dictionary<string, object>> GetJsonFromApi(string apiUrl)
        {
            var client = new RestClient();
            var request = new RestRequest(apiUrl, Method.Get);

            // Execute the request
            RestResponse? response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Deserialize the JSON response into a dictionary
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                return jsonResponse;
            }
            else
            {
                // Handle the case when the request was not successful
                Console.WriteLine($"Error occurred: {response.ErrorMessage}");
                return null;
            }
        }
    }
}
