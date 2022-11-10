using MemeMakerWPF.Properties;
using MemeMakerWPF.Utility.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Managers
{
    public class ApiConnection
    {
        private readonly string templateEndpoint = "/API";
        private readonly ApiConsumer apiConsumer;

        public ApiConnection()
        {
            templateEndpoint = Settings.Default.API_ENDPOINT_RAW + templateEndpoint;
            apiConsumer = new ApiConsumer();
        }

        public bool ChceckConnection()
        {
            var fullEndpoint = templateEndpoint + $"/IsAlive";
            bool result = false;

            try
            {
                var res = TaskTimedExecution.ExecuteForTime(() => apiConsumer.SendGetCommand(fullEndpoint, null).Result, 30000);
                result = res.ExecutionSucceded && res.Result == "true";
            }
            catch
            {
                return false;
            }

            return result;
        }
    }
}
