using FoodRecipeApp.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FoodRecipeApp
{
	/// <summary>
	/// Interaction logic for SearchingPage.xaml
	/// </summary>
	public partial class InfoPage : Page
	{
		class ListViewMember
		{
			public string Info { get; set; }
			public string Source { get; set; }
		}

		BindingList<ListViewMember> myMembers;

		public InfoPage()
		{
			InitializeComponent();
		}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
			var Members = new List<Member>(MemberDao.getData());
			myMembers = new BindingList<ListViewMember>();
			foreach(var item in Members)
            {
				myMembers.Add(new ListViewMember { Info = MemberDao.ConvertString(item), Source = item.ImgSource });
            }

			MembersListView.ItemsSource = myMembers;

			var Folder = AppDomain.CurrentDomain.BaseDirectory;
			var path = $"{Folder}Resources/Data/AppInfo.txt";

			MyFileManager.CheckFilePath(path);
			var Data = File.ReadAllText(path);

			AppInfo.Text = Data;
        }
    }
}
