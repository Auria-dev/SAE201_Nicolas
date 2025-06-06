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
        Blanc,
        Rouge,
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

        public Vin()
        {
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
                int num=0;
                string type;
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from vin ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                    {
                        num = (int)dr["numtype"];
                    }
                }
                switch (num)
                {
                    case 0: type = "type0";
                        break;

                    case 1:
                        type = "type1";
                        break;

                    case 2:
                        type = "type2";
                        break;

                    default: type = "UNKNOWN"; break; 
                }
                    
                return type;
            }
        }

        public string Appelation
        {
            get
            {
                return this.NomVin;
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
                            (int)dr["annee"]
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