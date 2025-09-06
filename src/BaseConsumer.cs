using System.IO;
using System.Net.Http;
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
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Blazor.Consumers.Base;

///<inheritdoc cref="IBaseConsumer"/>
public class BaseConsumer : CoreConsumer, IBaseConsumer
{
    protected BaseConsumer(IApiClient apiClient, ILogger<BaseConsumer> logger, string prefixUri) : base(apiClient, logger, prefixUri)
    {
    }

    public virtual ValueTask<OperationResult<TResponse>?> Get<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Get<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Get<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken);
        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<PagedResult<TResponse>>?> GetAll<TResponse>(RequestDataOptions? requestDataOptions = null,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;

        if (requestDataOptions != null)
            uri += requestDataOptions.ToQueryString();

        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return GetAll<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<PagedResult<TResponse>>?> GetAll<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken);
        return await message.ToResult<PagedResult<TResponse>>(Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Create<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Create<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Post<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? PrefixUri;
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Post<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Post<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Post(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Update<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(id, request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Update<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Put<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Put<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Put<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Put(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Delete<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken);

        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Delete<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> Upload<TResponse>(string? id, Stream stream, string fileName,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}/upload";
        var options = new RequestUploadOptions
            {Uri = uri, Stream = stream, FileName = fileName, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Upload<TResponse>(options, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> Upload<TResponse>(RequestUploadOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Upload(requestOptions, cancellationToken);
        return await message.ToResult<TResponse>(Logger, cancellationToken);
    }
}
