# Prerequisites

## Frameworks & Tooling 🧰

In order to complete the the lessons you need to install the following:

|Prerequisite|Description
|-|-
|[.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core)|The .NET runtime and SDK.
|[VSCode](https://code.visualstudio.com/Download)|A great cross platform code editor.
|[VSCode AzureFunctions extension](https://github.com/Microsoft/vscode-azurefunctions)|Extension for VSCode to easily develop and manage Azure Functions.
|[Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools)|Azure Functions runtime and CLI for local development.
|[RESTClient for VSCode](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) or [Postman](https://www.postman.com/)|A VSCode extension or application to make HTTP requests.
|[Azurite for VSCode](https://marketplace.visualstudio.com/items?itemName=Azurite.azurite)|[Cross platform storage emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio) for using Azure Storage services if you want to develop locally without connecting to a Storage Account in the cloud. If you can't use the emulator you need an [Azure Storage Account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal).
|[Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)|Application to manage Azure Storage resources (both in the cloud and local emulated).
|[Azure CosmosDB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator)|Cross platform CosmosDB emulator (NoSQL storage), that allows you to develop & test locally without connecting to an CosmosDB service in Azure.
|[CodeTour for VSCode](https://marketplace.visualstudio.com/items?itemName=vsls-contrib.codetour)|**OPTIONAL:** Code samples in the markdown files use CodeTour to explain the samples in more detail. CodeTour is a VS Code extension that guides you through source code.
| A localhost tunnel such as [ngrok](https://ngrok.com/) | **OPTIONAL:** You only need this if you're calling a 3rd party web service in the cloud and using its webhook functionality to call back to your local running Function App.

## Creating your local workspace 👩‍💻

Create a new folder (local git repository) and use the repository you're currently reading from for reference only (for when you're stuck).

- Create a new folder to work in:

    ```cmd
    C:\dev\mkdir fraud-detection
    C:\dev\cd .\fraud-detection\
    ```

- Turn this into a git repository:

    ```cmd
    C:\dev\fraud-detection\git init
    ```

- Add subfolders for the source code and test files:

    ```cmd
    C:\dev\fraud-detection\mkdir src
    C:\dev\fraud-detection\mkdir tst
    ```

You should be good to go now!

---
[🔼 Main README](../../README.md) | [Fraud Detection Challenge ▶](frauddetection.md)
