using Core.Tracer;
using Core.Tracer.Serialization;
using Core.Tracer.Serialization.Json;
using Core.Tracer.Serialization.Xml;
using Core.Tracer.Writer;

namespace ExampleApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Program started.");
        ITracer tracer = new Tracer();
        var foo = new Foo(tracer);
        var task1 = new Task(() =>
        {
            foo.MyMethod();
        });
        var task2 = new Task(() =>
        {
            foo.MyMethod();
            foo.MyMethod();
        });
        var task3 = new Task(() =>
        {
            foo.MyMethod();
            foo.MyMethod();
            foo.MyMethod();
        });
        
        task1.Start();
        task2.Start();
        task3.Start();
        
        task1.Wait();
        task2.Wait();
        task3.Wait();
        
        var traceResult = tracer.GetTraceResult();
        ISerializer jsonSerializer = new JsonSerializer();
        var jsonStr = jsonSerializer.Serialize(traceResult);

        ISerializer xmlSerializer = new XmlSerializer();
        var xmlStr = xmlSerializer.Serialize(traceResult);

        IWriter consoleWriter = new ConsoleWriter();
        consoleWriter.Write($"JSON Result: {jsonStr}");
        consoleWriter.Write($"XML Result: {xmlStr}");

        IWriter jsonFileWriter = new FileWriter("./result.json");
        IWriter xmlFileWriter = new FileWriter("./result.xml");

        jsonFileWriter.Write(jsonStr);
        xmlFileWriter.Write(xmlStr);
    }
}