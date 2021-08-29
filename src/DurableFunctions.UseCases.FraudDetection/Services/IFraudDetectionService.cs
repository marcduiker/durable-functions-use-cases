namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface IFraudDetectionService
    {
        Task<Guid> AnalyzeAuditRecord(AuditRecord auditRecord);
    }
}