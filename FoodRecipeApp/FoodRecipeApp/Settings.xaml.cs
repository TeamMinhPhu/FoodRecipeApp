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

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Page
	{
		public delegate void WidthChangedHandler(SolidColorBrush config);
		public event WidthChangedHandler ConfigChanged;
		public Settings()
		{
			InitializeComponent();
		}

		private void darkTheme_Click(object sender, RoutedEventArgs e)
		{
			if (darkTheme.IsChecked.Value)
			{
				App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
				var color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#212121"));
				this.Background = color;
				ConfigChanged?.Invoke(color);
			}
			else
			{
				App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
				this.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
			}
		}

		private void red_Checked(object sender, RoutedEventArgs e)
		{
			App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Red.xaml");
		}

		private void yellow_Checked(object sender, RoutedEventArgs e)
		{
			App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Yellow.xaml");

		}

		private void orange_Checked(object sender, RoutedEventArgs e)
		{
			App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Orange.xaml");

		}

		private void blue_Checked(object sender, RoutedEventArgs e)
		{
			App.Current.Resources.MergedDictionaries[2].Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Blue.xaml");

		}

		private void apply_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
