using SAE201_Nicolas.Dialog;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE201_Nicolas.View
{
    /// <summary>
    /// Logique d'interaction pour EspacePersonnel.xaml
    /// </summary>
    public partial class EspacePersonnel : UserControl
    {
        public EspacePersonnel()
        {
            InitializeComponent();
            UpdateDataContext();
            //this.DataContext = ConnectionWindow.EmployeActuel;
        }
        private void UpdateDataContext()
        {
            this.DataContext = ConnectionWindow.EmployeActuel;
        }

        // Ajoute cette méthode publique
        public void RefreshEmploye()
        {
            UpdateDataContext();
        }

        private void butConfirmer_Click(object sender, RoutedEventArgs e)
        {
            Employe vinSelectionne = ConnectionWindow.EmployeActuel;
            Employe copie = new Employe(vinSelectionne.NumEmploye, vinSelectionne.NumEmploye, vinSelectionne.Nom, vinSelectionne.Prenom, vinSelectionne.Login, vinSelectionne.Mdp);

            try
            {
                copie.UpdateEmploye();
                vinSelectionne.NumEmploye = copie.NumEmploye;
                vinSelectionne.Nom = copie.Nom;
                vinSelectionne.Prenom = copie.Prenom;
                vinSelectionne.Login = copie.Login;
                vinSelectionne.Mdp = copie.Mdp;
                MessageBox.Show("Vos informations personnelles ont été enregistrés.", "Données enregistrées", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Les infos n'ont pas pu être modifié.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
