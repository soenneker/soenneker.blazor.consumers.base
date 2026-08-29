[![](https://img.shields.io/nuget/v/soenneker.blazor.consumers.base.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.consumers.base/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.consumers.base/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.consumers.base/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.consumers.base.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.consumers.base/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.consumers.base/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.consumers.base/actions/workflows/codeql.yml)

# Soenneker.Blazor.Consumers.Base

A base class that sends common API requests through `IApiClient` and converts JSON success or problem-details responses into `OperationResult<T>`.

Use this package when one consumer needs different response types per operation. If every operation uses the same response type, `Soenneker.Blazor.Consumer` provides the typed `Consumer<TResponse>` layer.

## Installation

```bash
dotnet add package Soenneker.Blazor.Consumers.Base
```

## Define and register a consumer

`BaseConsumer` has a protected constructor, so derive an application-specific class and choose its URI prefix:

```csharp
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.ApiClient.Abstract;
using Soenneker.Blazor.Consumers.Base;

public sealed class CatalogConsumer : BaseConsumer
{
    public CatalogConsumer(
        IApiClient apiClient,
        ILogger<BaseConsumer> logger,
        IConfiguration configuration)
        : base(apiClient, logger, "catalog/products")
    {
        apiClient.Initialize(
            configuration["Api:BaseUrl"]!,
            requestResponseLogging: false);
    }
}
```

```csharp
using Soenneker.Blazor.ApiClient.Registrars;

builder.Services.AddApiClientAsScoped();
builder.Services.AddScoped<CatalogConsumer>();
```

## Send requests

The response type is selected on each call:

```csharp
OperationResult<ProductDto> product = await catalog.Get<ProductDto>(
    "42",
    cancellationToken: cancellationToken);

OperationResult<PagedResult<ProductSummaryDto>> page =
    await catalog.GetAll<ProductSummaryDto>(
        requestDataOptions,
        cancellationToken: cancellationToken);

OperationResult<ProductDto> created = await catalog.Create<ProductDto>(
    new CreateProductRequest(name, price),
    cancellationToken: cancellationToken);
```

Without `overrideUri`, single-resource GET, PUT, DELETE, and upload calls append the supplied ID directly to the configured prefix. Collection GET and POST use the prefix itself. An upload appends `/{id}/upload`. `overrideUri` replaces that entire generated relative URI.

Use a `RequestOptions` overload when you already have the exact URI, payload, anonymity choice, and logging flags:

```csharp
OperationResult<ProductDto> result = await catalog.Post<ProductDto>(
    new RequestOptions
    {
        Uri = "catalog/products/import",
        Object = request,
        AllowAnonymous = false,
        LogResponse = true
    },
    cancellationToken);
```

## Results and failures

Successful JSON is deserialized into `result.Value`; a problem-details response is exposed through `result.Problem`, with `Succeeded`, `Failed`, and `StatusCode` available for branching. Empty successful responses produce a successful result with a null value. Invalid or unexpected response JSON produces a failed result; transport, authentication, and cancellation failures may throw.

The consumer owns and disposes the underlying `HttpResponseMessage` after conversion.

Uploads are always authenticated. Passing `allowAnonymous: true` or setting `RequestUploadOptions.AllowAnonymous` throws `NotSupportedException`. Upload content uses `file` and optional `json` multipart fields, and the supplied stream is disposed after the request.
