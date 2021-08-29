namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string CreditorBankAccount { get; set; }
        public string DebtorBankAccount { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime TimeStampUtc { get; set; }
    
    }
}