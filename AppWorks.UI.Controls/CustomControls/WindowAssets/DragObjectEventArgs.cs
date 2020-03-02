namespace AppWorks.UI.Controls.CustomControls.WindowAssets
{
    using System;
    using System.Windows;

    public sealed class DragObjectEventArgs : EventArgs
    {
        public Point Position { get; set; }
        public Vector Delta { get; set; }
    }
}
