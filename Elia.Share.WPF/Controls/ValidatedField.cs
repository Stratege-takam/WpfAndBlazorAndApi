using System.Windows;
using System.Windows.Controls;

namespace Elia.Share.WPF.Controls {
    public class ValidatedField : StackPanel {
        public override void EndInit() {
            var err = new ErrorMessages { MyTarget = (FrameworkElement)Children[0] };
            Children.Add(err);
            base.EndInit();
        }
    }
}
