using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string Description { get; set; }
        public string Ingredient { get; set; }
        public bool Fav { get; set; }
        public string LinkVideo { get; set; }
        public string Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
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

    /// <summary>
    /// NewCode
    /// </summary>
    class RecipeDetail
    {
        public string dishID { get; set; }
        public int step { get; set; }
        public string stepDetail { get; set; }
        public int quantityOfImage { get; set; }

    }
    
    class StepImage
    {
        public string ImageLink { get; set; }
    }
    

    class Member
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string BirthDay { get; set; }
        public string Faculty { get; set; }
        public string Class { get; set; }
        public string School { get; set; }
        public string ImgSource { get; set; }
    }

}
