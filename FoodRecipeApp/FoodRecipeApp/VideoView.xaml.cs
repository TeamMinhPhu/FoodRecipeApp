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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : Page
    {
        string linkVideo = "";
        public VideoView(string video)
        {
            InitializeComponent();
            linkVideo = video;
            myVideo.Source = new Uri(GetEmbedUrlFromLink("Video"));
        }

        private static string GetEmbedUrlFromLink(string link)
        {
            try
            {
                string embedUrl = link.Substring(0, link.IndexOf("&")).Replace("watch?v=", "embed/");
                return embedUrl;
            }
            catch
            {
                return link;
            }
        }
    }
}
