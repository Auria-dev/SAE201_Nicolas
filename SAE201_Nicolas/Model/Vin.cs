using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
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
        Bordeaux,
        Bourgogne,
        Champagne,
        Alsace,
        Beaujolais,
        CôtesDuRhône,
        Loire,
        Provence,
        Languedoc,
        Roussillon,
        Savoie,
        Jura,
        SudOuest,
        Corse,
        Bergerac,
        Cahors,
        Gaillac,
        Madiran,
        Jurançon,
        CoteauxDuTricastin,
        CoteauxDelAubance,
        CoteauxDeSaumur,
        CôtesDeProvence,
        CôtesDeBergerac,
        CôtesDeBlaye,
        CôtesDeDuras,
        CôtesDeGascogne,
        CôtesDeMillau,
        CôtesDeSaintMont,
        CôtesDuFrontonnais,
        CôtesDuMarmandais,
        CôtesDuRoussillon,
        CôtesDuVentoux,
        Fitou,
        Italien,
        MuscatDeFrontignan,
        MuscatDeLunel,
        MuscatDeMireval,
        MuscatDeSaintJeandeMinervois,
        PineauDesCharentes,
        Rasteau,
        Tavel,
        Vacqueyras,
        VinDePaysDOc
    }

    public class Vin : INotifyPropertyChanged
    {
        private int numVin;
        private string nomVin;
        private double prixVin;
        private string descriptif;
        private int annee;
        private int numTypeVin;
        private int numAppelation;
        private int numFournisseur;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Vin() { }

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee)
        {
            this.NumVin = numVin;
            this.NomVin = nomVin;
            this.PrixVin = prixVin;
            this.Descriptif = descriptif;
            this.Annee = annee;
        }

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee, int numTypeVin, int numAppelation) : this(numVin, nomVin, prixVin, descriptif, annee)
        {
            this.NumTypeVin = numTypeVin;
            this.NumAppelation = numAppelation;
        }

        public Vin(int numVin, string nomVin, double prixVin, string descriptif, int annee, int numTypeVin, int numAppelation, int numFournisseur) : this(numVin, nomVin, prixVin, descriptif, annee, numTypeVin, numAppelation)
        {
            this.NumFournisseur = numFournisseur;
        }

        public int NumVin
        {
            get { return this.numVin; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Le numéro du vin doit être supérieur à zéro.");
                this.numVin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumVin)));
            }
        }

        public string NomVin
        {
            get { return this.nomVin; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le nom du vin ne peut pas être vide.");
                this.nomVin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomVin)));
            }
        }

        public double PrixVin
        {
            get { return this.prixVin; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Le prix du vin ne peut pas être négatif.");
                this.prixVin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrixVin)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Descriptif)));
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
                int currentYear = DateTime.Now.Year;
                if (value < 0 || value > currentYear)
                    throw new ArgumentException($"L'année doit être comprise entre 0 et {currentYear}.");
                this.annee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Annee)));
            }
        }

        public string NomTypeVin
        {
            get
            {

                if (Enum.IsDefined(typeof(Appelation), NumAppelation)) 
                    return ((Appelation)NumAppelation).ToString();
                else
                    return "Autre";
            }
        }

        public string NomAppelation
        {
            get
            {
                if (Enum.IsDefined(typeof(Appelation), NumAppelation))
                    return ((Appelation)NumAppelation).ToString();
                else 
                    return "Autre";
            }
        }

        public int NumTypeVin
        {
            get { return this.numTypeVin; }

            set
            {
                this.numTypeVin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumTypeVin)));
            }
        }

        public int NumAppelation
        {
            get { return this.numAppelation; }

            set
            {
                this.numAppelation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumAppelation)));
            }
        }

        public int NumFournisseur
        {
            get { return this.numFournisseur; }

            set
            {
                if (value < 0)
                    throw new ArgumentException("Le numéro du fournisseur doit être supérieur à zéro.");
                this.numFournisseur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumFournisseur)));
            }
        }

        public string NomFournisseur
        {
            get
            {
                return MainWindow.LaGestionDeVins.LesFournisseurs.FirstOrDefault(w => w.NumFournisseur == this.NumFournisseur).NomFournisseur; 
            }
        }

        public int EnumToInt(TypeVin vin)
        {
            return (int)vin + 1; // index starts at 1
        }

        public TypeVin IntToEnum(int val)
        {
            return (TypeVin)val - 1; // make index starts at 0
        }

        public List<Vin> FindAll()
        {
            List<Vin> lesVins = new List<Vin>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from vin ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesVins.Add(
                        new Vin(
                            (int)dr["numvin"],
                            (string)dr["nomvin"],
                            double.Parse(dr["prixvin"].ToString()),
                            (string)dr["descriptif"],
                            (int)dr["annee"],
                            (int)dr["numtype"],
                            (int)dr["numappelation"],
                            (int)dr["numfournisseur"]
                        )
                    );
                }
            }
            return lesVins;
        }

        public Vin FindFromID(int id)
        {
            Vin res;
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from vin where numvin = @targetNumVin"))
            {
                cmdSelect.Parameters.AddWithValue("targetNumVin", id);
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                DataRow dr = dt.Rows[0];
                res = new Vin(
                    (int)dr["numvin"],
                    (string)dr["nomvin"],
                    double.Parse(dr["prixvin"].ToString()),
                    (string)dr["descriptif"],
                    (int)dr["annee"],
                    (int)dr["numtype"],
                    (int)dr["numappelation"],
                    (int)dr["numfournisseur"]
                );
            }

            return res;
        }

        public int AjouterVin()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into vin (numfournisseur, numtype, numappelation, nomvin, prixvin, descriptif, annee) values (@numfournisseur, @numtype, @numappelation, @nomvin, @prixvin, @descriptif, @annee) RETURNING numvin"))
            {
                cmdInsert.Parameters.AddWithValue("numfournisseur", this.NumFournisseur);
                cmdInsert.Parameters.AddWithValue("numtype", this.NumTypeVin);
                cmdInsert.Parameters.AddWithValue("numappelation", this.NumAppelation);
                cmdInsert.Parameters.AddWithValue("nomvin", this.NomVin);
                cmdInsert.Parameters.AddWithValue("prixvin", this.PrixVin);
                cmdInsert.Parameters.AddWithValue("descriptif", this.Descriptif);
                cmdInsert.Parameters.AddWithValue("annee", this.Annee);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumVin = nb;
            return nb;
        }

        public int UpdateVin()
        {
            using (var cmdUpdate = new NpgsqlCommand("update vin set numfournisseur =@numfournisseur,  numtype = @numtype,  numappelation = @numappelation, nomvin = @nomvin, prixvin = @prixvin, descriptif = @descriptif, annee = @annee where numvin =@numvin;"))
            {
                cmdUpdate.Parameters.AddWithValue("numfournisseur", this.NumFournisseur);
                cmdUpdate.Parameters.AddWithValue("numtype", this.NumTypeVin);
                cmdUpdate.Parameters.AddWithValue("numappelation", this.NumAppelation);
                cmdUpdate.Parameters.AddWithValue("nomvin", this.NomVin);
                cmdUpdate.Parameters.AddWithValue("prixvin", this.PrixVin);
                cmdUpdate.Parameters.AddWithValue("descriptif", this.Descriptif);
                cmdUpdate.Parameters.AddWithValue("annee", this.Annee);
                cmdUpdate.Parameters.AddWithValue("numvin", this.NumVin);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int SupprimerVin()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from vin where numvin =@numvin;"))
            {
                cmdUpdate.Parameters.AddWithValue("numvin", this.NumVin);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
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
