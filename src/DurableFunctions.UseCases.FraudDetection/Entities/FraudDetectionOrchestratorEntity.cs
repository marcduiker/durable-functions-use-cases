using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Newtonsoft.Json;

namespace DurableFunctions.UseCases.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FraudDetectionOrchestratorEntity
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        public void Set(string instanceId) => InstanceId = instanceId;

        public void Reset() => InstanceId = string.Empty;

        [FunctionName(nameof(FraudDetectionOrchestratorEntity))]
        public static Task Run(
            [EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<FraudDetectionOrchestratorEntity>();
    }
}