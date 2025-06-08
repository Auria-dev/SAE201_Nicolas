using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Core
{
    public class ViewSwitcher
    {
        public static event Action<object> OnViewChangeRequested;
        private static Stack<object> _history = new Stack<object>(); // page history
        private static Dictionary<string, object> _views = new Dictionary<string, object>();

        private static object _currentView;

        public static void RequestViewChange(object newView)
        {
            if (_currentView != null) _history.Push(_currentView);

            _currentView = newView;
            OnViewChangeRequested?.Invoke(newView);
            //Console.WriteLine($"Switching to view: {_currentView.GetType().Name} @ position 0x{_currentView.GetHashCode():x}");
        }

        public static void GoBack()
        {
            if (_history.Count > 0)
            {
                _currentView = _history.Pop();
                OnViewChangeRequested?.Invoke(_currentView);
            }
        }

        // load each view in a dictionary, and with a key so we can easily find them
        public static void LoadView(object view, string key) // TODO: rename this to InitializeView
        {
            _views[key] = view;
        }

        // fetch view from the dictionary 
        public static object FetchView(string key) // TODO: rename this to GetView
        {
            return _views.TryGetValue(key, out var view) ? view : null;
        }

        public static void ChangeViewTo(string key) // TODO: rename this to LoadView
        {
            if (!_views.ContainsKey(key)) return;
            RequestViewChange(_views[key]);
        }
    }
}
