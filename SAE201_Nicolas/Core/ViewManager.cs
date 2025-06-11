using SAE201_Nicolas.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SAE201_Nicolas.Core
{
    public class ViewManager
    {
        private Dictionary<string, Lazy<UserControl>> _views;
        public event Action<ContentControl, string> OnMainContentChangeRequested;

        private static ViewManager _instance;

        private ViewManager()
        {
            _views = new Dictionary<string, Lazy<UserControl>>();
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

        public void RequestMainContentChange(ContentControl contentControl, string viewName)
        {
            OnMainContentChangeRequested?.Invoke(contentControl, viewName);
        }
    }
}
