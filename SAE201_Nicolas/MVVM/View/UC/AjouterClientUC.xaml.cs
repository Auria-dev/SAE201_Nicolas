using SAE201_Nicolas.Core;
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
    /// Logique d'interaction pour AjouterClientUC.xaml
    /// </summary>
    public partial class AjouterClientUC : UserControl
    {
        public AjouterClientUC()
        {
            InitializeComponent();
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewManager.Instance.GoBack(null);
        }

        private void butAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            Client unClient = new Client();
            unClient.NomClient = TxtboxNomClient.Text;
            unClient.PrenomClient = TxtboxPrenomClient.Text;
            unClient.MailClient = TxtboxMailClient.Text;

            unClient.NumClient = unClient.AjouterClient();

            MainWindow.LaGestionDeVins.LesClients.Add(unClient);
        }
    }
}
