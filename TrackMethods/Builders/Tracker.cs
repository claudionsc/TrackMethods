using TrackMethods.Interfaces;

namespace TrackMethods.Builders
{
    public class Tracker<T>
    {
        private readonly ITrackMethod _tracker;
        private string _method = string.Empty;
        private string _description = string.Empty;
        private string _user = string.Empty;
        private string? _filePath = null;
        private string _fileName = string.Empty;
        private string _saveAs = "json";
        private int _milisseconds = 1000;

        public Tracker(ITrackMethod tracker)
        {
            _tracker = tracker;
        }

        public Tracker<T> WithMethod(string method)
        {
            _method = method;
            return this;
        }

        public Tracker<T> WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Tracker<T> WithUser(string user)
        {
            _user = user;
            return this;
        }

        public Tracker<T> WithFilePath(string? path)
        {
            _filePath = path;
            return this;
        }

        public Tracker<T> WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public Tracker<T> WithSaveAs(string saveAs)
        {
            _saveAs = saveAs;
            return this;
        }

        public Tracker<T> WithMilliseconds(int ms)
        {
            _milisseconds = ms;
            return this;
        }

        public T Build(Func<T> func)
        {
            return _tracker.TrackExecution(
                func,
                _description,
                _method,
                _user,
                _filePath,
                _fileName,
                _saveAs,
                _milisseconds
            );
        }

        public string LastGeneratedJson => _tracker.LastGeneratedJson;
    }
}
