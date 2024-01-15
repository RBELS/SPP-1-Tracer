using System.Text.Json.Serialization;

namespace Core.Tracer.Serialization.Json;

public class JsonMethodInfo
{
    [JsonPropertyName("name")]
    public string Method { get; set; }
    [JsonPropertyName("class")]
    public string Class { get; set; }
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("methods")] public List<JsonMethodInfo> Methods { get; set; } = new List<JsonMethodInfo>();
    
    public JsonMethodInfo() {}
    public JsonMethodInfo(MethodInfo methodInfo)
    {
        Method = methodInfo.MethodName;
        Class = methodInfo.ClassName;
        Time = $"{methodInfo.GetDuration()}ms";
        foreach (var methodInfoInnerMethod in methodInfo.InnerMethods)
        {
            Methods.Add(new JsonMethodInfo(methodInfoInnerMethod));
        }
    }
}