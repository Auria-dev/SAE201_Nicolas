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

        private static object _currentView;

        public static void RequestViewChange(object newView)
        {
            if (_currentView != null) _history.Push(_currentView);
            
            _currentView = newView;
            OnViewChangeRequested?.Invoke(newView);
        }

        public static void GoBack()
        {
            if (_history.Count > 0)
            {
                _currentView = _history.Pop();
                OnViewChangeRequested?.Invoke(_currentView);
            }
            else { }
        }
    }
}
