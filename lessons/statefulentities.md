# Stateful Entities

Entity functions allow you to read and write small pieces of state, known as stateful entities. Entities can be accessed from client functions and orchestrator functions.

## Entity Definition

This is an example of a class-based entity function. It stores the game score for a player, and it allows setting & resetting of the score.

```csharp
[JsonObject(MemberSerialization.OptIn)]
public class PlayerScoreEntity
{
    [JsonProperty("score")]
    public int Score { get; set; }
    
    public void Set(int score) => Score = score;
    
    public void Reset() => Score = 0;
    
    [FunctionName(nameof(PlayerScoreEntity))]
    public static Task Run(
        [EntityTrigger] IDurableEntityContext context)
        => context.DispatchAsync<PlayerScoreEntity>();
}
```

Stateful entities are always accessed via their ID. The Entity ID is a combination of the entity type (the class name) and the entity key, a string value that uniquely identifies an entity instance. The key can be a GUID, a user name, an email address, as long as it is unique.

## Reading Entity State

In this example the `PlayerScoreEntity` state is read in a client function to retrieve the game score of the player:

```csharp
[FunctionName(nameof(CallbackHttpClient))]
public static async Task<IActionResult> Run(
    [HttpTrigger(
        AuthorizationLevel.Function,
        nameof(HttpMethod.Post),
        Route = null)] HttpRequestMessage message,
    [DurableClient] IDurableClient client,
    ILogger logger)
{
    var playerName = await message.Content.ReadAsAsync<string>();
    var entityId = new EntityId(nameof(PlayerScoreEntity), playerName);
    var playerScoreEntity = await client.ReadEntityStateAsync<PlayerScoreEntity>(entityId);
    if (playerScoreEntity.EntityExists)
    {
        return new OkObjectResult($"{playerName}:{playerScoreEntity.EntityState.Score}");
    }
    
    return return new BadRequestObjectResult($"{playerName} not found.");
}
```

## Updating Entity State

In this example the `PlayerScoreEntity` state is updated by signalling (one way communication) the entity:

```csharp
[FunctionName(nameof(SendNotificationOrchestrator))]
public async Task Run(
    [OrchestrationTrigger] IDurableOrchestrationContext context,
    ILogger logger)
{
    var playerScore = context.GetInput<PlayerScore>();
    var entityId = new EntityId(nameof(PlayerScoreEntity), playerScore.Name);
    context.SignalEntity(
        entityId,
        nameof(PlayerScoreEntity.Set),
        playerScore.Score);

    // Rest of the orchestrator logic
}
```

## Official Docs

- [Entity functions](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-entities?tabs=csharp)
- [Developer's guide to durable entities in .NET](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-dotnet-entities)

---
[◀ Events](events.md) | [🔼 Hands-on Lab](notifysupport.md)
