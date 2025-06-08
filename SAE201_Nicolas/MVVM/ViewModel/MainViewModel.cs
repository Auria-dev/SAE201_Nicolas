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
            ViewSwitcher.LoadView(new RechercherVinViewModel(), "RechercherVin");
            ViewSwitcher.LoadView(new HistoriqueCommandesViewModel(), "HistoriqueCommandes");
            ViewSwitcher.LoadView(new GestionCommandesViewModel(), "GestionCommandes");
            ViewSwitcher.LoadView(new EspacePersonnelViewModel(), "EspacePersonnel");
            // // todo: ModifierVM
            ViewSwitcher.LoadView(new AjouterViewModel(), "Ajouter");
            ViewSwitcher.LoadView(new AjouterClientViewModel(), "AjouterClient");
            ViewSwitcher.LoadView(new AjouterFournisseurViewModel(), "AjouterFournisseur");
            ViewSwitcher.LoadView(new AjouterVinViewModel(), "AjouterVin");

            // init view switcher
            ViewSwitcher.OnViewChangeRequested += (newView) => { CurrentView = newView; };

            // set the default one
            ViewSwitcher.ChangeViewTo("RechercherVin");

            // when we receive a command from the menu, swap to that one
            RechercherVinVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("RechercherVin"); });
            HistoriqueCommandesVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("HistoriqueCommandes"); });
            GestionCommandesVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("GestionCommandes"); });
            EspacePersonnelVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("EspacePersonnel"); });
            AjouterVC = new RelayCommand(o => { ViewSwitcher.ChangeViewTo("Ajouter"); });
        }
    }
}