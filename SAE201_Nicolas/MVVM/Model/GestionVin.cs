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
        private ObservableCollection<DetailCommande> lesDetailsCommandes;

        public GestionVin(string nom)
        {
            this.Nom = nom;
            this.lesVins = new ObservableCollection<Vin>(new Vin().FindAll());
            this.lesCommandes = new ObservableCollection<Commande>(new Commande().FindAll());
            this.lesDetailsCommandes = new ObservableCollection<DetailCommande>(new DetailCommande().FindAll());
        }

        public GestionVin() : this("")
        {
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public ObservableCollection<Vin> LesVins
        {
            get
            {
                return this.lesVins;
            }

            set
            {
                this.lesVins = value;
            }
        }

        public ObservableCollection<Demande> LesDemandes
        {
            get
            {
                return this.lesDemandes;
            }

            set
            {
                this.lesDemandes = value;
            }
        }

        public ObservableCollection<Commande> LesCommandes
        {
            get
            {
                return this.lesCommandes;
            }

            set
            {
                this.lesCommandes = value;
            }
        }

        public ObservableCollection<DetailCommande> LesDetailsCommandes
        {
            get
            {
                return this.lesDetailsCommandes;
            }

            set
            {
                this.lesDetailsCommandes = value;
            }
        }
    }
}
