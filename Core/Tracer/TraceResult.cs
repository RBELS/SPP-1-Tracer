namespace Core.Tracer;

public class TraceResult
{
    public List<ThreadInfo> Threads { get; private set; }

    internal TraceResult(IDictionary<int, ThreadInfo> dictionary)
    {
        Threads = new List<ThreadInfo>(dictionary.Values);
    }
}