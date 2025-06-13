using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
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
        }

        public GestionVin() : this("") { }

        public string Nom
        {
            get { return this.nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Le nom de la gestion ne peut pas être vide.");
                this.nom = value; 
            }
        }

        public ObservableCollection<Vin> LesVins
        {
            get { return this.lesVins; }
            set { this.lesVins = value ?? throw new ArgumentNullException(nameof(LesVins), "La liste des vins ne peut pas être null."); }
        }

        public ObservableCollection<Demande> LesDemandes
        {
            get { return this.lesDemandes; }
            set { this.lesDemandes = value ?? throw new ArgumentNullException(nameof(LesDemandes), "La liste des demandes ne peut pas être null."); }
        }

        public ObservableCollection<Commande> LesCommandes
        {
            get { return this.lesCommandes; }
            set { this.lesCommandes = value ?? throw new ArgumentNullException(nameof(LesCommandes), "La liste des commandes ne peut pas être null."); }
        }

        public ObservableCollection<Fournisseur> LesFournisseurs
        {
            get { return this.lesFournisseurs; }
            set { this.lesFournisseurs = value ?? throw new ArgumentNullException(nameof(LesFournisseurs), "La liste des fournisseurs ne peut pas être null."); }
        }

        public ObservableCollection<DetailCommande> LesDetailsCommandes
        {
            get { return this.lesDetailsCommandes; }
            set { this.lesDetailsCommandes = value ?? throw new ArgumentNullException(nameof(LesDetailsCommandes), "La liste des détails de commande ne peut pas être null."); }
        }

        public ObservableCollection<Employe> LesEmployes
        {
            get { return this.lesEmployes; }
            set { this.lesEmployes = value ?? throw new ArgumentNullException(nameof(LesEmployes), "La liste des employés ne peut pas être null."); }
        }

        public ObservableCollection<Client> LesClients
        {
            get { return this.lesClients; }
            set { this.lesClients = value ?? throw new ArgumentNullException(nameof(LesClients), "La liste des clients ne peut pas être null."); }
        }
    }
}
