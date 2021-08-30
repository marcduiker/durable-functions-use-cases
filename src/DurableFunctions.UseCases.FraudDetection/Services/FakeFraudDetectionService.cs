using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class FakeFraudDetectionService : IFraudDetectionService
    {
        public async Task<string> AnalyzeAuditRecord(AuditRecord auditRecord)
        {
            return await Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}