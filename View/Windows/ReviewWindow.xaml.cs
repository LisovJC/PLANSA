using System.Windows;
using System.Windows.Input;

namespace PLANSA.View.Windows
{
    public partial class ReviewWindow : Window
    {
        public ReviewWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
