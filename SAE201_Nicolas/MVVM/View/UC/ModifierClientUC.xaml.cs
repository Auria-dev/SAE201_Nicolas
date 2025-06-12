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

namespace SAE201_Nicolas.MVVM.View.UC
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
    }
}
