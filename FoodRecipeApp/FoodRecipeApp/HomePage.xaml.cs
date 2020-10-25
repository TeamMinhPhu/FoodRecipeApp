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

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		int _current_page = 0;
		FoodPage _content = new FoodPage(0, 1);
		private System.Timers.Timer _timer = new System.Timers.Timer(300);

		int _items_per_pages;	
		public HomePage()
		{
			InitializeComponent();
			foodPage.Content = _content;
		}

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() =>
			{
				_items_per_pages = itemsPerPage();
				if (_current_page > getTotalPages())
				{
					_current_page = getTotalPages();
				}
				_content.setdata(_current_page, _items_per_pages);
			});
			_timer.Stop();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_items_per_pages = itemsPerPage();
			_content.setdata(_current_page, _items_per_pages);
			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;
			this.SizeChanged += _size_changed;
		}

		private void _size_changed(object sender, SizeChangedEventArgs e)
		{
			_timer.Stop();
			_timer.Start();
		}

		private int itemsPerPage()
		{
			int result, row, column;
			column = (int)_content.ActualWidth / 150;
			row = (int)_content.ActualHeight / 140;
			result =  row * column;
			return result;
		}

		const int _database_length = 30; //test database
		private int getTotalPages()
		{
			int items = _items_per_pages;
			if (items == 0)
			{
				items = 1;
			}
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
			_content.setdata(_current_page, _items_per_pages);
		}
	}
}
