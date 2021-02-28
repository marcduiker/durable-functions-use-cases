# Tips for building the Notify Support solution

## Sub-orchestrations

Use an orchestrator to iterate over the support contacts. This calls a sub-orchestrator that takes care of sending the notifications and waiting for the callback event.

## Eternal orchestrations

Use the `ContinueAsNew` functionality to (re)start the orchestrator which sends notifications to a support contact and waits for the callback event. Make sure to only restart the orchestration for the max number of retries per support contact.

The `ContinueAsNew` functionality can also be used in the main orchestrator when the next support contact needs to be notified.

## Timings when waiting for events

Messaging in Durable Functions is done using Storage Queues. The Durable Functions framework polls these queues to determine if there are tasks to be done. The polling frequency is a configurable value called `maxQueuePollingInterval` in the `hosts.json` file. By default this value is set at 30 seconds. For short wait times regarding events it is useful to set this to a smaller value (such as 2 seconds) to allow more accurate timings.

For more info read:

- [Durable Functions Queue Polling](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-perf-and-scale#queue-polling).
- [Durable Functions hosts.json defaults](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-bindings#durable-functions-2-0-host-json)

## Handling the callback

When the callback is received, the only information we have is a phone number. If an orchestrator is waiting for the callback event before it is successfully completed, that event should be raised. But we don't receive the instance ID of the orchestrator in order to raise the event. A solution for this is to use Stateful Entities to store the instance ID when the notification is sent, and to read the instance ID when we receive the callback.
