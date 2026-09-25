using System;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.ApiClient.Abstract;
using Soenneker.Blazor.ApiClient.Dtos;
using Soenneker.Blazor.Consumers.Base.Abstract;
using Soenneker.Blazor.Consumers.Core;
using Soenneker.Dtos.RequestDataOptions;
using Soenneker.Dtos.Results.Operation;
using Soenneker.Dtos.Results.Paged;
using Soenneker.Extensions.HttpResponseMessage;
using Soenneker.Extensions.Object;

namespace Soenneker.Blazor.Consumers.Base;

public class BaseConsumer : CoreConsumer, IBaseConsumer
{
    protected BaseConsumer(IApiClient apiClient, ILogger<BaseConsumer> logger, string prefixUri) : base(apiClient, logger, prefixUri)
    {
    }

    public virtual ValueTask<OperationResult<TResponse>> Get<TResponse>(JsonTypeInfo<TResponse> typeInfo, string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        return Get<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Get<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);
        using HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken);
        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<PagedResult<TResponse>>> GetAll<TResponse>(JsonTypeInfo<PagedResult<TResponse>> typeInfo, RequestDataOptions? requestDataOptions = null,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;

        if (requestDataOptions != null)
            uri += requestDataOptions.ToQueryString();

        var requestOptions = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        return GetAll<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<PagedResult<TResponse>>> GetAll<TResponse>(JsonTypeInfo<PagedResult<TResponse>> typeInfo, RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);
        using HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken);
        return await message.ToResult<PagedResult<TResponse>>(typeInfo, Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Create<TResponse>(JsonTypeInfo<TResponse> typeInfo, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(typeInfo, request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Create<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Post<TResponse>(JsonTypeInfo<TResponse> typeInfo, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? PrefixUri;
        var requestOptions = new RequestOptions
            { Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        return Post<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Post<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);
        using HttpResponseMessage message = await ApiClient.Post(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Update<TResponse>(JsonTypeInfo<TResponse> typeInfo, string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(typeInfo, id, request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Update<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Put<TResponse>(JsonTypeInfo<TResponse> typeInfo, string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions
            { Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        return Put<TResponse>(typeInfo, requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Put<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);
        using HttpResponseMessage message = await ApiClient.Put(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Delete<TResponse>(JsonTypeInfo<TResponse> typeInfo, string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions { Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        using HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken);

        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Delete<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestOptions requestOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);
        using HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>> Upload<TResponse>(JsonTypeInfo<TResponse> typeInfo, string? id, Stream stream, string fileName, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        if (allowAnonymous)
            throw new NotSupportedException("Anonymous uploads are not supported by the underlying API client.");

        string uri = overrideUri ?? $"{PrefixUri}/{id}/upload";
        var options = new RequestUploadOptions
            { Uri = uri, Stream = stream, FileName = fileName, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse };

        return Upload<TResponse>(typeInfo, options, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>> Upload<TResponse>(JsonTypeInfo<TResponse> typeInfo, RequestUploadOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        ArgumentNullException.ThrowIfNull(requestOptions);

        if (requestOptions.AllowAnonymous.GetValueOrDefault())
            throw new NotSupportedException("Anonymous uploads are not supported by the underlying API client.");

        using HttpResponseMessage message = await ApiClient.Upload(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(typeInfo, Logger, cancellationToken);
    }
}
