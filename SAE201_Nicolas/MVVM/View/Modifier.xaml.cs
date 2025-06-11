using SAE201_Nicolas.Core;
using SAE201_Nicolas.MVVM.View.UC;
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
    /// Logique d'interaction pour Modifier.xaml
    /// </summary>
    public partial class Modifier : UserControl
    {
        public Modifier()
        {
            InitializeComponent();
        }

        private void BtnModifierVinClick(object sender, RoutedEventArgs e)
        {
            //ViewManager.Instance.RequestMainContentChange(null, nameof(ModifierVinUC));
        }

        private void BtnModifierClientClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(null, nameof(ModifierClientUC));
        }

        private void BtnModifierFournisseurClick(object sender, RoutedEventArgs e)
        {
            //ViewManager.Instance.RequestMainContentChange(null, nameof(ModifierFournisseurUC));
        }
    }
}
