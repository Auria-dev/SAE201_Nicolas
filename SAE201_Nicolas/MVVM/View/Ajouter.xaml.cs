using SAE201_Nicolas.Core;
using SAE201_Nicolas.MVVM.Model;
using SAE201_Nicolas.MVVM.View.UC;
using SAE201_Nicolas.MVVM.ViewModel;
using SAE201_Nicolas.MVVM.ViewModel.UC;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE201_Nicolas.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour Ajouter.xaml
    /// </summary>
    public partial class Ajouter : UserControl
    {

        public Ajouter()
        {
            InitializeComponent();
        }

        private void BtnAjouterVinClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.ChangeViewTo("AjouterVin");
        }

        private void BtnAjouterClientClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.ChangeViewTo("AjouterClient");
        }

        private void BtnAjouterFournisseurClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.ChangeViewTo("AjouterFournisseur");
        }
    }
}
