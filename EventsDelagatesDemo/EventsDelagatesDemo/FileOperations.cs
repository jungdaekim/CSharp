﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace EventsDelagatesDemo
{
    internal class FileOperations
    {
        Bitmap newImage;

        // Open file and make a new bitmap
        public Bitmap OpenFile()
        {
            OpenFileDialog ofd=new OpenFileDialog();

            ofd.InitialDirectory = "";
            ofd.Filter = "images| *.jpg;*.png;*.bmp";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                newImage=new Bitmap(Image.FromFile(ofd.FileName));
            }

            return newImage;
        }
    }
}
