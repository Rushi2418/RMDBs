    using Newtonsoft.Json;
    using RMDB_Utility;
    using RMDBs_Web.Models;
using RMDBs_Web.Services.IServices;
using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
using static RMDB_Utility.Class1;

namespace RMDBs_Web.Services
    {
        public class BaseServices : IBasesevices
        {
            private readonly IHttpClientFactory _httpClientFactory;
            private APIResponse<object>? _response;


            public BaseServices(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

        public APIResponse<object> Response { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<APIResponse<T>> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var message = new HttpRequestMessage();

                // Set API Type (GET, POST, PUT, DELETE)
                message.Method = apiRequest.apiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                // Add URL
                message.RequestUri = new Uri(apiRequest.Url);

                // Add Authorization Header for requests (except Login & Register)
                if (!string.IsNullOrEmpty(apiRequest.Token) && apiRequest.Url.ToLower().Contains("auth") == false)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                // Add request body for POST/PUT
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                // Send Request
                HttpResponseMessage apiResponse = await client.SendAsync(message);
                string apiContent = await apiResponse.Content.ReadAsStringAsync();

                APIResponse<T> response = JsonConvert.DeserializeObject<APIResponse<T>>(apiContent);

                return response;
            }
            catch (Exception ex)
            {
                return new APIResponse<T> { IsSuccess = false, ErrorMessages = new List<string> { ex.Message } };
            }
        }



    }
}
