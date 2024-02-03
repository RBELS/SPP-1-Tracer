using System.Xml;

namespace Core.Tracer.Serialization.Xml;

public class XmlSerializer : ISerializer
{
    public string Serialize(TraceResult traceResult)
    {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTraceResult));
        using (var stringWriter = new StringWriter())
        {
            var settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(xmlWriter, new XmlTraceResult(traceResult));
                return stringWriter.ToString();
            }
        }
    }
}