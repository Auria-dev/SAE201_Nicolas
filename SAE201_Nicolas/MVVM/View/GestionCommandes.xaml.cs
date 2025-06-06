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
    /// Logique d'interaction pour GestionCommandes.xaml
    /// </summary>
    public partial class GestionCommandes : UserControl
    {
        public GestionVin LaGestionDeVins { get; set; }
        public GestionCommandes()
        {
            ChargeData();
            InitializeComponent();
        }
        public void ChargeData()
        {
            try
            {
                LaGestionDeVins = new GestionVin("Gestion Vins");
                this.DataContext = LaGestionDeVins;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin");
                Application.Current.Shutdown();
            }
        }
    }
}
