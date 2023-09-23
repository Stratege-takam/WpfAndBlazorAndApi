using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Elia.Share.WPF.Converters {
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ObjectNotEqualToVisibleConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
