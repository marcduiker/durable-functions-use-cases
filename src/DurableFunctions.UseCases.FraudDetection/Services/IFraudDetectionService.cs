using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface IFraudDetectionService
    {
        Task<Guid> AnalyzeAuditRecord(AuditRecord auditRecord);
    }
}