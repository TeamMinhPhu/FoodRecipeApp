using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using FoodRecipeApp.Classes;

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
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
	}
}
