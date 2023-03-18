using System;
using System.Collections.Generic;
using System.Text;

namespace PicApp.Models
{
    public class Picture
    {
        public string Name { get; set; }
        public string FullPath { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is Picture picture)
            {
                if (Name != picture.Name) return false;
                if (FullPath != picture.FullPath) return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
