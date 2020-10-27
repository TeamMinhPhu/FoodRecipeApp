using System;
using System.Collections.Generic;
using System.ComponentModel;
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
			//IEnumerable<Dish> dataShown = _data.Skip((currentPage - 1) * itemsPerPages).Take(itemsPerPages);
			//BindingList<Dish> result = new BindingList<Dish>(dataShown.ToList());
			BindingList<Dish> result = new BindingList<Dish>();
			int beginIndex, endIndex;
			beginIndex = (currentPage - 1) * itemsPerPages;
			endIndex = (currentPage - 1) * itemsPerPages + itemsPerPages;
			if (endIndex > _data.Count)
			{
				endIndex = _data.Count;
			}
			for (int i = beginIndex; i < endIndex; i++)
			{
				result.Add(_data[i]);
			}
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


		private static BindingList<Dish> ReadData()
		{
			var folder = AppDomain.CurrentDomain.BaseDirectory;
			var filepath = $"{folder}data.txt";
			const string comma =",";
			var separator = new string[] { comma };

			var fileLines = File.ReadAllLines(filepath).Skip((0)).Take(30).ToList();
			BindingList<Dish> result = new BindingList<Dish>();

			foreach (string line in fileLines)
			{
				string[] temp = line.Split(separator, StringSplitOptions.None);
				if (temp.Length >= 4)
				{
					Dish dish = new Dish();
					dish.Id = temp[0].Trim();
					dish.Name = temp[1].Trim();
					dish.Source = "Resources/Images/" + temp[2].Trim();
					dish.Fav = bool.Parse(temp[3].Trim());
					dish.Description = temp[4].Trim();
					result.Add(dish);
				}
				
			}
			return result;
		}

		
	}
}
