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
public partial class BaseConsumer : CoreConsumer, IBaseConsumer
{
    protected BaseConsumer(IApiClient apiClient, ILogger<BaseConsumer> logger, string prefixUri) : base(apiClient, logger, prefixUri)
    {
    }

    public virtual ValueTask<OperationResult<TResponse>?> GetResult<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return GetResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> GetResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken).NoSync();
        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }

    public virtual ValueTask<OperationResult<PagedResult<TResponse>>?> GetAllResult<TResponse>(RequestDataOptions? requestDataOptions = null,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;

        if (requestDataOptions != null)
            uri += requestDataOptions.ToQueryString();

        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return GetAllResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<PagedResult<TResponse>>?> GetAllResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken).NoSync();
        return await message.ToResult<PagedResult<TResponse>>(Logger, cancellationToken).NoSync();
    }

    public virtual ValueTask<OperationResult<TResponse>?> CreateResult<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return PostResult<TResponse>(request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> CreateResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return PostResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> PostResult<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? PrefixUri;
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return PostResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> PostResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Post(requestOptions, cancellationToken).NoSync();
        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }

    public virtual ValueTask<OperationResult<TResponse>?> UpdateResult<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return PutResult<TResponse>(id, request, overrideUri, allowAnonymous, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> UpdateResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return PutResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual ValueTask<OperationResult<TResponse>?> PutResult<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return PutResult<TResponse>(requestOptions, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> PutResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Put(requestOptions, cancellationToken).NoSync();
        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }

    public virtual async ValueTask<OperationResult<TResponse>?> DeleteResult<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken).NoSync();

        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }

    public virtual async ValueTask<OperationResult<TResponse>?> DeleteResult<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken).NoSync();
        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }

    public virtual ValueTask<OperationResult<TResponse>?> UploadResult<TResponse>(string? id, Stream stream, string fileName,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}/upload";
        var options = new RequestUploadOptions
            {Uri = uri, Stream = stream, FileName = fileName, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return UploadResult<TResponse>(options, cancellationToken);
    }

    public virtual async ValueTask<OperationResult<TResponse>?> UploadResult<TResponse>(RequestUploadOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Upload(requestOptions, cancellationToken).NoSync();
        return await message.ToResult<TResponse>(Logger, cancellationToken).NoSync();
    }
}