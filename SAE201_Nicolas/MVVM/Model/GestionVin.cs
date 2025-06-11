using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public class GestionVin
    {
        private string nom;
        private ObservableCollection<Vin> lesVins;
        private ObservableCollection<Demande> lesDemandes;
        private ObservableCollection<Commande> lesCommandes;
        private ObservableCollection<Fournisseur> lesFournisseurs;
        private ObservableCollection<DetailCommande> lesDetailsCommandes;
        private ObservableCollection<Employe> lesEmployes;
        private ObservableCollection<Client> lesClients;

        public GestionVin(string nom)
        {
            this.Nom = nom;
            this.lesVins = new ObservableCollection<Vin>(new Vin().FindAll());
            this.lesDemandes = new ObservableCollection<Demande>(new Demande().FindAll());
            this.lesCommandes = new ObservableCollection<Commande>(new Commande().FindAll());
            this.lesFournisseurs = new ObservableCollection<Fournisseur>(new Fournisseur().FindAll());
            this.lesDetailsCommandes = new ObservableCollection<DetailCommande>(new DetailCommande().FindAll(this));
            this.lesEmployes = new ObservableCollection<Employe>(new Employe().FindAll());
            this.lesClients = new ObservableCollection<Client>(new Client().FindAll());

            Console.WriteLine(
                "Found:\n" +
                this.lesEmployes.Count + " employes\n"             +
                this.lesVins.Count + " vins\n"             +
                this.lesDemandes.Count + " demandes\n"         +
                this.lesCommandes.Count + " commandes\n"        +
                this.lesFournisseurs.Count + " fournisseurs\n"     +
                this.lesDetailsCommandes.Count + " detailscommandes\n" 
            );
        }

        public GestionVin() : this("") { }

        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }

        public ObservableCollection<Vin> LesVins
        {
            get { return this.lesVins; }
            set { this.lesVins = value; }
        }

        public ObservableCollection<Demande> LesDemandes
        {
            get { return this.lesDemandes; }
            set { this.lesDemandes = value; }
        }

        public ObservableCollection<Commande> LesCommandes
        {
            get { return this.lesCommandes; }
            set { this.lesCommandes = value; }
        }

        public ObservableCollection<Fournisseur> LesFournisseurs
        {
            get { return this.lesFournisseurs; }
            set { this.lesFournisseurs = value; }
        }

        public ObservableCollection<DetailCommande> LesDetailsCommandes
        {
            get { return this.lesDetailsCommandes; }
            set { this.lesDetailsCommandes = value; }
        }

        public ObservableCollection<Employe> LesEmployes
        {
            get { return this.lesEmployes; }
            set { this.lesEmployes = value; }
        }

        public ObservableCollection<Client> LesClients
        {
            get
            {
                return this.lesClients;
            }

            set
            {
                this.lesClients = value;
            }
        }
    }
}
