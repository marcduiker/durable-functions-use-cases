using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using DurableFunctions.UseCases.FraudDetection.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctions.UseCases.FraudDetection.Activities
{
    public class AnalyzeAuditRecordActivity
    {
         private readonly IFraudDetectionService _fraudDetectionService;

         public AnalyzeAuditRecordActivity(IFraudDetectionService fraudDetectionService)
         {
             _fraudDetectionService = fraudDetectionService;
         }

        [FunctionName(nameof(AnalyzeAuditRecordActivity))]
        public async Task<string> Run(
            [ActivityTrigger] AuditRecord auditRecord)
        {
            return await _fraudDetectionService.AnalyzeAuditRecordAsync(auditRecord);
        }
    }
}