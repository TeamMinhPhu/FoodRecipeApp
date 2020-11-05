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
    /// Interaction logic for DishDetailScreen.xaml
    /// </summary>
    public partial class DishDetailScreen : Window
    {
        public event System.Windows.RoutedEventHandler Selected;

        string myDishId = "";
        Dish myDish;
        string myMainImg = "";
        ListViewData myListStep;
        BindingList<FoodType> myListType;
        BindingList<RecipeDetail> StepDetail;
        BindingList<ButtonList> myBtnList;

        //Carousel Element
        private int currentElement = 0;
        private int TotalElement = 1;

        class ListViewData : INotifyPropertyChanged
        {
            public ObservableCollection<List<StepImage>> ImageList { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        class ButtonList : INotifyPropertyChanged
        {
            public string buttonName { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        public DishDetailScreen(string dishId)
        {
            InitializeComponent();
            myDishId = dishId;
            MouseDown += Window_MouseDown;
            CarouselBtnSkip.PreviewMouseLeftButtonDown += ListViewItem_MouseLeftButtonDown;
            CarouselBtnSkip.PreviewMouseLeftButtonUp += ListViewItem_MouseLeftButtonDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Dish
            var DishDB = new List<Dish>(DishDao.ReadData());
            myDish = DishDB.Where(x => x.Id == myDishId).FirstOrDefault();

            myListStep = new ListViewData();             
            myListStep.ImageList = new ObservableCollection<List<StepImage>>();

            //Step
            var StepDB = RecipeDetailDao.getData();
            StepDetail = new BindingList<RecipeDetail>(StepDB.Where(x => x.dishID == myDishId).ToList());
            TotalElement += StepDetail.Count;

            myBtnList = new BindingList<ButtonList>();
            myBtnList.Add(new ButtonList() { buttonName = "Main" });
            //if(myDish.LinkVideo!="")
            //{
            //    myBtnList.Add(new ButtonList() { buttonName = "Video" });
            //}

            ///           
            for (int i = 0; i < StepDetail.Count; i++) 
            {
                var ListImgTemp = RecipeDetailDao.getStepImageData(myDishId, i + 1, StepDetail[i].quantityOfImage);
                myBtnList.Add(new ButtonList() { buttonName = $"Step {i + 1}" });
                myListStep.ImageList.Add(ListImgTemp);
            }

            stepDetailView.ItemsSource = StepDetail;
            this.DataContext = myListStep;
            CarouselBtnSkip.ItemsSource = myBtnList;            

            //Food Type
            myListType = new BindingList<FoodType>();
            var DishTypeDB = Dish_TypeDao.getData();
            var myDishType = new List<string>(DishTypeDB.Where(x => x.dishID == myDishId).Select(x => x.typeID).ToList());
            var TypeDB = FoodTypeDao.getData();
            foreach (var item in myDishType)
            {
                var temp = TypeDB.Where(x => x.typeID == item).FirstOrDefault();
                myListType.Add(temp);
            }

            DishNameLabel.Content = myDish.Name;
            dishDesTextBlock.Text = myDish.Description;
            dishIngTextBlock.Text = myDish.Ingredient;
            var Folder = AppDomain.CurrentDomain.BaseDirectory;
            var path = $"{Folder}Resources\\Images\\{myDishId}.jpg";
            var Bitmap = new BitmapImage(new Uri(path, UriKind.Absolute));
            dishImage.Source = Bitmap;
            tagList.ItemsSource = myListType;

            //videoScreen.Content = new VideoView(myDish.LinkVideo);
            

           
        }


        private void AnimateCarousel()
        {
            Storyboard storyboard = (this.Resources["CarouselStoryboard"] as Storyboard);
            DoubleAnimation animation = storyboard.Children.First() as DoubleAnimation;
            animation.Duration = TimeSpan.FromSeconds(0.5);
            animation.To = -this.Width * currentElement;
            storyboard.Begin();
        }


        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentElement > 0)
            {
                currentElement--;
                AnimateCarousel();
            }
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {            
            if (currentElement < TotalElement - 1)
            {
                currentElement++;
                AnimateCarousel();
            }
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var Index = CarouselBtnSkip.SelectedIndex;
            if (Index >= 0 && Index < TotalElement && Index != currentElement)
            { 
                currentElement = CarouselBtnSkip.SelectedIndex;
                AnimateCarousel();
            }

            
        }
    }
}
