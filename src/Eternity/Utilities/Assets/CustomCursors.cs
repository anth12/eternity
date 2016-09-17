using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Eternity.Utilities.Assets
{
    internal static class CustomCursors
    {
        internal static Cursor Default = Cursors.Arrow;

        internal static Cursor DownArrow = new Cursor(
                Application.GetResourceStream(
                    new Uri("Assets/Cursors/Crop Down.cur", UriKind.Relative)
                ).Stream
            );
    }
}
