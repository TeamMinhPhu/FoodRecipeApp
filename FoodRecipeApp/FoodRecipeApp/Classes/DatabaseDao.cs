using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

//Classes
using FoodRecipeApp.Classes;

namespace FoodRecipeApp.Classes
{    
    class FoodTypeDao
    {        
        public static List<FoodType> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\DataFiles\\FoodType.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<FoodType>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 2)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                result.Add(new FoodType { typeID = Items[0], typeName = Items[1] });
            }

            return result;
        }
    }

    class DishDao
    {
        public static List<Dish> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;            
            string Path = $"{Folder}Resources\\DataFiles\\Dish.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<Dish>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 6) 
                {
                    continue;
                }
                else { /*Do nothing*/ }

                bool dishFavoriteFromData = false;
                if (Items[5] == "False") 
                {
                    dishFavoriteFromData = false;
                }
                else if (Items[5] == "True")
                {
                    dishFavoriteFromData = true;
                }

                result.Add(new Dish { dishID = Items[0], 
                                      dishName = Items[1], 
                                      dishDescription = Items[2], 
                                      dishIngredient = Items[3],                                        
                                      dishVideo = Items[4], 
                                      dishFavorite = dishFavoriteFromData });
            }

            return result;
        }
    }

    class Dish_TypeDao
    {
        public static List<Dish_Type> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\DataFiles\\Dish_Type.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<Dish_Type>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 2)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                result.Add(new Dish_Type { dishID = Items[0], typeID = Items[1] });
            }

            return result;
        }
    }

    class RecipeDetailDao
    {
        public static List<RecipeDetail> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\DataFiles\\RecipeDetail.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<RecipeDetail>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 3)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                result.Add(new RecipeDetail { dishID = Items[0], step = int.Parse(Items[1]), stepDetail = Items[2] });
            }

            return result;
        }
    }

    class SearchDao
    {
        public static List<Search> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\DataFiles\\Search.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<Search>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 2)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                result.Add(new Search { trueName = Items[0], otherName = Items[1] });
            }

            return result;
        }
    }
}
