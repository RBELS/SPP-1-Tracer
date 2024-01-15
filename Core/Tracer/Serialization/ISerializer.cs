namespace Core.Tracer.Serialization;

public interface ISerializer
{
    string Serialize(TraceResult traceResult);
}