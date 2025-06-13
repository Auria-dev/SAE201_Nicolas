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
using System.Windows.Shapes;

namespace SAE201_Nicolas.Dialog
{
    /// <summary>
    /// Logique d'interaction pour ModifierCommandeWindow.xaml
    /// </summary>
    public partial class ModifierCommandeWindow : Window
    {
        private DetailCommande dtcmd;
        
        public ModifierCommandeWindow()
        {
            InitializeComponent();
        }

        public ModifierCommandeWindow(ref DetailCommande dtcmd)
        {
            InitializeComponent();
            this.DataContext = dtcmd;

            this.dtcmd = dtcmd;

            foreach (ComboBoxItem item in cbFournisseur.Items) {
                if ((string)item.Content == dtcmd.FournisseurVin) {
                    cbFournisseur.SelectedItem = item;
                    break;
                }
            }

            txtBoxQteVin.Text = this.dtcmd.Quantite.ToString();
            txtBoxPrix.Text = (this.dtcmd.Vin.PrixVin * dtcmd.Quantite).ToString();
        }

        private void updatePrix(object sender, TextChangedEventArgs e)
        {
            int nQte;

            if (int.TryParse(this.txtBoxQteVin.Text, out nQte))
            {
                dtcmd.Quantite = nQte;
                dtcmd.Prix = (this.dtcmd.Vin.PrixVin * this.dtcmd.Quantite);
                txtBoxPrix.Text = dtcmd.Prix.ToString();
            }
        }

        private void confirmerModifClick(object sender, RoutedEventArgs e)
        {
            if (dtcmd == null) return;
            if (Validation.GetHasError(txtBoxQteVin))
                return;

            // nouvelle quantité
            int nQte;
            if (int.TryParse(this.txtBoxQteVin.Text, out nQte))
            {
                dtcmd.Quantite = nQte;
                dtcmd.Prix = (this.dtcmd.Vin.PrixVin * this.dtcmd.Quantite);
                txtBoxPrix.Text = dtcmd.Prix.ToString();
            }

            // nouveau fournisseur
            dtcmd.Fournisseur.NomFournisseur = cbFournisseur.Text;

            Window.GetWindow(this).Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeEllipse_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseEllipse_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
