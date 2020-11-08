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

			NewBackGroundColor = backGroundColor;
			NewMenuColor = newPrimaryColor;
			NewTitleBarColor = newDarkPrimaryColor;

			this.Background = NewBackGroundColor;

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
			var configSplashScreen = ConfigurationManager.AppSettings["ShowSplashScreen"];
			_show_splashScreen = bool.Parse(configSplashScreen);

			var configDarkTheme = ConfigurationManager.AppSettings["DarkTheme"];
			_is_dark_theme = bool.Parse(configDarkTheme);

			var configColor = ConfigurationManager.AppSettings["Color"];
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
	}
}
