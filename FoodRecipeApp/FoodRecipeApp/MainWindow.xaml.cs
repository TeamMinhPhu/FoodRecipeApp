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
                    var oldBackGroundColor = (SolidColorBrush)this.Background;
                    var oldMenuColor = (SolidColorBrush)this.MenuBar.Background;
                    var oldTitleBarColor = (SolidColorBrush)this.TitleBar.Background;
				    var oldBackGroundTheme = App.Current.Resources.MergedDictionaries[0].Source;
                    var oldForeGroundTheme = App.Current.Resources.MergedDictionaries[2].Source;

                    var settingScreen = new AppSetting();
                    settingScreen.BackGroundThemeChanged += BackGround_ColorChanged;
                    settingScreen.PrimaryColorChanged += Menu_ColorChanged;
                    settingScreen.SecondaryColorChanged += Title_ColorChanged;
					if(settingScreen.ShowDialog() == true)
					{
                        this.Background = settingScreen.NewBackGroundColor;
                        this.MenuBar.Background = settingScreen.NewMenuColor;
                        this.TitleBar.Background = settingScreen.NewTitleBarColor;
					}
                    else
					{
                        this.Background = oldBackGroundColor;
                        this.MenuBar.Background = oldMenuColor;
                        this.TitleBar.Background = oldTitleBarColor;
                        App.Current.Resources.MergedDictionaries[0].Source = oldBackGroundTheme;
                        App.Current.Resources.MergedDictionaries[2].Source = oldForeGroundTheme;
                    }
                    
                    break;
			}
		}

        private void BackGround_ColorChanged(SolidColorBrush color)
		{
            this.Background = color;
		}

        private void Menu_ColorChanged(SolidColorBrush color)
        {
            this.MenuBar.Background = color;
        }

        private void Title_ColorChanged(SolidColorBrush color)
		{
            this.TitleBar.Background = color;
		}

		#region "Config"
		bool _is_dark_theme;
        int _color;
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
				MenuBar.Width = 60;
			}
			else
			{
				MenuBar.Width = 250;
			}

            var configDarkTheme = ConfigurationManager.AppSettings["DarkTheme"];
            _is_dark_theme = bool.Parse(configDarkTheme);

            var configColor = ConfigurationManager.AppSettings["Color"];
            _color = int.Parse(configColor);
            setColor();
        }

		private void saveConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings["Width"].Value = this.Width.ToString();
			config.AppSettings.Settings["Height"].Value = this.Height.ToString();
			config.AppSettings.Settings["HiddenMenu"].Value = _menu_state_closed.ToString();
			config.Save(ConfigurationSaveMode.Minimal);
		}

        private void setColor()
        {
            const int RED = 1;
            const int ORANGE = 2;
            const int YELLOW = 3;
            const int BLUE = 4;
            const int GREEN = 5;

            SolidColorBrush backGroundColor = new SolidColorBrush();
            SolidColorBrush newPrimaryColor = new SolidColorBrush();
            SolidColorBrush newDarkPrimaryColor = new SolidColorBrush();

            if (_is_dark_theme == false)
            {
                backGroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
                App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");

                if (_color == RED)
                {
                    newPrimaryColor = new BrushConverter().ConvertFromString("#ff1717") as SolidColorBrush;
                    newDarkPrimaryColor = new BrushConverter().ConvertFromString("#de0000") as SolidColorBrush;
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Red.xaml");
                }
                else if (_color == ORANGE)
                {
                    newPrimaryColor = new BrushConverter().ConvertFromString("#ff6d17") as SolidColorBrush;
                    newDarkPrimaryColor = new BrushConverter().ConvertFromString("#de5200") as SolidColorBrush;
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Orange.xaml");
                }
                else if (_color == YELLOW)
                {
                    newPrimaryColor = new BrushConverter().ConvertFromString("#ffe817") as SolidColorBrush;
                    newDarkPrimaryColor = new BrushConverter().ConvertFromString("#dec800") as SolidColorBrush;
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Yellow.xaml");
                }
                else if (_color == BLUE)
                {
                    newPrimaryColor = new BrushConverter().ConvertFromString("#1776ff") as SolidColorBrush;
                    newDarkPrimaryColor = new BrushConverter().ConvertFromString("#005bde") as SolidColorBrush;
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");
                }
                else if (_color == GREEN)
                {
                    newPrimaryColor = new BrushConverter().ConvertFromString("#00d900") as SolidColorBrush;
                    newDarkPrimaryColor = new BrushConverter().ConvertFromString("#00ad00") as SolidColorBrush;
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Green.xaml");
                }
                else
                {
                    //do nothing
                }
            }
            else
            {
                backGroundColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#212121"));
                newPrimaryColor = new BrushConverter().ConvertFromString("#3b3b3b") as SolidColorBrush;
                newDarkPrimaryColor = new BrushConverter().ConvertFromString("#323232") as SolidColorBrush;
                App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
                if (_color == RED)
                {
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Red.xaml");
                }
                else if (_color == ORANGE)
                {
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Orange.xaml");
                }
                else if (_color == YELLOW)
                {
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Yellow.xaml");
                }
                else if (_color == BLUE)
                {
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");
                }
                else if (_color == GREEN)
                {
                    App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Green.xaml");
                }
                else
                {
                    //do nothing
                }
            }

            this.MenuBar.Background = newPrimaryColor;
            this.TitleBar.Background = newDarkPrimaryColor;
            this.Background = backGroundColor;
        }
		#endregion
	}
}
