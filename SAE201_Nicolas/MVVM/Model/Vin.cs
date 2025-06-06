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

        public int NumTypeVinToEnum() { return 0; }
        public TypeVin EnumToNumTypeVin() { return TypeVin.Blanc; }
        public Appelation EnumToNumAppelation() { return Appelation.Bourgogne; }
        
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

        //public void Read()
        //{
        //    using (var cmdSelect = new NpgsqlCommand("select * from vin where numvin =@numvin;"))
        //    {
        //        cmdSelect.Parameters.AddWithValue("numvin", this.NumVin);

        //        DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
        //        this.Nom = (String)dt.Rows[0]["nom"];
        //        this.Maitre = (String)dt.Rows[0]["maitre"];
        //        this.Poids = (double)dt.Rows[0]["poids"];
        //    }
        //}

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