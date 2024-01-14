namespace Core.ThreadInfo;

using MethodInfo;

public class ThreadInfo
{
    private readonly Stack<MethodInfo> _methodInfoStack = new Stack<MethodInfo>();
    private readonly List<MethodInfo> _rootMethodInfoList = new List<MethodInfo>();
    private readonly int _threadId;
    
    internal ThreadInfo(int threadId)
    {
        _threadId = threadId;
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
            _rootMethodInfoList.Add(methodInfo);
        }
    }
}