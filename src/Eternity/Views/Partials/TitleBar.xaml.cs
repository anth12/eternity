using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Eternity.Views.Partials
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();

            //this.Parent.
        }

        public string TitleText { get; set; }

        public static readonly DependencyProperty FoldersProperty = DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(TitleBar), new FrameworkPropertyMetadata(null));

        public ICommand BackCommand
        {
            get { return GetValue(FoldersProperty) as ICommand; }
            set { SetValue(FoldersProperty, value); }
        }
        
    }
}
