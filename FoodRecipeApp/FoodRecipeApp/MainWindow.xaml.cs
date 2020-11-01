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
using System.Configuration;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            loadConfig();

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
                    new Menu() { Content = "New Dish", Icon = "Resources/Icons/plus.png" },
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
        bool _menu_state_closed = true;
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (_menu_state_closed)
            {
                Storyboard sb = this.FindResource("openMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("closeMenu") as Storyboard;
                sb.Begin();
            }

            _menu_state_closed = !_menu_state_closed;
        }


        private BindingList<Menu> _list_menu;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;

            //Check file path
            string newFolder = $"{Folder}Resources\\Data";
            MyFileManager.CheckDictionary(newFolder);

            newFolder = $"{Folder}Resources\\Images";
            MyFileManager.CheckDictionary(newFolder);

            newFolder = $"{Folder}Resources\\Icons";
            MyFileManager.CheckDictionary(newFolder);

            this.Background = Brushes.Bisque;
            menuPage.Content = new HomePage();
            _list_menu = MenuDao.GetAll();
            menuList.ItemsSource = _list_menu;           
        }

        //Close button (exit window)
		private void closeProgramButton_Click(object sender, RoutedEventArgs e)
		{
            saveConfig();
            this.Close();
		}

		private void selectedTab(object sender, MouseButtonEventArgs e)
		{
            var item = (sender as ListView).SelectedIndex;
            switch (item)
			{
                case 0:
                    menuPage.Content = new HomePage();
                    
                    break;
                case 1:
                    menuPage.Content = new SearchingPage();
                    break;
                case 2:
                    var newScreen = new UploadNewDishScreen();
                    newScreen.Show();
                    this.Close();
                    break;
                case 3:
                    break;
			}
		}

		private void loadConfig()
		{
			var configWidth = ConfigurationManager.AppSettings["Width"];
			this.Width = double.Parse(configWidth);
			var configHeight = ConfigurationManager.AppSettings["Height"];
			this.Height = double.Parse(configHeight);

			var configMenu = ConfigurationManager.AppSettings["HiddenMenu"];
			_menu_state_closed = bool.Parse(configMenu);
			if (_menu_state_closed)
			{
				gridMenu.Width = 60;
			}
			else
			{
				gridMenu.Width = 250;
			}
		}

		private void saveConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["Width"].Value = this.Width.ToString();
			config.AppSettings.Settings["Height"].Value = this.Height.ToString();
			config.AppSettings.Settings["HiddenMenu"].Value = _menu_state_closed.ToString();
			config.Save(ConfigurationSaveMode.Minimal);
		}
	}
}
