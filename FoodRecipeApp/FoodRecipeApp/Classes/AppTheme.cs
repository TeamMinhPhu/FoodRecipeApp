using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.Classes
{
	class AppTheme : INotifyPropertyChanged
	{
		public string PrimaryColorTheme { get; set; }
		public string SecondaryColorTheme { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
