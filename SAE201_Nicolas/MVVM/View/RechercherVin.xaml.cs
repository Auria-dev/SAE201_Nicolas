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
            listVins.Filter = RechercherListVin;
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

        private bool RechercherListVin(object obj)
        {
            Vin unVin = (Vin)obj;
            bool verifType = true;
            bool verifPrix = true;
            int prixMin = 0, prixMax = int.MaxValue;

            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Blanc)) verifType = (bool)FiltreBlanc.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Rouge)) verifType = (bool)FiltreRouge.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Rosé)) verifType = (bool)FiltreRose.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Champagne)) verifType = (bool)FiltreChampagne.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Mousseux)) verifType = (bool)FiltreMousseux.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Doux)) verifType = (bool)FiltreDoux.IsChecked;
            if (unVin.NumTypeVin == unVin.EnumToNumTypeVin(TypeVin.Liquoreux)) verifType = (bool)FiltreLiquoreux.IsChecked;

            if (!string.IsNullOrEmpty(FiltrePrixMin.Text) && !string.IsNullOrWhiteSpace(FiltrePrixMin.Text)) {
                Console.WriteLine($"prix min {prixMin} prix max {prixMax}");

                int.TryParse(FiltrePrixMin.Text, out prixMin);
                verifPrix = false;
                // unVin.PrixVin < prixMin
            }

            if (!string.IsNullOrEmpty(FiltrePrixMax.Text) && !string.IsNullOrWhiteSpace(FiltrePrixMax.Text)) {
                int.TryParse(FiltrePrixMax.Text, out prixMax);
                verifPrix = false;
                // unVin.PrixVin < prixMax
            }


            return verifType && verifPrix;
        }
    }
}