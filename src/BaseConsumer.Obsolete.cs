using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.ApiClient.Dtos;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Dtos.RequestDataOptions;
using Soenneker.Dtos.Results.Paged;
using Soenneker.Extensions.HttpResponseMessage;
using Soenneker.Extensions.Object;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Blazor.Consumers.Base;

/// <summary>
/// Obsolete methods for BaseConsumer. These methods are deprecated and will be removed in a future version.
/// Use the corresponding *Result methods instead.
/// </summary>
public partial class BaseConsumer
{
    [Obsolete("Use GetResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Get<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Get<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use GetResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Get<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken).NoSync();
        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use GetAllResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(PagedResult<TResponse>? response, ProblemDetailsDto? details)> GetAll<TResponse>(RequestDataOptions? requestDataOptions = null,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? PrefixUri;

        if (requestDataOptions != null)
            uri += requestDataOptions.ToQueryString();

        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return GetAll<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use GetAllResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(PagedResult<TResponse>? response, ProblemDetailsDto? details)> GetAll<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Get(requestOptions, cancellationToken: cancellationToken).NoSync();
        return await message.ToWithDetails<PagedResult<TResponse>>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use CreateResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Create<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(request, overrideUri, allowAnonymous, cancellationToken);
    }

    [Obsolete("Use CreateResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Create<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return Post<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use PostResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Post<TResponse>(object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? PrefixUri;
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Post<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use PostResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Post<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Post(requestOptions, cancellationToken).NoSync();
        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use UpdateResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Update<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(id, request, overrideUri, allowAnonymous, cancellationToken);
    }

    [Obsolete("Use UpdateResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Update<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        return Put<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use PutResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Put<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        request.ThrowIfNull();

        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions
            {Uri = uri, Object = request, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Put<TResponse>(requestOptions, cancellationToken);
    }

    [Obsolete("Use PutResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Put<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Put(requestOptions, cancellationToken).NoSync();
        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use DeleteResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Delete<TResponse>(string? id, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}";
        var requestOptions = new RequestOptions {Uri = uri, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken).NoSync();

        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use DeleteResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Delete<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Delete(requestOptions, cancellationToken).NoSync();
        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }

    [Obsolete("Use UploadResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual ValueTask<(TResponse? response, ProblemDetailsDto? details)> Upload<TResponse>(string? id, Stream stream, string fileName,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default)
    {
        string uri = overrideUri ?? $"{PrefixUri}/{id}/upload";
        var options = new RequestUploadOptions
            {Uri = uri, Stream = stream, FileName = fileName, AllowAnonymous = allowAnonymous, LogRequest = LogRequest, LogResponse = LogResponse};

        return Upload<TResponse>(options, cancellationToken);
    }

    [Obsolete("Use UploadResult<TResponse> instead. This method will be removed in a future version.")]
    public virtual async ValueTask<(TResponse? response, ProblemDetailsDto? details)> Upload<TResponse>(RequestUploadOptions requestOptions,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage message = await ApiClient.Upload(requestOptions, cancellationToken).NoSync();
        return await message.ToWithDetails<TResponse>(Logger, cancellationToken).NoSync();
    }
}
