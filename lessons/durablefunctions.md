# Durable Functions

Durable Functions allows you to write workflows (orchestrations) in code. The state of the workflow instances are stored which enables many use cases such as:

- Chaining function calls.
- Execute functions in parallel and wait for their completion (fan-out/fan-in).
- Waiting for external events.
- Executing long running, and even eternal workflows.

## Function types

Durable Functions uses four types of functions:

- Client functions
- Orchestrator functions
- Activity functions
- Entity functions

### Orchestrator Functions

Orchestrator functions define the workflow as code. It contains the logic which steps (activities) are performed, and in which order.

This is an example of an orchestrator where two functions are chained. The second activity function (`UpdateUserScoreActivity`) requires the output of the first activity function (`GetUserActivity`).

```csharp
[FunctionName(nameof(UpdateUserScoreOrchestrator))]
public async Task Run(
    [OrchestrationTrigger] IDurableOrchestrationContext context,
    ILogger logger)
{
    var userId = context.GetInput<string>();

    var user = await context.CallActivityAsync<User>(
        nameof(GetUserActivity),
        userId);
    
    await context.CallActivityAsync(
        nameof(UpdateUserScoreActivity),
        user);
}
```

It is important to realize that an orchestrator function is not executed once. It is replayed several times, depending on the number of activities. For each activity the orchestrator calls, the orchestrator execution is stopped. As soon as an activity is done, the orchestrator restarts. Since the state of the orchestrator instance is persisted (including the inputs and outputs of activity functions) it won't execute activity functions multiple times. The Durable Functions framework checks if the activity function has been executed and if so, it will retrieve the state to use that in the orchestrator.

### Activity Functions

Activity functions are the units of work which the workflow orchestrates. These functions usually deal with calling external APIs and interacting with databases.

The Durable Function framework guarantees that activity functions are executed at least once as part of an orchestrator.

```csharp
[FunctionName(nameof(GetUserActivity))]
public async Task<User> Run(
    [ActivityTrigger] string userId,
    ILogger logger)
{
    // Call external API or database.
    // var user = await gameUserService.GetUserAsync(userId);
    // return user;
}
```

### Client Functions

A client function is responsible for creating a new instance of a workflow, also known as an orchestrator instance. It can perform other operations on an orchestrator instance as well, such as: querying the orchestrator state, and terminating the instance.

```csharp
[FunctionName(nameof(UpdateUserScoreClient))]
public static async Task<HttpResponseMessage> Run(
[HttpTrigger(
    AuthorizationLevel.Function,
    nameof(HttpMethod.Post),
    Route = null)] HttpRequestMessage message,
[DurableClient] IDurableClient client,
ILogger logger)
{
    var userId = await message.Content.ReadAsAsync<string>();

    string instanceId = await client.StartNewAsync(
        nameof(UpdateUserScoreOrchestrator),
        userId);

    return client.CreateCheckStatusResponse(message, instanceId);
}
```

### Entity Functions

Entity functions are described in [this section](statefulentities.md).

## Official Docs

[Durable Functions Overview](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview?tabs=csharp)

---
[🔼 Hands-on Lab](notifysupport.md) | [Sub-orchestrations ▶](suborchestrations.md)
