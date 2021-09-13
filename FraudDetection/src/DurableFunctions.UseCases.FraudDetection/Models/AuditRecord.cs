using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class AuditRecord
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }
        
        [JsonProperty("creditor")]
        public Customer Creditor { get; set; }
        
        [JsonProperty("debtor")]
        public Customer Debtor { get; set; }
        
        [JsonProperty("isSuspiciousTransaction")]
        public bool IsSuspiciousTransaction { get; set; }
    }
}