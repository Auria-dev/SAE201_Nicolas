using SAE201_Nicolas.Core;
using SAE201_Nicolas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Logique d'interaction pour AjouterVinUC.xaml
    /// </summary>
    public partial class AjouterVinUC : UserControl
    {
        public AjouterVinUC()
        {
            InitializeComponent();
            this.DataContext = new Vin();
        }

        public AjouterVinUC(Vin unVin)
        {
            InitializeComponent();
            this.DataContext = unVin;
            TxtboxNomVin.Text = "";
            TxtboxAnnee.Text = "";
            TxtboxPrixVin.Text = "";
            cbFournisseur.SelectedIndex = 1;
            cbTypeVin.SelectedIndex = 1;
            ComboxBoxAppellation.SelectedIndex = 1;
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewManager.Instance.GoBack();
        }

        private void BtnAjouterVinValider(object sender, RoutedEventArgs e)
        {

            bool ok = true;
            foreach (UIElement uie in spAjouterVin.Children)
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
                MessageBox.Show("Les informations renseigner sont invalide. Impossible de créer le vin.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } 

            int a;
            double p;
            Vin unVin = new Vin();
            unVin.NumFournisseur = cbFournisseur.SelectedIndex;
            unVin.NumTypeVin = cbTypeVin.SelectedIndex;
            unVin.NumAppelation = ComboxBoxAppellation.SelectedIndex;
            unVin.NomVin = TxtboxNomVin.Text;
            unVin.PrixVin = double.Parse(TxtboxPrixVin.Text);
            unVin.Descriptif = "";
            unVin.Annee = int.Parse(TxtboxAnnee.Text);
            unVin.NumVin = unVin.AjouterVin();

            MainWindow.LaGestionDeVins.LesVins.Add(unVin);

            MessageBox.Show("Vin enregistré.", $"Insertion du nouveau vin réussite.", MessageBoxButton.OK, MessageBoxImage.Information);
            
            cbFournisseur.SelectedIndex = 1;
            cbTypeVin.SelectedIndex = 1;
            ComboxBoxAppellation.SelectedIndex = 1;
            TxtboxAnnee.Text = "";
            TxtboxNomVin.Text = "";
            TxtboxPrixVin.Text = "";
        }
    }
}
