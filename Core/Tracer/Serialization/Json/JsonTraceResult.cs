using System.Text.Json.Serialization;

namespace Core.Tracer.Serialization.Json;

public class JsonTraceResult
{
    [JsonPropertyName("threads")]
    public List<JsonThreadInfo> threads { get; set; } = new List<JsonThreadInfo>();
    
    public JsonTraceResult() {}

    public JsonTraceResult(TraceResult traceResult)
    {
        foreach (var traceResultThread in traceResult.Threads)
        {
            threads.Add(new JsonThreadInfo(traceResultThread));
        }
    }
}