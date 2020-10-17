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
using System.Windows.Navigation;
using System.Windows.Shapes;

////////////
using System.Data;
using System.Data.SqlClient;
using FoodRecipeApp.Classes;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
		{
            btnCloseMenu.Visibility = Visibility.Visible;
            btnOpenMenu.Visibility = Visibility.Hidden;
        }

		private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
		{
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Hidden;
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQL_DB.openConnection();

            /*SQL_DB.sql = "SELECT [ID], [Data], [Image] FROM RecipeInfo";
            SQL_DB.cmd.CommandType = CommandType.Text;
            SQL_DB.cmd.CommandText = SQL_DB.sql;
            SQL_DB.da = new SqlDataAdapter(SQL_DB.cmd);
            SQL_DB.dt = new DataTable();
            SQL_DB.da.Fill(SQL_DB.dt);

            SQL_Demo.ItemsSource = SQL_DB.dt.DefaultView;*/

            SQL_DB.closeConnection();
        }
    }
}
