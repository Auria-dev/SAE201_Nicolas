using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
{
    public class Fournisseur : INotifyPropertyChanged
    {
        private int numFournisseur;
        private string nomFournisseur;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Fournisseur() { }

        public Fournisseur(int numFournisseur, string nomFournisseur)
        {
            this.NumFournisseur = numFournisseur;
            this.NomFournisseur = nomFournisseur;
        }

        public int NumFournisseur
        {
            get
            {
                return this.numFournisseur;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentException("Le numéro du fournisseur doit être supérieur à zéro.");
                this.numFournisseur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumFournisseur)));
            }
        }

        public string NomFournisseur
        {
            get
            {
                return this.nomFournisseur;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le nom du fournisseur ne peut pas être vide.");
                this.nomFournisseur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomFournisseur)));
            }
        }

        public List<Fournisseur> FindAll()
        {
            List<Fournisseur> lesFournisseurs = new List<Fournisseur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from Fournisseur;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesFournisseurs.Add(
                        new Fournisseur(
                            (int)dr["numfournisseur"],
                            (string)dr["nomfournisseur"]
                        )
                    );
                }
            }
            return lesFournisseurs;
        }
    }
}
