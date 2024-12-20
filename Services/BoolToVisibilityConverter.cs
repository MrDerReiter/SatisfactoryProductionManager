﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SatisfactoryProductionManager.Services
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible) return true;
            else return false;
        }
    }
}
