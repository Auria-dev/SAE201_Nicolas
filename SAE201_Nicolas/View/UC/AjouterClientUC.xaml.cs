using SAE201_Nicolas.Core;
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

namespace SAE201_Nicolas.View.UC
{
    /// <summary>
    /// Logique d'interaction pour AjouterClientUC.xaml
    /// </summary>
    public partial class AjouterClientUC : UserControl
    {
        public AjouterClientUC()
        {
            InitializeComponent();
            this.DataContext = new Client();
        }

        public AjouterClientUC(Client unClient)
        {
            InitializeComponent();
            this.DataContext = unClient;
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewManager.Instance.GoBack();
        }

        private void butAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in spFicheClient.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie)) ok = false;
            }

            if (!ok)
            {
                return;
            }

            Client unClient = new Client();
            unClient.NomClient = TxtboxNomClient.Text;
            unClient.PrenomClient = TxtboxPrenomClient.Text;
            unClient.MailClient = TxtboxMailClient.Text;

            unClient.NumClient = unClient.AjouterClient();

            MainWindow.LaGestionDeVins.LesClients.Add(unClient);

            MessageBox.Show($"Création du client {unClient.PrenomClient} {unClient.NomClient} réussite.", "Création du client.", MessageBoxButton.OK, MessageBoxImage.Information);
            //ViewManager.Instance.RequestMainContentChange(nameof(Ajouter));
            TxtboxNomClient.Text = "";
            TxtboxPrenomClient.Text = "";
            TxtboxMailClient.Text = "";
        }
    }
}
