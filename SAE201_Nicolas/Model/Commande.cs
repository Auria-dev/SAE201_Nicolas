using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
{
    public enum EtatCommande
    {
        Validée,
        EnAttante,
        Supprimée
    }

    public class Commande
    {
        private int numCommande;
        private int numEmploye;
        private DateTime dateCommande;
        private EtatCommande etatCommande;
        private double prixTotal;

        public Commande(int numCommande, int numEmploye, DateTime dateCommande, EtatCommande etatCommande, double prixTotal)
        {
            this.NumCommande = numCommande;
            this.NumEmploye = numEmploye;
            this.DateCommande = dateCommande;
            this.EtatCommande = etatCommande;
            this.PrixTotal = prixTotal;
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public int NumEmploye
        {
            get
            {
                return this.numEmploye;
            }

            set
            {
                this.numEmploye = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public EtatCommande EtatCommande
        {
            get
            {
                return this.etatCommande;
            }

            set
            {
                this.etatCommande = value;
            }
        }

        public double PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        public void AjouterCommande() { }
        public void ModifierCommande() { }
        public void SupprimerCommande() { }
    }
}
