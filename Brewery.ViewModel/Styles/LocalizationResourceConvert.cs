using System;
using System.Windows.Data;

namespace Brewery.ViewModel.Styles
{
    public class LocalizationResourceConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           var  lang = value as string;
               
           lang = string.IsNullOrEmpty(lang) || lang == "fr" ? "" : $"{lang}.";
            
            return $"../Resources/Home/HomeTranslationResource.{lang}xaml";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
