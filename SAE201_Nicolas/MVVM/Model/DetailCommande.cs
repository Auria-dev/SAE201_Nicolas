using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Controls;

namespace SAE201_Nicolas.MVVM.Model
{
    public class DetailCommande : INotifyPropertyChanged
    {
        private int numCommande;
        private int numVin;
        private int quantite;
        private double prix;
        private Vin vin;
        private Commande commande;
        private Fournisseur fournisseur;

        public event PropertyChangedEventHandler? PropertyChanged;

        public DetailCommande(int numCommande, int numVin, int quantite, double prix, GestionVin gestionVin) // loading it from the GestionVin
        {
            this.NumCommande = numCommande;
            this.NumVin = numVin;
            this.Quantite = quantite;
            this.Prix = prix;

            // refence the other things 
            this.Vin = gestionVin.LesVins.SingleOrDefault(w => w.NumVin == this.NumVin);
            this.Commande = gestionVin.LesCommandes.SingleOrDefault(w => w.NumCommande == this.NumCommande);
            if (this.Vin != null) this.Fournisseur = gestionVin.LesFournisseurs.SingleOrDefault(w => w.NumFournisseur == this.Vin.NumFournisseur);
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumCommande)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumVin)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantite)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prix)));
            }
        }

        public string NomVin
        {
            get
            {
                return this.Vin == null ? "Erreur" : this.Vin.NomVin;
            }
        }

        public int AnneeVin
        {
            get
            {
                return this.Vin == null ? -1 : this.Vin.Annee;
            }
        }

        public string TypeVin
        {
            get
            {
                return this.Vin == null ? "Erreur" : this.Vin.NomTypeVin;
            }
        }

        public string FournisseurVin
        {
            get
            {
                return this.Fournisseur == null ? "Erreur" : this.Fournisseur.NomFournisseur;
            }
        }

        public string EtatCommande
        {
            get
            {
                return this.Commande == null ? "Erreur" : this.Commande.EtatCommande;
            }
        }

        public Vin Vin
        {
            get
            {
                return this.vin;
            }

            set
            {
                this.vin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vin)));
            }
        }

        public Commande Commande
        {
            get
            {
                return this.commande;
            }

            set
            {
                this.commande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Commande)));
            }
        }

        public Fournisseur Fournisseur
        {
            get
            {
                return this.fournisseur;
            }

            set
            {
                this.fournisseur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Fournisseur)));
            }
        }

        public void AjouterDetailCommande()
        {
            using (var cmdInsert = new NpgsqlCommand(
                "insert into detailcommande (numcommande, numvin, quantite, prix) values " +
               "(@numcommande, @numvin, @quantite, @prix)"))
            {
                cmdInsert.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdInsert.Parameters.AddWithValue("numvin", this.NumVin);
                cmdInsert.Parameters.AddWithValue("quantite", this.Quantite);
                cmdInsert.Parameters.AddWithValue("prix", this.Prix);
                DataAccess.Instance.ExecuteVoidInsert(cmdInsert);
            }
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from detailcommande where numcommande=@numcommande and numvin=@numvin;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdUpdate.Parameters.AddWithValue("numvin", this.NumVin);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
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
