using System.Xml.Serialization;

namespace Core.Tracer.Serialization.Xml;

[XmlRoot(ElementName = "root")]
public class XmlTraceResult
{
    [XmlElement(ElementName = "thread")]
    public List<XmlThreadInfo> threads { get; set; } = new List<XmlThreadInfo>();
    
    public XmlTraceResult() {}

    public XmlTraceResult(TraceResult traceResult)
    {
        foreach (var traceResultThread in traceResult.Threads)
        {
            threads.Add(new XmlThreadInfo(traceResultThread));
        }
    }
}