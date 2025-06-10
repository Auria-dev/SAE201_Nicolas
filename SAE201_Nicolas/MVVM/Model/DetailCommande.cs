using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;

namespace SAE201_Nicolas.MVVM.Model
{
    public class DetailCommande
    {
        private int numCommande;
        private int numVin;
        private int quantite;
        private double prix;
        private Vin vin;
        private Commande commande;
        private Fournisseur fournisseur;

        public DetailCommande(int numCommande, int numVin, int quantite, double prix, GestionVin gestionVin) // loading it from the GestionVin
        {
            this.NumCommande = numCommande;
            this.NumVin = numVin;
            this.Quantite = quantite;
            this.Prix = prix;

            // refence the other things 
            this.vin = gestionVin.LesVins.SingleOrDefault(w => w.NumVin == this.NumVin);
            this.commande = gestionVin.LesCommandes.SingleOrDefault(w => w.NumCommande == this.NumCommande);
            if (this.vin != null) this.fournisseur = gestionVin.LesFournisseurs.SingleOrDefault(w => w.NumFournisseur == this.vin.NumFournisseur);
        }

        public DetailCommande() { }

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

        public string NomVin
        {
            get
            {
                return this.vin == null ? "Erreur" : this.vin.NomVin;
            }
        }

        public int AnneeVin
        {
            get
            {
                return this.vin == null ? -1 : this.vin.Annee;
            }
        }

        public string TypeVin
        {
            get
            {
                return this.vin == null ? "Erreur" : this.vin.NomTypeVin;
            }
        }

        public string FournisseurVin
        {
            get
            {
                return this.fournisseur == null ? "Erreur" : this.fournisseur.NomFournisseur;
            }
        }

        public string EtatCommande
        {
            get
            {
                return this.commande == null ? "Erreur" : this.commande.EtatCommande;
            }
        }

        public List<DetailCommande> FindAll(GestionVin gestionVin)
        {
            List<DetailCommande> lesDetailsDeCommandes = new List<DetailCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM detailcommande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesDetailsDeCommandes.Add(
                        new DetailCommande(
                            (int)dr["numcommande"],
                            (int)dr["numvin"],
                            (int)dr["quantite"],
                            double.Parse(dr["prix"].ToString()),
                            gestionVin
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
