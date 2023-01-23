using System.Windows;
using System.Windows.Controls;

namespace L4D2DevTools.Themes.Controls
{
    public class ButtonIcon : Button
    {
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(ButtonIcon), new PropertyMetadata(null));
    }
}
