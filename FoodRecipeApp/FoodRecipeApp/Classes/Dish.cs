using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.Classes
{
	class Dish : INotifyPropertyChanged
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Source { get; set; }
		public bool Fav { get; set; }
		public string Description { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
