using System;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Builders
{
    public static class AuditRecordBuilder
    {
        public static AuditRecord Create(
            string id,
            Transaction transaction,
            Customer creditor,
            Customer debtor)
        {
            return new AuditRecord 
            {
                Id = id,
                Transaction = transaction,
                Creditor = creditor,
                Debtor = debtor
            };
        }
    }
}