using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using Refit;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface IFraudDetectionService
    {
        [Post("/FakeFraudDetectionService")]
        Task<string> AnalyzeAuditRecordAsync(AuditRecord auditRecord);

        [Post("/FraudResultWebhookClient")]
        Task CallWebhookAsync(FraudResult fraudResult);
    }
}