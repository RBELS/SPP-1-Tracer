using System.Xml.Schema;
using System.Xml.Serialization;

namespace Core.Tracer.Serialization.Xml;

public class XmlThreadInfo
{
    [XmlAttribute(AttributeName = "id", Form = XmlSchemaForm.Unqualified)]
    public int Id { get; set; }
    
    [XmlAttribute(AttributeName = "time", Form = XmlSchemaForm.Unqualified)]
    public string Time { get; set; }

    [XmlElement(ElementName = "method", Form = XmlSchemaForm.Unqualified)]
    public List<XmlMethodInfo> Methods { get; set; } = new List<XmlMethodInfo>();

    public XmlThreadInfo() {}
    
    public XmlThreadInfo(ThreadInfo threadInfo)
    {
        Id = threadInfo.ThreadId;
        
        int timeSum = 0;
        foreach (var methodInfo in threadInfo.RootMethodInfoList)
        {
            timeSum += methodInfo.GetDuration();
            Methods.Add(new XmlMethodInfo(methodInfo));
        }
        
        Time = $"{timeSum}ms";
    }
}