[![](https://img.shields.io/nuget/v/soenneker.blazor.consumers.base.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.consumers.base/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.consumers.base/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.consumers.base/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.consumers.base.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.consumers.base/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.consumers.base/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.consumers.base/actions/workflows/codeql.yml)

# Soenneker.Blazor.Consumers.Base

A wrapper around Soenneker.Blazor.ApiClient supporting auto (de)serialization for requests/responses/ProblemDetails.

## Install

```bash
dotnet add package Soenneker.Blazor.Consumers.Base
```

## Quick start

```csharp
using Soenneker.Blazor.Consumers.Base.Abstract;

IBaseConsumer baseConsumer = /* resolve from DI */;
var result = await baseConsumer.Get("value", default);
```

Retrieves a single resource by ID asynchronously using OperationResult.

## What you get

- `IBaseConsumer` — A wrapper around Soenneker.Blazor.ApiClient supporting auto (de)serialization for requests/responses/ProblemDetails.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IBaseConsumer.Get(id, overrideUri, allowAnonymous, cancellationToken)` | Retrieves a single resource by ID asynchronously using OperationResult. | An OperationResult containing the response or problem details. |
| `IBaseConsumer.Get(requestOptions, cancellationToken)` | Asynchronously retrieves a response of the specified type using the provided request options. | A task whose result is the requested operation Result. |
| `IBaseConsumer.GetAll(requestDataOptions, overrideUri, allowAnonymous, cancellationToken)` | Retrieves all resources asynchronously using OperationResult. | An OperationResult containing a list of responses or problem details. |
| `IBaseConsumer.GetAll(requestOptions, cancellationToken)` | Retrieves a paged collection of items that match the specified request options. | A task whose result is the requested operation Result. |
| `IBaseConsumer.Create(request, overrideUri, allowAnonymous, cancellationToken)` | Creates a new resource asynchronously using OperationResult. | An OperationResult containing the created response or problem details. |
| `IBaseConsumer.Create(requestOptions, cancellationToken)` | Creates a new resource using the specified request options and returns the result asynchronously. | A value task that represents the asynchronous creation operation. The result contains an `OperationResult{TResponse}` indicating the outcome and, if successful, the created resource. |
| `IBaseConsumer.Post(request, overrideUri, allowAnonymous, cancellationToken)` | Sends an HTTP POST request with the specified request payload and returns the deserialized response. | A ValueTask that represents the asynchronous operation. The result contains the deserialized response or error information. |
| `IBaseConsumer.Post(requestOptions, cancellationToken)` | Sends an HTTP POST request using the specified options and returns the deserialized response. | A value task that represents the asynchronous operation. The result contains the outcome of the request and the deserialized response of type `TResponse`. |
| `IBaseConsumer.Update(id, request, overrideUri, allowAnonymous, cancellationToken)` | Updates an existing resource asynchronously by ID using OperationResult. | An OperationResult containing the updated response or problem details. |
| `IBaseConsumer.Update(requestOptions, cancellationToken)` | Asynchronously updates the resource using the specified request options. | A ValueTask that represents the asynchronous update operation. The task result contains an OperationResult of type TResponse with the outcome of the update. |
| `IBaseConsumer.Put(id, request, overrideUri, allowAnonymous, cancellationToken)` | Sends an HTTP PUT request with the specified payload and returns the deserialized response. | A ValueTask that represents the asynchronous operation. The result contains the deserialized response of type TResponse and information about the operation's success or failure. |
| `IBaseConsumer.Put(requestOptions, cancellationToken)` | Sends a PUT request using the specified options and returns the result asynchronously. | A value task that represents the asynchronous operation. The result contains the outcome of the PUT request, including the deserialized response of type TResponse if successful. |
| `IBaseConsumer.Delete(id, overrideUri, allowAnonymous, cancellationToken)` | Deletes a resource asynchronously by ID using OperationResult. | An OperationResult containing the deleted response or problem details. |
| `IBaseConsumer.Delete(requestOptions, cancellationToken)` | Sends a request to delete a resource and returns the result of the operation asynchronously. | A ValueTask that represents the asynchronous delete operation. The result contains an OperationResult with the response of type TResponse. |
| `IBaseConsumer.Upload(id, stream, fileName, overrideUri, allowAnonymous, cancellationToken)` | Uploads a file stream asynchronously using OperationResult. | An OperationResult containing the upload response or problem details. |
| `IBaseConsumer.Upload(requestOptions, cancellationToken)` | Initiates an asynchronous upload operation using the specified request options and returns the result upon completion. | A value task that represents the asynchronous upload operation. The result contains an OperationResult with the response of type TResponse. |

## Important behavior

- `IBaseConsumer.Post(request, overrideUri, allowAnonymous, cancellationToken)`: If authentication is required and allowAnonymous is false, the request will include authentication headers as configured. The request object must be serializable to the expected format (such as JSON).
- `IBaseConsumer.Put(id, request, overrideUri, allowAnonymous, cancellationToken)`: If allowAnonymous is set to false and authentication is required, the request may fail if the user is not authenticated. The request object must be serializable to the expected format (such as JSON).

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
