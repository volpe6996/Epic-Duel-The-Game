﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicDuelTheGame.Models
{
    public class HeroImageModel /*: INotifyPropertyChanged*/
    {
        //private string _imagePath;
        //public string ImagePath
        //{
        //    get { return _imagePath; }
        //    set
        //    {
        //        if (_imagePath != value)
        //        {
        //            _imagePath = value;
        //            OnPropertyChanged(nameof(ImagePath));
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        public string HeroName { get; set; }
        public string Path { get; set; }
    }
}
