using MemeMakerWPF.Models;
using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Managers
{
    public class ApiConsumer
    {
        public async Task<string> SendGetCommand(string apiEndpoint, IDictionary<string, string> parameters, CancellationTokenSource tokenSource = null)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            using (var clientHandler = new HttpClientHandler())
            using (var client = new HttpClient(clientHandler))
            {
                if (parameters != null)
                    if (parameters.Count > 0)
                        fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

                try
                {
                    HttpResponseMessage result;
                    client.DefaultRequestHeaders.Add("X-Api-Key", Settings.Default.API_KEY);

                    if (tokenSource == null)
                        result = await client.GetAsync(fullUrl);
                    else
                        result = await client.GetAsync(fullUrl, tokenSource.Token);

                    if (result.IsSuccessStatusCode)
                        return await result.Content.ReadAsStringAsync();
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<string> SendPostCommand(string apiEndpoint, IDictionary<string, string> parameters, CancellationTokenSource tokenSource = null)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            using (var clientHandler = new HttpClientHandler())
            using (var client = new HttpClient(clientHandler))
            {
                if (parameters != null)
                    if (parameters.Count > 0)
                        fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

                try
                {
                    HttpResponseMessage result;
                    client.DefaultRequestHeaders.Add("X-Api-Key", Settings.Default.API_KEY);

                    if (tokenSource == null)
                        result = await client.PostAsync(fullUrl, queryParams);
                    else
                        result = await client.PostAsync(fullUrl, queryParams, tokenSource.Token);

                    if (result.IsSuccessStatusCode)
                        return await result.Content.ReadAsStringAsync();
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<string> SendPostFormDataCommand(string apiEndpoint, IDictionary<string, string> parameters, FileUploadModel file, CancellationTokenSource tokenSource = null)
        {
            string fullUrl = apiEndpoint;
            var queryParams = new FormUrlEncodedContent(parameters ?? new Dictionary<string, string>());

            if (parameters != null)
                if (parameters.Count > 0)
                    fullUrl += $"?{queryParams.ReadAsStringAsync().Result}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", Settings.Default.API_KEY);
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), fullUrl))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");

                    var multipartContent = new MultipartFormDataContent();
                    var fileData = new ByteArrayContent(file.FileData);
                    fileData.Headers.Add("Content-Type", file.ContentType);
                    multipartContent.Add(fileData, "image", file.FileName);
                    request.Content = multipartContent;

                    try
                    {
                        HttpResponseMessage response;
                        if (tokenSource == null)
                            response = await httpClient.SendAsync(request);
                        else
                            response = await httpClient.SendAsync(request, tokenSource.Token);

                        if (response.IsSuccessStatusCode)
                            return await response.Content.ReadAsStringAsync();
                        else
                        {
                            var what = await response.Content.ReadAsStringAsync();
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

    }
}
