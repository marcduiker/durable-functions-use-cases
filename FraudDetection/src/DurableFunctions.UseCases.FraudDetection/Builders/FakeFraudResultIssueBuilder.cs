using Bogus;
using DurableFunctions.UseCases.FraudDetection.Models;
using Newtonsoft.Json;
using Octokit;

namespace DurableFunctions.UseCases.FraudDetection.Builders
{
    public static class FakeFraudResultIssueBuilder
    {
        public static NewIssue Create(AuditRecord auditRecord)
        {
            var fakeFraudResult = new Faker<FraudResult>()
                .RuleFor(f => f.RecordId, auditRecord.Id)
                .RuleFor(f => f.IsSuspiciousTransaction, f => f.Random.Bool())
                .Generate();

            var fraudResult = JsonConvert.SerializeObject(
                fakeFraudResult, 
                Formatting.Indented);
            return new NewIssue("FraudDetection") { Body = fraudResult };
        }
    }
}