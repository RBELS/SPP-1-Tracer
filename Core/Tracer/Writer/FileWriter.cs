namespace Core.Tracer.Writer;

public class FileWriter : IWriter
{
    private readonly string _filepath;
    
    public FileWriter(string filepath)
    {
        _filepath = filepath;
    }
    
    public void Write(string text)
    {
        File.WriteAllText(_filepath, text);
    }
}