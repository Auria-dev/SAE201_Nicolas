using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public class DetailCommande
    {
        private int numCommande;
        private int numVin;
        private int quantite;
        private double prix;

        public DetailCommande(int numCommande, int numVin, int quantite, double prix)
        {
            this.NumCommande = numCommande;
            this.NumVin = numVin;
            this.Quantite = quantite;
            this.Prix = prix;
        }

        public DetailCommande()
        {
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

        public int Quantite
        {
            get
            {
                return this.quantite;
            }

            set
            {
                this.quantite = value;
            }
        }

        public double Prix
        {
            get
            {
                return this.prix;
            }

            set
            {
                this.prix = value;
            }
        }

        public List<DetailCommande> FindAll()
        {
            List<DetailCommande> lesDetailsDeCommandes = new List<DetailCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from detailcommande ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesDetailsDeCommandes.Add(
                        new DetailCommande(
                             (int)dr["numcommande"],
                             (int)dr["numvin"],
                             (int)dr["quantite"],
                             double.Parse(dr["prix"].ToString())
                        )
                    );
                }
            }
            return lesDetailsDeCommandes;
        }

        public override bool Equals(object? obj)
        {
            return obj is DetailCommande commande &&
                   this.NumCommande == commande.NumCommande &&
                   this.NumVin == commande.NumVin &&
                   this.Quantite == commande.Quantite &&
                   this.Prix == commande.Prix;
        }

        public static bool operator ==(DetailCommande? left, DetailCommande? right)
        {
            return EqualityComparer<DetailCommande>.Default.Equals(left, right);
        }

        public static bool operator !=(DetailCommande? left, DetailCommande? right)
        {
            return !(left == right);
        }
    }
}
