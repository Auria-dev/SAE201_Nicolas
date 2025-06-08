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
        // public RechercherVinViewModel RechercherVinVM { get; set; }
        // public HistoriqueCommandesViewModel HistoriqueCommandesVM { get; set; }
        // public GestionCommandesViewModel GestionCommandesVM { get; set; }
        // public EspacePersonnelViewModel EspacePersonnelVM { get; set; }
        // public AjouterViewModel AjouterVM { get; set; }
        // public AjouterClientViewModel AjouterClientVM { get; set; }
        // public AjouterFournisseurViewModel AjouterFournisseurVM { get; set; }
        // public AjouterVinViewModel AjouterVinVM { get; set; }

        // the main view that gets replaces
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            // init every pages
            // RechercherVinVM = new RechercherVinViewModel();
            // HistoriqueCommandesVM = new HistoriqueCommandesViewModel();
            // GestionCommandesVM = new GestionCommandesViewModel();
            // EspacePersonnelVM = new EspacePersonnelViewModel();
            // // todo: ModifierVM
            // AjouterVM = new AjouterViewModel();
            // AjouterClientVM = new AjouterClientViewModel();
            // AjouterFournisseurVM = new AjouterFournisseurViewModel();
            // AjouterVinVM = new AjouterVinViewModel();

            ViewSwitcher.LoadView(new RechercherVinViewModel(), "RechercherVin");
            ViewSwitcher.LoadView(new HistoriqueCommandesViewModel(), "HistoriqueCommandes");
            ViewSwitcher.LoadView(new GestionCommandesViewModel(), "GestionCommandes");
            ViewSwitcher.LoadView(new EspacePersonnelViewModel(), "EspacePersonnel");
            ViewSwitcher.LoadView(new AjouterViewModel(), "Ajouter");
            ViewSwitcher.LoadView(new AjouterClientViewModel(), "AjouterClient");
            ViewSwitcher.LoadView(new AjouterFournisseurViewModel(), "AjouterFournisseur");
            ViewSwitcher.LoadView(new AjouterVinViewModel(), "AjouterVin");

            // set the default one
            //CurrentView = RechercherVinVM;
            ViewSwitcher.ChangeViewTo("RechercherVin");

            // init view switcher
            ViewSwitcher.OnViewChangeRequested += (newView) => { CurrentView = newView; };

            // when we receive a command from the menu, swap to that one
            // RechercherVinVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(RechercherVinVM); });
            // HistoriqueCommandesVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(HistoriqueCommandesVM); });
            // GestionCommandesVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(GestionCommandesVM); });
            // EspacePersonnelVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(EspacePersonnelVM); });
            // AjouterVC = new RelayCommand(o => { ViewSwitcher.RequestViewChange(AjouterVM); });
            RechercherVinVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("RechercherVin"); });
            HistoriqueCommandesVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("HistoriqueCommandes"); });
            GestionCommandesVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("GestionCommandes"); });
            EspacePersonnelVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("EspacePersonnel"); });
            AjouterVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("Ajouter"); });
        }
    }
}