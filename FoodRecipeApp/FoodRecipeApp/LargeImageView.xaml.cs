using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for LargeImageView.xaml
    /// </summary>
    public partial class LargeImageView : Window
    {
        string myImgLink = "";
        public LargeImageView(string ImgLink)
        {
            InitializeComponent();
            myImgLink = ImgLink;
        }

        private void picViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var Folder = AppDomain.CurrentDomain.BaseDirectory;
            var path = $"{Folder}{myImgLink}";
            var Bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));
            picViewer.Source = Bitmap;
        }
    }
}
