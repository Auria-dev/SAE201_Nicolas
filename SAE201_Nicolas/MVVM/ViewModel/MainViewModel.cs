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
        public RelayCommand ModifierVC { get; set; }
        public RelayCommand AjouterVC { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            // init every pages
            ViewSwitcher.InitializeView(new RechercherVinViewModel(), "RechercherVin");
            ViewSwitcher.InitializeView(new HistoriqueDemandesViewModel(), "HistoriqueCommandes");
            ViewSwitcher.InitializeView(new GestionCommandesViewModel(), "GestionCommandes");
            ViewSwitcher.InitializeView(new EspacePersonnelViewModel(), "EspacePersonnel");
            ViewSwitcher.InitializeView(new ModifierViewModel(), "Modifier"); // TODO: all select & edit screens
            ViewSwitcher.InitializeView(new AjouterViewModel(), "Ajouter");
            ViewSwitcher.InitializeView(new AjouterClientViewModel(), "AjouterClient");
            ViewSwitcher.InitializeView(new AjouterFournisseurViewModel(), "AjouterFournisseur");
            ViewSwitcher.InitializeView(new AjouterVinViewModel(), "AjouterVin");

            // init view switcher
            ViewSwitcher.OnViewChangeRequested += (newView) => { CurrentView = newView; };

            // set the default one
            ViewSwitcher.LoadView("RechercherVin");

            // when we receive a command from the menu, swap to that one
            RechercherVinVC = new RelayCommand(o => { ViewSwitcher.LoadView("RechercherVin"); });
            HistoriqueCommandesVC = new RelayCommand(o => { ViewSwitcher.LoadView("HistoriqueCommandes"); });
            GestionCommandesVC = new RelayCommand(o => { ViewSwitcher.LoadView("GestionCommandes"); });
            EspacePersonnelVC = new RelayCommand(o => { ViewSwitcher.LoadView("EspacePersonnel"); });
            ModifierVC = new RelayCommand(o => { ViewSwitcher.LoadView("Modifier"); });
            AjouterVC = new RelayCommand(o => { ViewSwitcher.LoadView("Ajouter"); });
        }
    }
}