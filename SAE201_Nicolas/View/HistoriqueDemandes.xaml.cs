using Microsoft.Ajax.Utilities;
using Npgsql.PostgresTypes;
using SAE201_Nicolas.Model;
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
    /// Logique d'interaction pour HistoriqueCommandes.xaml
    /// </summary>
    public partial class HistoriqueCommandes : UserControl
    {
        private ListCollectionView listCommandes;

        public HistoriqueCommandes()
        {
            InitializeComponent();
            
            if (MainWindow.EmployeActuel.RoleEmploye == Role.Vendeur)
            {
                spBtns.Height = 0;
                butSupDemande.IsEnabled = false;
                butCommanderDemande.IsEnabled = false;
                dgDemandes.Height = 375;
            } 
            else
            {
                spBtns.Height = 42;
                butSupDemande.IsEnabled = true;
                butCommanderDemande.IsEnabled = true;
                dgDemandes.Height = 320;

            }

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
            if (!demandes.Any()) {
                MessageBox.Show("Veuillez sélectionner au moins une demande à commander.", "Aucune sélection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (demandes.Any(de => de.EtatDemande != EnumEtatCommande.EnAttante)) {
                MessageBox.Show("Impossible de commander une demande qui n'est pas en attente (déjà validée ou supprimée).", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Vin premierVin = MainWindow.LaGestionDeVins.LesVins.FirstOrDefault(v => v.NumVin == demandes.First().NumVin);
            if (premierVin == null) {
                MessageBox.Show("Le vin associé à la première demande est introuvable.", "Erreur de données", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int numFournisseurVerif = premierVin.NumFournisseur;
            bool tousMemeFournisseurs = demandes.All(de => {
                Vin vinActuel = MainWindow.LaGestionDeVins.LesVins.FirstOrDefault(v => v.NumVin == de.NumVin);
                return vinActuel != null && vinActuel.NumFournisseur == numFournisseurVerif;
            });

            if (!tousMemeFournisseurs) {
                MessageBox.Show("Impossible de créer une commande pour des vins de fournisseurs différents.", "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var mergedDetails = demandes.GroupBy(de => de.NumVin)
                                        .Select(group => new {
                                            NumVin         = group.Key,
                                            QuantiteTotale = group.Sum(d => d.QuantiteDemande),
                                            PrixTotal      = group.Sum(d => d.PrixDemande)
                                        }).ToList();

            int qteTotaleCommande = mergedDetails.Sum(d => d.QuantiteTotale);
            if (qteTotaleCommande > 100) {
                MessageBox.Show($"Une commande ne doit pas contenir plus de 100 bouteilles au total. Quantité actuelle: {qteTotaleCommande}.", "Limite de quantité dépassée", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int numCommande = 1;
                if (MainWindow.LaGestionDeVins.LesCommandes.Any())
                    numCommande = MainWindow.LaGestionDeVins.LesCommandes.Max(c => c.NumCommande) + 1;
                

                int prixTotalCommande = mergedDetails.Sum(d => d.PrixTotal);
                Commande newCommande = new Commande(numCommande, MainWindow.EmployeActuel.NumEmploye, DateTime.Now, "Validée", prixTotalCommande);

                newCommande.AjouterCommande();
                MainWindow.LaGestionDeVins.LesCommandes.Add(newCommande);

                foreach (var detailData in mergedDetails) {
                    DetailCommande newDetail = new DetailCommande(numCommande, detailData.NumVin, detailData.QuantiteTotale, detailData.PrixTotal, MainWindow.LaGestionDeVins);
                    newDetail.AjouterDetailCommande();
                    MainWindow.LaGestionDeVins.LesDetailsCommandes.Add(newDetail);
                }

                foreach (Demande de in demandes) {
                    de.EtatDemande = EnumEtatCommande.Validée;
                    de.UpdateDemande();
                }

                if (dgDemandes?.ItemsSource != null)
                    CollectionViewSource.GetDefaultView(dgDemandes.ItemsSource).Refresh();
                

                MessageBox.Show("Votre commande a bien été enregistrée.", "Commande Effectuée", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception ex) {
                MessageBox.Show("Une erreur est survenue lors de la création de la commande.", "Erreur d'insertion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void supprimerDemande(object sender, RoutedEventArgs e)
        {
            List<Demande> demandes = dgDemandes.SelectedItems.Cast<Demande>().ToList();
               
            MessageBoxResult supprimerResult = MessageBox.Show(
                    $"Etes vous sur de vouloir supprimer {(demandes.Count > 1 ? "ces" : "cette")} commande{(demandes.Count > 1 ? "s" : "")} ?",
                    "Supprimer",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
            );

            if (supprimerResult == MessageBoxResult.Yes)
            {

                foreach (Demande de in demandes) if (de.EtatDemande == EnumEtatCommande.Validée)
                {
                        MessageBox.Show("Impossible de supprimer des demandes desja validée.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
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
            }
        }
    }
}
