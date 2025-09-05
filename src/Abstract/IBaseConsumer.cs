using Soenneker.Blazor.ApiClient.Dtos;
using Soenneker.Blazor.Consumers.Core.Abstract;
using Soenneker.Dtos.RequestDataOptions;
using Soenneker.Dtos.Results.Operation;
using Soenneker.Dtos.Results.Paged;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Consumers.Base.Abstract;

/// <summary>
/// A wrapper around Soenneker.Blazor.ApiClient supporting auto (de)serialization for requests/responses/ProblemDetails.
/// </summary>
public partial interface IBaseConsumer : ICoreConsumer
{
    // New OperationResult methods
    /// <summary>
    /// Retrieves a single resource by ID asynchronously using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to retrieve.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the response or problem details.</returns>
    [Pure]
    ValueTask<OperationResult<TResponse>?> GetResult<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Pure]
    ValueTask<OperationResult<TResponse>?> GetResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all resources asynchronously using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="requestDataOptions"></param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing a list of responses or problem details.</returns>
    [Pure]
    ValueTask<OperationResult<PagedResult<TResponse>>?> GetAllResult<TResponse>(RequestDataOptions? requestDataOptions = null, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default);

    [Pure]
    ValueTask<OperationResult<PagedResult<TResponse>>?> GetAllResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new resource asynchronously using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="request">The request object to create the resource.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the created response or problem details.</returns>
    ValueTask<OperationResult<TResponse>?> CreateResult<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> CreateResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> PostResult<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> PostResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing resource asynchronously by ID using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to update.</param>
    /// <param name="request">The request object to update the resource.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the updated response or problem details.</returns>
    ValueTask<OperationResult<TResponse>?> UpdateResult<TResponse>(string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> UpdateResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> PutResult<TResponse>(string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> PutResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a resource asynchronously by ID using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to delete.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the deleted response or problem details.</returns>
    ValueTask<OperationResult<TResponse>?> DeleteResult<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> DeleteResult<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a file stream asynchronously using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id"></param>
    /// <param name="stream">The file stream to upload.</param>
    /// <param name="fileName">The name of the file being uploaded.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the upload response or problem details.</returns>
    ValueTask<OperationResult<TResponse>?> UploadResult<TResponse>(string? id, Stream stream, string fileName, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    ValueTask<OperationResult<TResponse>?> UploadResult<TResponse>(RequestUploadOptions requestOptions, CancellationToken cancellationToken = default);
}