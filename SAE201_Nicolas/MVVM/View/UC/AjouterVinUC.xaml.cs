using SAE201_Nicolas.Core;
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

        public AjouterVinUC(Vin unVin)
        {
            InitializeComponent();
            this.DataContext = unVin;
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewManager.Instance.GoBack(null);
        }

        private void BtnAjouterVinValider(object sender, RoutedEventArgs e)
        {
            Vin unVin = new Vin();
            unVin.NumFournisseur = cbFournisseur.SelectedIndex;
            unVin.NumTypeVin = cbTypeVin.SelectedIndex;
            unVin.NumAppelation = ComboxBoxAppellation.SelectedIndex;
            unVin.NomVin = TxtboxNomVin.Text;
            unVin.PrixVin = double.Parse(TxtboxPrixVin.Text);
            unVin.Descriptif = "";
            unVin.Annee = int.Parse(TxtboxAnnee.Text);
            
            unVin.NumVin = unVin.AjouterVin();

            //Console.WriteLine("ComboxBoxAppellation = " + ComboxBoxAppellation.SelectedIndex);
            //Console.WriteLine("cbFournisseur = " + cbFournisseur.SelectedIndex);
            //Console.WriteLine("cbTypeVin = " + cbTypeVin.SelectedIndex);

            Console.WriteLine("Num avant: " + MainWindow.LaGestionDeVins.LesVins.Count);
            MainWindow.LaGestionDeVins.LesVins.Add(unVin);
            Console.WriteLine("Num apres: " + MainWindow.LaGestionDeVins.LesVins.Count);
        }
    }
}
