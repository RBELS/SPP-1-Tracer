using System.Text.Json;
using Core.Tracer;
using Core.Tracer.Serialization.Json;
using Core.Tracer.Serialization.Xml;
using ExampleApp;
using JsonSerializer = Core.Tracer.Serialization.Json.JsonSerializer;

namespace Tests;

public class Tests
{
    private ITracer _tracer;
    private Foo _foo;
    private TraceResult _traceResult;
    
    [SetUp]
    public void Setup()
    {
        _tracer = new Tracer();
        _foo = new Foo(_tracer);
    }

    [Test]
    public void CallInSingleThreadOk()
    {
        _foo.MyMethod();
        _foo.MyMethod();
        _traceResult = _tracer.GetTraceResult();

        Assert.That(_traceResult.Threads.Count, Is.EqualTo(1));
        
        var threadInfo = _traceResult.Threads[0];
        
        Assert.That(threadInfo.RootMethodInfoList, Has.Count.EqualTo(2));

        var methodInfo = threadInfo.RootMethodInfoList[0];
        Assert.Multiple(() =>
        {
            Assert.That(methodInfo.MethodName, Is.EqualTo("MyMethod"));
            Assert.That(methodInfo.ClassName, Is.EqualTo("Foo"));
            Assert.That(methodInfo.InnerMethods, Has.Count.EqualTo(1));
        });

        var innerMethodInfo = methodInfo.InnerMethods[0];
        Assert.Multiple(() =>
        {
            Assert.That(innerMethodInfo.MethodName, Is.EqualTo("InnerMethod"));
            Assert.That(innerMethodInfo.ClassName, Is.EqualTo("Bar"));
            Assert.That(innerMethodInfo.InnerMethods, Has.Count.EqualTo(0)); 
        });
        
        // json. Построить dto
        Assert.That(new JsonSerializer().Serialize(_traceResult), Is.Not.Null);
        var jsonTraceResult = new JsonTraceResult(_traceResult);
        Assert.Multiple(() =>
        {
            Assert.That(jsonTraceResult.threads, Has.Count.EqualTo(1));
            Assert.That(jsonTraceResult.threads[0].Methods, Has.Count.EqualTo(2));
        });

        var jsonMethodInfo = jsonTraceResult.threads[0].Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(jsonMethodInfo.Class, Is.EqualTo("Foo"));
            Assert.That(jsonMethodInfo.Method, Is.EqualTo("MyMethod"));
            Assert.That(jsonMethodInfo.Methods, Has.Count.EqualTo(1));
        });

        var jsonInnerMethodInfo = jsonMethodInfo.Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(jsonInnerMethodInfo.Class, Is.EqualTo("Bar"));
            Assert.That(jsonInnerMethodInfo.Method, Is.EqualTo("InnerMethod"));
            Assert.That(jsonInnerMethodInfo.Methods, Has.Count.EqualTo(0));
        });
        
        
        // xml. Построить dto
        Assert.That(new XmlSerializer().Serialize(_traceResult), Is.Not.Null);
        var xmlTraceResult = new XmlTraceResult(_traceResult);
        Assert.Multiple(() =>
        {
            Assert.That(xmlTraceResult.threads, Has.Count.EqualTo(1));
            Assert.That(xmlTraceResult.threads[0].Methods, Has.Count.EqualTo(2));
        });

        var xmlMethodInfo = xmlTraceResult.threads[0].Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(xmlMethodInfo.Class, Is.EqualTo("Foo"));
            Assert.That(xmlMethodInfo.Method, Is.EqualTo("MyMethod"));
            Assert.That(xmlMethodInfo.Methods, Has.Count.EqualTo(1));
        });

        var xmlInnerMethodInfo = xmlMethodInfo.Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(xmlInnerMethodInfo.Class, Is.EqualTo("Bar"));
            Assert.That(xmlInnerMethodInfo.Method, Is.EqualTo("InnerMethod"));
            Assert.That(xmlInnerMethodInfo.Methods, Has.Count.EqualTo(0));
        });
    }
    
    [Test]
    public void CallInMultipleThreadsOk()
    {
        var task1 = new Task(() =>
        {
            _foo.MyMethod();
        });
        var task2 = new Task(() =>
        {
            _foo.MyMethod();
            _foo.MyMethod();
        });
        var task3 = new Task(() =>
        {
            _foo.MyMethod();
            _foo.MyMethod();
            _foo.MyMethod();
        });
        
        task1.Start();
        task2.Start();
        task3.Start();
        
        task1.Wait();
        task2.Wait();
        task3.Wait();
        
        _traceResult = _tracer.GetTraceResult();

        Assert.That(_traceResult.Threads.Count, Is.EqualTo(3));
        
        var threadInfo = _traceResult.Threads[2];
        
        Assert.That(threadInfo.RootMethodInfoList, Has.Count.EqualTo(3));

        var methodInfo = threadInfo.RootMethodInfoList[0];
        Assert.Multiple(() =>
        {
            Assert.That(methodInfo.MethodName, Is.EqualTo("MyMethod"));
            Assert.That(methodInfo.ClassName, Is.EqualTo("Foo"));
            Assert.That(methodInfo.InnerMethods, Has.Count.EqualTo(1));
        });

        var innerMethodInfo = methodInfo.InnerMethods[0];
        Assert.Multiple(() =>
        {
            Assert.That(innerMethodInfo.MethodName, Is.EqualTo("InnerMethod"));
            Assert.That(innerMethodInfo.ClassName, Is.EqualTo("Bar"));
            Assert.That(innerMethodInfo.InnerMethods, Has.Count.EqualTo(0)); 
        });
        
        // json. Построить dto
        Assert.That(new JsonSerializer().Serialize(_traceResult), Is.Not.Null);
        var jsonTraceResult = new JsonTraceResult(_traceResult);
        Assert.Multiple(() =>
        {
            Assert.That(jsonTraceResult.threads, Has.Count.EqualTo(3));
            Assert.That(jsonTraceResult.threads[2].Methods, Has.Count.EqualTo(3));
        });

        var jsonMethodInfo = jsonTraceResult.threads[2].Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(jsonMethodInfo.Class, Is.EqualTo("Foo"));
            Assert.That(jsonMethodInfo.Method, Is.EqualTo("MyMethod"));
            Assert.That(jsonMethodInfo.Methods, Has.Count.EqualTo(1));
        });

        var jsonInnerMethodInfo = jsonMethodInfo.Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(jsonInnerMethodInfo.Class, Is.EqualTo("Bar"));
            Assert.That(jsonInnerMethodInfo.Method, Is.EqualTo("InnerMethod"));
            Assert.That(jsonInnerMethodInfo.Methods, Has.Count.EqualTo(0));
        });
        
        
        // xml. Построить dto
        Assert.That(new XmlSerializer().Serialize(_traceResult), Is.Not.Null);
        var xmlTraceResult = new XmlTraceResult(_traceResult);
        Assert.Multiple(() =>
        {
            Assert.That(xmlTraceResult.threads, Has.Count.EqualTo(3));
            Assert.That(xmlTraceResult.threads[2].Methods, Has.Count.EqualTo(3));
        });

        var xmlMethodInfo = xmlTraceResult.threads[2].Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(xmlMethodInfo.Class, Is.EqualTo("Foo"));
            Assert.That(xmlMethodInfo.Method, Is.EqualTo("MyMethod"));
            Assert.That(xmlMethodInfo.Methods, Has.Count.EqualTo(1));
        });

        var xmlInnerMethodInfo = xmlMethodInfo.Methods[0];
        Assert.Multiple(() =>
        {
            Assert.That(xmlInnerMethodInfo.Class, Is.EqualTo("Bar"));
            Assert.That(xmlInnerMethodInfo.Method, Is.EqualTo("InnerMethod"));
            Assert.That(xmlInnerMethodInfo.Methods, Has.Count.EqualTo(0));
        });
    }
}