using Microsoft.Ajax.Utilities;
using Npgsql;
using SAE201_Nicolas.Core;
using SAE201_Nicolas.Model;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace SAE201_Nicolas
{
    /// <summary>
    /// Logique d'interaction pour ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public MainWindow mainWindow;
        
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

                    if (tbLogin.Text.IsNullOrWhiteSpace()) return;
                    List<Employe> lesEmploye = new Employe().FindAll();

                    if (!lesEmploye.Any(w => w.Login == tbLogin.Text)) return;
                    
                    Employe emp = lesEmploye.FirstOrDefault(e => e.Login == tbLogin.Text);

                    if (emp.Login == login)
                    {
                        Console.WriteLine("Employe found!");
                        Console.WriteLine($"Role: {emp.RoleEmploye}");
                    } else {
                        emp = lesEmploye.Last();
                        Console.WriteLine("Employe not found. Using last one instead");
                    }

                    if (mainWindow != null)
                    {
                        mainWindow.Close();
                        mainWindow = null;
                    }

                    mainWindow = new MainWindow(emp);
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
