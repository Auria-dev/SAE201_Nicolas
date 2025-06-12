using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public enum EnumEtatCommande
    {
        Validée,
        EnAttante,
        Supprimée
    }

    public class Commande : INotifyPropertyChanged
    {
        private int numCommande;
        private int numEmploye;
        private DateTime dateCommande;
        private string etatCommande;
        private double prixTotal;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Commande()
        {
        }

        public Commande(int numCommande, int numEmploye, DateTime dateCommande, string etatCommande, double prixTotal)
        {
            this.NumCommande = numCommande;
            this.NumEmploye = numEmploye;
            this.DateCommande = dateCommande;
            this.EtatCommande = etatCommande;
            this.PrixTotal = prixTotal;
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumCommande)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumEmploye)));
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return this.dateCommande;
            }

            set
            {
                this.dateCommande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateCommande)));
            }
        }

        public string EtatCommande
        {
            get
            {
                return this.etatCommande;
            }

            set
            {
                this.etatCommande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EtatCommande)));
            }
        }

        public double PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrixTotal)));
            }
        }

        public EnumEtatCommande StringToEtatCommande(string s) {
            switch (s)
            {
                case "Validée":
                    return EnumEtatCommande.Validée;

                case "EnAttante":
                    return EnumEtatCommande.EnAttante;

                case "Supprimée":
                    return EnumEtatCommande.Supprimée;

                default:
                    return EnumEtatCommande.EnAttante;
            }
        }

        public string EtatCommandeToString(EnumEtatCommande e) {
            if (e == EnumEtatCommande.EnAttante) return "En attante";
            else return e.ToString();
        }

        public void AjouterCommande()
        {
            using (var cmdInsert = new NpgsqlCommand(
                "insert into commande (numcommande, numemploye, datecommande, etatcommande, prixtotal) values "+
               "(@numcommande, @numemploye, @datecommande, @etatcommande, @prixtotal)"))
            {
                cmdInsert.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdInsert.Parameters.AddWithValue("numemploye", this.NumEmploye);
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("etatcommande", this.EtatCommande);
                cmdInsert.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                DataAccess.Instance.ExecuteVoidInsert(cmdInsert);
            }
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande where numcommande=@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public void ModifierCommande() { }

        public List<Commande> FindAll()
        {
            List<Commande> lesVins = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from Commande ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesVins.Add(
                        new Commande(
                             (int)dr["numcommande"],
                             (int)dr["numemploye"],
                             DateTime.Parse(dr["datecommande"].ToString()),
                             (string)dr["etatcommande"],
                             double.Parse(dr["prixtotal"].ToString())
                        )
                    );
                }
            }
            return lesVins;
        }

        public Commande FindFromID(int id)
        {
            Commande res;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from Commande where numcommande = @targetNumCmd"))
            {
                cmdSelect.Parameters.AddWithValue("targetNumCmd", id);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                DataRow dr = dt.Rows[0];
                res = new Commande(
                    (int)dr["numcommande"],
                    (int)dr["numemploye"],
                    DateTime.Parse(dr["datecommande"].ToString()),
                    (string)dr["etatcommande"],
                    double.Parse(dr["prixtotal"].ToString())
                );
            }

            return res;
        }
    }
}
