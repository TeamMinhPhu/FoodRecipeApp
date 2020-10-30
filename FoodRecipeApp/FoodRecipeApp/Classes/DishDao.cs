using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodRecipeApp.Classes
{
	class DishDao
	{
        // Store data
		static BindingList<Dish> _data = ReadData();

		public int TotalItems { get; set; }

        /// <summary>
        /// get all data
        /// </summary>
        /// <returns>BindingList<Dish></returns>
        public static BindingList<Dish> GetAll()
		{
            return _data;
		}

        /// <summary>
        /// Get data of current page
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="currentPage"></param>
        /// <returns>return BindingList<Dish></returns>
        public static BindingList<Dish> GetAll(double width, double height, int currentPage)
		{
			int itemsPerPages = GetItemsPerPage(width, height);
			var data = _data.Skip((currentPage - 1) * itemsPerPages)
				.Take(itemsPerPages).ToList();

			BindingList<Dish> result = new BindingList<Dish>(data);
			return result;
		}

        /// <summary>
        /// get the amount of items
        /// </summary>
        /// <returns>int</returns>
		public static int GetTotalItems() {
			return _data.Count;
		}

        /// <summary>
        /// Calculate the amount of items can be displayed on one page
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>int</returns>
		public static int GetItemsPerPage(double width, double height)
		{
			int result, row, column;
			row = (int)height / 180;
			column = (int)width / 266;
			result = row * column;
			return result;
		}

        /// <summary>
        /// Calculate the total pages
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>int</returns>
		public static int GetTotalPages(double width, double height)
		{
			int result;
			int totalItems = _data.Count;
			int itemsPerPage = GetItemsPerPage(width,height);
			result = totalItems/itemsPerPage + ((totalItems%itemsPerPage) == 0 ? 0 : 1);
			return result;
		}

        /// <summary>
        /// Write data to file
        /// </summary>
        public static void UpdateData()
		{
            List<string> fileLines = new List<string>();
            foreach (var item in _data)
			{
                string line = item.Id + "|" + item.Name + "|" + item.Description +
                    "|" + item.Ingredient + "|" + item.LinkVideo + "|" + item.Fav;
                fileLines.Add(line);
            }

            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = $"{folder}Resources\\Data\\Dish.txt";
            File.WriteAllLines(filepath, fileLines);
        }

        /// <summary>
        /// Read data from file
        /// </summary>
        /// <returns>BindingList</returns>
		private static BindingList<Dish> ReadData()
		{
			var folder = AppDomain.CurrentDomain.BaseDirectory;
			var filepath = $"{folder}Resources\\Data\\Dish.txt";
			const string comma ="|";
			var separator = new string[] { comma };

            MyFileManager.CheckFilePath(filepath);
			var fileLines = File.ReadAllLines(filepath).ToList();

			BindingList<Dish> result = new BindingList<Dish>();


			foreach (string line in fileLines)
			{
				string[] temp = line.Split(separator, StringSplitOptions.None);
				if (temp.Length == 6)
				{
					Dish dish = new Dish();
					dish.Id = temp[0].Trim();
					dish.Name = temp[1].Trim();
					dish.Description = temp[2].Trim();
					dish.Ingredient = temp[3].Trim();
					dish.LinkVideo = temp[4];
                    //dish.Source = $"Resources/Images/{dish.Id}_Main.jpg"; //+ temp[2].Trim();
                    dish.Source = "Resources/Images/realimage.jpg";
                    dish.Fav = bool.Parse(temp[5].Trim());
					result.Add(dish);
				}
				else
				{
					//do nothing
				}
			}
            
			return result;
		}
    }

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
