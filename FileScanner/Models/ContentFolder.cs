using System;
using System.Collections.Generic;
using System.Text;

namespace FileScanner.Models
{
    public class ContentFolder
    {
        private string name;

        public string Name
        {
            get { return name;}
            set { name = value;}
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public ContentFolder(string nm, string Img)
        {
            Name = nm;
            Image = Img;
        }
    }
}
