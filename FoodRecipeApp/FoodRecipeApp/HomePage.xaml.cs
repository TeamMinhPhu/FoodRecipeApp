using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
=======
using System.Threading.Tasks;
>>>>>>> minh
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
<<<<<<< HEAD
using System.Timers;
using FoodRecipeApp.Classes;
=======
>>>>>>> minh

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
<<<<<<< HEAD
		int _current_page = 1;
		private System.Timers.Timer _timer = new System.Timers.Timer(300);


		public HomePage()
		{
			InitializeComponent();
		}
		BindingList<Dish> _dishes_list;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;
			this.SizeChanged += _size_changed;
			_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page);
			dishesView.ItemsSource = _dishes_list;
		}

		// Delay time before auto generate items in page
		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() =>
			{
				_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page);
				dishesView.ItemsSource = _dishes_list;
			});
			_timer.Stop();
		}

		//stop time when changing size
		private void _size_changed(object sender, SizeChangedEventArgs e)
		{
			_timer.Stop();
			_timer.Start();
		}

		//Click "Next"
		private void DEMONEXT_Click(object sender, RoutedEventArgs e)
		{
			if (_current_page < DishDao.GetTotalPages(this.ActualWidth, this.ActualHeight))
			{
				_current_page++;
			}
			else
			{
				_current_page = 1;
			}
			_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page);
			dishesView.ItemsSource = _dishes_list;

		}

		int _selected_index;
		private void dishesListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			try
			{
				_selected_index = dishesView.SelectedIndex;
			}
			catch
			{
				//do nothing
			}
		}

		//Click item in page -> show detail (carousel)
		private void dishView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(_dishes_list[_selected_index].Name);
		}

		//Add item to favourite | Remove item from favourite
		private void favButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

			_dishes_list[_selected_index].Fav = !_dishes_list[_selected_index].Fav;
			dishesView.Items.Refresh();
		}
=======
		public HomePage()
		{
			InitializeComponent();
	
		}

		BindingList<Dish> _dishes_list;
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
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
				var result = new BindingList<Dish>()
				{
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 1", Source = "Resources/Images/Sora.png"},
					new Dish() {Name = "Dish 2", Source = "Resources/Images/Sora.png"},
				};
				return result;
			}
		}


		//class Student // DTO = Data transfer object - Entity
		//{
		//    public string Fullname { get; set; }
		//    public string Avatar { get; set; }
		//}

		//// DAO - Data access object

		//class StudentDao
		//{
		//    public static BindingList<Student> GetAll()
		//    {
		//        var result = new BindingList<Student>()
		//        {
		//            new Student() { Fullname="Chu Tùng Nhân", Avatar="/Images/avatar01.jpg" },
		//            new Student() { Fullname="Nguyen Ánh Du", Avatar="/Images/avatar02.jpg" },
		//            new Student() { Fullname="Lều Bách Khánh", Avatar="/Images/avatar03.jpg" },
		//            new Student() { Fullname="Thiều Duy Hành", Avatar="/Images/avatar04.jpg" },
		//            new Student() { Fullname="Nhiệm Băng Đoan", Avatar="/Images/avatar05.jpg" },
		//            new Student() { Fullname="Mang Đình Từ", Avatar="/Images/avatar06.jpg" },
		//            new Student() { Fullname="Bùi Tuyền", Avatar="/Images/avatar07.jpg" },
		//            new Student() { Fullname="Triệu Triều Hải", Avatar="/Images/avatar08.jpg" },
		//            new Student() { Fullname="Tạ Đoan Huệ", Avatar="/Images/avatar09.jpg" },
		//            new Student() { Fullname="Đào Sương Thư", Avatar="/Images/avatar10.jpg" }
		//        };

		//        return result;
		//    }
		//}

		//BindingList<Student> _list = new BindingList<Student>();



>>>>>>> minh
	}
}
