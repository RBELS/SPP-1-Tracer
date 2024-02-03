using Core.Tracer;

namespace ExampleApp;

public class Foo
{
    private readonly Bar _bar;
    private readonly ITracer _tracer;

    internal Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(_tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();
        _bar.InnerMethod();
        Thread.Sleep(1000);
        _tracer.StopTrace();
    }
}