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
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		public HomePage()
		{
			InitializeComponent();
			foodPage.Content = _content;
		}
		FoodPage _content = new FoodPage(0,1);
		int _current_page = 0;
		//BindingList<Dish> _dishes_list;
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//_dishes_list = DishDao.GetAll();
			//dishesView.ItemsSource = _dishes_list;
			//foodPage.Content = new FoodPage();

			_content = new FoodPage(_current_page, itemsPerPage());
			foodPage.Content = _content;
		}

		private int itemsPerPage()
		{
			int result, row, column;
			column = (int)(_content.ActualWidth) / 160;
			row = (int)(_content.ActualHeight) / 150;
			result =  row * column;
			return result;
		}

		const int _database_length = 30; //test database
		private int getTotalPages()
		{
			int items = itemsPerPage();
			int result;
			result = _database_length / items;
			if (result * items == _database_length && result > 0)
			{
				result--;
			}
			return result;
		}


		private void DEMONEXT_Click(object sender, RoutedEventArgs e)
		{
	
			if (_current_page < getTotalPages())
			{

				_current_page++;
			}
			else
			{
				_current_page = 0;
			}
			_content = new FoodPage(_current_page, itemsPerPage());
			foodPage.Content = _content;
		}
		//class Dish
		//{
		//	public string Name { get; set; }
		//	public string Source { get; set; }
		//}

		//class DishDao
		//{
		//	public static BindingList<Dish> GetAll()
		//	{
		//		var result = new BindingList<Dish>()
		//		{
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
		//			new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
		//		};
		//		return result;
		//	}
		//}
	}
}
