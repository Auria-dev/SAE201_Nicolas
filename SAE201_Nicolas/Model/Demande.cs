using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAE201_Nicolas.Model;

namespace SAE201_Nicolas.Model
{
    public class Demande
    {
        private int numDemande;
        private int numVin;
        private int numEmploye;
        private int numCommande;
        private int numClient;
        private DateTime dateDemande;
        private int quantiteDemande;
        private EtatCommande etatDemande;

        public int NumDemande
        {
            get
            {
                return this.numDemande;
            }

            set
            {
                this.numDemande = value;
            }
        }

        public int NumVin
        {
            get
            {
                return this.numVin;
            }

            set
            {
                this.numVin = value;
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

        public int NumClient
        {
            get
            {
                return this.numClient;
            }

            set
            {
                this.numClient = value;
            }
        }

        public DateTime DateDemande
        {
            get
            {
                return this.dateDemande;
            }

            set
            {
                this.dateDemande = value;
            }
        }

        public int QuantiteDemande
        {
            get
            {
                return this.quantiteDemande;
            }

            set
            {
                this.quantiteDemande = value;
            }
        }

        public EtatCommande EtatDemande
        {
            get
            {
                return this.etatDemande;
            }

            set
            {
                this.etatDemande = value;
            }
        }
    }
}
