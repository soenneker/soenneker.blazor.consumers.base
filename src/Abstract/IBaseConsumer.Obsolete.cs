using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.ApiClient.Dtos;
using Soenneker.Dtos.ProblemDetails;
using Soenneker.Dtos.RequestDataOptions;
using Soenneker.Dtos.Results.Paged;

namespace Soenneker.Blazor.Consumers.Base.Abstract;

/// <summary>
/// Obsolete methods for IBaseConsumer. These methods are deprecated and will be removed in a future version.
/// Use the corresponding *Result methods instead.
/// </summary>
public partial interface IBaseConsumer
{
    /// <summary>
    /// Retrieves a single resource by ID asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to retrieve.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the response and any problem details.</returns>
    [Obsolete("Use GetResult<TResponse> instead. This method will be removed in a future version.")]
    [Pure]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Get<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    [Obsolete("Use GetResult<TResponse> instead. This method will be removed in a future version.")]
    [Pure]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Get<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all resources asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="requestDataOptions"></param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing a list of responses and any problem details.</returns>
    [Obsolete("Use GetAllResult<TResponse> instead. This method will be removed in a future version.")]
    [Pure]
    ValueTask<(PagedResult<TResponse>? response, ProblemDetailsDto? details)> GetAll<TResponse>(RequestDataOptions? requestDataOptions = null,
        string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Obsolete("Use GetAllResult<TResponse> instead. This method will be removed in a future version.")]
    [Pure]
    ValueTask<(PagedResult<TResponse>? response, ProblemDetailsDto? details)> GetAll<TResponse>(RequestOptions requestOptions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new resource asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="request">The request object to create the resource.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the created response and any problem details.</returns>
    [Obsolete("Use CreateResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Create<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    [Obsolete("Use CreateResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)>
        Create<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    [Obsolete("Use PostResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Post<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    [Obsolete("Use PostResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Post<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing resource asynchronously by ID.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to update.</param>
    /// <param name="request">The request object to update the resource.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the updated response and any problem details.</returns>
    [Obsolete("Use UpdateResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Update<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Obsolete("Use UpdateResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)>
        Update<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    [Obsolete("Use PutResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Put<TResponse>(string? id, object request, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Obsolete("Use PutResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Put<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a resource asynchronously by ID.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to delete.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the deleted response and any problem details.</returns>
    [Obsolete("Use DeleteResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Delete<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    [Obsolete("Use DeleteResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)>
        Delete<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file stream asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id"></param>
    /// <param name="stream">The file stream to upload.</param>
    /// <param name="fileName">The name of the file being uploaded.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the upload response and any problem details.</returns>
    [Obsolete("Use UploadResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Upload<TResponse>(string? id, Stream stream, string fileName, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Obsolete("Use UploadResult<TResponse> instead. This method will be removed in a future version.")]
    ValueTask<(TResponse? response, ProblemDetailsDto? details)> Upload<TResponse>(RequestUploadOptions requestOptions,
        CancellationToken cancellationToken = default);
}
