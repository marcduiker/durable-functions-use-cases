# Sub-orchestrations

Orchestrator functions can also call other orchestrator functions (sub-orchestrators). This allows re-use of orchestrators, and keeping them small and maintainable. Just like activity functions, sub-orchestrators can be chained, or executed in parallel.

This is an example where two sub-orchestrators are chained:

```csharp
[FunctionName(nameof(CollectAndUpdateGameStatsOrchestrator))]
public async Task Run(
    [OrchestrationTrigger] IDurableOrchestrationContext context,
    ILogger logger)
{
    var userId = context.GetInput<string>();

    var userGameStats = await context.CallSubOrchestratorAsync<UserGameStats>(
        nameof(CollectUserGameStatsOrchestrator),
        userId);
    
    await context.CallSubOrchestratorAsync(
        nameof(UpdateUserGameStatsOrchestrator),
        userGameStats);
}
    
```

## Official Docs

[Sub-orchestrations in Durable Functions](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-sub-orchestrations?tabs=csharp)

---
[◀ Durable Functions](durablefunctions.md) | [🔼 Hands-on Lab](notifysupport.md) | [Eternal Orchestrations ▶](eternalorchestrations.md)
