using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE201_Nicolas
{
    /// <summary>
    /// Logique d'interaction pour ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow()
        {
            InitializeComponent();
        }

        private void butConnexion_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pswrdBox.Password;

            string connectionString = $"Server=srv-peda-new;" +
                                      "port=5433;" +
                                      "Database=sae201_nicolas;" +
                                      "Search Path=nicolas;" +
                                      $"uid={login};" +
                                      $"password={password};";

            string connectionStringTemp = $"Server=srv-peda-new;" +
                                      "port=5433;" +
                                      "Database=sae201_nicolas;" +
                                      "Search Path=nicolas;" +
                                      $"uid=ismaimal;" +
                                      $"password=YLrlDE;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionStringTemp))
            {
                try
                {
                    connection.Open();

                    //montrer application ici
                    MainWindow mainWindow = new MainWindow();
                    this.Hide();
                    mainWindow.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
