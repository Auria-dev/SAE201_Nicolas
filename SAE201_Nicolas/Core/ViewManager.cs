using SAE201_Nicolas.View;
using System.Windows.Controls;

namespace SAE201_Nicolas.Core
{
    public class ViewManager
    {
        private Dictionary<string, Lazy<UserControl>> _views;
        private Stack<string> _viewHistory;
        public event Action<string> OnMainContentChangeRequested;
        private static ViewManager _instance;

        private ViewManager()
        {
            _views = new Dictionary<string, Lazy<UserControl>>();
            _viewHistory = new Stack<string>();
        }

        public static ViewManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ViewManager();
                return _instance;
            }
        }

        public void RegisterView<T>(string viewName) where T : UserControl, new()
        {
            if (!_views.ContainsKey(viewName))
                _views[viewName] = new Lazy<UserControl>(() => new T());
        }

        public UserControl GetView(string viewName)
        {
            if (_views.TryGetValue(viewName, out var lazyView)) return lazyView.Value;
            throw new ArgumentException("View not found");
        }

        public void RequestMainContentChange(string viewName)
        {
            if (_viewHistory.Count == 0 || _viewHistory.Peek() != viewName)
                _viewHistory.Push(viewName);
            
            OnMainContentChangeRequested?.Invoke(viewName);
        }

        public void GoBack()
        {
            if (_viewHistory.Count > 1) {
                _viewHistory.Pop();
                string previousView = _viewHistory.Peek();
                OnMainContentChangeRequested?.Invoke(previousView);
            }
        }

        public void Reset()
        {
            _views.Clear();
            _viewHistory.Clear();
        }
    }
}
