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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//Classes
using FoodRecipeApp.Classes;
using System.Runtime.Remoting.Channels;
using System.IO;
using Microsoft.Win32;

namespace FoodRecipeApp  
{
    /// <summary>
    /// Interaction logic for UploadNewDishScreen.xaml
    /// </summary>   

    public partial class UploadNewDishScreen : Window
    {
        int currentStep = 1;
        string _ImageLink = "";
        string myDishId = "";
        BindingList<StepImage> myViewImgList;
        List<List<StepImage>> SavedImgList;
        List<RecipeDetail> myStepList;
        BindingList<FoodType> _MyTypeDB;
        BindingList<FoodType> _NewListType;


        public UploadNewDishScreen(SolidColorBrush backgroundColor, SolidColorBrush titleColor)
        {
            InitializeComponent();
            MouseDown += Window_MouseDown;
            this.Background = backgroundColor;
            this.TitleBar.Background = titleColor;

            string Folder = AppDomain.CurrentDomain.BaseDirectory;
            var Bitmap = new BitmapImage(new Uri($"{Folder}Resources\\Icons\\picture.png", UriKind.Absolute));
            dishImage.Source = Bitmap;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainScreen = new MainWindow();
            mainScreen.Show();
            this.Close();
        }       

       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myDishId = getId();
            SavedImgList = new List<List<StepImage>>();
            myStepList = new List<RecipeDetail>();
            myViewImgList = new BindingList<StepImage>();

            _MyTypeDB = new BindingList<FoodType>(FoodTypeDao.getData());
            selectTagCB.ItemsSource = _MyTypeDB;

            _NewListType = new BindingList<FoodType>();
            selectedTagList.ItemsSource = _NewListType;

            stepName.Content=$"Bước {currentStep}";
        }

        private string getId()
        {
            string result = "";

            List<Dish> DishDB = DishDao.GetAll().ToList();

            //Get ID in DB
            var myDishIDs = DishDB.Select(x => x.Id).ToList();

            bool Loop = true;
            int count = 0;

            do
            {
                count++;
                string newID = $"DISH{count}";
                //Check if new ID haven't existed in database
                var checkItem = myDishIDs.Where(x => x.Contains(newID)).FirstOrDefault();
                if (checkItem == null)
                {
                    result = newID;
                    Loop = false;
                }
                else { /*Do nothing*/ }
            } while (Loop);

            return result;
        }

        public static bool IsImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            bool result = false;
            if (!String.IsNullOrEmpty(targetExtension))
            {
                List<string> recognisedImageExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png", ".bmp", ".tiff", ".ico" };
                foreach (string extension in recognisedImageExtensions)
                {
                    if (extension.Equals(targetExtension))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else { /*do nothing*/ }
            return result;
        }

        private void dishImage_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void dishImage_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Get file link
                string[] ImageFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                //check image file and show image
                if (ImageFiles[0].Length > 0)
                {
                    _ImageLink = ImageFiles[0];
                    if (IsImageFile(_ImageLink))
                    {
                        var Bitmap = new BitmapImage(new Uri(_ImageLink, UriKind.Absolute));
                        dishImage.Source = Bitmap;
                        ImageHint.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageBox.Show("Không mở được ảnh");
                        _ImageLink = "";
                    }
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                }

            }

        }

        private void addTagBtn_Click(object sender, RoutedEventArgs e)
        {

            int Index = selectTagCB.SelectedIndex;

            if (Index >= 0 || newTagTextBox.Text.Length > 0)
            {
                string _TagName = "";
                string _TagID = "";

                FoodType newType = new FoodType();
                int checkflag = 0; //case 0: user add textbox item and already exist item in list box

                //case 1: User add combobox item
                if (Index >= 0 && newTagTextBox.Text.Length <= 0)
                {
                    _TagName = _MyTypeDB[Index].typeName;
                    _TagID = _MyTypeDB[Index].typeID;
                    newType.typeID = _TagID;
                    newType.typeName = _TagName;
                    checkflag = 1;
                }
                else
                {
                    _TagName = newTagTextBox.Text;
                    //case 0: user add textbox item and already exist item in list box
                    checkflag = 2;
                    foreach (var checkType in _NewListType)
                    {
                        if (checkType.typeName == _TagName)
                        {
                            checkflag = 0;
                            break;
                        }
                        else
                        {
                            checkflag = 2;
                        }
                    }

                    //case 2: user add textbox item and not existed in list box, then check database
                    if (checkflag == 2)
                    {
                        foreach (var checkType in _MyTypeDB)
                        {
                            //if item existed in database return to case 1
                            if (checkType.typeName == _TagName)
                            {
                                checkflag = 1;
                                newType.typeID = checkType.typeID;
                                newType.typeName = _TagName;
                                break;
                            }
                        }
                    }
                }


                //case 1: check list box again, if haven't contain item yet, then add 
                if (checkflag == 1)
                {
                    List<string> _TypeIDDBList = new List<string>();
                    foreach (var checkType in _NewListType)
                    {
                        _TypeIDDBList.Add(checkType.typeID);
                    }

                    var checkItem = _TypeIDDBList.Where(x => x.Contains(newType.typeID)).FirstOrDefault();
                    if (checkItem == null)
                    {
                        _NewListType.Add(newType);
                        selectTagCB.SelectedItem = null;
                        newTagTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Tag đã tồn tại");
                    }
                }
                else if (checkflag == 2)
                {
                    _TagID = getNewTypeID();

                    newType.typeID = _TagID;
                    newType.typeName = _TagName;

                    _MyTypeDB.Add(newType);
                    _NewListType.Add(newType);
                    selectTagCB.SelectedItem = null;
                    newTagTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Tag đã tồn tại");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn tag");

            }

        }

        private string getNewTypeID()
        {
            string result = "";
            int count = 0;
            bool Loop = true;

            var query = _MyTypeDB.Select(x => x.typeID).ToList();

            do
            {
                string newTypeID = $"T{count + 1}";

                var checkItem = query.Where(x => x.Contains(newTypeID)).FirstOrDefault();

                if (checkItem == null)
                {
                    result = newTypeID;
                    Loop = false;
                }
                count++;
            } while (Loop);
            return result;
        }

        private void removeTagBtn_Click(object sender, RoutedEventArgs e)
        {
            int Index = selectedTagList.SelectedIndex;
            if (Index >= 0)
            {
                _NewListType.RemoveAt(Index);
            }
        }

        private void imageInput_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                bool loadImageSucceeded = false;
                //Get file link
                string[] ImageFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                var Temp = new BindingList<StepImage>();
                //check image file and show image
                if (ImageFiles.Length > 0)
                {                    
                    //Case: an image or a folder
                    if (ImageFiles.Length == 1)
                    {
                        //Case: an image
                        if (IsImageFile(ImageFiles[0]))
                        {
                            Temp.Add(new StepImage() { ImageLink = ImageFiles[0] });

                            StepImageHint.Visibility = Visibility.Hidden;
                            loadImageSucceeded = true;
                        }//case: a folder
                        else if (MyFileManager.IsDictionaryExisted(ImageFiles[0]))
                        {
                            var myFiles = System.IO.Directory.GetFiles(ImageFiles[0]);
                            foreach (var myFile in myFiles)
                            {
                                if (IsImageFile(myFile))
                                {
                                    Temp.Add(new StepImage() { ImageLink = myFile });
                                }
                            }

                            if (Temp.Count > 0)
                            {
                                StepImageHint.Visibility = Visibility.Hidden;
                                loadImageSucceeded = true;
                            }
                            else
                            {
                                myViewImgList = Temp;
                                MessageBox.Show("Không tìm thấy file ảnh");
                            }
                        }

                    }
                    else //Case multi files
                    {
                        foreach (var myFile in ImageFiles)
                        {
                            if (IsImageFile(myFile))
                            {
                                Temp.Add(new StepImage() { ImageLink = myFile });
                            }
                        }

                        if (Temp.Count > 0)
                        {
                            StepImageHint.Visibility = Visibility.Hidden;
                            loadImageSucceeded = true;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy file ảnh");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file ảnh");
                }

                if (loadImageSucceeded == true)
                {
                    myViewImgList = Temp;
                    imagePreview.ItemsSource = myViewImgList;
                    imagePreview.Visibility = Visibility.Visible;
                }
                else { /*do nothing*/ }

            }
        }

        private bool checkStep()
        {
            bool breakProcess = false;
            
            do
            {
                //step description
                if (StepDescriptionTextBox.Text == "")
                {
                    MessageBox.Show("Chưa nhập mô tả bước làm");
                    break;
                }

                var newImgList = new List<StepImage>(myViewImgList.ToList());
                if (newImgList.Count <= 0)
                {
                    if (MessageBox.Show("Chưa thêm hình, Bạn có muốn tiếp tục", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SavedImgList.Add(newImgList);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    SavedImgList.Add(newImgList);
                }

                breakProcess = true;
            } while (!breakProcess);
            
            if(breakProcess==true)
            {
                RecipeDetail newStep = new RecipeDetail();
                newStep.dishID = myDishId;
                newStep.step = currentStep;
                newStep.stepDetail = StepDescriptionTextBox.Text;
                newStep.quantityOfImage = myViewImgList.Count;
                myStepList.Add(newStep);
            }

            return breakProcess;
        }

        private void addStepBtn_Click(object sender, RoutedEventArgs e)
        {       
            if(checkStep()==true)
            {            
                StepDescriptionTextBox.Text = "";
                myViewImgList = new BindingList<StepImage>();
                imagePreview.ItemsSource = myViewImgList;
                currentStep++;
                stepName.Content = $"Bước {currentStep}";
            }
            
        }

        private bool checkSaveData()
        {
            bool breakProcess = false;

            do
            {
                if (dishName.Text.Length <= 0)
                {
                    MessageBox.Show("Chưa nhập tên món ăn");
                    break;
                }

                if (dishDescription.Text.Length <= 0)
                {
                    MessageBox.Show("Chưa nhập mô tả món ăn");
                    break;
                }

                if (dishIngredient.Text.Length <= 0)
                {
                    if (MessageBox.Show("Chưa thêm thành phần món ăn, Bạn có muốn tiếp tục", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        break;
                    }
                    else
                    { /*do nothing*/ }
                }

                if (linkVideo.Text.Length <= 0)
                {
                    if (MessageBox.Show("Chưa thêm link video, Bạn có muốn tiếp tục", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        break;
                    }
                    else
                    { /*do nothing*/ }
                }

                if (_ImageLink.Length <= 0)
                {
                    MessageBox.Show("Chưa chọn hình");
                    break;
                }

                if (_NewListType.Count <= 0)
                {
                    MessageBox.Show("Chưa thêm tag món ăn");
                    break;
                }

                if (checkStep() == false)
                {
                    break;
                }

                breakProcess = true;
            } while (!breakProcess);

            return breakProcess;
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {            
            if(checkSaveData()==true)
            {
                //New element
                Dish newDish = new Dish();

                newDish.Id = myDishId;
                newDish.Name = dishName.Text;
                newDish.Description = dishDescription.Text;
                newDish.Ingredient = dishIngredient.Text;
                newDish.LinkVideo = linkVideo.Text;
                newDish.Fav = false;
                newDish.Date = DateTime.UtcNow.ToString();

                var myFolder = AppDomain.CurrentDomain.BaseDirectory;
                var imageFolder = $"{myFolder}Resources\\Images\\";
                var DataFolder = $"{myFolder}Resources\\Data\\";

                //Save Image
                var myFilePath = $"{imageFolder}{newDish.Id}.jpg";

                //Check if existed image having same name then replace
                MyFileManager.CheckExistedFile(myFilePath);
                //Copy image to new folder
                System.IO.File.Copy(_ImageLink, myFilePath);


                //Save dish
                newDish.Description = newDish.Description.Replace(System.Environment.NewLine, @"\r\n");
                newDish.Ingredient = newDish.Ingredient.Replace(System.Environment.NewLine, @"\r\n");

                DishDao.Append(newDish);

                /////////////
                //string newData = $"{newDish.Id}|{newDish.Name}|{newDish.Description}|{newDish.Ingredient}" +
                //    $"|{newDish.LinkVideo}|{newDish.Fav.ToString()}|{newDish.Date}\n";
                //myFilePath = $"{DataFolder}Dish.txt";
                //File.AppendAllText(myFilePath, newData);
                //////////////////////



                //Save dish_type
                //Create list to check new Type
                List<FoodType> foodTypesDB = FoodTypeDao.getData();
                var myTypeIDs = foodTypesDB.Select(x => x.typeID).ToList();

                myFilePath = $"{DataFolder}FoodType.txt";
                MyFileManager.CheckFilePath(myFilePath);

                //Save Foodtype into database if new
                foreach (var item in _NewListType)
                {
                    //Check if ID haven't extisted in database
                    var checkItem = myTypeIDs.Where(x => x.Contains(item.typeID)).FirstOrDefault();
                    //If new then save
                    if (checkItem == null)
                    {
                        string newType = $"{item.typeID}|{item.typeName}\n";
                        File.AppendAllText(myFilePath, newType);
                    }
                }

                //Save Dish_Type into database
                myFilePath = $"{DataFolder}Dish_Type.txt";
                MyFileManager.CheckFilePath(myFilePath);

                foreach (var item in _NewListType)
                {
                    string newDish_Type = $"{newDish.Id}|{item.typeID}\n";
                    File.AppendAllText(myFilePath, newDish_Type);
                }

                //Save step
                myFilePath = $"{DataFolder}RecipeDetail.txt";
                MyFileManager.CheckFilePath(myFilePath);

                for (int i = 0; i < currentStep; i++)
                {
                    if (SavedImgList[i] != null) 
                    {
                        var count = 1;
                        foreach(var item in SavedImgList[i])
                        {
                            var ImgFilePath = $"{imageFolder}{myDishId}_{i + 1}_{count}.jpg";
                            //Copy image to new folder
                            MyFileManager.CheckExistedFile(ImgFilePath);
                            System.IO.File.Copy(item.ImageLink, ImgFilePath);
                            count++;
                        }
                    }

                    var tempDetail = myStepList[i].stepDetail.Replace(System.Environment.NewLine, @"\r\n");
                    string newStep = $"{myDishId}|{i+1}|{tempDetail}|{myStepList[i].quantityOfImage}\n";
                    File.AppendAllText(myFilePath, newStep);
                }

                var mainScreen = new MainWindow();
                mainScreen.Show();
                this.Close();

            }
        }

        private void removeStepBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentStep > 1) 
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa bước làm?", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    currentStep--;
                    StepDescriptionTextBox.Text = myStepList[currentStep - 1].stepDetail;
                    myViewImgList = new BindingList<StepImage>(SavedImgList[currentStep - 1]);
                    imagePreview.ItemsSource = myViewImgList;
                    myStepList.RemoveAt(currentStep - 1);
                    SavedImgList.RemoveAt(currentStep - 1);
                    stepName.Content = $"Bước {currentStep}";
                }
                else { /*do nothing*/ }
            }
            else { /*do nothing*/ }
        }

        private void dishImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                _ImageLink = dlg.FileName;
                if (IsImageFile(_ImageLink))
                {
                    var Bitmap = new BitmapImage(new Uri(_ImageLink, UriKind.Absolute));
                    dishImage.Source = Bitmap;
                    ImageHint.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Không mở được ảnh");
                    _ImageLink = "";
                }
            }
        }

        private void imageInput_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dlg = new OpenFileDialog();
            var Temp = new BindingList<StepImage>();
            if (dlg.ShowDialog() == true)
            {
                var ImageFile = dlg.FileName;
                if (IsImageFile(ImageFile))
                {
                    Temp.Add(new StepImage() { ImageLink = ImageFile });

                    StepImageHint.Visibility = Visibility.Hidden;
                    imagePreview.Visibility = Visibility.Visible;

                    myViewImgList = Temp;
                    imagePreview.ItemsSource = myViewImgList;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy file ảnh");
                }
            }
        }
    }
}
