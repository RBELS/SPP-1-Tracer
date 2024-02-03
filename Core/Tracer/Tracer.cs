using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace Core.Tracer;

public class Tracer : ITracer
{
    private readonly ConcurrentDictionary<int, ThreadInfo> _threadInfoDict = new ConcurrentDictionary<int, ThreadInfo>();
    
    public void StartTrace()
    {
        int threadId = Environment.CurrentManagedThreadId;
        ThreadInfo threadInfo = _threadInfoDict.GetOrAdd(threadId, new ThreadInfo(threadId));

        StackTrace stackTrace = new StackTrace();
        MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
        MethodInfo methodInfo = new MethodInfo(methodBase);
        
        threadInfo.PushMethodInfo(methodInfo);
    }

    public void StopTrace()
    {
        int threadId = Environment.CurrentManagedThreadId;
        ThreadInfo threadInfo = _threadInfoDict[threadId];
        MethodInfo methodInfo = threadInfo.PopMethodInfo();
        methodInfo.EndMethodInvocation();
        threadInfo.AddInnerMethod(methodInfo);
    }

    public TraceResult GetTraceResult()
    {
        return new TraceResult(_threadInfoDict);
    }
}