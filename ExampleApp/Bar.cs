using Core.Tracer;

namespace ExampleApp;

public class Bar
{
    private readonly ITracer _tracer;

    internal Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(3000);
        _tracer.StopTrace();
    }
}