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
         private readonly IFraudAnalysisService _fraudAnalysisService;

         public AnalyzeAuditRecordActivity(IFraudAnalysisService fraudAnalysisService)
         {
             _fraudAnalysisService = fraudAnalysisService;
         }

        // This function simulates a call to a 3rd party service that analyses the AuditRecord.
        // The service is asynchronous and will not return a result. The service is capable of 
        // sending a webhook that contains the analysis result once that is ready. 
        // The webhook calls back to the FraudResultWebhookClient function in this Function App.
        //
        // GitHub is being used here as since that can trigger webhooks easily. 
        // In this case the webhook will be called when a new GitHub issue is created.
        [FunctionName(nameof(AnalyzeAuditRecordActivity))]
        public async Task Run(
            [ActivityTrigger] AuditRecord auditRecord)
        {
            var owner = Environment.GetEnvironmentVariable("GitHubOwner");
            var repo = Environment.GetEnvironmentVariable("GitHubRepoName");
            var workflowFile = Environment.GetEnvironmentVariable("GitHubWorkflowFile");
            var input = new WorkflowDispatchEvent(auditRecord.Id);
            var response = await _fraudAnalysisService.AnalyzeAsync(input, owner, repo, workflowFile);
            if (!response.IsSuccessStatusCode)
            {
                throw response.Error.GetBaseException();
            }
        }
    }
}