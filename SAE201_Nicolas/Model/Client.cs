using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
{
    public class Client : INotifyPropertyChanged
    {
        private int numClient;
        private string nomClient;
        private string prenomClient;
        private string mailClient;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Client()
        {
        }

        public Client(int numClient, string nomClient, string prenomClient, string mailClient)
        {
            this.NumClient = numClient;
            this.NomClient = nomClient;
            this.PrenomClient = prenomClient;
            this.MailClient = mailClient;
        }

        public int NumClient
        {
            get
            {
                return this.numClient;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentException("Numéro client ne peut pas être négatif.");
                this.numClient = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumClient)));
            }
        }

        public string NomClient
        {
            get
            {
                return this.nomClient;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nom client ne peut pas être vide.");
                this.nomClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomClient)));
            }
        }

        public string PrenomClient
        {
            get
            {
                return this.prenomClient;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Préom client ne peut pas être vide.");
                this.prenomClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrenomClient)));
            }
        }

        public string MailClient
        {
            get
            {
                return this.mailClient;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le mail du client ne peut pas être vide.");

                if (!Regex.IsMatch(value, "^[\\w.-]+@[\\w.-]+\\.[a-zA-Z]{2,}$"))
                    throw new ArgumentException("Le mail du client n'est pas valide.");

                this.mailClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MailClient)));
            }
        }

        public List<Client> FindAll()
        {
            List<Client> lesClients = new List<Client>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from client ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                {
                    lesClients.Add(
                        new Client(
                            (int)dr["numclient"],
                            (string)dr["nomclient"],
                            (string)dr["prenomclient"],
                            (string)dr["mailclient"]
                        )
                    );
                }
            }
            return lesClients;
        }

        public int AjouterClient()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into client (nomclient, prenomclient, mailclient) values (@nomclient, @prenomclient, @mailclient) RETURNING numclient"))
            {
                cmdInsert.Parameters.AddWithValue("nomclient", this.NomClient);
                cmdInsert.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdInsert.Parameters.AddWithValue("mailclient", this.MailClient);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumClient = nb;
            return nb;
        }

        public int SupprimerClient()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from client where numclient =@numclient;"))
            {
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int UpdateClient()
        {
            using (var cmdUpdate = new NpgsqlCommand("update client set nomclient =@nomclient ,  prenomclient = @prenomclient,  mailclient = @mailclient  where numclient =@numclient;"))
            {
                cmdUpdate.Parameters.AddWithValue("nomclient", this.NomClient);
                cmdUpdate.Parameters.AddWithValue("prenomclient", this.PrenomClient);
                cmdUpdate.Parameters.AddWithValue("mailclient", this.MailClient);
                cmdUpdate.Parameters.AddWithValue("numclient", this.NumClient);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}
