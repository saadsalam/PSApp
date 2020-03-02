namespace AppWorks.UI.Controls.CustomControls.WindowAssets
{
    using Extensions;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class DragNDropObject
    {
        private readonly UIElement sender;
        private readonly IInputElement relativeTo;
        private Point position;
        private bool isInverted;

        public event EventHandler<DragObjectEventArgs> PositionChanged;
        public event EventHandler<DragObjectEventArgs> Dropped;

        public DragNDropObject(UIElement sender)
            : this(sender, null, false)
        {
        }

        public DragNDropObject(UIElement sender, IInputElement relativeTo, bool isInverted)
        {
            this.isInverted = isInverted;
            this.sender = sender;
            this.relativeTo = relativeTo;
            this.sender.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.position = Mouse.GetPosition(this.relativeTo);
            this.sender.CaptureMouse();
            this.sender.MouseMove += OnSenderMouseMove;
            this.sender.MouseLeftButtonUp += OnSenderMouseUp;
            this.sender.LostMouseCapture += OnSenderLostMouseCapture;
        }

        private void OnSenderLostMouseCapture(object sender, MouseEventArgs e)
        {
            this.sender.MouseMove -= OnSenderMouseMove;
            this.sender.MouseLeftButtonUp -= OnSenderMouseUp;
            this.sender.LostMouseCapture -= OnSenderLostMouseCapture;

            Point currentPosition = Mouse.GetPosition(this.relativeTo);
            Vector delta = this.position - currentPosition;
            RaiseDropped(currentPosition, delta);
            this.position = new Point(0, 0);
        }

        private void OnSenderMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.sender.ReleaseMouseCapture();
        }

        private void OnSenderMouseMove(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            Point currentPosition = Mouse.GetPosition(this.relativeTo);
            Point positionAccordingToSender = Mouse.GetPosition(this.sender);
            if (Point.Equals(currentPosition, this.position))
                return;

            Vector delta = this.position - currentPosition;
            RaisePositionChanged(currentPosition, delta);
            this.position = this.isInverted ? currentPosition : this.position;
        }

        private void RaisePositionChanged(Point position, Vector delta)
        {
            DragObjectEventArgs args = new DragObjectEventArgs { Delta = delta, Position = position };
            args.RaiseEventHandler(this.sender, ref PositionChanged);
        }

        private void RaiseDropped(Point position, Vector delta)
        {
            DragObjectEventArgs args = new DragObjectEventArgs { Delta = delta, Position = position };
            args.RaiseEventHandler(this.sender, ref Dropped);
        }

        public void ReleaseSender()
        {
            this.sender.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        }
    }
}
