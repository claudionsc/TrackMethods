using TrackMethods.Interfaces;

public class Execution
{
    private readonly ITrackMethod _tracker;

    public Execution(ITrackMethod tracker)
    {
        _tracker = tracker;
    }

    // Versão completa com todos os parâmetros
    public T ExecutionTracker<T>(
        Func<T> func,
        string methodCalled,
        string description,
        string user,
        string? filePath,
        string fileName,
        string saveAs,
        int milisseconds)
    {
        return _tracker.TrackExecution(func, description, methodCalled, user, filePath, fileName, saveAs, milisseconds);
    }

    // ✅ Sobrecarga simplificada (sem filePath)
    public T ExecutionTracker<T>(
        Func<T> func,
        string methodCalled,
        string description,
        string user,
        string fileName,
        string saveAs,
        int milisseconds)
    {
        return ExecutionTracker(func, methodCalled, description, user, null, fileName, saveAs, milisseconds);
    }

    public string LastGeneratedJson => _tracker.LastGeneratedJson;
}
