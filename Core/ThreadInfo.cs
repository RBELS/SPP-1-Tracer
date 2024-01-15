namespace Core;

public class ThreadInfo
{
    private readonly Stack<MethodInfo> _methodInfoStack = new Stack<MethodInfo>();
    public List<MethodInfo> RootMethodInfoList { get; private set; } = new List<MethodInfo>();
    public int ThreadId { get; private set; }
    
    internal ThreadInfo(int threadId)
    {
        ThreadId = threadId;
    }

    public void PushMethodInfo(MethodInfo methodInfo)
    {
        _methodInfoStack.Push(methodInfo);
    }

    public MethodInfo PopMethodInfo()
    {
        return _methodInfoStack.Pop();
    }

    public void AddInnerMethod(MethodInfo methodInfo)
    {
        if (_methodInfoStack.Count != 0)
        {
            _methodInfoStack.Peek().InnerMethods.Add(methodInfo);
        }
        else
        {
            RootMethodInfoList.Add(methodInfo);
        }
    }
}