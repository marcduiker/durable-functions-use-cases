using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Models;
using Refit;

namespace DurableFunctions.UseCases.FraudDetection.Services
{

    public interface IFraudAnalysisService
    {
        [Headers("Authorization: token", "Accept: application/vnd.github.v3+json")]
        [Post("/repos/{repoOwner}/{repoName}/actions/workflows/{workflowFile}/dispatches")]
        public Task<ApiResponse<string>> AnalyzeAsync(
            [Body] WorkflowDispatchEvent dispatch,
            string repoOwner,
            string repoName,
            string workflowFile);
    }
}