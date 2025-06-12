using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SAE201_Nicolas.Model
{
    public class Demande : INotifyPropertyChanged
    {
        private int numDemande;
        private int numVin;
        private int numEmploye;
        private int numCommande;
        private int numClient;
        private DateTime dateDemande;
        private int quantiteDemande;
        private EnumEtatCommande etatDemande;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Demande() { }

        public Demande(int numDemande, int numVin, int numEmploye, int numCommande, DateTime dateDemande, int quantiteDemande, EnumEtatCommande etatDemande, int numClient)
        {
            this.NumDemande = numDemande;
            this.NumVin = numVin;
            this.NumEmploye = numEmploye;
            this.NumCommande = numCommande;
            this.DateDemande = dateDemande;
            this.QuantiteDemande = quantiteDemande;
            this.EtatDemande = etatDemande;
            this.NumClient = numClient;
        }

        public Demande(int numDemande, int numVin, int numEmploye, int numCommande, DateTime dateDemande, int quantiteDemande, EnumEtatCommande etatDemande)
        {
            this.NumDemande = numDemande;
            this.NumVin = numVin;
            this.NumEmploye = numEmploye;
            this.NumCommande = numCommande;
            this.DateDemande = dateDemande;
            this.QuantiteDemande = quantiteDemande;
            this.EtatDemande = etatDemande;
        }

        public int NumDemande
        {
            get { return this.numDemande; }
            set
            {
                this.numDemande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumDemande)));
            }
        }

        public int NumVin
        {
            get { return this.numVin; }
            set
            {
                this.numVin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumVin)));
            }
        }

        public int NumEmploye
        {
            get { return this.numEmploye; }
            set
            {
                this.numEmploye = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumEmploye)));
            }
        }

        public int NumCommande
        {
            get { return this.numCommande; }
            set
            {
                this.numCommande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumCommande)));
            }
        }

        public DateTime DateDemande
        {
            get { return this.dateDemande; }
            set
            {
                this.dateDemande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateDemande)));
            }
        }

        public int QuantiteDemande
        {
            get { return this.quantiteDemande; }
            set
            {
                this.quantiteDemande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuantiteDemande)));
            }
        }

        public EnumEtatCommande EtatDemande
        {
            get { return this.etatDemande; }
            set
            {
                this.etatDemande = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EtatDemande)));
            }
        }

        public string EtatDemandeString
        {
            get
            {
                return this.EtatDemande.ToString();
            }
        }

        public string NomEtatDemande
        {
            get { return EtatCommandeToString(EtatDemande); }
        }

        public string NomEmploye
        {
            get { return MainWindow.LaGestionDeVins.LesEmployes.SingleOrDefault(w => w.NumEmploye == this.NumEmploye).NomEmployerCourt; ; }
        }

        public string NomVin
        {
            get { return MainWindow.LaGestionDeVins.LesVins.SingleOrDefault(w => w.NumVin == this.NumVin).NomVin; }
        }

        public int PrixDemande
        {
            get { return (int)MainWindow.LaGestionDeVins.LesVins.SingleOrDefault(w => w.NumVin == this.NumVin).PrixVin * this.QuantiteDemande; }
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

        public EnumEtatCommande StringToEtatCommande(string s)
        {
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

        public string EtatCommandeToString(EnumEtatCommande e)
        {
            if (e == EnumEtatCommande.EnAttante) return "En attante";
            else return e.ToString();
        }

        public int EtatDemandeToInt(EnumEtatCommande Enum)
        {
            if (Enum == EnumEtatCommande.Validée) return 1;
            if (Enum == EnumEtatCommande.EnAttante) return 2;
            if (Enum == EnumEtatCommande.Supprimée) return 3;
            return 0;
        }

        public List<Demande> FindAll()
        {
            List<Demande> lesDemandes = new List<Demande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from demande;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesDemandes.Add(
                        new Demande(
                             (int)dr["numdemande"],
                             (int)dr["numvin"],
                             (int)dr["numemploye"],
                             (int)dr["numcommande"],
                             DateTime.Parse(dr["datedemande"].ToString()),
                             (int)dr["quantitedemande"],
                             StringToEtatCommande(dr["etatdemande"].ToString()),
                             (int)dr["numclient"]
                        )
                    );
                }
            }
            return lesDemandes;
        }

        public int AjouterDemande()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into demande"
                + "(numvin, numemploye, numcommande, numclient, datedemande, quantitedemande, etatdemande) values "
                + "(@numvin, @numemploye, @numcommande, @numclient, @datedemande, @quantitedemande, @etatdemande) RETURNING numdemande"))
            {
                cmdInsert.Parameters.AddWithValue("numvin", this.NumVin);
                cmdInsert.Parameters.AddWithValue("numemploye", this.NumEmploye);
                cmdInsert.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdInsert.Parameters.AddWithValue("numclient", this.NumClient);
                cmdInsert.Parameters.AddWithValue("datedemande", this.DateDemande);
                cmdInsert.Parameters.AddWithValue("quantitedemande", this.QuantiteDemande);
                cmdInsert.Parameters.AddWithValue("etatdemande", this.EtatDemandeString);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumDemande = nb;
            return nb;
        }

        public int UpdateDemande()
        {
            using (var cmdUpdate = new NpgsqlCommand("update demande set "
                + "numvin = @numvin, numemploye = @numemploye, numcommande = @numcommande, numclient = @numclient, datedemande = @datedemande, quantitedemande = @quantitedemande, etatdemande = @etatdemande "
                + "where numdemande =@numdemande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numvin", this.NumVin);
                cmdUpdate.Parameters.AddWithValue("numemploye", this.NumEmploye);
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                cmdUpdate.Parameters.AddWithValue("datedemande", this.DateDemande);
                cmdUpdate.Parameters.AddWithValue("quantitedemande", this.QuantiteDemande);
                cmdUpdate.Parameters.AddWithValue("etatdemande", this.NomEtatDemande);
                cmdUpdate.Parameters.AddWithValue("numdemande", this.NumDemande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}
