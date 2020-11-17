using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FoodRecipeApp.Classes
{
	class DishDao
	{
        // Store data
		static BindingList<Dish> _data = ReadData();
        static List<Dish> filteredData = new List<Dish>();

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

        private static readonly string[] VietNamChar = new string[]
{
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
};
        public static string RemoveSign(string str)
        {
            str = str.Normalize(NormalizationForm.FormC);
            //replace unicode char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }

        public static BindingList<Dish> GetAll(double width, double height, int currentPage, int filter, bool favourite, string search)
        {
            int itemsPerPages = GetItemsPerPage(width, height);



            BindingList<Dish> searchedResult = new BindingList<Dish>();
            if (search == "")
			{
                searchedResult = _data;
			}
            else
			{
                search = RemoveSign(search);
                searchedResult = new BindingList<Dish>(_data.Where(c => RemoveSign(c.Name).ToLower().Normalize(NormalizationForm.FormC)
                                                                    .Contains(RemoveSign(search).ToLower().Normalize(NormalizationForm.FormC) )
                                                                     )
                                                                    .ToList()) ;
			}

            if (favourite == true)
            {
                filteredData = searchedResult.Where(c => c.Fav == true).ToList();
            }
            else
            {
                filteredData = searchedResult.ToList();
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
			row = (int)height / 230;
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
			int totalItems = filteredData.Count;
			int itemsPerPage = GetItemsPerPage(width,height);
            result = totalItems / itemsPerPage + ((totalItems % itemsPerPage) == 0 ? 0 : 1);
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
                item.Description = item.Description.Replace(System.Environment.NewLine, @"\r\n");
                item.Ingredient = item.Ingredient.Replace(System.Environment.NewLine, @"\r\n");

                string line = item.Id + "|" + item.Name + "|" + item.Description +
                    "|" + item.Ingredient + "|" + item.LinkVideo + "|" + item.Fav + "|" + item.Date;
                fileLines.Add(line);
            }

            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = $"{folder}Resources\\Data\\Dish.txt";
            File.WriteAllLines(filepath, fileLines);
        }

        /// <summary>
        /// Add new dish to database
        /// </summary>
        /// <param name="newDish"></param>
        public static void Append(Dish newDish)
		{
            newDish.Source = $"Resources/Images/{newDish.Id}.jpg";
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

    class MemberDao
    {
        public static List<Member> getData()
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            string Path = $"{Folder}Resources\\Data\\Members.txt";

            MyFileManager.CheckFilePath(Path);

            var Data = File.ReadAllLines(Path);
            var result = new List<Member>();

            foreach (var Line in Data)
            {
                var Items = Line.Split('|');

                if (Items.Count() != 6)
                {
                    continue;
                }
                else { /*Do nothing*/ }

                result.Add(new Member { Name = Items[0], ID = Items[1], BirthDay = Items[2], School = Items[3], Faculty = Items[4], Class = Items[5], ImgSource = $"Resources\\Images\\{Items[1]}.jpg" });
            }

            return result;
        }

        public static string ConvertString(Member Mem)
        {
            string result = "";
            string writeline = "\r\n";

            result = $"Họ và tên: {Mem.Name}{writeline}MSSV: {Mem.ID}{writeline}Ngày sinh: {Mem.BirthDay}{writeline}Trường: {Mem.School}{writeline}Khoa: {Mem.Faculty}{writeline}Lớp: {Mem.Class}{writeline}";

            return result;
        }
    }
}
