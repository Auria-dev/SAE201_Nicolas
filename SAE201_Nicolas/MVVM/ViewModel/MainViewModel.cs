using SAE201_Nicolas.Core;
using SAE201_Nicolas.MVVM.View;
using SAE201_Nicolas.MVVM.ViewModel.UC;

namespace SAE201_Nicolas.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        // for switching between the view models (pages)
        public RelayCommand RechercherVinVC { get; set; }
        public RelayCommand HistoriqueCommandesVC { get; set; }
        public RelayCommand GestionCommandesVC { get; set; }
        public RelayCommand EspacePersonnelVC { get; set; }
        public RelayCommand AjouterVC { get; set; }

        // the actual view models (pages)
        public RechercherVinViewModel RechercherVinVM { get; set; }
        public HistoriqueCommandesViewModel HistoriqueCommandesVM { get; set; }
        public GestionCommandesViewModel GestionCommandesVM { get; set; }
        public EspacePersonnelViewModel EspacePersonnelVM { get; set; }
        public AjouterViewModel AjouterVM { get; set; }

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
            AjouterVM = new AjouterViewModel();
            EspacePersonnelVM = new EspacePersonnelViewModel();

            // set the default one
            CurrentView = RechercherVinVM;

            // init theme switcher
            ViewSwitcher.OnViewChangeRequested += (newView) => { CurrentView = newView; };


            // when we receive a command fomr the home view, swap to that one
            RechercherVinVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(RechercherVinVM); });
            HistoriqueCommandesVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(HistoriqueCommandesVM); });
            GestionCommandesVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(GestionCommandesVM); });
            EspacePersonnelVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(EspacePersonnelVM); });
            AjouterVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(AjouterVM); });
        }
    }
}
