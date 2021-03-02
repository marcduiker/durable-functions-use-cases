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
|[Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)|Application to manage Azure Storage resources (both in the cloud and local emulated).
|[Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator) (Windows only)|Emulator for using Azure Storage services if you want to develop locally without connecting to a Storage Account in the cloud. If you can't use the emulator you need an [Azure Storage Account](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal).
|[CodeTour](https://marketplace.visualstudio.com/items?itemName=vsls-contrib.codetour)|**OPTIONAL:** Code samples in the markdown files use CodeTour to explain the samples in more detail. CodeTour is a VS Code extension that guides you through source code.

## Creating your local workspace 👩‍💻

Create a new folder (local git repository) and use the repository you're currently reading from for reference only (for when you're stuck).

- Create a new folder to work in:

    ```cmd
    C:\dev\mkdir df-usecases
    C:\dev\cd .\df-usecases\
    ```

- Turn this into a git repository:

    ```cmd
    C:\dev\df-usecases\git init
    ```

- Add subfolders for the source code and test files:

    ```cmd
    C:\dev\df-usecases\mkdir src
    C:\dev\df-usecases\mkdir tst
    ```

You should be good to go now!

---
[🔼 Main README](../README.md) | [Notify Support Challenge ▶](notifysupport.md)
