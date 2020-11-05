﻿using System;
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
using FoodRecipeApp.Classes;
using System.Windows.Controls.Primitives;

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		int _current_page = 1;
		private System.Timers.Timer _timer = new System.Timers.Timer(300);
		BindingList<Dish> _dishes_list;
		BindingList<Paging> _page = new BindingList<Paging>();
		bool _selecting_page = true;
		bool _is_only_fav = false;

		public HomePage()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;
			this.SizeChanged += _size_changed;
			//// Get view for page content (dishes)
			//_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page, filter.SelectedIndex, _is_only_fav);
			//dishesView.ItemsSource = _dishes_list;

			filter.SelectedIndex = 0;
			UpdatePage();
		}

		// Delay time before auto generate items in page
		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Dispatcher.Invoke(() =>
			{
				int totalPages = DishDao.GetTotalPages(this.ActualWidth, this.ActualHeight);
				if (_current_page > totalPages) {
					_current_page = totalPages;
				}
				else
				{
					//do nothing
				}
				UpdateView();
				UpdatePage();
			});
			_timer.Stop();
		}

		//stop time when changing size
		private void _size_changed(object sender, SizeChangedEventArgs e)
		{
			_timer.Stop();
			_timer.Start();
		}


		int _selected_index;
		//ListView handle (get selected item index)
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
			var newScreen = new DishDetailScreen(_dishes_list[_selected_index].Id);
			newScreen.Show();
		}

		//Add item to favourite | Remove item from favourite
		private void favButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_dishes_list[_selected_index].Fav = !_dishes_list[_selected_index].Fav;
			dishesView.Items.Refresh();
			DishDao.WriteUpdatedData(); //write to file
		}

		//next button handle
		private void nextButton_Click(object sender, RoutedEventArgs e)
		{
			if (_current_page < DishDao.GetTotalPages(this.ActualWidth, this.ActualHeight))
			{
				_current_page++;
			}
			else
			{
				_current_page = 1;
			}
			paging.SelectedIndex = _current_page - 1; //update selected page
			// update data for view
			//_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page, filter.SelectedIndex, _is_only_fav);
			//dishesView.ItemsSource = _dishes_list;
		}

		// prev button handle
		private void previousButton_Click(object sender, RoutedEventArgs e)
		{
			if (_current_page > 1)
			{
				_current_page--;
			}
			else
			{
				_current_page = DishDao.GetTotalPages(this.ActualWidth, this.ActualHeight);
			}
			paging.SelectedIndex = _current_page - 1;  //update selected page
			// update data for view
			//_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page, filter.SelectedIndex, _is_only_fav);
			//dishesView.ItemsSource = _dishes_list;
		}

		// select page (combobox handle)
		private void paging_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_selecting_page == true)
			{
				_current_page = paging.SelectedIndex + 1;
				UpdateView();
			}
		}

		private void favoriteToggle_Click(object sender, RoutedEventArgs e)
		{
			_current_page = 1;
			_is_only_fav = favoriteToggle.IsChecked.Value;
			UpdateView();
			UpdatePage();
		}

		private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_current_page = 1;
			UpdateView();
		}

		private void UpdateView()
		{
			_dishes_list = DishDao.GetAll(this.ActualWidth, this.ActualHeight, _current_page, filter.SelectedIndex, _is_only_fav);
			dishesView.ItemsSource = _dishes_list;
		}

		private void UpdatePage()
		{
			int totalPages = DishDao.GetTotalPages(this.ActualWidth, this.ActualHeight);
			viewTotalPages.Text = "/ " + totalPages.ToString();

			// _selecting_page = false avoid updating view in paging_SelectionChanged(sender, e);
			_selecting_page = false;
			_page = Paging.UpdatePage(totalPages);
			paging.ItemsSource = _page;
			paging.SelectedIndex = _current_page - 1;  //update selected page
			paging.Items.Refresh();
			_selecting_page = true;
		}
	}
}
