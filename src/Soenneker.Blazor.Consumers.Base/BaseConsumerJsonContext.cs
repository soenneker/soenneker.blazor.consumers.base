using System.Text.Json.Serialization;
using Soenneker.Dtos.RequestDataOptions;

namespace Soenneker.Blazor.Consumers.Base;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(RequestDataOptions))]
internal partial class BaseConsumerJsonContext : JsonSerializerContext
{
}
