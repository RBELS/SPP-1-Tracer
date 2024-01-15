using Core.Tracer;
using Core.Tracer.Serialization;
using Core.Tracer.Serialization.Json;
using Core.Tracer.Serialization.Xml;

namespace ExampleApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Program started.");
        ITracer tracer = new Tracer();
        Foo foo = new Foo(tracer);
        foo.MyMethod();
        
        TraceResult traceResult = tracer.GetTraceResult();
        ISerializer jsonSerializer = new JsonSerializer();
        string jsonStr = jsonSerializer.Serialize(traceResult);

        ISerializer xmlSerializer = new XmlSerializer();
        string xmlStr = xmlSerializer.Serialize(traceResult);
     
        Console.WriteLine(xmlStr);
        // Console.WriteLine(jsonStr);
    }
}