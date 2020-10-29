﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FoodRecipeApp.Classes
{
    class MyFileManager
    {
        public static void CheckFilePath(string filepath)
        {
            if (!File.Exists(filepath))
            {
                var onCreate = File.Create(filepath);
                onCreate.Close();
            }
            else { /*Do nothing*/ }
        }

        public static void CheckDictionary(string folder)
        {
            //Auto check if folder existed
            System.IO.Directory.CreateDirectory(folder);
        }

        public static void CheckExistedFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
