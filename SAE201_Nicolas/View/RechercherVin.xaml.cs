using SAE201_Nicolas.Dialog;
using SAE201_Nicolas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
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
    /// Logique d'interaction pour RechercherVin.xaml
    /// </summary>
    public partial class RechercherVin : UserControl
    {
        private ListCollectionView listVins;
        
        public RechercherVin()
        {
            InitializeComponent();
            listVins = new ListCollectionView(MainWindow.LaGestionDeVins.LesVins);
            dgVins.ItemsSource = listVins;
            listVins.Filter = FiltrerVins;
        }

        private bool FiltrerVins(object obj)
        {
            Vin unVin = (Vin)obj;
            bool verifType = true;
            bool verifPrixMin = true;
            bool verifPrixMax = true;
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
            if (int.TryParse(FiltrePrixMin.Text, out prixMin)) 
                verifPrixMin = unVin.PrixVin >= prixMin;
            
            if (int.TryParse(FiltrePrixMax.Text, out prixMax)) 
                verifPrixMax = unVin.PrixVin <= prixMax;
            
            // filtre appellation
            if (ComboxBoxAppellation.SelectedIndex != 0)
                verifAppellation = (unVin.NumAppelation == ComboxBoxAppellation.SelectedIndex);

            // rechercher 
            string motclefs = barDeRechercheVins.Text.ToLower();
            if (!string.IsNullOrEmpty(motclefs) && !string.IsNullOrWhiteSpace(motclefs))
                rechercherVin = unVin.NomVin.ToLower().Contains(motclefs) || unVin.Annee.ToString().ToLower().Contains(motclefs);

            return verifType && verifPrixMin && verifPrixMax && verifAppellation && rechercherVin;
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

        private void btnDemanderClick(object sender, RoutedEventArgs e)
        {
            int qte = 0;
            if (!int.TryParse(TxtboxQuantiteVins.Text, out qte))
            {
                MessageBox.Show("Selectionner un vin et indiqué une quantité.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (qte > 100)
            {
                MessageBox.Show("la quantité doit être inférieure à 100.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TxtboxQuantiteVins.Text = "";

            int numCommande = MainWindow.LaGestionDeVins.LesCommandes.OrderBy(w => w.NumCommande).Last().NumCommande;
            Demande uneDemande = new Demande();
            uneDemande.NumVin = ((Vin)dgVins.SelectedItem).NumVin;
            uneDemande.NumEmploye = 1;
            uneDemande.NumCommande = numCommande;
            uneDemande.DateDemande = DateTime.Now;
            uneDemande.QuantiteDemande = qte;
            uneDemande.EtatDemande = EnumEtatCommande.EnAttante;
            uneDemande.NumClient = 1;

            uneDemande.NumDemande = uneDemande.AjouterDemande();
            MainWindow.LaGestionDeVins.LesDemandes.Add(uneDemande);

            MessageBox.Show("Votre demande a été enregistrée", "Demande sauvegardée", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void butModifierVin_Click(object sender, RoutedEventArgs e)
        {
            if (dgVins.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Vin vinSelectionne = (Vin)dgVins.SelectedItem;
                Vin copie = new Vin(vinSelectionne.NumVin, vinSelectionne.NomVin, vinSelectionne.PrixVin, vinSelectionne.Descriptif, vinSelectionne.Annee);

                ModifierVinWindow winVin = new ModifierVinWindow(copie);
                bool? result = winVin.ShowDialog();

                if (result == true)
                {
                    try
                    {
                        copie.UpdateVin();
                        vinSelectionne.NumVin = copie.NumVin;
                        vinSelectionne.NomVin = copie.NomVin;
                        vinSelectionne.PrixVin = copie.PrixVin;
                        vinSelectionne.Descriptif = copie.Descriptif;
                        vinSelectionne.Annee = copie.Annee;
                        dgVins.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le vin n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}