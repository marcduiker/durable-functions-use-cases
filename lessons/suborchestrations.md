# Sub-orchestrations

Orchestrator functions can also call other orchestrator functions (sub-orchestrators).

![Sub-orchestrations](../diagrams/sub-orchestrators.png)

This enables re-use of orchestrators, and keeping them small and maintainable. Just like activity functions, sub-orchestrators can be chained, or executed in parallel.

This is an example where two sub-orchestrators are chained:

```csharp
[FunctionName(nameof(CollectAndUpdateGameStatsOrchestrator))]
public async Task Run(
    [OrchestrationTrigger] IDurableOrchestrationContext context,
    ILogger logger)
{
    var playerId = context.GetInput<string>();

    var playerGameStats = await context.CallSubOrchestratorAsync<PlayerGameStats>(
        nameof(CollectPlayerGameStatsOrchestrator),
        playerId);
    
    await context.CallSubOrchestratorAsync(
        nameof(UpdatePlayerGameStatsOrchestrator),
        playerGameStats);
}
    
```

## Official Docs

[Sub-orchestrations in Durable Functions](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-sub-orchestrations?tabs=csharp)

---
[◀ Durable Functions](durablefunctions.md) | [🔼 Challenge](notifysupport.md) | [Eternal Orchestrations ▶](eternalorchestrations.md)
