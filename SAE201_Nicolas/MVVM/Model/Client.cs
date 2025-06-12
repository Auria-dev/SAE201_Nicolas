using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.MVVM.Model
{
    public class Client
    {
        private int numClient;
        private string nomClient;
        private string prenomClient;
        private string mailClient;

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
                this.numClient = value;
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
                this.nomClient = value;
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
                this.prenomClient = value;
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
                this.mailClient = value;
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
    }
}
