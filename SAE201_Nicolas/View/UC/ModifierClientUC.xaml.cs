using SAE201_Nicolas.Core;
using SAE201_Nicolas.Model;
using SAE201_Nicolas.Dialog;
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

namespace SAE201_Nicolas.View.UC
{
    /// <summary>
    /// Logique d'interaction pour ModifierClientUC.xaml
    /// </summary>
    public partial class ModifierClientUC : UserControl
    {
        public ModifierClientUC()
        {
            InitializeComponent();
        }

        private void butSupClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
            else
            {
                Client clientnAsupprimer = (Client)dgClients.SelectedItem;
                MessageBoxResult supprimerResult = MessageBox.Show(
                    $"Etes vous sur de vouloir supprimmer le client {clientnAsupprimer.PrenomClient} {clientnAsupprimer.NomClient} ?", 
                    "Supprimmer",
                    MessageBoxButton.YesNoCancel, 
                    MessageBoxImage.Warning
                    );
                if (supprimerResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        clientnAsupprimer.SupprimerClient();
                        MainWindow.LaGestionDeVins.LesClients.Remove(clientnAsupprimer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le client n'a pas pu être supprimé.", "Attention",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void butModifClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgClients.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un client", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Client clientSelectionne = (Client)dgClients.SelectedItem;
                Client copie = new Client(clientSelectionne.NumClient, clientSelectionne.NomClient, clientSelectionne.PrenomClient, clientSelectionne.MailClient);
                ModifierClientWindow wChien = new ModifierClientWindow(copie);
                bool? result = wChien.ShowDialog();
                if (result == true)
                {
                    try
                    {
                        copie.UpdateClient();
                        clientSelectionne.NomClient = copie.NomClient;
                        clientSelectionne.PrenomClient = copie.PrenomClient;
                        clientSelectionne.MailClient = copie.MailClient;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Le client n'a pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
