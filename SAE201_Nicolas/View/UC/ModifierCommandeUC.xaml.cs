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
    /// Logique d'interaction pour ModifierCommandeUC.xaml
    /// </summary>
    public partial class ModifierCommandeUC : UserControl
    {
        private DetailCommande dtcmd;

        public ModifierCommandeUC()
        {
            InitializeComponent();
        }

        public ModifierCommandeUC(ref DetailCommande dtcmd)
        {
            InitializeComponent();
            this.dtcmd = dtcmd;

            foreach (ComboBoxItem item in cbFournisseur.Items) {
                if ((string)item.Content == this.dtcmd.Fournisseur.NomFournisseur) {
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
                txtBoxPrix.Text = (this.dtcmd.Vin.PrixVin * this.dtcmd.Quantite).ToString();
            }
        }

        private void confirmerModifClick(object sender, RoutedEventArgs e)
        {
            if (dtcmd == null) return;

            // nouvelle quantité
            int nQte;
            if (int.TryParse(this.txtBoxQteVin.Text, out nQte))
            {
                dtcmd.Quantite = nQte;
                txtBoxPrix.Text = (this.dtcmd.Vin.PrixVin * this.dtcmd.Quantite).ToString();
            }

            // nouveau fournisseur
            dtcmd.Fournisseur.NomFournisseur = cbFournisseur.Text;

            Window.GetWindow(this).Close();
        }
    }
}