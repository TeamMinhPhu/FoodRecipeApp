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
        public string dishID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredient { get; set; }
        public bool Favorite { get; set; }
        public string LinkVideo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class Dish_Type : INotifyPropertyChanged
    {
        public string dishID { get; set; }
        public string typeID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class FoodType : INotifyPropertyChanged
    {
        public string typeID { get; set; }
        public string typeName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class RecipeDetail : INotifyPropertyChanged
    {
        public string dishID { get; set; }
        public int step { get; set; }
        public string stepDetail { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    class Search : INotifyPropertyChanged
    {
        public string trueName { get; set; }        
        public string otherName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
