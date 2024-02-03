using System.Text.Json;

namespace Core.Tracer.Serialization.Json;

public class JsonSerializer : ISerializer
{
    public string Serialize(TraceResult traceResult)
    {
        return System.Text.Json.JsonSerializer.Serialize(new JsonTraceResult(traceResult), new JsonSerializerOptions()
        {
            WriteIndented = true
        });
    }
}