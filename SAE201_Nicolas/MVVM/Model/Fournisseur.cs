using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public class Fournisseur
    {
        private int numFournisseur;
        private string nomFournisseur;

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
                this.numFournisseur = value;
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
                this.nomFournisseur = value;
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
