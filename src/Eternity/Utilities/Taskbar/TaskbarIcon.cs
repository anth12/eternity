using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;
using ContextMenu = System.Windows.Forms.ContextMenu;
using MenuItem = System.Windows.Forms.MenuItem;

namespace Eternity.Utilities.Taskbar
{
    public class TaskbarIcon
    {
        private static NotifyIcon icon;
        private static bool closing;

        public static void Initialize()
        {
            var iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Eternity;component/Icon.ico"))?.Stream;

            icon = new NotifyIcon
            {
                Icon = new Icon(iconStream),
                Visible = true,

                ContextMenu = new ContextMenu()
            };
            
            icon.ContextMenu.MenuItems.Add(new MenuItem("Open Eternity", ContextMenuOpen)
            {
                DefaultItem = true
            });
            icon.ContextMenu.MenuItems.Add("-");
            icon.ContextMenu.MenuItems.Add("Quit", ContextMenuQuit);

            icon.DoubleClick += ContextMenuOpen;
        }

        private static void ContextMenuOpen(object sender, EventArgs eventArgs)
        {
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private static void ContextMenuQuit(object sender, EventArgs eventArgs)
        {
            Application.Current.Shutdown();
        }

        public static void MainWindowClosing(CancelEventArgs e)
        {
            if (!closing)
            {
                // When closing using the standard close button, minimize the window
                e.Cancel = true;

                Application.Current.MainWindow.WindowState = WindowState.Minimized;
                Application.Current.MainWindow.Hide();
            }
        }
    }
}
