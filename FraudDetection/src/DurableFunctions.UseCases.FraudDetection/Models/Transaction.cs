using System;
using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("creditorBankAccount")]
        public string CreditorBankAccount { get; set; }
        
        [JsonProperty("debtorBankAccount")]
        public string DebtorBankAccount { get; set; }
        
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("currency")]
        public string Currency { get; set; }
        
        [JsonProperty("timeStampUtc")]
        public DateTime TimeStampUtc { get; set; }
    
    }
}