using System.Diagnostics;
using System.Linq;
using MahApps.Metro.Controls;

namespace Eternity.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : MetroWindow
    {
        public HomeWindow()
        {
            InitializeComponent();

            var processes = Process.GetProcesses();

            var a = string.Join("\n", processes.Select(p => p.ProcessName + "\t\t" + p.MainWindowTitle));
            var b = processes.Where(p => p.ProcessName.Contains("devenv")).ToList();
            
        }

    }
}
