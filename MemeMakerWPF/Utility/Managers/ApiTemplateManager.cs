using MemeMakerWPF.Models;
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
    public class ApiTemplateManager
    {
        private readonly string templateEndpoint = "/Template";
        private readonly ApiConsumer apiConsumer;

        public ApiTemplateManager()
        {
            templateEndpoint = Settings.Default.API_ENDPOINT_RAW + templateEndpoint;
            apiConsumer = new ApiConsumer();
        }

        /// <summary>
        /// Download image data of template with given Id
        /// </summary>
        /// <param name="id">Id of template given in <seealso cref="TemplateListItem.Id"/></param>
        /// <returns><seealso cref="TemplateRawData"/></returns>
        public TemplateRawData GetTemplateById(int id)
        {
            var fullEndpoint = templateEndpoint + $"/{id}";

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, null));
            t.Wait();

            string jsonResult = t.Result;
            TemplateRawData result = JsonConvert.DeserializeObject<TemplateRawData>(jsonResult);

            return result;
        }

        /// <summary>
        /// Reuturns list of templates which name or filename contains given phrase
        /// </summary>
        /// <param name="name">Name of searched template</param>
        /// <returns><seealso cref="TemplateListItem"/></returns>
        public List<TemplateListItem> SearchForTemplate(string name)
        {
            var fullEndpoint = templateEndpoint + $"/contains";

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, new Dictionary<string, string> { { "phrase", name } }));
            t.Wait();

            string jsonResult = t.Result;

            List<TemplateListItem> result = new List<TemplateListItem>();
            if (jsonResult != null)
                result = JsonConvert.DeserializeObject<List<TemplateListItem>>(jsonResult);

            return result;
        }

        /// <summary>
        /// Get list of most popular templates with number restriction.
        /// </summary>
        /// <param name="limit">Limit of templates per request</param>
        /// <returns><seealso cref="TemplateListItem"/></returns>
        public List<TemplateListItem> GetPopularTemplates(int limit)
        {
            var fullEndpoint = templateEndpoint + $"/popular";

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, 
                                                              new Dictionary<string, string> {{ "limit", limit.ToString() }}, 
                                                              cts));
            t.Wait();

            string jsonResult = t.Result;

            List<TemplateListItem> result = new List<TemplateListItem>();
            if(jsonResult != null)
                result = JsonConvert.DeserializeObject<List<TemplateListItem>>(jsonResult);

            return result;
        }

        /// <summary>
        /// Upload template from device
        /// </summary>
        /// <param name="file">Information about file and file data itself, see also <seealso cref="UploadImageData"/></param>
        /// <returns>Boolean result of operation</returns>
        public bool UploadTemplate(UploadImageData file)
        {
            var fullEndpoint = templateEndpoint;

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendPostFormDataCommand(fullEndpoint, 
                                                                       new Dictionary<string, string> { { "templateName", file.ImageName } }, 
                                                                       file.File, 
                                                                       cts));
            t.Wait();

            string jsonResult = t.Result;

            if (jsonResult == null)
                return false;
            else
                return true;

        }

        public bool SaveUsage(int templateId)
        {
            var fullEndpoint = templateEndpoint + "/usage";

            var cts = AppCancellationToken.LinkedTokenSource;
            cts.CancelAfter(TimeSpan.FromSeconds(Settings.Default.API_REQUEST_TIMEOUT));

            var t = Task.Run(() => apiConsumer.SendPostCommand(fullEndpoint, 
                                                               new Dictionary<string, string> { { "templateId", templateId.ToString() }}, 
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
