using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FoodRecipeApp.Classes
{
	class AppColorPalette
	{
		const int RED = 1;
		const int ORANGE = 2;
		const int YELLOW = 3;
		const int BLUE = 4;
		const int GREEN = 5;

		public SolidColorBrush backGroundColor { get; set; }
		public SolidColorBrush newPrimaryColor { get; set; }
		public SolidColorBrush newDarkPrimaryColor { get; set; }

		public AppColorPalette(bool _is_dark_theme, int _color)
		{
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
		}
	}
}
