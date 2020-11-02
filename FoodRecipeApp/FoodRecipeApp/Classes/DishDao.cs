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
		static BindingList<Dish> _data = ReadData();

		public int TotalItems { get; set; }

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

		public static int GetTotalItems() {
			return _data.Count;
		}

		public static int GetItemsPerPage(double width, double height)
		{
			int result, row, column;
			row = (int)height / 180;
			column = (int)width / 266;
			result = row * column;
			return result;
		}

		public static int GetTotalPages(double width, double height)
		{
			int result;
			int totalItems = _data.Count;
			int itemsPerPage = GetItemsPerPage(width,height);
			result = totalItems/itemsPerPage + ((totalItems%itemsPerPage) == 0 ? 0 : 1);
			return result;
		}


		public static BindingList<Dish> ReadData()
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
				if (temp.Length == 7)
				{
					Dish dish = new Dish();
					dish.Id = temp[0].Trim();
					dish.Name = temp[1].Trim();

                    var Des = temp[2].Replace("\\r\\n", System.Environment.NewLine);
                    dish.Description = Des;

                    var Ing = temp[3].Replace("\\r\\n", System.Environment.NewLine);
                    dish.Ingredient = Ing;

					dish.LinkVideo = temp[4];
                    dish.Source = $"Resources/Images/{dish.Id}.jpg";
                    dish.Fav = bool.Parse(temp[5].Trim());
                    dish.Date = temp[6];
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
            string Path = $"{Folder}Resources\\Data\\FoodType.txt";

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
            string Path = $"{Folder}Resources\\Data\\Dish_Type.txt";

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

    /// <summary>
    /// NewCode
    /// </summary>
    class RecipeDetailDao
    {        
        public static List<RecipeDetail> getData(string Id, int Step)
        {
            var StepDB = getData();
            var result = StepDB.Where(x => x.dishID == Id && x.step == Step).ToList();
            return result;
        }

        public static List<StepImage> getStepImageData(string Id, int Step, int Quantity)
        {
            var result = new List<StepImage>();

            for (int i = 1; i <= Quantity; i++) 
            {
                result.Add(new StepImage() { ImageLink = $"Resources/Images/{Id}_{Step}_{i}.jpg" });
            }

            return result;
        }

        public static List<RecipeDetail> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\Data\\RecipeDetail.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<RecipeDetail>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 4)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                var Des = Items[2].Replace("\\r\\n", System.Environment.NewLine);

                result.Add(new RecipeDetail { dishID = Items[0], step = int.Parse(Items[1]), stepDetail = Des, quantityOfImage = int.Parse(Items[3]) });
            }

            return result;
        }
    }
    ////////////
    

    class SearchDao
    {
        public static List<Search> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\Data\\Search.txt";

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
