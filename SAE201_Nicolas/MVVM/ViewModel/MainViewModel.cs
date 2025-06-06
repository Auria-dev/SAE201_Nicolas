using SAE201_Nicolas.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        // for switching between the view models (pages)
        public RelayCommand RechercherVinVC { get; set; }
        public RelayCommand HistoriqueCommandesVC { get; set; }
        public RelayCommand GestionCommandesVC { get; set; }

        // the actual view models (pages)
        public RechercherVinViewModel RechercherVinVM { get; set; }
        public HistoriqueCommandesViewModel HistoriqueCommandesVM { get; set; }
        public GestionCommandesViewModel GestionCommandesVM { get; set; }

        // the main view that gets replaces
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            // dont forget to init every new pages
            RechercherVinVM = new RechercherVinViewModel();
            HistoriqueCommandesVM = new HistoriqueCommandesViewModel();
            GestionCommandesVM = new GestionCommandesViewModel();

            // set the default one
            CurrentView = RechercherVinVM;

            // when we receive a command fomr the home view, swap to that one
            RechercherVinVC = new RelayCommand(o => { CurrentView = RechercherVinVM; });
            HistoriqueCommandesVC = new RelayCommand(o => { CurrentView = HistoriqueCommandesVM; });
            GestionCommandesVC = new RelayCommand(o => { CurrentView = GestionCommandesVM; });
        }
    }
}
