using SAE201_Nicolas.Core;
using SAE201_Nicolas.Model;
using SAE201_Nicolas.View;
using SAE201_Nicolas.View.UC;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE201_Nicolas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static GestionVin LaGestionDeVins { get; set; }

        public MainWindow()
        {
            ChargeData();
            InitializeComponent();

            ViewManager.Instance.OnMainContentChangeRequested += SetMainContent;

            ViewManager.Instance.RegisterView<RechercherVin>(nameof(RechercherVin));
            ViewManager.Instance.RegisterView<HistoriqueCommandes>(nameof(HistoriqueCommandes));
            ViewManager.Instance.RegisterView<GestionCommandes>(nameof(GestionCommandes));
            ViewManager.Instance.RegisterView<EspacePersonnel>(nameof(EspacePersonnel));
            ViewManager.Instance.RegisterView<Modifier>(nameof(Modifier));
            //ViewManager.Instance.RegisterView<ModifierVinUC>(nameof(ModifierVinUC));
            ViewManager.Instance.RegisterView<ModifierClientUC>(nameof(ModifierClientUC));
            //ViewManager.Instance.RegisterView<ModifierFournisseurUC>(nameof(ModifierFournisseurUC));

            ViewManager.Instance.RegisterView<Ajouter>(nameof(Ajouter));
            ViewManager.Instance.RegisterView<AjouterVinUC>(nameof(AjouterVinUC));
            ViewManager.Instance.RegisterView<AjouterClientUC>(nameof(AjouterClientUC));
            ViewManager.Instance.RegisterView<AjouterFournisseurUC>(nameof(AjouterFournisseurUC));

            ViewManager.Instance.RequestMainContentChange(nameof(RechercherVin));
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
                MessageBox.Show("Problème lors de récupération des données, veuillez consulter votre admin\n" + ex);
                Application.Current.Shutdown();
            }
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

        private void butDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult deconnexion = MessageBox.Show(
                this, 
                "Etes-vous sur de vous déconnecter ?", 
                "Déconexion",
                MessageBoxButton.YesNo, 
                MessageBoxImage.Warning
                );
            if (deconnexion == MessageBoxResult.Yes)
            {
                this.Owner.Show();
                this.Hide();
            }
        }

        private void SetMainContent(string viewName)
        {
            UserControl view = ViewManager.Instance.GetView(viewName);
            MainContentControl.Content = view;
        }

        private void RechercherVinClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(RechercherVin));
        }

        private void ModifierClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(Modifier));
        }

        private void HistoriqueCommandesClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(HistoriqueCommandes));
        }

        private void GestionCommandesClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(GestionCommandes));
        }

        private void EspacePersonnelClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(EspacePersonnel));
        }

        private void AjouterClick(object sender, RoutedEventArgs e)
        {
            ViewManager.Instance.RequestMainContentChange(nameof(Ajouter));
        }
    }
}