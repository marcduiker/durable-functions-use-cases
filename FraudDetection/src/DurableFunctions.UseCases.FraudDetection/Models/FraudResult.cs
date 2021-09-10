using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class FraudResult
    {
        [JsonProperty("recordId")]
        public string RecordId { get; set; }

        [JsonProperty("isSuspiciousTransaction")]
        public bool IsSuspiciousTransaction { get; set; }
    }
}