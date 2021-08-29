using System;

namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class AuditRecord
    {
        public Guid Id { get; set; }
        public Transaction Transaction { get; set; }
        public Customer Creditor { get; set; }
        public Customer Debtor { get; set; }
    }
}