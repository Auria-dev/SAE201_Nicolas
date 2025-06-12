using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SAE201_Nicolas.MVVM.Model
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public Employe(int numEmploye, string nom, string prenom, string login, string mdp)
        {
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
            set { this.numEmploye = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumEmploye)));
            }
        }

        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nom)));
            }
        }

        public string Prenom
        {
            get { return this.prenom; }
            set { this.prenom = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prenom)));
            }
        }

        public string Login
        {
            get { return this.login; }
            set { this.login = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }

        public string Mdp
        {
            get { return this.mdp; }
            set { this.mdp = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mdp)));
            }
        }

        public string NomEmployerCourt
        {
            get { return this.Nom + " " + this.Prenom.Substring(0, 1) + "."; }
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
    }
}
