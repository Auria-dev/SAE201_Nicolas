using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
{
    public enum TypeVin
    {
        Blanc,
        Rouge,
        Rosé
    }

    public enum Appelation
    {
        Bourgogne, 
        Bordeaux, 
        Italien
    }

    public class Vin
    {
        private int numVin;
        private string nomVin;
        private double prixVin;
        private string descriptif;
        private int annee;

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee)
        {
            this.NumVin = numVin;
            this.NomVin = nomVin;
            this.PrixVin = prixVin;
            this.Descriptif = descriptif;
            this.Annee = annee;
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

        public string NomVin
        {
            get
            {
                return this.nomVin;
            }

            set
            {
                this.nomVin = value;
            }
        }

        public double PrixVin
        {
            get
            {
                return this.prixVin;
            }

            set
            {
                this.prixVin = value;
            }
        }

        public string Descriptif
        {
            get
            {
                return this.descriptif;
            }

            set
            {
                this.descriptif = value;
            }
        }

        public int Annee
        {
            get
            {
                return this.annee;
            }

            set
            {
                this.annee = value;
            }
        }

        public int NumTypeVinToEnum() { return 0;  }
        public TypeVin EnumToNumTypeVin() { return TypeVin.Blanc; }
        public Appelation EnumToNumAppelation() { return Appelation.Bourgogne; }
    }
}
