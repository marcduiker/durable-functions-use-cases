using System;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BankAccount { get; set; }
        public string CountryCode { get; set; }
    }
}