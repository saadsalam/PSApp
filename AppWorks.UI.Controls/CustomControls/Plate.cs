namespace AppWorks.UI.Controls.CustomControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public class Plate : Control, ICommandSource
    {
        private static readonly Type typeOfThis = typeof(Plate);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
            typeof(ICommand),
            typeOfThis,
            new PropertyMetadata(null, OnCommandChanged));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter",
            typeof(object),
            typeOfThis,
            new PropertyMetadata(null));

        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget",
            typeof(IInputElement),
            typeOfThis,
            new PropertyMetadata(null));

        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content",
            typeof(object),
            typeOfThis,
            new PropertyMetadata(null));

        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate",
            typeof(DataTemplate),
            typeOfThis,
            new PropertyMetadata(null));

        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
            typeof(object),
            typeOfThis,
            new PropertyMetadata(null));

        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }
        public static readonly DependencyProperty TitleTemplateProperty =
            DependencyProperty.Register("TitleTemplate",
            typeof(DataTemplate),
            typeOfThis,
            new PropertyMetadata(null));

        public Dock DockTitle
        {
            get { return (Dock)GetValue(DockTitleProperty); }
            set { SetValue(DockTitleProperty, value); }
        }
        public static readonly DependencyProperty DockTitleProperty =
            DependencyProperty.Register("DockTitle", typeof(Dock), typeOfThis, new PropertyMetadata(null));

        public Thickness TitlePadding
        {
            get { return (Thickness)GetValue(TitlePaddingProperty); }
            set { SetValue(TitlePaddingProperty, value); }
        }
        public static readonly DependencyProperty TitlePaddingProperty =
            DependencyProperty.Register("TitlePadding",
            typeof(Thickness),
            typeOfThis,
            new PropertyMetadata(null));

        public HorizontalAlignment HorizontalTitleAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalTitleAlignmentProperty); }
            set { SetValue(HorizontalTitleAlignmentProperty, value); }
        }
        public static readonly DependencyProperty HorizontalTitleAlignmentProperty =
            DependencyProperty.Register("HorizontalTitleAlignment", 
            typeof(HorizontalAlignment), 
            typeOfThis, 
            new PropertyMetadata(HorizontalAlignment.Left));

        public VerticalAlignment VerticalTitleAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalTitleAlignmentProperty); }
            set { SetValue(VerticalTitleAlignmentProperty, value); }
        }
        public static readonly DependencyProperty VerticalTitleAlignmentProperty =
            DependencyProperty.Register("VerticalTitleAlignment", 
            typeof(VerticalAlignment), 
            typeOfThis, 
            new PropertyMetadata(VerticalAlignment.Bottom));

        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            private set { SetValue(IsPressedProperty, value); }
        }
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed",
            typeof(bool),
            typeOfThis,
            new PropertyMetadata(false));

        static Plate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeOfThis, new FrameworkPropertyMetadata(typeOfThis));
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
            Plate control = (Plate)d;
            control.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= CommandCanExecuteChanged;
            }

            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += CommandCanExecuteChanged;
            }
        }

        private void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;
                if (command != null)
                {
                    this.IsEnabled = command.CanExecute(CommandParameter, CommandTarget);
                }
                else
                {
                    this.IsEnabled = Command.CanExecute(CommandParameter);
                }
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsPressed = false;
            Mouse.Capture(null);

            if (Command != null && IsMouseOver)
            {
                if (Command is RoutedCommand)
                {
                    (Command as RoutedCommand).Execute(CommandParameter, CommandTarget);
                }
                else
                {
                    (Command as ICommand).Execute(CommandParameter);
                }
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                IsPressed = true;
            }
            Mouse.Capture(this);            
        }
    }
}
