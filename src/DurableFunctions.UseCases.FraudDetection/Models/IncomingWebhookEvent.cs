using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class IncomingWebhookEvent
    {
        [JsonProperty("issue")]
        public Payload Payload { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }
    }

    public class Payload
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        
        [JsonIgnore]
        public FraudResult FraudResult => !string.IsNullOrEmpty(Body) ? JsonConvert.DeserializeObject<FraudResult>(Body) : null;
        
    }
}