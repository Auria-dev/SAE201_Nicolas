using SAE201_Nicolas.Core;
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
        private static AjouterClientViewModel AjouterClientVM { get; set; }
        private static AjouterFournisseurViewModel AjouterFournisseurVM { get; set; }
        private static AjouterVinViewModel AjouterVinVM { get; set; }

        public Ajouter()
        {
            InitializeComponent();

            AjouterClientVM = new AjouterClientViewModel();
            AjouterFournisseurVM = new AjouterFournisseurViewModel();
            AjouterVinVM = new AjouterVinViewModel();
        }

        private void BtnAjouterVinClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.RequestViewChange(AjouterVinVM);
        }

        private void BtnAjouterClientClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.RequestViewChange(AjouterClientVM);
        }

        private void BtnAjouterFournisseurClick(object sender, RoutedEventArgs e)
        {
            ViewSwitcher.RequestViewChange(AjouterFournisseurVM);
        }
    }
}
