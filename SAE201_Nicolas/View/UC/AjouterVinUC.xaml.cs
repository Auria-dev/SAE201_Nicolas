﻿using SAE201_Nicolas.Core;
using SAE201_Nicolas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace SAE201_Nicolas.View.UC
{
    /// <summary>
    /// Logique d'interaction pour AjouterVinUC.xaml
    /// </summary>
    public partial class AjouterVinUC : UserControl
    {
        public AjouterVinUC()
        {
            InitializeComponent();
            cbFournisseur.SelectedIndex = 1;
            cbTypeVin.SelectedIndex = 1;
            ComboxBoxAppellation.SelectedIndex = 1;
        }

        public AjouterVinUC(Vin unVin)
        {
            InitializeComponent();
            this.DataContext = unVin;
            cbFournisseur.SelectedIndex = 1;
            cbTypeVin.SelectedIndex = 1;
            ComboxBoxAppellation.SelectedIndex = 1;
        }

        private void ClickedReturn(object sender, MouseButtonEventArgs e)
        {
            ViewManager.Instance.GoBack();
        }

        private void BtnAjouterVinValider(object sender, RoutedEventArgs e)
        {
            int a;
            double p;
            string n;
            
            if (string.IsNullOrEmpty(TxtboxNomVin.Text)) {
                MessageBox.Show("Nom invalide. Impossible de créer le vin", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            
            if (!int.TryParse(TxtboxAnnee.Text, out a))
            {
                MessageBox.Show("Année invalide. Impossible de créer le vin", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(TxtboxPrixVin.Text, out p))
            {
                MessageBox.Show("Prix invalide. Impossible de créer le vin", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Vin unVin = new Vin();
            unVin.NumFournisseur = cbFournisseur.SelectedIndex;
            unVin.NumTypeVin = cbTypeVin.SelectedIndex;
            unVin.NumAppelation = ComboxBoxAppellation.SelectedIndex;
            if (String.IsNullOrEmpty(TxtboxNomVin.Text))
            {
                MessageBox.Show("Nom invalide. Impossible de créer le vin.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            unVin.NomVin = TxtboxNomVin.Text;
            unVin.PrixVin = p;
            unVin.Descriptif = "";
            unVin.Annee = a;
            unVin.NumVin = unVin.AjouterVin();

            MainWindow.LaGestionDeVins.LesVins.Add(unVin);

            MessageBox.Show("Vin enregistré.", $"Insertion du nouveau vin réussite.", MessageBoxButton.OK, MessageBoxImage.Information);
            //ViewManager.Instance.RequestMainContentChange(nameof(Ajouter));
            cbFournisseur.SelectedIndex = 1;
            cbTypeVin.SelectedIndex = 1;
            ComboxBoxAppellation.SelectedIndex = 1;
            TxtboxAnnee.Text = "";
            TxtboxNomVin.Text = "";
            TxtboxPrixVin.Text = "";
        }
    }
}