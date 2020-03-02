using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AppWorks.UI.Controls.CustomControls
{
    using iAppWorks.UI.Controls.CustomControls.WindowAssets;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using WindowAssets;
    using Point = System.Drawing.Point;

    [TemplatePart(Name = "PART_Header", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_MinimizeButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MaximizeButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_LeftSizeTool", Type = typeof(Border))]
    [TemplatePart(Name = "PART_TopSizeTool", Type = typeof(Border))]
    [TemplatePart(Name = "PART_RightSizeTool", Type = typeof(Border))]
    [TemplatePart(Name = "PART_BottomSizeTool", Type = typeof(Border))]
    [TemplatePart(Name = "PART_Shadow", Type = typeof(Border))]
    public class VSWindow : Window
    {
        private static readonly Type typeOfThis = typeof(VSWindow);

        private Rectangle screen;

        private IntPtr handle;

        private System.Windows.Controls.Grid header;

        private Button minimizeButton,
                       closeButton;

        private ToggleButton maximizeButton;

        private Border PART_DialogShadowBorder;

        private DragNDropObject leftSizeTool,
                                topSizeTool,
                                rightSizeTool,
                                bottomSizeTool;

        private int dialogShadowBorderCount;

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", 
            typeof(Brush), 
            typeOfThis, 
            new PropertyMetadata(null));

        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register("HeaderContent",
            typeof(object),
            typeOfThis,
            new PropertyMetadata(null, OnHeaderContentChanged));

        public DataTemplate HeaderContentTemplate
        {
            get { return (DataTemplate)GetValue(HeaderContentTemplateProperty); }
            set { SetValue(HeaderContentTemplateProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentTemplateProperty =
            DependencyProperty.Register("HeaderContentTemplate",
            typeof(DataTemplate),
            typeOfThis,
            new PropertyMetadata(null));

        public DataTemplateSelector HeaderContentTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(HeaderContentTemplateSelectorProperty); }
            set { SetValue(HeaderContentTemplateSelectorProperty, value); }
        }
        public static readonly DependencyProperty HeaderContentTemplateSelectorProperty =
            DependencyProperty.Register("HeaderContentTemplateSelector", 
            typeof(DataTemplateSelector), 
            typeOfThis, 
            new PropertyMetadata(null));

        public bool IsGlowing
        {
            get { return (bool)GetValue(IsGlowingProperty); }
            set { SetValue(IsGlowingProperty, value); }
        }
        public static readonly DependencyProperty IsGlowingProperty =
            DependencyProperty.Register("IsGlowing",
            typeof(bool),
            typeOfThis,
            new PropertyMetadata(true));

        public IntPtr Handle { get { return this.handle; } }

        public bool IsModal { get { return ComponentDispatcher.IsThreadModal; } }        

        static VSWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeOfThis, new FrameworkPropertyMetadata(typeOfThis));
        }

        private static void OnHeaderContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VSWindow control = d as VSWindow;
            if (control.HeaderContentTemplateSelector != null)
            {
                control.HeaderContentTemplate = control.HeaderContentTemplateSelector.SelectTemplate(e.NewValue, control);
            }
        }

        private void OnHeaderClicked(object sender, MouseButtonEventArgs e)
        {            
            if (e.ClickCount > 1)
            {
                e.Handled = true;
                ChangeWindowState();
            }
            else if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Close();
        }

        private void OnMinimizeClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            WindowState = WindowState.Minimized;
        }

        private void OnMaximizeClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ChangeWindowState();
        }

        private void ChangeWindowState()
        {
            screen = Screen.GetWorkingArea(new Point((int)(this.Left + this.ActualWidth / 2),
                          (int)(this.Top + this.ActualHeight / 2)));

            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiX = (int)dpiXProperty.GetValue(null, null);

            MaxHeight = screen.Size.Height + (BorderThickness.Right + SystemParameters.FixedFrameHorizontalBorderHeight * 2); ;
            MaxWidth = screen.Size.Width + (BorderThickness.Bottom + SystemParameters.FixedFrameVerticalBorderWidth * 2); ;

            if (dpiX == 120)
            {
                MaxHeight = MaxHeight / 125 * 100 + 2;
                MaxWidth = MaxWidth / 125 * 100 + 2;
            }
            else if (dpiX == 144)
            {
                MaxHeight = MaxHeight / 150 * 100 + 3;
                MaxWidth = MaxWidth / 150 * 100 + 3;
            }

            WindowState = WindowState.HasFlag(WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void CalculateMaxSize()
        {
            bool isInitialized = !double.IsNaN(this.Left);
            if (isInitialized)
            {
                Rectangle scr =
                    Screen.GetWorkingArea(new Point((int) (this.Left + this.ActualWidth/2),
                        (int) (this.Top + this.ActualHeight/2)));
                MaxHeight = scr.Height + (BorderThickness.Right + SystemParameters.FixedFrameHorizontalBorderHeight * 2);
                MaxWidth = scr.Width + (BorderThickness.Bottom + SystemParameters.FixedFrameVerticalBorderWidth * 2);
            }
            else
            {
                MaxHeight = SystemParameters.WorkArea.Height + (BorderThickness.Right + SystemParameters.FixedFrameHorizontalBorderHeight * 2);
                MaxWidth = SystemParameters.WorkArea.Width + (BorderThickness.Bottom + SystemParameters.FixedFrameVerticalBorderWidth * 2);
            }
        }

        private void OnBottomSizeToolPositionChanged(object sender, DragObjectEventArgs e)
        {
            WindowApi.RECT rect = new WindowApi.RECT();
            WindowApi.GetWindowRect(this.handle, ref rect);
            rect.bottom -= (int)e.Delta.Y;
            WindowApi.MoveWindow(this.handle, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, false);
        }

        private void OnRightSizeToolPositionChanged(object sender, DragObjectEventArgs e)
        {
            WindowApi.RECT rect = new WindowApi.RECT();
            WindowApi.GetWindowRect(this.handle, ref rect);
            rect.right -= (int)e.Delta.X;
            WindowApi.MoveWindow(this.handle, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, false);
        }

        private void OnTopSizeToolPositionChanged(object sender, DragObjectEventArgs e)
        {
            WindowApi.RECT rect = new WindowApi.RECT();
            WindowApi.GetWindowRect(this.handle, ref rect);
            rect.top -= (int)e.Delta.Y;
            WindowApi.MoveWindow(this.handle, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, false);
        }

        private void OnLeftSizeToolPositionChanged(object sender, DragObjectEventArgs e)
        {
            WindowApi.RECT rect = new WindowApi.RECT();
            WindowApi.GetWindowRect(this.handle, ref rect);
            rect.left -= (int)e.Delta.X;
            WindowApi.MoveWindow(this.handle, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top, false);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (this.leftSizeTool != null)
            {
                this.leftSizeTool.ReleaseSender();
            }
            if (this.topSizeTool != null)
            {
                this.topSizeTool.ReleaseSender();
            }
            if (this.rightSizeTool != null)
            {
                this.rightSizeTool.ReleaseSender();
            }
            if (this.bottomSizeTool != null)
            {
                this.bottomSizeTool.ReleaseSender();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            MaxHeight = SystemParameters.WorkArea.Height + (BorderThickness.Right + SystemParameters.FixedFrameHorizontalBorderHeight * 3);
            MaxWidth = SystemParameters.WorkArea.Width + (BorderThickness.Bottom + SystemParameters.FixedFrameVerticalBorderWidth * 3);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.handle = new WindowInteropHelper(this).Handle;

            this.header = GetTemplateChild("PART_Header") as System.Windows.Controls.Grid;
            this.header.MouseLeftButtonDown += OnHeaderClicked;

            this.minimizeButton = GetTemplateChild("PART_MinimizeButton") as Button;
            this.minimizeButton.Click += OnMinimizeClicked;

            this.closeButton = GetTemplateChild("PART_CloseButton") as Button;
            this.closeButton.Click += OnCloseClicked;

            this.maximizeButton = GetTemplateChild("PART_MaximizeButton") as ToggleButton;
            this.maximizeButton.Click += OnMaximizeClicked;

            this.leftSizeTool = new DragNDropObject(GetTemplateChild("PART_LeftSizeTool") as UIElement);
            this.leftSizeTool.PositionChanged += OnLeftSizeToolPositionChanged;
            this.topSizeTool = new DragNDropObject(GetTemplateChild("PART_TopSizeTool") as UIElement);
            this.topSizeTool.PositionChanged += OnTopSizeToolPositionChanged;
            this.rightSizeTool = new DragNDropObject(GetTemplateChild("PART_RightSizeTool") as UIElement, null, true);
            this.rightSizeTool.PositionChanged += OnRightSizeToolPositionChanged;
            this.bottomSizeTool = new DragNDropObject(GetTemplateChild("PART_BottomSizeTool") as UIElement, null, true);
            this.bottomSizeTool.PositionChanged += OnBottomSizeToolPositionChanged;

            this.PART_DialogShadowBorder = GetTemplateChild("PART_DialogShadowBorder") as Border;
        }

        /// <summary>
        /// Override to be able to show shadow.
        /// </summary>
        /// <returns></returns>
        public new bool? ShowDialog()
        {
            // as owner is not always set, we have to set it
            if (Owner == null) Owner = Application.Current.MainWindow;

            if (Owner is VSWindow == false) return base.ShowDialog();

            ((VSWindow)Owner).ShowShadow();
            bool? result = base.ShowDialog();
            ((VSWindow)Owner).HideShadow();
            return result;
        }

        internal void ShowShadow()
        {
            this.dialogShadowBorderCount++;
            PART_DialogShadowBorder.Visibility = Visibility.Visible;
        }

        internal void HideShadow()
        {
            this.dialogShadowBorderCount--;
            if (this.dialogShadowBorderCount == 0)
            PART_DialogShadowBorder.Visibility = Visibility.Collapsed;
        }

        public static void Alert(string title, string message, Window owner = null)
        {
            VSWindow window = new Controls.UserControls.AlertWindow
            {
                Title = title,
                Content = message,
                Owner = owner
            };            
            if (!(window.Owner is VSWindow))
            {
                window.ShowDialog();
            }
            else
            {
                ((VSWindow)window.Owner).ShowShadow();
                window.ShowDialog();
                ((VSWindow)window.Owner).HideShadow();
            }
        }

        public static bool? Confirm(string title, string message, Window owner = null)
        {
            VSWindow window = new UserControls.ConfirmWindow
            {
                Title = title,
                Content = message,
                Owner = owner
            };
            if (!(window.Owner is VSWindow))
            {
                window.ShowDialog();
            }
            else
            {
                ((VSWindow)window.Owner).ShowShadow();
                window.ShowDialog();
                ((VSWindow)window.Owner).HideShadow();
            }
            return window.DialogResult;
        }

        public static bool? ShowDialog(string title, UserControl dialog)
        {
            UserControls.DialogWindow window = new UserControls.DialogWindow
            {
                Title = title,
                Content = dialog,
                Owner = Application.Current.MainWindow
            };
            ((VSWindow)window.Owner).ShowShadow();
            bool? result = window.ShowDialog();
            ((VSWindow)window.Owner).HideShadow();
            return result;
        }
    }
}
