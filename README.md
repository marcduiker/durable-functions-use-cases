# Durable Functions Challenges

In this repository, I'll collect some real-life challenges you can solve using Azure Functions & Durable Functions.

These challenges do assume some experience with Azure Functions (in .NET) and a basic understanding of Durable Functions. If you want to learn more about those first, then please visit [Azure Functions University](https://github.com/marcduiker/azure-functions-university).

## 1. [Notify Support](NotifySupport/challenge/README.md)

Write a Function App that responds to alerts and notifies members of a support team to investigate the issue.

![Notify Support overview diagram](NotifySupport/challenge/diagrams/notifysupport_overview.png)

This challenge combines sub-orchestrations, eternal orchestrations, webhooks, waiting for external events, and using stateful entities.

Go to the [Notify Support Challenge](NotifySupport/challenge/README.md).

## 2. [Fraud Detection](FraudDetection/challenge/README.md)

Write a Function App that takes in financial transactions, calls a web service to analyze the transactions to detect fraud,waits for a call back with the analysis result and finally stores an audit record of the transaction.

![Fraud Detection overview diagram](FraudDetection/challenge/diagrams/frauddetection_overview.png)

This challenge combines orchestrations, webhooks, waiting for external events, and stateful entities.

Go to the [Fraud Detection Challenge](FraudDetection/challenge/README.md).