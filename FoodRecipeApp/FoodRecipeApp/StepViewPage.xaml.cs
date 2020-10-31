using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/////////////////
using FoodRecipeApp.Classes;


namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for StepViewPage.xaml
    /// </summary>
    public partial class StepViewPage : Page
    {        
        string DishID;
        int Step;
        string StepDescription = "";
        int Quantity = 0;
        //Mode 0: load from database
        //Mode 1: preview mode
        int Mode;

        //mode 0
        public StepViewPage(string dishId, int step, int mode)
        {
            InitializeComponent();

            this.DishID = dishId;
            this.Step = step;
            this.Mode = mode;
        }

        //mode 1
        public StepViewPage(string dishId, int step, int mode, string stepDes, int quantityOfImage)
        {
            InitializeComponent();

            this.DishID = dishId;
            this.Step = step;
            this.Mode = mode;
            this.StepDescription = stepDes;
            this.Quantity = quantityOfImage;
        }


        BindingList<RecipeDetail> myStep;
        BindingList<StepImage> myImages;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Mode == 0)
            {
                myStep = new BindingList<RecipeDetail>(RecipeDetailDao.getData(DishID, Step));
                if (myStep.Count == 1)
                {
                    descriptionText.Text = myStep[0].stepDetail;
                    myImages = new BindingList<StepImage>(RecipeDetailDao.getStepImageData(DishID, Step, myStep[0].quantityOfImage));
                    imageList.ItemsSource = myImages;
                }
                else
                {
                    MessageBox.Show("Không tải được thông tin");
                }
            }
            else if (Mode==1)
            {
                descriptionText.Text = StepDescription;
                myImages = new BindingList<StepImage>(RecipeDetailDao.getStepImageData(DishID, Step, Quantity));
                imageList.ItemsSource = myImages;
                
                stepName.Content = $"Step {Step}";
            }
        }
    }
}
