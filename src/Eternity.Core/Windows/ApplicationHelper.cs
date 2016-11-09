using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Eternity.Core.Models;

namespace Eternity.Core.Windows
{
    public class ApplicationHelper
    {
        #region External imports

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();


        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        // Delegate to filter which windows to include 
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        #endregion

        /// <summary> Get the text for the window pointed to by hWnd </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            int size = GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return string.Empty;
        }

        public WindowsApplication GetApplication(int processId)
        {
            var process = Process.GetProcessById(processId);

            var result = new WindowsApplication
            {
                ProcessId = processId,
                StarTime = process.StartTime,
                ProcessName = process.ProcessName,
                WindowName = process.MainWindowTitle
            };

            return result;
        }

        public static IDictionary<IntPtr, string> GetOpenWindows()
        {
            var shellWindow = GetShellWindow();
            var windows = new Dictionary<IntPtr, string>();

            EnumWindows((IntPtr wnd, IntPtr param) =>
            {
                if (wnd == shellWindow) return true;
                if (!IsWindowVisible(wnd)) return true;

                int length = GetWindowTextLength(wnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(wnd, builder, length + 1);

                windows[wnd] = builder.ToString();
                return true;
            }, IntPtr.Zero);

            return windows;
        }


        public static List<WindowsApplication> GetOpenApplications()
        {
            var shellWindow = GetShellWindow();
            var windows = new List<WindowsApplication>();

            EnumWindows((IntPtr wnd, IntPtr param) =>
            {
                if (wnd == shellWindow) return true;
                if (!IsWindowVisible(wnd)) return true;

                int length = GetWindowTextLength(wnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(wnd, builder, length + 1);

                uint processId;
                GetWindowThreadProcessId(wnd, out processId);

                var process = Process.GetProcessById((int) processId);
                
                windows.Add(new WindowsApplication
                {
                    ProcessId = wnd.ToInt32(),
                    ProcessName = process.ProcessName,
                    WindowName = builder.ToString(),
                    StarTime = process.StartTime
                });
                
                return true;
            }, IntPtr.Zero);

            var forgroundProcessId = GetForegroundWindow().ToInt32();
            var forgroundApplication = windows.FirstOrDefault(p => p.ProcessId == forgroundProcessId);
            if (forgroundApplication != null)
                forgroundApplication.HasFocus = true;

            return windows;
        }
        
        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter">
        /// A delegate that returns true for windows
        /// that should be returned and false for windows that should
        /// not be returned 
        /// </param>
        public static IEnumerable<IntPtr> FindWindows(EnumWindowsProc filter)
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();

            EnumWindows((IntPtr wnd, IntPtr param) =>
            {
                if (filter(wnd, param))
                {
                    // only add the windows that pass the filter
                    windows.Add(wnd);
                }

                // but return true here so that we iterate all windows
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
        {
            return FindWindows((wnd, param) => GetWindowText(wnd).Contains(titleText));
        }
    }
}
