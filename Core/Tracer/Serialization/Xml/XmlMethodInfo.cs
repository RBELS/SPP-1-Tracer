using System.Xml.Schema;
using System.Xml.Serialization;

namespace Core.Tracer.Serialization.Xml;

public class XmlMethodInfo
{
    [XmlAttribute(AttributeName = "name", Form = XmlSchemaForm.Unqualified)]
    public string Method { get; set; }
    [XmlAttribute(AttributeName = "class", Form = XmlSchemaForm.Unqualified)]
    public string Class { get; set; }
    [XmlAttribute(AttributeName = "time", Form = XmlSchemaForm.Unqualified)]
    public string Time { get; set; }

    [XmlElement(ElementName = "method")]
    public List<XmlMethodInfo> Methods { get; set; } = new List<XmlMethodInfo>();
    
    public XmlMethodInfo() {}
    public XmlMethodInfo(MethodInfo methodInfo)
    {
        Method = methodInfo.MethodName;
        Class = methodInfo.ClassName;
        Time = $"{methodInfo.GetDuration()}ms";
        foreach (var methodInfoInnerMethod in methodInfo.InnerMethods)
        {
            Methods.Add(new XmlMethodInfo(methodInfoInnerMethod));
        }
    }
}