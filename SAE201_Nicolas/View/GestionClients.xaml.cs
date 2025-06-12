using SAE201_Nicolas.Core;
using SAE201_Nicolas.View.UC;
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

namespace SAE201_Nicolas.View
{
    /// <summary>
    /// Logique d'interaction pour GestionClients.xaml
    /// </summary>
    public partial class GestionClients : UserControl
    {
        public GestionClients()
        {
            InitializeComponent();
        }

        private void btnModifierClient(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(ModifierClientUC));
        }

        private void btnAjouterClient(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(AjouterClientUC));
        }
    }
}
