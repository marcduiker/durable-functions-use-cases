using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using Refit;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public interface IFraudAnalysisService
    {
        [Headers("Authorization: Bearer")]
        [Post("/repos/{repoOwner}/{repoName}/actions/workflows/{workflowFile}/dispatches")]
        public Task Call(WorkflowDispatchEvent dispatch, string repoOwner, string repoName, string workflowFile);
    }
}