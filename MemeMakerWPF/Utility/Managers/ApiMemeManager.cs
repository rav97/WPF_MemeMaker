using MemeMakerWPF.Models.API;
using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Managers
{
    public class ApiMemeManager
    {
        private readonly string templateEndpoint = "/Memes";
        private readonly ApiConsumer apiConsumer;

        public ApiMemeManager()
        {
            templateEndpoint = Settings.Default.API_ENDPOINT_RAW + templateEndpoint;
            apiConsumer = new ApiConsumer();
        }

        public bool UploadMeme(int templateId, UploadImageData file)
        {
            var fullEndpoint = templateEndpoint;

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
    }
}
