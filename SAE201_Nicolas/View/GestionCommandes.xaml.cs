using SAE201_Nicolas.Model;
using SAE201_Nicolas.View.UC;
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

namespace SAE201_Nicolas.View
{
    /// <summary>
    /// Logique d'interaction pour GestionCommandes.xaml
    /// </summary>
    public partial class GestionCommandes : UserControl
    {
        private ListCollectionView listGestionsVins;

        public GestionCommandes()
        {
            InitializeComponent();
            listGestionsVins = new ListCollectionView(MainWindow.LaGestionDeVins.LesDetailsCommandes);
            dgCommandes.ItemsSource = listGestionsVins;
            listGestionsVins.Filter = FiltrerCommandes;
        }

        private bool FiltrerCommandes(object obj)
        {
            DetailCommande uneCommande = (DetailCommande)obj;
            bool verifType = true;
            bool verifAppellation = true;
            bool rechercherVin = true;

            // filtre type vin
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Blanc)) verifType = (bool)FiltreBlanc.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Rouge)) verifType = (bool)FiltreRouge.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Rosé)) verifType = (bool)FiltreRose.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Champagne)) verifType = (bool)FiltreChampagne.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Mousseux)) verifType = (bool)FiltreMousseux.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Doux)) verifType = (bool)FiltreDoux.IsChecked;
            if (uneCommande.Vin.NumTypeVin == uneCommande.Vin.EnumToInt(TypeVin.Liquoreux)) verifType = (bool)FiltreLiquoreux.IsChecked;

            // filtre appellation
            if (ComboxBoxAppellation.SelectedIndex != 0)
                verifAppellation = (uneCommande.Vin.NumAppelation == ComboxBoxAppellation.SelectedIndex);

            // rechercher 
            string motclefs = barDeRechercheCommandes.Text.ToLower();
            if (!string.IsNullOrEmpty(motclefs) && !string.IsNullOrWhiteSpace(motclefs))
                rechercherVin = 
                       uneCommande.NomVin.ToLower().Contains(motclefs) 
                    || uneCommande.Vin.NumVin.ToString().ToLower().Contains(motclefs) 
                    || uneCommande.Vin.Annee.ToString().ToLower().Contains(motclefs) 
                    || uneCommande.FournisseurVin.ToLower().ToString().Contains(motclefs);

            return verifType && verifAppellation && rechercherVin;
        }

        private void updateFiltreTypeVin(object sender, RoutedEventArgs e)
        {

            if (dgCommandes != null)
                CollectionViewSource.GetDefaultView(dgCommandes.ItemsSource).Refresh();
        }

        private void updateFiltreAppellationVin(object sender, SelectionChangedEventArgs e)
        {
            if (dgCommandes != null)
                CollectionViewSource.GetDefaultView(dgCommandes.ItemsSource).Refresh();
        }

        private void updateRechercheCommande(object sender, RoutedEventArgs e)
        {
            if (dgCommandes != null)
                CollectionViewSource.GetDefaultView(dgCommandes.ItemsSource).Refresh();
        }

        private void dgCommandesSupprimer(object sender, RoutedEventArgs e)
        {

            // delete le detail commande ET la commande

            List<DetailCommande> dtcm = dgCommandes.SelectedItems.Cast<DetailCommande>().ToList();

            MessageBoxResult supprimerResult = MessageBox.Show(
                    $"Etes vous sur de vouloir supprimmer {(dtcm.Count > 1 ? "ces" : "cette")} commande{(dtcm.Count > 1 ? "s" : "")} ?",
                    "Supprimmer",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
            );

            if (supprimerResult == MessageBoxResult.Yes)
            {
                foreach (DetailCommande commande in dtcm)
                {
                    try
                    {
                        commande.Delete();
                        commande.Commande.Delete();

                        MainWindow.LaGestionDeVins.LesDetailsCommandes.Remove(commande);
                        MainWindow.LaGestionDeVins.LesCommandes.Remove(commande.Commande);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppr de la commande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void dgCommandesModifier(object sender, RoutedEventArgs e)
        {
            // dtcmdl = detail commande list
            List<DetailCommande> dtcmdl = dgCommandes.SelectedItems.Cast<DetailCommande>().ToList();
            if (dtcmdl.Count > 1)
            {
                MessageBox.Show("cant edit multiple at once");
                return;
            }

            DetailCommande dtcmd = dtcmdl[0];
            Window window = new Window();
            ModifierCommandeUC modifierCommandeUC = new ModifierCommandeUC(ref dtcmd);
            
            window.Content = modifierCommandeUC;
            window.Width = 480;
            window.Height = 600;
            window.Title = "Modifier Commande";
            window.Show();

            if (dgCommandes != null)
                CollectionViewSource.GetDefaultView(dgCommandes.ItemsSource).Refresh();
        }
    }
}
