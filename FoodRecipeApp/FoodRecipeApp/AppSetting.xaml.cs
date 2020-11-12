using FoodRecipeApp.Classes;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Configuration;
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


namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for AppSetting.xaml
	/// </summary>
	public partial class AppSetting : Window
	{
		public delegate void WidthChangedHandler(SolidColorBrush config);
		public event WidthChangedHandler BackGroundThemeChanged;
		public event WidthChangedHandler SecondaryColorChanged;
		public event WidthChangedHandler PrimaryColorChanged;

		private bool _show_splashScreen;
		private bool _is_dark_theme;
		int _color = 0;
		const int RED = 1;
		const int ORANGE = 2;
		const int YELLOW = 3;
		const int BLUE = 4;
		const int GREEN = 5;

		public SolidColorBrush NewBackGroundColor { get; set; }
		public SolidColorBrush NewMenuColor { get; set; }
		public SolidColorBrush NewTitleBarColor { get; set; }


		public AppSetting()
		{
			InitializeComponent();
			loadConfig();
			MouseDown += Window_MouseDown;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				DragMove();
		}

		private void darkTheme_Click(object sender, RoutedEventArgs e)
		{
			if (darkTheme.IsChecked.Value)
			{
				_is_dark_theme = true;
				setColor();
			}
			else
			{
				_is_dark_theme = false;
				setColor();
			}
		}

		private void red_Checked(object sender, RoutedEventArgs e)
		{
			_color = RED;
			setColor();
		}

		private void yellow_Checked(object sender, RoutedEventArgs e)
		{
			_color = YELLOW;
			setColor();
		}

		private void orange_Checked(object sender, RoutedEventArgs e)
		{
			_color = ORANGE;
			setColor();

		}

		private void blue_Checked(object sender, RoutedEventArgs e)
		{
			_color = BLUE;
			setColor();
		}

		private void green_Checked(object sender, RoutedEventArgs e)
		{
			_color = GREEN;
			setColor();
		}

		private void applyButton_Click(object sender, RoutedEventArgs e)
		{
			updateConfig();
			DialogResult = true;
		}
		private void cancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void setColor()
		{
			AppColorPalette palette = new AppColorPalette(_is_dark_theme, _color);

			NewBackGroundColor = palette.backGroundColor;
			NewMenuColor = palette.newPrimaryColor;
			NewTitleBarColor = palette.newDarkPrimaryColor;

			this.Background = NewBackGroundColor;
			this.Border.BorderBrush = NewTitleBarColor;
			this.TitleBar.Background = NewTitleBarColor;

			PrimaryColorChanged?.Invoke(NewMenuColor);
			BackGroundThemeChanged?.Invoke(NewBackGroundColor);
			SecondaryColorChanged?.Invoke(NewTitleBarColor);
		}

		private void updateConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			config.AppSettings.Settings["ShowSplashScreen"].Value = _show_splashScreen.ToString();
			config.AppSettings.Settings["Color"].Value = _color.ToString();
			config.AppSettings.Settings["DarkTheme"].Value = _is_dark_theme.ToString();
			config.Save(ConfigurationSaveMode.Minimal);


		}

		private void loadConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var configSplashScreen = config.AppSettings.Settings["ShowSplashScreen"].Value;
			_show_splashScreen = bool.Parse(configSplashScreen);

			var configDarkTheme = config.AppSettings.Settings["DarkTheme"].Value;
			_is_dark_theme = bool.Parse(configDarkTheme);

			var configColor = config.AppSettings.Settings["Color"].Value;
			_color = int.Parse(configColor);
			
			if (_show_splashScreen == true)
			{
				splashScreen.IsChecked = true;
			}
			else
			{
				splashScreen.IsChecked = false;
			}

			if (_is_dark_theme == true)
			{
				darkTheme.IsChecked = true;
			}
			else
			{
				darkTheme.IsChecked = false;
			}

			switch (_color)
			{
				case 1:
					red.IsChecked = true;
					break;
				case 2:
					orange.IsChecked = true;
					break;
				case 3:
					yellow.IsChecked = true;
					break;
				case 4:
					blue.IsChecked = true;
					break;
				case 5:
					green.IsChecked = true;
					break;
				default:
					orange.IsChecked = true;
					break;
			}
			setColor();
		}

		private void splashScreen_Click(object sender, RoutedEventArgs e)
		{
			if (splashScreen.IsChecked.Value)
			{
				_show_splashScreen = true;
			}
			else
			{
				_show_splashScreen = false;
			}
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
