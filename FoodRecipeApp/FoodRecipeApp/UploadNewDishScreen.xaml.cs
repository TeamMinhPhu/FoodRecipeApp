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


//Drawing
using System.Drawing;
using System.Drawing.Imaging;

//Classes
using FoodRecipeApp.Classes;
using System.Runtime.Remoting.Channels;
using System.IO;

//SQL
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for UploadNewDishScreen.xaml
    /// </summary>
    public partial class UploadNewDishScreen : Window
    {
        public UploadNewDishScreen()
        {
            InitializeComponent();
        }
    }

    public partial class UploadNewDishScreen : Window
    {
        //public static FoodRecipeDBDataContext getConnectionToDB()
        //{
        //    string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ToString();
        //    FoodRecipeDBDataContext db = new FoodRecipeDBDataContext(connectString);
        //    return db;
        //}

        
        string _ImageLink = "";

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void nextStepBtn_Click(object sender, RoutedEventArgs e)
        {
            //New element
            Dish newDish = new Dish();
            List<Dish> DishDB = DishDao.getData();

            //Get database
            //var db = getConnectionToDB();

            //Get existed ID
            var myDishIDs = DishDB.Select(x => x.dishID).ToList();

            bool breakProcess = false;

            do
            {
                //Dish Name                
                if (dishName.Text.Length > 0)
                {
                    newDish.Name = dishName.Text;
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên món ăn");
                    break;
                }

                //Dish Description
                if (dishDescription.Text.Length > 0)
                {
                    newDish.Description = dishDescription.Text;
                }
                else
                {
                    MessageBox.Show("Chưa nhập mô tả món ăn");
                    break;
                }

                //Dish Ingredient
                if (dishIngredient.Text.Length > 0)
                {
                    newDish.Ingredient = dishIngredient.Text;
                }


                //Link Video
                if (linkVideo.Text.Length > 0)
                {
                    newDish.LinkVideo = linkVideo.Text;
                }


                //Dish Favorite
                newDish.Favorite = false;


                //Create new dish ID

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
                        newDish.dishID = newID;
                        Loop = false;
                    }
                    else { /*Do nothing*/ }
                } while (Loop);

                //Save dish main image to new folder, ImageID: [DISHID]_Main
                if (_ImageLink.Length > 0)
                {
                    var Folder = AppDomain.CurrentDomain.BaseDirectory;
                    var newFilePath = $"{Folder}Resources\\Images\\{newDish.dishID}_Main.jpg";

                    //Check if existed image having same name then replace
                    MyFileManager.CheckExistedFile(newFilePath);
                    //Copy image to new folder
                    System.IO.File.Copy(_ImageLink, newFilePath);                    
                }
                else
                {
                    MessageBox.Show("Chưa chọn hình");
                    break;
                }

                //Dish Type
                int Temp = _NewListType.Count;

                if (Temp > 0)
                {
                    //Save Dish into database
                    var Folder = AppDomain.CurrentDomain.BaseDirectory;
                    var newFilePath = $"{Folder}Resources\\DataFiles\\Dish.txt";

                    newDish.Description = newDish.Description.Replace(System.Environment.NewLine, @"\r\n");
                    newDish.Ingredient = newDish.Ingredient.Replace(System.Environment.NewLine, @"\r\n");

                    string newData=$"{newDish.dishID}|{newDish.Name}|{newDish.Description}|{newDish.Ingredient}|{newDish.LinkVideo}|{newDish.Favorite.ToString()}\n";
                    File.AppendAllText(newFilePath, newData);

                    //Create list to check new Type
                    List<FoodType> foodTypesDB = FoodTypeDao.getData();
                    var myTypeIDs = foodTypesDB.Select(x => x.typeID).ToList();

                    newFilePath = $"{Folder}Resources\\DataFiles\\FoodType.txt";
                    MyFileManager.CheckFilePath(newFilePath);

                    //Save Foodtype into database if new
                    foreach (var item in _NewListType)
                    {
                        //Check if ID haven't extisted in database
                        var checkItem = myTypeIDs.Where(x => x.Contains(item.typeID)).FirstOrDefault();
                        //If new then save
                        if (checkItem == null)
                        {
                            string newType = $"{item.typeID}|{item.typeName}\n";
                            File.AppendAllText(newFilePath, newType);
                        }
                    }

                    //Save Dish_Type into database
                    newFilePath = $"{Folder}Resources\\DataFiles\\Dish_Type.txt";
                    MyFileManager.CheckFilePath(newFilePath);

                    foreach (var item in _NewListType)
                    {
                        string newDish_Type = $"{newDish.dishID}|{item.typeID}\n";
                        File.AppendAllText(newFilePath, newDish_Type);
                    }                

                    MessageBox.Show("Đã thêm món ăn");
                }
                else
                {
                    MessageBox.Show("Chưa thêm tag món ăn");
                    break;
                }
                break;
            } while (breakProcess);
        }


        BindingList<FoodType> _MyTypeDB;
        BindingList<FoodType> _NewListType;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Folder = AppDomain.CurrentDomain.BaseDirectory;

            //Check file path
            string newFolder = $"{Folder}Resources\\DataFiles";
            MyFileManager.CheckDictionary(newFolder);

            newFolder = $"{Folder}Resources\\Images";
            MyFileManager.CheckDictionary(newFolder);

            newFolder = $"{Folder}Resources\\Icons";
            MyFileManager.CheckDictionary(newFolder);

            _MyTypeDB = new BindingList<FoodType>(FoodTypeDao.getData());
            selectTagCB.ItemsSource = _MyTypeDB;           

            _NewListType = new BindingList<FoodType>();
            selectedTagList.ItemsSource = _NewListType;

            List<Dish> myDish = DishDao.getData();
            Debug.WriteLine(myDish[0].Description);
        }

        public static bool IsImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            if (String.IsNullOrEmpty(targetExtension))
            {
                return false;
            }
            else
            {
                targetExtension = "*" + targetExtension.ToLowerInvariant();
            }

            List<string> recognisedImageExtensions = new List<string>();

            foreach (System.Drawing.Imaging.ImageCodecInfo imageCodec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                recognisedImageExtensions.AddRange(imageCodec.FilenameExtension.ToLowerInvariant().Split(";".ToCharArray()));

            foreach (string extension in recognisedImageExtensions)
            {
                if (extension.Equals(targetExtension))
                {
                    return true;
                }
            }
            return false;
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
                    //case 2: create new item in database and add to list box
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
            //var db = getConnectionToDB();
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
    }
}
