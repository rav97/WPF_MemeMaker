using MemeMakerWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Managers
{
    public class ApiConsumer
    {
        public async Task<string> SendGetCommand(string apiEndpoint, IDictionary<string, string> parameters)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            using (var clientHandler = new HttpClientHandler())
            using (var client = new HttpClient(clientHandler))
            {
                if (parameters != null)
                    if (parameters.Count > 0)
                        fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

                var result = await client.GetAsync(fullUrl);

                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStringAsync();
                else
                    return null;
            }
        }

        public async Task<string> SendPostCommand(string apiEndpoint, IDictionary<string, string> parameters)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            using (var clientHandler = new HttpClientHandler())
            using (var client = new HttpClient(clientHandler))
            {
                if (parameters != null)
                    if (parameters.Count > 0)
                        fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

                var result = await client.PostAsync(fullUrl, queryParams);

                if (result.IsSuccessStatusCode)
                    return await result.Content.ReadAsStringAsync();
                else
                    return null;
            }
        }

        public async Task<string> SendPostFormDataCommand(string apiEndpoint, IDictionary<string, string> parameters, FileUploadModel file)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            if (parameters != null)
                if (parameters.Count > 0)
                    fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), fullUrl))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var multipartContent = new MultipartFormDataContent();
                    var fileData = new ByteArrayContent(file.FileData);
                    fileData.Headers.Add("Content-Type", file.ContentType);
                    multipartContent.Add(fileData, "image", file.FileName);
                    request.Content = multipartContent;

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        return null;
                }
            }
        }

    }
}
