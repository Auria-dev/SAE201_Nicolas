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
        }
    }
}
