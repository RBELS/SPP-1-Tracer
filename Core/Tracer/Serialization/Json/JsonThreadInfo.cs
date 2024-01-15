using System.Text.Json.Serialization;

namespace Core.Tracer.Serialization.Json;

public class JsonThreadInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("methods")]
    public List<JsonMethodInfo> Methods { get; set; } = new List<JsonMethodInfo>();

    public JsonThreadInfo() {}
    
    public JsonThreadInfo(ThreadInfo threadInfo)
    {
        Id = threadInfo.ThreadId;
        
        int timeSum = 0;
        foreach (var methodInfo in threadInfo.RootMethodInfoList)
        {
            timeSum += methodInfo.GetDuration();
            Methods.Add(new JsonMethodInfo(methodInfo));
        }
        
        Time = $"{timeSum}ms";
    }
}