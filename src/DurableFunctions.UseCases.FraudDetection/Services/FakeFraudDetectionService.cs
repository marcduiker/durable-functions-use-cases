namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class FakeFraudDetectionService : IFraudDetectionService
    {
        public async Task<Guid> AnalyzeAuditRecord(AuditRecord auditRecord)
        {
            return await Task.FromResult(Guid.NewGuid);
        }
    }
}