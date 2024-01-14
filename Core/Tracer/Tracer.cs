using System.Diagnostics;
using System.Reflection;

namespace Core.Tracer;

using ThreadInfo;
using MethodInfo;

public class Tracer : ITracer
{
    private readonly IDictionary<int, ThreadInfo> _threadInfoDict = new Dictionary<int, ThreadInfo>();
    
    public void StartTrace()
    {
        int threadId = Environment.CurrentManagedThreadId;
        ThreadInfo threadInfo;
        if (_threadInfoDict.ContainsKey(threadId))
        {
            threadInfo = _threadInfoDict[threadId];
        }
        else
        {
            threadInfo = new ThreadInfo(threadId);
            _threadInfoDict.Add(threadId, threadInfo);
        }

        StackTrace stackTrace = new StackTrace();
        MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        MethodInfo methodInfo = new MethodInfo(methodBase);
        
        threadInfo.PushMethodInfo(methodInfo);
        
        Console.WriteLine("Called StartTrace");
    }

    public void StopTrace()
    {
        int threadId = Environment.CurrentManagedThreadId;
        ThreadInfo threadInfo = _threadInfoDict[threadId];
        MethodInfo methodInfo = threadInfo.PopMethodInfo();
        methodInfo.EndMethodInvocation();
        threadInfo.AddInnerMethod(methodInfo);
        
        Console.WriteLine("Called StopTrace");
    }

    public TraceResult GetTraceResult()
    {
        Console.WriteLine("Called GetTraceResult");
        return new TraceResult();
    }
}