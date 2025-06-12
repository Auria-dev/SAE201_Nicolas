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
using System.Windows.Shapes;

namespace SAE201_Nicolas
{
    /// <summary>
    /// Logique d'interaction pour ModifierClientWindow.xaml
    /// </summary>
    public partial class ModifierClientWindow : Window
    {
        public ModifierClientWindow()
        {
            InitializeComponent();
        }
        public ModifierClientWindow(Client unClient)
        {
            InitializeComponent();
            this.DataContext = unClient;
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

        private void butAjouterClient_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            foreach (UIElement uie in panelFormClient.Children)
            {
                if (uie is TextBox)
                {
                    TextBox txt = (TextBox)uie;
                    txt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }

                if (Validation.GetHasError(uie))
                {
                    ok = false;
                    MessageBox.Show(this, "Erreurs de saisies. Impossible de créer le chien.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            DialogResult = true;
        }
    }
}
