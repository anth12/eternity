using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eternity.Properties;
using Eternity.Utilities.Taskbar;
using MahApps.Metro.Controls;
using Application = System.Windows.Application;

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
        
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        private void HomeWindow_OnClosing(object sender, CancelEventArgs e)
        {
            TaskbarIcon.MainWindowClosing(e);
        }
    }
}
