using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.Classes
{
    class Dish
    {
        public string dishID { get; set; }
        public string dishName { get; set; }
        public string dishDescription { get; set; }
        public string dishIngredient { get; set; }
        public bool dishFavorite { get; set; }
        public string dishVideo { get; set; }

    }

    class Dish_Type
    {
        public string dishID { get; set; }
        public string typeID { get; set; }
    }

    class FoodType
    {
        public string typeID { get; set; }
        public string typeName { get; set; }
    }

    class RecipeDetail
    {
        public string dishID { get; set; }
        public int step { get; set; }
        public string stepDetail { get; set; }

    }

    class Search
    {
        public string trueName { get; set; }        
        public string otherName { get; set; }

    }
}
