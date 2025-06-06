using SAE201_Nicolas.Core;
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

namespace SAE201_Nicolas.MVVM.View.UC
{
    /// <summary>
    /// Logique d'interaction pour AjouterVinUC.xaml
    /// </summary>
    public partial class AjouterVinUC : UserControl
    {
        public AjouterVinUC()
        {
            InitializeComponent();
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewSwitcher.GoBack();
        }
    }
}
