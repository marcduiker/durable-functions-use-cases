using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;
using Octokit;

namespace DurableFunctions.UseCases.FraudDetection.Activities
{
    public class AnalyzeAuditRecordActivity
    {
         private readonly IGitHubClient _githubClient;

         public AnalyzeAuditRecordActivity(IGitHubClient githubClient)
         {
             _githubClient = githubClient;

         }

        [FunctionName(nameof(AnalyzeAuditRecordActivity))]
        public async Task<string> Run(
            [ActivityTrigger] AuditRecord auditRecord)
        {
            var fraudResult = JsonConvert.SerializeObject(new FraudResult { RecordId = auditRecord.Id, IsSuspiciousTransaction = false }, Formatting.Indented);
            var issue = new NewIssue("FraudDetection") { Body = fraudResult };
            var createdIssue = await _githubClient.Issue.Create("marcduiker","webhook-playground", issue);

            return auditRecord.Id;
        }
    }
}