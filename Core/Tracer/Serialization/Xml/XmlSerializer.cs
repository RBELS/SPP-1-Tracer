namespace Core.Tracer.Serialization.Xml;

public class XmlSerializer : ISerializer
{
    public string Serialize(TraceResult traceResult)
    {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTraceResult));
        using (var stringWriter = new StringWriter())
        {
            xmlSerializer.Serialize(stringWriter, new XmlTraceResult(traceResult));
            return stringWriter.ToString();
        }
    }
}