using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for FoodPage.xaml
	/// </summary>
	public partial class FoodPage : Page
	{
		static int _page_number;
		static int _items_per_page;
		string[] _data;
		public FoodPage(int pageNumber, int itemsPerPage)
		{
			InitializeComponent();
			_page_number = pageNumber;
			_items_per_page = itemsPerPage;
		}
		BindingList<Dish> _dishes_list;
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_data = GenerateData();
			_dishes_list = DishDao.GetAll();
			dishesView.ItemsSource = _dishes_list;
		}

		class Dish
		{
			public string Name { get; set; }
			public string Source { get; set; }
		}

		class DishDao
		{
			public static BindingList<Dish> GetAll()
			{
				var result = new BindingList<Dish>();
				//{
				//new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 3", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 4", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 5", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 6", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 7", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 8", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 9", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 10", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 11", Source = "Resources/Images/Sora.png"},
				//new Dish() {Name = "Dish 12", Source = "Resources/Images/Sora.png"},
				//};

				string[] myname = GenerateData();
				for (int i = 0; i < _items_per_page; i++)
				{
					int index = _items_per_page * _page_number + i;
					if (index >= myname.Length)
					{
						break;
					}
					result.Add(new Dish() { Name = myname[index], Source = mySource});
				}
				return result;
			}
		}
		static string mySource = "Resources/Images/Sora.jpg";

		public void setdata(int pageNumber, int itemsPerPage)
		{
			_page_number = pageNumber;
			_items_per_page = itemsPerPage;
			_dishes_list = DishDao.GetAll();
			dishesView.ItemsSource = _dishes_list;
		}
		public static string[] GenerateData()
		{
			string[] Name = new string[] {
				"Dish 1",
				"Dish 2",
				"Dish 3",
				"Dish 4",
				"Dish 5",
				"Dish 6",
				"Dish 7",
				"Dish 8",
				"Dish 9",
				"Dish 10",
				"Dish 11",
				"Dish 12",
				"Dish 13",
				"Dish 14",
				"Dish 15",
				"Dish 16",
				"Dish 17",
				"Dish 18",
				"Dish 19",
				"Dish 20",
				"Dish 21",
				"Dish 22",
				"Dish 23",
				"Dish 24",
				"Dish 25",
				"Dish 26",
				"Dish 27",
				"Dish 28",
				"Dish 29",
				"Dish 30",
			};
			return Name;
		}

		private void dishes_View_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var item = (sender as ListView).SelectedIndex;
			MessageBox.Show("You choose: " + _data[_page_number * _items_per_page + item].ToString());
		}
	}
}
