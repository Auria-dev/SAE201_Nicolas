using Npgsql;
using SAE201_Nicolas.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SAE201_Nicolas.Model
{
    public enum Role
    {
        Vendeur,
        ResponsableDeMagasin
    }

    public class Employe : INotifyPropertyChanged
    {
        private int numEmploye;
        private string nom;
        private string prenom;
        private string login;
        private string mdp;
        private Role roleEmploye;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Employe(int numEmploye, int numRole, string nom, string prenom, string login, string mdp)
        {
            this.roleEmploye = IntToRole(numEmploye);
            this.NumEmploye = numEmploye;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Login = login;
            this.Mdp = mdp;
        }
        
        public Employe() { }

        public int NumEmploye
        {
            get { return this.numEmploye; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Le numéro de l'employé doit être supérieur à zéro.");
                this.numEmploye = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumEmploye)));
            }
        }

        public string Nom
        {
            get { return this.nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le nom ne peut pas être vide.");
                this.nom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nom)));
            }
        }

        public string Prenom
        {
            get { return this.prenom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le prénom ne peut pas être vide.");
                this.prenom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prenom)));
            }
        }

        public string Login
        {
            get { return this.login; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le login ne peut pas être vide.");
                this.login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }

        public string Mdp
        {
            get { return this.mdp; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new ArgumentException("Le mot de passe ne peut pas être vide et doit contenir au moins 6 caractères.");
                this.mdp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mdp)));
            }
        }

        public string NomEmployerCourt
        {
            get { return this.Nom + " " + this.Prenom.Substring(0, 1) + "."; }
        }

        public Role IntToRole(int r)
        {
            if (r == 1) return Role.ResponsableDeMagasin;
            else return Role.Vendeur;
        }

        public List<Employe> FindAll()
        {
            List<Employe> lesEmployer = new List<Employe>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from employe ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesEmployer.Add(
                        new Employe(
                            (int)dr["numemploye"],
                            (int)dr["numrole"],
                            dr["nom"].ToString(),
                            dr["prenom"].ToString(),
                            dr["login"].ToString(),
                            dr["mdp"].ToString()
                        )
                    );
                }
            }
            return lesEmployer;
        }

        public int UpdateEmploye()
        {
            using (var cmdUpdate = new NpgsqlCommand("update employe set nom = @nom,  prenom = @prenom where numemploye =@numemploye;"))
            {
                cmdUpdate.Parameters.AddWithValue("nom", this.Nom);
                cmdUpdate.Parameters.AddWithValue("prenom", this.Prenom);
                cmdUpdate.Parameters.AddWithValue("numemploye", this.NumEmploye);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}
