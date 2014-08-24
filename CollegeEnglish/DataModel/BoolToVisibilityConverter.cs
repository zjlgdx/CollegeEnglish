using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CollegeEnglish.DataModel
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility retVal = Visibility.Collapsed;
            bool locVal = (bool)value;
            if (locVal)
                retVal = Visibility.Visible;
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
