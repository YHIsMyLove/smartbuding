﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartConstructionServices.Common
{
    public class NotConverter : IValueConverter
    {
        public NotConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? false : true;
        }
    }
}
