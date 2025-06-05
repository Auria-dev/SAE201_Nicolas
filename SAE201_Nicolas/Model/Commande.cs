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
        En_attante,
        Supprimé
    }

    public class Commande
    {
        private int numCommande;
        private int numEmploye;
        private DateTime dateCommande;
        private EtatCommande etatCommande;
        private double prixTotal;

        public int NumCommande { 
            get => numCommande; 
            set => numCommande = value; 
        }

        public int NumEmploye { 
            get => numEmploye; 
            set => numEmploye = value; 
        }

        public DateTime DateCommande { 
            get => dateCommande; 
            set => dateCommande = value; 
        }

        public EtatCommande EtatCommande { 
            get => etatCommande; 
            set => etatCommande = value; 
        }

        public double PrixTotal { 
            get => prixTotal; 
            set => prixTotal = value; 
        }

        public void AjouterCommande() { }
        public void ModifierCommande() { }
        public void SupprimerCommande() { }
    }
}
