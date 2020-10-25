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
        public static FoodRecipeDBDataContext getConnectionToDB()
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ToString();
            FoodRecipeDBDataContext db = new FoodRecipeDBDataContext(connectString);
            return db;
        }
        class TYPEDBDao
        {
            public static BindingList<TYPEDB> getData()
            {
                FoodRecipeDBDataContext db = getConnectionToDB();

                var result = new BindingList<TYPEDB>(db.TYPEDBs.ToList());
                return result;
            }
        }

        string _ImageLink = "";

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void nextStepBtn_Click(object sender, RoutedEventArgs e)
        {
            //New element
            DISH newDish = new DISH();

            //Get database
            var db = getConnectionToDB();

            //Get existed ID
            var myIDs = db.DISHes.Select(p => p.ID).ToList();

            bool flag = true;

            do
            {
                //Dish Name                
                if (dishName.Text.Length > 0)
                {
                    newDish.DISH_NAME = dishName.Text;
                }
                else
                {
                    MessageBox.Show("Chưa nhập tên món ăn");
                    break;
                }

                //Dish Description
                if (dishDescription.Text.Length > 0)
                {
                    newDish.DISH_DESCRIPTION = dishDescription.Text;
                }
                else
                {
                    MessageBox.Show("Chưa nhập mô tả món ăn");
                    break;
                }

                //Dish Ingredient
                if (dishIngredient.Text.Length > 0)
                {
                    newDish.DISH_INGREDIENT = dishIngredient.Text;
                }


                //Link Video
                if (linkVideo.Text.Length > 0)
                {
                    newDish.DISH_VIDEO = linkVideo.Text;
                }


                //Dish Favorite
                newDish.DISH_FAVORITE = false;


                //Create new ID

                bool flag1 = true;
                int count = 0;

                do
                {
                    count++;
                    string newID = $"DISH{count}";
                    //Check if new ID haven't existed in database
                    var checkItem = myIDs.Where(x => x.Contains(newID)).FirstOrDefault();
                    if (checkItem == null)
                    {
                        newDish.ID = newID;
                        flag1 = false;
                    }
                    else { /*Do nothing*/ }
                } while (flag1);

                //Save dish main image to new folder, ImageID: [DISHID]_Main
                if (_ImageLink.Length > 0)
                {
                    var FileExtension = _ImageLink.Split('.');
                    var Folder = AppDomain.CurrentDomain.BaseDirectory;
                    var newFilePath = $"{Folder}ImagesDB//{newDish.ID}Images//{newDish.ID}_Main.{FileExtension[FileExtension.Length - 1]}";
                    System.IO.Directory.CreateDirectory($"{Folder}ImagesDB//{newDish.ID}Images");
                    //Copy image to new folder
                    System.IO.File.Copy(_ImageLink, newFilePath);
                    newDish.DISH_IMAGE = newFilePath;
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
                    db.DISHes.InsertOnSubmit(newDish);
                    db.SubmitChanges();

                    //Create list to check new Type
                    var myTypeIDs = db.TYPEDBs.Select(p => p.TYPEID).ToList();
                    //Save TypeDB into database
                    foreach (var item in _NewListType)
                    {
                        //Check if ID haven't extisted in database
                        var checkItem = myTypeIDs.Where(x => x.Contains(item.TYPEID)).FirstOrDefault();
                        //If new then save
                        if (checkItem == null)
                        {
                            TYPEDB newType = new TYPEDB() { TYPEID = item.TYPEID, TYPEDB_NAME = item.TYPEDB_NAME };
                            db.TYPEDBs.InsertOnSubmit(newType);
                            db.SubmitChanges();
                        }
                    }

                    //Save DishType into database
                    foreach (var item in _NewListType)
                    {
                        DISH_TYPE newDish_Type = new DISH_TYPE() { ID = newDish.ID, TYPEID = item.TYPEID };
                        db.DISH_TYPEs.InsertOnSubmit(newDish_Type);
                        db.SubmitChanges();
                    }

                    MessageBox.Show("Đã thêm món ăn");
                }
                else
                {
                    MessageBox.Show("Chưa thêm tag món ăn");
                    break;
                }



                break;
            } while (flag);
        }

        private void DishName_GotFocus(object sender, RoutedEventArgs e)
        {
            dishNameHint.Visibility = Visibility.Hidden;
        }

        private void DishName_LostFocus(object sender, RoutedEventArgs e)
        {
            dishNameHint.Visibility = Visibility.Visible;
            if (dishName.Text.Length > 0)
            {
                dishNameHint.Visibility = Visibility.Hidden;
            }
        }

        private void dishDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            dishDesHint.Visibility = Visibility.Hidden;
        }

        private void dishDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            dishDesHint.Visibility = Visibility.Visible;
            if (dishDescription.Text.Length > 0)
            {
                dishDesHint.Visibility = Visibility.Hidden;
            }
        }

        private void dishIngredient_GotFocus(object sender, RoutedEventArgs e)
        {
            dishIngHint.Visibility = Visibility.Hidden;
        }

        private void dishIngredient_LostFocus(object sender, RoutedEventArgs e)
        {
            dishIngHint.Visibility = Visibility.Visible;
            if (dishIngredient.Text.Length > 0)
            {
                dishIngHint.Visibility = Visibility.Hidden;
            }
        }

        private void newTagTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            newTagHint.Visibility = Visibility.Hidden;
        }

        private void newTagTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            newTagHint.Visibility = Visibility.Visible;
            if (newTagTextBox.Text.Length > 0)
            {
                newTagHint.Visibility = Visibility.Hidden;
            }
        }

        private void linkVideo_GotFocus(object sender, RoutedEventArgs e)
        {
            dishVideoHint.Visibility = Visibility.Hidden;
        }

        private void linkVideo_LostFocus(object sender, RoutedEventArgs e)
        {
            dishVideoHint.Visibility = Visibility.Visible;
            if (linkVideo.Text.Length > 0)
            {
                dishVideoHint.Visibility = Visibility.Hidden;
            }
        }

        BindingList<TYPEDB> _MyTypeDB;
        BindingList<TYPEDB> _NewListType;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _MyTypeDB = TYPEDBDao.getData();
            selectTagCB.ItemsSource = _MyTypeDB;
            _NewListType = new BindingList<TYPEDB>();
            selectedTagList.ItemsSource = _NewListType;
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
            bool flag = true;
            int Index = selectTagCB.SelectedIndex;
            do
            {

                if (Index >= 0 || newTagTextBox.Text.Length > 0)
                {
                    string _NewTag = "";
                    string _TagID = "";
                    TYPEDB newType = new TYPEDB();
                    int checkflag = 0;

                    //case 1: User add combobox item
                    if (Index >= 0 && newTagTextBox.Text.Length <= 0)
                    {
                        _NewTag = _MyTypeDB[Index].TYPEDB_NAME;
                        _TagID = _MyTypeDB[Index].TYPEID;
                        newType.TYPEID = _TagID;
                        newType.TYPEDB_NAME = _NewTag;
                        checkflag = 1;
                    }
                    else
                    {
                        _NewTag = newTagTextBox.Text;
                        //case 0: user add textbox item and already exist item in list box
                        foreach (var checkType in _NewListType)
                        {
                            if (checkType.TYPEDB_NAME == _NewTag)
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
                                if (checkType.TYPEDB_NAME == _NewTag)
                                {
                                    checkflag = 1;
                                    newType.TYPEID = checkType.TYPEID;
                                    newType.TYPEDB_NAME = _NewTag;
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
                            _TypeIDDBList.Add(checkType.TYPEID);
                        }

                        var checkItem = _TypeIDDBList.Where(x => x.Contains(newType.TYPEID)).FirstOrDefault();
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

                        newType.TYPEID = _TagID;
                        newType.TYPEDB_NAME = _NewTag;

                        _MyTypeDB.Add(newType);
                        _NewListType.Add(newType);
                        selectTagCB.SelectedItem = null;
                        newTagTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Tag đã tồn tại");
                    }


                    flag = false;
                }
                else
                {
                    MessageBox.Show("Chưa chọn tag");
                    flag = false;
                }
            } while (flag);
        }

        private string getNewTypeID()
        {
            var db = getConnectionToDB();
            string result = "";
            int count = 0;
            bool flag1 = true;

            var myTypeIDs = _MyTypeDB.Select(p => p.TYPEID).ToList();

            do
            {
                string newTypeID = $"T{count + 1}";

                var checkItem = myTypeIDs.Where(x => x.Contains(newTypeID)).FirstOrDefault();

                if (checkItem == null)
                {
                    result = newTypeID;
                    flag1 = false;
                }
                count++;
            } while (flag1);
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
