namespace DurableFunctions.UseCases.FraudDetection.Models
{
    public class FraudResult
    {
        public string RecordId { get; set; }
        public bool IsSuspiciousTransaction { get; set; }
    }
}