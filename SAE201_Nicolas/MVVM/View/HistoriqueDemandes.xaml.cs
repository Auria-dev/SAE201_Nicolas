using SAE201_Nicolas.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE201_Nicolas.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour HistoriqueCommandes.xaml
    /// </summary>
    public partial class HistoriqueCommandes : UserControl
    {
        private ListCollectionView listCommandes;

        public HistoriqueCommandes()
        {
            InitializeComponent();
            listCommandes = new ListCollectionView(MainWindow.LaGestionDeVins.LesDemandes);
            dgDemandes.ItemsSource = listCommandes;
            listCommandes.Filter = FiltrerDemandes;
        }

        private bool FiltrerDemandes(object obj)
        {
            Demande d = (Demande)obj;

            bool recherche = true;
            bool filtrePrixMin = true, filtrePrixMax = true;
            bool filtreDateDebut = true, filtreDateFin = true;
            bool filtreEtat = true;

            int prixMin, prixMax;
            DateTime dateDebut, dateFin;

            recherche 
                =  d.NomVin.Contains(BarDeRechercheDemandes.Text) 
                || d.NomEmploye.Contains(BarDeRechercheDemandes.Text)
                || d.NumDemande.ToString().Contains(BarDeRechercheDemandes.Text) 
                || d.NumCommande.ToString().Contains(BarDeRechercheDemandes.Text) 
                || d.QuantiteDemande.ToString().Contains(BarDeRechercheDemandes.Text);

            if (int.TryParse(TextboxFiltrePrixMin.Text, out prixMin))
                filtrePrixMin = (d.PrixDemande >= prixMin);

            if (int.TryParse(TextboxFiltrePrixMax.Text, out prixMax))
                filtrePrixMax = (d.PrixDemande <= prixMax);

            if (DateTime.TryParse(TxtboxDateDebut.Text, out dateDebut))
                filtreDateDebut = d.DateDemande >= dateDebut;

            if (DateTime.TryParse(TxtboxDateFin.Text, out dateFin))
                filtreDateFin = d.DateDemande <= dateFin;

            if (ComboxBoxEtat.SelectedIndex != 0)
                filtreEtat = ComboxBoxEtat.SelectedIndex == d.EtatDemandeToInt(d.EtatDemande);

            return recherche && filtrePrixMin && filtrePrixMax && filtreDateDebut && filtreDateFin && filtreEtat;
        }

        private void updateFiltres(object sender, TextChangedEventArgs e)
        {
            if (dgDemandes != null)
                CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
        }

        private void updateFiltreType(object sender, SelectionChangedEventArgs e)
        {
            if (dgDemandes != null)
                CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
        }

        private void UpdateSearch(object sender, RoutedEventArgs e)
        {
            if (dgDemandes != null)
                CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
        }
    }
}
