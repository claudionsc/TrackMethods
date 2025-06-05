using TrackMethods.Builders;
using TrackMethods.Interfaces;

public class Execution
{
    private readonly ITrackMethod _tracker;

    public Execution(ITrackMethod tracker)
    {
        _tracker = tracker;
    }
    public Tracker<T> Create<T>()
    {
        return new Tracker<T>(_tracker);
    }

}
