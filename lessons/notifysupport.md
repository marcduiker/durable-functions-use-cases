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

2. Support contact information must be read from Table Storage. A [data export](../data/SupportContacts.csv) has been made available which can be imported in a `SupportContacts` table. Only the people in **Team A** are part of the support schedule. For more info about *Table Storage bindings* see [this Azure Functions University lesson](https://github.com/marcduiker/azure-functions-university/blob/main/lessons/table-dotnet.md).

3. The notification process must start with the first support contact in the list (ordered ascending by the `Order` field), if the contact do not respond within 5 minutes the next contact should be notified. This continues until there's no-one available in the support contact list. The time should be a configurable value.

4. Each support contact must be notified 3 times (in total) in case they do not respond immediately. These notification attempts should occur evenly spread within the 5 minute window. The retry number should be a configurable value.

5. Once a support contact responds with a callback the notification process should stop and no other contacts should be notified.

6. The callback response is received as a POST request where the body of the request contains only the phone number of the support contact:

    ```json
    {
        "+31611111111"
    }
    ```

7. For times sake, the actual notification functionality (making phone calls or sending text messages (incl callbacks) can be faked for this lab. If you do have the time, you could try Twilio or Azure Communication Services.

## Durable Functions Theory

Please familiarize yourself with some Durable Functions theory and code samples. These are the building blocks for the solution.

- [Client, Orchestrator & Activity functions](durablefunctions.md)
- [Suborchestrations](suborchestrations.md)
- [Events](events.md)
- [Eternal orchestrations](eternalorchestrations.md)
- [Stateful Entities](statefulentities.md)

## Build it!

Now it's time to build your solution.

If you need some tips, check [this page](tips.md).

---
[🔼 Main README](../README.md)
