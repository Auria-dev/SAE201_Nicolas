using Microsoft.Ajax.Utilities;
using Npgsql.PostgresTypes;
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
            DateTime dateDebut, dateFin;
            int prixMin, prixMax;

            string motclefs = BarDeRechercheDemandes.Text.ToLower();
            recherche = d.NomVin.ToLower().Contains(motclefs) 
                     || d.NomEmploye.ToLower().Contains(motclefs)
                     || d.NumDemande.ToString().ToLower().Contains(motclefs) 
                     || d.NumCommande.ToString().ToLower().Contains(motclefs) 
                     || d.QuantiteDemande.ToString().ToLower().Contains(motclefs);

            if (int.TryParse(TextboxFiltrePrixMin.Text, out prixMin)) filtrePrixMin = (d.PrixDemande >= prixMin);
            if (int.TryParse(TextboxFiltrePrixMax.Text, out prixMax)) filtrePrixMax = (d.PrixDemande <= prixMax);
            if (DateTime.TryParse(TxtboxDateDebut.Text, out dateDebut)) filtreDateDebut = d.DateDemande >= dateDebut;
            if (DateTime.TryParse(TxtboxDateFin.Text, out dateFin)) filtreDateFin = d.DateDemande <= dateFin;
            if (ComboxBoxEtat.SelectedIndex != 0) filtreEtat = ComboxBoxEtat.SelectedIndex == d.EtatDemandeToInt(d.EtatDemande);

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


        private void commanderDemande(object sender, RoutedEventArgs e)
        {
            List<Demande> demandes = dgDemandes.SelectedItems.Cast<Demande>().ToList();

            bool isValid = true;
            int qteTotal = 0;
            string nomVinVerif = "";
            int numFournisseurVerif = -1;

            foreach (Demande de in demandes)
            {
                int deNumFournisseur = MainWindow.LaGestionDeVins.LesVins.SingleOrDefault(w => w.NumVin == de.NumVin).NumFournisseur;
                if (numFournisseurVerif == -1) numFournisseurVerif = deNumFournisseur;
                else if (numFournisseurVerif != deNumFournisseur) {
                    MessageBox.Show("Impossible de commander plusieurs vins de fournisseurs differents.", "Erreur lors de la creation de la commande", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }

                if (nomVinVerif.IsNullOrWhiteSpace()) nomVinVerif = de.NomVin;
                else if (nomVinVerif != de.NomVin && numFournisseurVerif != deNumFournisseur)
                {
                    MessageBox.Show("Impossible de commander plusieurs vins differents.", "Erreur lors de la creation de la commande", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }

                if (de.EtatDemande != EnumEtatCommande.EnAttante)
                {
                    // checking for isvalid cause we dont wanna show 10 errors to the user
                    if (isValid) MessageBox.Show("Impossible de commander une demande déjà validé ou supprimer.", "Erreur lors de la creation de la commande", MessageBoxButton.OK, MessageBoxImage.Error);
                    isValid = false;
                    break;
                }

                qteTotal += de.QuantiteDemande;
            }

            // checking for isvalid cause we dont wanna show 10 errors to the user
            if (qteTotal > 100 && isValid)
            {
                MessageBox.Show("Une demande ne dois pas contenire plus de 100 vins.", "Erreur lors de la creation de la commande", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (isValid)
            {
                foreach (Demande de in demandes)
                {
                    // TODO: change the 1 to an actual employe ID when we have a proper connection system 
                    int numCommande = MainWindow.LaGestionDeVins.LesCommandes.OrderByDescending(w => w.NumCommande).First().NumCommande + 1;
                    Commande newCommande = new Commande(numCommande, 1, DateTime.Now, "Validée", de.PrixDemande);
                    DetailCommande newDetail = new DetailCommande(numCommande, de.NumVin, qteTotal, de.PrixDemande, MainWindow.LaGestionDeVins);

                    try
                    {
                        de.UpdateDemande();
                        newCommande.AjouterCommande();
                        newDetail.AjouterDetailCommande();

                        MainWindow.LaGestionDeVins.LesCommandes.Add(newCommande);
                        MainWindow.LaGestionDeVins.LesDetailsCommandes.Add(newDetail);

                        de.EtatDemande = EnumEtatCommande.Validée;
                        if (dgDemandes != null)
                            { CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh(); }

                        MessageBox.Show("Votre commande a bien été enregistrée.", "Commande effectuée", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'insertion de la demande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void supprimerDemande(object sender, RoutedEventArgs e)
        {
            List<Demande> demandes = dgDemandes.SelectedItems.Cast<Demande>().ToList();
            
            MessageBoxResult supprimerResult = MessageBox.Show(
                    $"Etes vous sur de vouloir supprimmer ?",
                    "Supprimmer",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning
            );

            if (supprimerResult == MessageBoxResult.Yes)
            {
                foreach (Demande de in demandes)
                {
                    try
                    {
                        de.EtatDemande = EnumEtatCommande.Supprimée;
                        de.UpdateDemande();

                        if (dgDemandes != null)
                            CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la mis a jour de la demande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        Console.WriteLine(ex.Message);
                    }
                }

                //Demande demandeSelectionne = (Demande)dgDemandes.SelectedItem;
                //Demande copie = new Demande(
                //    demandeSelectionne.NumDemande, 
                //    demandeSelectionne.NumVin, 
                //    demandeSelectionne.NumEmploye, 
                //    demandeSelectionne.NumCommande, 
                //    demandeSelectionne.DateDemande, 
                //    demandeSelectionne.QuantiteDemande, 
                //    demandeSelectionne.EtatDemande
                //    );
            }
        }
    }
}
