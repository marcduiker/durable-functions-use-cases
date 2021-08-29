using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Builders
{
    public static class AuditRecordBuilder
    {
        public static AuditRecord Create(
            Transaction transaction,
            Customer creditor,
            Customer debtor)
        {
            return new AuditRecord 
            {
                Transaction = transaction,
                Creditor = creditor,
                Debtor = debtor
            };
        }
    }
}