using System.Diagnostics;
using System.Reflection;

namespace Core;

public class MethodInfo
{
    public string MethodName { get; private set; }
    public string ClassName { get; private set; }
    public List<MethodInfo> InnerMethods { get; } = new List<MethodInfo>();
    private readonly Stopwatch _stopwatch;

    internal MethodInfo(MethodBase methodBase)
    {
        MethodName = methodBase.Name;
        ClassName = methodBase.DeclaringType.Name;
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public void EndMethodInvocation()
    {
        _stopwatch.Stop();
    }

    public int GetDuration()
    {
        return _stopwatch.Elapsed.Milliseconds;
    }
}