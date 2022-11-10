using MemeMakerWPF.Models;
using MemeMakerWPF.Models.API;
using MemeMakerWPF.Properties;
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

        public List<TemplateListItem> GetTemplateList()
        {
            var fullEndpoint = templateEndpoint + $"/all";

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, null));
            t.Wait();

            string jsonResult = t.Result;
            List<TemplateListItem> result = JsonConvert.DeserializeObject<List<TemplateListItem>>(jsonResult);

            return result;
        }

        public TemplateRawData GetTemplateById(int id)
        {
            var fullEndpoint = templateEndpoint + $"/{id}";

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, null));
            t.Wait();

            string jsonResult = t.Result;
            TemplateRawData result = JsonConvert.DeserializeObject<TemplateRawData>(jsonResult);

            return result;
        }

        public List<TemplateListItem> SearchForTemplate(string name)
        {
            var fullEndpoint = templateEndpoint + $"/contains";

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, new Dictionary<string, string> { { "phrase", name } }));
            t.Wait();

            string jsonResult = t.Result;
            List<TemplateListItem> result = JsonConvert.DeserializeObject<List<TemplateListItem>>(jsonResult);

            return result;
        }

        public List<TemplateListItem> GetPopularTemplates(int limit)
        {
            var fullEndpoint = templateEndpoint + $"/popular";

            var t = Task.Run(() => apiConsumer.SendGetCommand(fullEndpoint, new Dictionary<string, string> { { "limit", limit.ToString() } }));
            t.Wait();

            string jsonResult = t.Result;
            List<TemplateListItem> result = JsonConvert.DeserializeObject<List<TemplateListItem>>(jsonResult);

            return result;
        }

        public bool UploadTemplate(UploadImageData file)
        {
            var fullEndpoint = templateEndpoint;

            var t = Task.Run(() => apiConsumer.SendPostFormDataCommand(fullEndpoint, new Dictionary<string, string> { { "templateName", file.ImageName } }, file.File));
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

            var t = Task.Run(() => apiConsumer.SendPostCommand(fullEndpoint, new Dictionary<string, string> { { "templateId", templateId.ToString() }}));
            t.Wait();

            string jsonResult = t.Result;

            if (jsonResult == null)
                return false;
            else
                return true;

        }
    }
}
