﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Eternity.Core.Windows
{
    public class ApplicationHelper
    {
        #region External imports

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

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
                Process = process,
                ProcessId = processId,
                StarTime = process.StartTime,
                ProcessName = process.ProcessName,
                WindowName = process.MainWindowTitle
            };

            return result;
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
            return FindWindows(delegate (IntPtr wnd, IntPtr param)
            {
                return GetWindowText(wnd).Contains(titleText);
            });
        }
    }
}
