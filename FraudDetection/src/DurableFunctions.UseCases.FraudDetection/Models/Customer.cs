using System;
using Newtonsoft.Json;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("bankAccount")]
        public string BankAccount { get; set; }
        
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }
}