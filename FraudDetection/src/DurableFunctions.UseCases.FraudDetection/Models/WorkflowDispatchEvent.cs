using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class WorkflowDispatchEvent
    {
        public WorkflowDispatchEvent(string recordId)
        {
            Ref = "main";
            Inputs = new { recordId = recordId };
        }

        [JsonProperty("ref")]
        public string Ref { get; set;}

        [JsonProperty("inputs")]
        public object Inputs { get; set; }
    }
}