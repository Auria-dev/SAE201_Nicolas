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

    }
}
