using Core.Tracer;

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
        Console.WriteLine(traceResult.ToString());
    }
}