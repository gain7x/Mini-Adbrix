using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Dtos.Converters
{
    public class JsonDocumentJsonConverter : JsonConverter<JsonDocument>
    {
        public override JsonDocument Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => JsonDocument.Parse(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            JsonDocument document,
            JsonSerializerOptions options) =>
                writer.WriteRawValue(document.RootElement.ToString());
    }
}
