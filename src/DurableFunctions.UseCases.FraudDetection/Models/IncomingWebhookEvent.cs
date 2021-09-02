using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class IncomingWebhookEvent
    {
        [JsonProperty("issue")]
        public Payload Payload { get; set; }
    }

    public class Payload
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
        public FraudResult FraudResult => !string.IsNullOrEmpty(Body) ? JsonConvert.DeserializeObject<FraudResult>(Body) : null;
        
    }
}