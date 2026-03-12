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
public interface IBaseConsumer : ICoreConsumer
{
    // OperationResult methods
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
    ValueTask<OperationResult<TResponse>> Get<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a response of the specified type using the provided request options.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response object to retrieve.</typeparam>
    /// <param name="requestOptions">The options that configure the request, such as headers, query parameters, or other request-specific settings.
    /// Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an OperationResult<TResponse> with
    /// the response data if the operation succeeds, or error information if it fails.</returns>
    [Pure]
    ValueTask<OperationResult<TResponse>> Get<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

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
    ValueTask<OperationResult<PagedResult<TResponse>>> GetAll<TResponse>(RequestDataOptions? requestDataOptions = null, string? overrideUri = null,
        bool allowAnonymous = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paged collection of items that match the specified request options.
    /// </summary>
    /// <typeparam name="TResponse">The type of the items to be returned in the paged result.</typeparam>
    /// <param name="requestOptions">The options that define filtering, sorting, and paging criteria for the query. Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an OperationResult with a
    /// PagedResult of items of type TResponse. The result may be empty if no items match the criteria.</returns>
    [Pure]
    ValueTask<OperationResult<PagedResult<TResponse>>> GetAll<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new resource asynchronously using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="request">The request object to create the resource.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the created response or problem details.</returns>
    ValueTask<OperationResult<TResponse>> Create<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new resource using the specified request options and returns the result asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response object returned upon successful creation of the resource.</typeparam>
    /// <param name="requestOptions">The options that configure the creation request, including parameters such as headers, payload, and other
    /// request-specific settings. Cannot be null.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation. The default value is <see
    /// cref="CancellationToken.None"/>.</param>
    /// <returns>A value task that represents the asynchronous creation operation. The result contains an <see
    /// cref="OperationResult{TResponse}"/> indicating the outcome and, if successful, the created resource.</returns>
    ValueTask<OperationResult<TResponse>> Create<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP POST request with the specified request payload and returns the deserialized response.
    /// </summary>
    /// <remarks>If authentication is required and allowAnonymous is false, the request will include
    /// authentication headers as configured. The request object must be serializable to the expected format (such as
    /// JSON).</remarks>
    /// <typeparam name="TResponse">The type to which the response content is deserialized.</typeparam>
    /// <param name="request">The payload to send in the body of the POST request. Cannot be null.</param>
    /// <param name="overrideUri">An optional URI to override the default endpoint for this request. If null, the default URI is used.</param>
    /// <param name="allowAnonymous">true to allow the request to be sent without authentication; otherwise, false.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A ValueTask that represents the asynchronous operation. The result contains the deserialized response or error
    /// information.</returns>
    ValueTask<OperationResult<TResponse>> Post<TResponse>(object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP POST request using the specified options and returns the deserialized response.
    /// </summary>
    /// <typeparam name="TResponse">The type to which the response content will be deserialized.</typeparam>
    /// <param name="requestOptions">The options that configure the HTTP request, including the target URI, headers, and content. Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A value task that represents the asynchronous operation. The result contains the outcome of the request and the
    /// deserialized response of type <typeparamref name="TResponse"/>.</returns>
    ValueTask<OperationResult<TResponse>> Post<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

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
    ValueTask<OperationResult<TResponse>> Update<TResponse>(string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates the resource using the specified request options.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the update operation.</typeparam>
    /// <param name="requestOptions">The options that configure the update request. Cannot be null.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the update operation.</param>
    /// <returns>A ValueTask that represents the asynchronous update operation. The task result contains an OperationResult of
    /// type TResponse with the outcome of the update.</returns>
    ValueTask<OperationResult<TResponse>> Update<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends an HTTP PUT request with the specified payload and returns the deserialized response.
    /// </summary>
    /// <remarks>If allowAnonymous is set to false and authentication is required, the request may fail if the
    /// user is not authenticated. The request object must be serializable to the expected format (such as
    /// JSON).</remarks>
    /// <typeparam name="TResponse">The type to which the response content is deserialized.</typeparam>
    /// <param name="id">The identifier of the resource to update. Can be null if the endpoint does not require an ID.</param>
    /// <param name="request">The payload to send in the PUT request body. Must not be null.</param>
    /// <param name="overrideUri">An optional URI to override the default endpoint. If null, the standard URI is used.</param>
    /// <param name="allowAnonymous">true to allow the request to be sent without authentication; otherwise, false.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A ValueTask that represents the asynchronous operation. The result contains the deserialized response of type
    /// TResponse and information about the operation's success or failure.</returns>
    ValueTask<OperationResult<TResponse>> Put<TResponse>(string? id, object request, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PUT request using the specified options and returns the result asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected from the PUT request.</typeparam>
    /// <param name="requestOptions">The options that configure the PUT request, including the target endpoint, headers, and payload. Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation is canceled if the token is triggered.</param>
    /// <returns>A value task that represents the asynchronous operation. The result contains the outcome of the PUT request,
    /// including the deserialized response of type TResponse if successful.</returns>
    ValueTask<OperationResult<TResponse>> Put<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a resource asynchronously by ID using OperationResult.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected.</typeparam>
    /// <param name="id">The unique identifier of the resource to delete.</param>
    /// <param name="overrideUri"></param>
    /// <param name="allowAnonymous">Indicates whether anonymous access is allowed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>An OperationResult containing the deleted response or problem details.</returns>
    ValueTask<OperationResult<TResponse>> Delete<TResponse>(string? id, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a request to delete a resource and returns the result of the operation asynchronously.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned upon successful deletion of the resource.</typeparam>
    /// <param name="requestOptions">The options to configure the delete request, such as headers, query parameters, or authentication settings.
    /// Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation is canceled if the token is triggered.</param>
    /// <returns>A ValueTask that represents the asynchronous delete operation. The result contains an OperationResult with the
    /// response of type TResponse.</returns>
    ValueTask<OperationResult<TResponse>> Delete<TResponse>(RequestOptions requestOptions, CancellationToken cancellationToken = default);

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
    ValueTask<OperationResult<TResponse>> Upload<TResponse>(string? id, Stream stream, string fileName, string? overrideUri = null, bool allowAnonymous = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Initiates an asynchronous upload operation using the specified request options and returns the result upon
    /// completion.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response expected from the upload operation.</typeparam>
    /// <param name="requestOptions">The options that configure the upload request, including destination, content, and any additional parameters
    /// required for the operation. Cannot be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation is canceled if the token is signaled before
    /// completion. Optional.</param>
    /// <returns>A value task that represents the asynchronous upload operation. The result contains an OperationResult with the
    /// response of type TResponse.</returns>
    ValueTask<OperationResult<TResponse>> Upload<TResponse>(RequestUploadOptions requestOptions, CancellationToken cancellationToken = default);
}
