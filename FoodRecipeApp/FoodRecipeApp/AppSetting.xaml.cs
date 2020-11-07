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
using System.Windows.Shapes;

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for AppSetting.xaml
	/// </summary>
	public partial class AppSetting : Window
	{
		public delegate void WidthChangedHandler(SolidColorBrush config);
		public event WidthChangedHandler ConfigChanged;

		public SolidColorBrush NewColor { get; set; }
		SolidColorBrush _oldColor;

		public AppSetting(SolidColorBrush color)
		{
			InitializeComponent();
			NewColor = _oldColor = color;
		}

		private void darkTheme_Click(object sender, RoutedEventArgs e)
		{
			if (darkTheme.IsChecked.Value)
			{
				App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");
				SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#212121"));
				this.Background = color;
				NewColor = color;
				ConfigChanged?.Invoke(color);
			}
			else
			{
				App.Current.Resources.MergedDictionaries[0].Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
				SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffffff"));
				this.Background = color;
				NewColor = color;
				ConfigChanged?.Invoke(color);
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
			
			DialogResult = true;
		}
	}
}
