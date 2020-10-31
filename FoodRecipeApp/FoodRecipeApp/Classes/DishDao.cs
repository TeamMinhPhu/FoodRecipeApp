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
        /// <param name="filter">0: name asc, 1: name desc, 2: date_asc 3: date desc</param>
        /// <param name="favourite">only favorite</param>
        /// <returns>return BindingList<Dish></returns>
        public static BindingList<Dish> GetAll(double width, double height, int currentPage, int filter, bool favourite)
		{
			int itemsPerPages = GetItemsPerPage(width, height);

            List<Dish> filteredData = new List<Dish>();
            if (favourite == true)
			{
                filteredData = _data.Where(c => c.Fav == true).ToList();
            }
			else
			{
                filteredData = _data.ToList();
            }
       
            switch (filter)
			{
                case 0:
                    filteredData = filteredData.OrderBy(c => c.Name).ToList();
                    break;
                case 1:
                    filteredData = filteredData.OrderByDescending(c => c.Name).ToList();
                    break;
                case 2:
                    filteredData = filteredData.OrderBy(c => c.Date).ToList();
                    break;
                case 3:
                    filteredData = filteredData.OrderByDescending(c => c.Date).ToList();
                    break;
                default:
                    break;
            }

			var viewData = filteredData.Skip((currentPage - 1) * itemsPerPages)
				.Take(itemsPerPages).ToList();

			BindingList<Dish> result = new BindingList<Dish>(viewData);
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
        public static void WriteUpdatedData()
		{
            List<string> fileLines = new List<string>();
            foreach (var item in _data)
			{
                string line = item.Id + "|" + item.Name + "|" + item.Description +
                    "|" + item.Ingredient + "|" + item.LinkVideo + "|" + item.Fav + "|" + item.Date;
                fileLines.Add(line);
            }

            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = $"{folder}Resources\\Data\\Dish.txt";
            File.WriteAllLines(filepath, fileLines);
        }

        public static void Append(Dish newDish)
		{
            _data.Add(newDish);
            string line = newDish.Id + "|" + newDish.Name + "|" + newDish.Description +
                "|" + newDish.Ingredient + "|" + newDish.LinkVideo + "|" + newDish.Fav + "|" + newDish.Date;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = $"{folder}Resources\\Data\\Dish.txt";
            File.AppendAllText(filepath, line + Environment.NewLine);
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

                if (temp.Length == 7)
				{
					Dish dish = new Dish();
					dish.Id = temp[0].Trim();
					dish.Name = temp[1].Trim();
					dish.Description = temp[2].Trim();
					dish.Ingredient = temp[3].Trim();
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

                result.Add(new RecipeDetail { dishID = Items[0], step = int.Parse(Items[1]), stepDetail = Items[2], quantityOfImage = int.Parse(Items[3]) });
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
