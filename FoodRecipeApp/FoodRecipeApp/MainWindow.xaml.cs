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
using System.Windows.Media.Animation;
using System.ComponentModel;

////////////
using System.Data;
using System.Data.SqlClient;
using FoodRecipeApp.Classes;
using System.Runtime.CompilerServices;

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
            MouseDown += Window_MouseDown;
            
        }

        class Menu : INotifyPropertyChanged
        {
            public string Content { get; set; }
            public string Icon { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        class MenuDao
        {
            public static BindingList<Menu> GetAll()
            {
                var list = new BindingList<Menu>()
                {
                    new Menu() { Content = "Home", Icon = "Resources/Icons/home.png" },
                    new Menu() { Content = "Search", Icon = "Resources/Icons/search.png" },
                    new Menu() { Content = "History", Icon = "Resources/Icons/history.png" },
                    new Menu() { Content = "Settings", Icon = "Resources/Icons/settings.png" },
                };
                return list;

            }
        }
        
        //drag window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        //Menu Open - Collapse
        bool StateClosed = true;
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }
        private BindingList<Menu> _list_menu;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _list_menu = MenuDao.GetAll();
            menuList.ItemsSource = _list_menu;
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

        //Close button (exit window)
		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
            this.Close();
		}

		private void SelectedTab(object sender, MouseButtonEventArgs e)
		{
            var item = (sender as ListView).SelectedIndex;
            switch (item)
			{
                case 0:
                    this.Background = Brushes.White;
                    break;
                case 1:
                    this.Background = Brushes.Blue;
                    break;
                case 2:
                    this.Background = Brushes.Green;
                    break;
                case 3:
                    this.Background = Brushes.Red;
                    break;
			}
		}
	}
}
