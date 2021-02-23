# Hands-on Lab: Notify Support

## Goal

The goal of this lab is to write a Function App which responds to incidents (posted as HTTP request) and notifies members of the support team so they can investigate the issue.

## Prerequisites

Read the [prerequisites](prerequisites.md) to ensure you have all the right tools installed.

## Requirements

The serverless application you'll write, need to do the following things:

1. Respond to an incoming HTTP POST request. The json body is as follows:

    ```json
    {
        "message" : "HALP!",
        "severity" : 1
    }
    ```

2. Support contact information must be read from Table Storage. A [data export](../data/SupportContacts.csv) has been made available which can be imported in a `SupportContacts` table. Only the people in team A are part of the support schedule.

3. The notification process must start with the first support contact in the list (ordered ascending by the `Order` field), if the contact do not respond within 5 minutes the next contact should be notified. This continues until there's no-one available in the support contact list. The time should be a configurable value.

4. Each support contact must be notified 3 times (in total) in case they do not respond immediately. These notification attempts should occur evenly spread within the 5 minute window. The retry number should be a configurable value.

5. Once a support contact responds with a callback the notification process should stop and no other contacts should be notified.

6. The callback response is received as a POST request where the body of the request contains only the phone number of the support contact:

    ```json
    {
        "+31611111111"
    }
    ```

## Durable Functions Theory

Please familiarize yourself with some Durable Functions theory and code samples. This are the building blocks for the solution.

- [Client, Orchestrator & Activity](durablefunctions.md)
- [Suborchestrations](suborchestrations.md)
- [Events](events.md)
- [Eternal orchestrations](eternalorchestrations.md)
- [Entities](entities.md)

## Build it!

Now it's time to build your solution.
// TODO add tips