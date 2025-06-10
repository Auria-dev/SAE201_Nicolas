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
    /// Logique d'interaction pour RechercherVin.xaml
    /// </summary>
    public partial class RechercherVin : UserControl
    {
        private ListCollectionView listVins;
        public GestionVin LaGestionDeVins { get; set; }
        
        public RechercherVin()
        {
            ChargeData();
            InitializeComponent();
            listVins = new ListCollectionView(LaGestionDeVins.LesVins);
            dgVins.ItemsSource = listVins;
            listVins.Filter = FiltrerVins;
        }

        public void ChargeData()
        {
            try
            {
                LaGestionDeVins = new GestionVin("Gestion Vins");
                this.DataContext = LaGestionDeVins;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                Application.Current.Shutdown();
            }
        }

        private bool FiltrerVins(object obj)
        {
            Vin unVin = (Vin)obj;
            bool verifType = true;
            bool verifPrix = true;
            bool verifAppellation = true;
            bool rechercherVin = true;
            int prixMin = 0, prixMax = int.MaxValue;

            // filtre type vin
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Blanc)) verifType = (bool)FiltreBlanc.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Rouge)) verifType = (bool)FiltreRouge.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Rosé)) verifType = (bool)FiltreRose.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Champagne)) verifType = (bool)FiltreChampagne.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Mousseux)) verifType = (bool)FiltreMousseux.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Doux)) verifType = (bool)FiltreDoux.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToInt(TypeVin.Liquoreux)) verifType = (bool)FiltreLiquoreux.IsChecked;

            // filtre prix vin
            if (!string.IsNullOrEmpty(FiltrePrixMin.Text) && !string.IsNullOrWhiteSpace(FiltrePrixMin.Text) && int.TryParse(FiltrePrixMin.Text, out prixMin)) 
                verifPrix = unVin.PrixVin >= prixMin;
            
            if (!string.IsNullOrEmpty(FiltrePrixMax.Text) && !string.IsNullOrWhiteSpace(FiltrePrixMax.Text) && int.TryParse(FiltrePrixMax.Text, out prixMax)) 
                verifPrix = unVin.PrixVin <= prixMax;
            
            // filtre appellation
            if (ComboxBoxAppellation.SelectedIndex != 0)
                verifAppellation = (unVin.NumAppelation == ComboxBoxAppellation.SelectedIndex);

            // rechercher 
            if (!string.IsNullOrEmpty(barDeRechercheVins.Text) && !string.IsNullOrWhiteSpace(barDeRechercheVins.Text))
                rechercherVin = unVin.NomVin.Contains(barDeRechercheVins.Text) || unVin.Annee.ToString().Contains(barDeRechercheVins.Text);

            return verifType && verifPrix && verifAppellation && rechercherVin;
        }

        private void updateFiltreTypeVin(object sender, RoutedEventArgs e)
        {
            if (dgVins != null)
                CollectionViewSource.GetDefaultView(dgVins.ItemsSource).Refresh();
        }

        private void updateFiltreAppellationVin(object sender, SelectionChangedEventArgs e)
        {
            if (dgVins != null)
                CollectionViewSource.GetDefaultView(dgVins.ItemsSource).Refresh();
        }

        private void updateFiltrePrix(object sender, TextChangedEventArgs e)
        {
            if (dgVins != null)
                CollectionViewSource.GetDefaultView(dgVins.ItemsSource).Refresh();
        }

        private void updateRechercheVin(object sender, RoutedEventArgs e)
        {
            if (dgVins != null)
                CollectionViewSource.GetDefaultView(dgVins.ItemsSource).Refresh();
        }
    }
}