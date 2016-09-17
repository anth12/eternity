using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Eternity.Utilities.Assets;

namespace Eternity.Views.Partials
{
    /// <summary>
    /// Interaction logic for TimeSlotView.xaml
    /// </summary>
    public partial class TimeSlotView : UserControl
    {
        public TimeSlotView()
        {
            InitializeComponent();
        }

        private bool MouseDown { get; set; }
        private double StartHeight { get; set; }
        private Point StartingPoint { get; set; }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseDown = true;
            StartHeight = Block.Height;
            StartingPoint = e.GetPosition(Grid);
        }

        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseDown = false;
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDown)
            {
                var height = StartHeight - (StartingPoint.Y - e.GetPosition(Grid).Y);
                Block.Height = height;
            }
        }

        private void Block_OnMouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = CustomCursors.DownArrow;
        }

        private void Block_OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = CustomCursors.Default;
            MouseDown = false;
        }
    }
}
