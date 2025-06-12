using Npgsql;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SAE201_Nicolas
{
    /// <summary>
    /// Logique d'interaction pour ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        private MainWindow mainWindow = new MainWindow();
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    this.Hide();
                    mainWindow.Show();
                    mainWindow.Owner = this;
                    tbLogin.Text = "";
                    pswrdBox.Password = "";
                }
            }
        }



        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeEllipse_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseEllipse_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
