using MemeMakerWPF.Models.API;
using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Managers
{
    public class ApiMemeManager
    {
        private readonly string memeEndpoint = "/Memes";
        private readonly ApiConsumer apiConsumer;

        public ApiMemeManager()
        {
            memeEndpoint = Settings.Default.API_ENDPOINT_RAW + memeEndpoint;
            apiConsumer = new ApiConsumer();
        }

        public bool UploadMeme(int templateId, UploadImageData file)
        {
            var fullEndpoint = memeEndpoint;

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendPostFormDataCommand(fullEndpoint, 
                                                                       new Dictionary<string, string> { { "templateId", templateId.ToString() }, 
                                                                                                        { "memeName", file.ImageName } }, 
                                                                       file.File,
                                                                       cts));
            t.Wait();

            string jsonResult = t.Result;

            if (jsonResult == null)
                return false;
            else
                return true;

        }

        public List<MemeRawData> GetRecentMemes(int skip, int take)
        {
            var fullEndpoint = memeEndpoint + "/recent-images";

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, 
                                                              new Dictionary<string, string> { { "skip", skip.ToString() }, 
                                                                                               { "take", take.ToString() } },
                                                              cts));
            t.Wait();

            string jsonResult = t.Result;

            List<MemeRawData> result = new List<MemeRawData>();
            if (jsonResult != null)
                result = JsonConvert.DeserializeObject<List<MemeRawData>>(jsonResult);

            return result;
        }
    }
}
