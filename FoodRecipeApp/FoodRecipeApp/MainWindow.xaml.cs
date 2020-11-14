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
                    new Menu() { Content = "Trang chủ", Icon = "Resources/Icons/home.png" },
                    new Menu() { Content = "Thêm món ăn", Icon = "Resources/Icons/plus.png" },
                    new Menu() { Content = "Thông tin", Icon = "Resources/Icons/info.png" },
                    new Menu() { Content = "Cài đặt", Icon = "Resources/Icons/settings.png" },
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

            menuPage.Content = new HomePage();
            menuPage.Visibility = Visibility.Visible;
            aboutUsPage.Content = new InfoPage();
        }

        //Close button (exit window)
		private void closeProgramButton_Click(object sender, RoutedEventArgs e)
		{
            saveConfig();
            Application.Current.Shutdown();
		}

		private void selectedTab(object sender, MouseButtonEventArgs e)
		{
            var item = (sender as ListView).SelectedIndex;
            switch (item)
			{
                case 0:
                    aboutUsPage.Visibility = Visibility.Collapsed;
                    aboutUsPageScrollViewer.Visibility = Visibility.Collapsed;
                    menuPage.Visibility = Visibility.Visible;                                       
                    break;
                case 1:
                    var newScreen = new UploadNewDishScreen( (SolidColorBrush)this.Background, (SolidColorBrush)this.TitleBar.Background);
                    newScreen.Show();
                    this.Close();
                    break;
                case 2:
                    menuPage.Visibility = Visibility.Collapsed;
                    aboutUsPage.Visibility = Visibility.Visible;
                    aboutUsPageScrollViewer.Visibility = Visibility.Visible;
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
            AppColorPalette palette = new AppColorPalette(_is_dark_theme, _color);

            this.MenuBar.Background = palette.newPrimaryColor;
            this.TitleBar.Background = palette.newDarkPrimaryColor;
            this.Background = palette.backGroundColor;
        }
		#endregion
	}
}
