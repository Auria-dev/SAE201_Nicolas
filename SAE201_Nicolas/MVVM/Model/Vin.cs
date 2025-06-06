using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public enum TypeVin
    {
        Rouge,
        Blanc,
        Rosé,
        Champagne,
        Mousseux,
        Doux,
        Liquoreux
    }

    public enum Appelation
    {
        Bourgogne,
        Bordeaux,
        Champagne,
        Alsace,
        Beaujolais
    }

    public class Vin
    {
        private int numVin;
        private string nomVin;
        private double prixVin;
        private string descriptif;
        private int annee;
        private int numTypeVin;
        private int numAppelation;

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee)
        {
            this.NumVin = numVin;
            this.NomVin = nomVin;
            this.PrixVin = prixVin;
            this.Descriptif = descriptif;
            this.Annee = annee;
        }

        public Vin()
        {
        }

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee, int numTypeVin, int numAppelation) : this(numVin, nomVin, prixVin, descriptif, annee)
        {
            this.NumTypeVin = numTypeVin;
            this.NumAppelation = numAppelation;
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

        public string NomTypeVin
        {
            get
            {
                string type;
                switch (NumTypeVin)
                {
                    case 1:
                        type = TypeVin.Rouge.ToString();
                        break;
                    case 2:
                        type = TypeVin.Blanc.ToString();
                        break;
                    case 3:
                        type = TypeVin.Rosé.ToString();
                        break;
                    case 4:
                        type = TypeVin.Champagne.ToString();
                        break;
                    case 5:
                        type = TypeVin.Mousseux.ToString();
                        break;
                    case 6:
                        type = TypeVin.Doux.ToString();
                        break;
                    case 7:
                        type = TypeVin.Liquoreux.ToString();
                        break;

                    default: type = "INCONNU"; break; 
                }
                return type;
            }
        }

        public string NomAppelation
        {
            get
            {
                string appelationVin;
                switch (NumAppelation)
                {
                    case 1:
                        appelationVin = Appelation.Bordeaux.ToString();
                        break;
                    case 2:
                        appelationVin = Appelation.Bourgogne.ToString();
                        break;
                    case 3:
                        appelationVin = Appelation.Champagne.ToString();
                        break;
                    case 4:
                        appelationVin = Appelation.Alsace.ToString();
                        break;
                    case 5:
                        appelationVin = Appelation.Beaujolais.ToString();
                        break;

                    default: appelationVin = "Autre"; break;
                }
                return appelationVin;
            }
        }

        public int NumTypeVin
        {
            get
            {
                return this.numTypeVin;
            }

            set
            {
                this.numTypeVin = value;
            }
        }

        public int NumAppelation
        {
            get
            {
                return this.numAppelation;
            }

            set
            {
                this.numAppelation = value;
            }
        }

        public int EnumToNumTypeVin(TypeVin vin) {
            return ((int)vin) + 1; // index starts at 1
        }

        public TypeVin NumTypeVinToEnum(int val) { 
            return (TypeVin) val-1; // make index starts at 0
        }

        public Appelation EnumToNumAppelation() { return Model.Appelation.Bourgogne; }
        
        public List<Vin> FindAll()
        {
            List<Vin> lesVins = new List<Vin>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from vin ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["nomvin"].ToString());
                    lesVins.Add(
                        new Vin(
                            (int)dr["numvin"], 
                            (string)dr["nomvin"],
                            double.Parse(dr["prixvin"].ToString()), 
                            (string)dr["descriptif"],
                            (int)dr["annee"],
                            (int)dr["numtype"],
                            (int)dr["numappelation"]
                        )
                    );
                }
            }
            return lesVins;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vin vin &&
                   this.NumVin == vin.NumVin &&
                   this.NomVin == vin.NomVin &&
                   this.PrixVin == vin.PrixVin &&
                   this.Descriptif == vin.Descriptif &&
                   this.Annee == vin.Annee;
        }
    }
}