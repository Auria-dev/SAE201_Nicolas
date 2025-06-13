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
    /// Logique d'interaction pour ModifierVinWindow.xaml
    /// </summary>
    public partial class ModifierVinWindow : Window
    {
        public ModifierVinWindow()
        {
            InitializeComponent();
        }

        public ModifierVinWindow(Vin unVin)
        {
            InitializeComponent();
            this.DataContext = unVin;
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

        private void butValider_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormVin.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie))
                {
                    ok = false;
                    MessageBox.Show(this, "Les informations renseigner sont invalide. Impossible de créer le vin.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (Validation.GetHasError(TxtboxPrixVin))
            {
                ok = false;
            }

            if (ok) DialogResult = true;
            else
            {
                MessageBox.Show(this, "Les informations renseigner sont invalide. Impossible de créer le vin.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
