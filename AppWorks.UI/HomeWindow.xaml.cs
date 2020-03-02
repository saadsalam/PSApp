using AppWorks.UI.Infrastructure.Navigation;
using AppWorks.UI.ViewModel;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Linq;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using AppWorks.UI.ViewModel.Vehicle;
using System;
using AppWorks.UI.Common;
using System.Windows.Interop;
using System.Diagnostics;

namespace AppWorks.UI
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </scomoummary>
    public partial class HomeWindow
    {
        private RadTreeView radTreeView;
        private const int MenuBarMaxWidth = 230;
        private const int MenuBarMinWidth = 55;
        public int logOffTime = 1;//in Minutes
        Window window = null;
        public HomeWindow()
        {
            InitializeComponent();
            this.DataContext = new HomeWindowVM();
            Application.Current.MainWindow = this;
            MenuBar.Width = MenuBarMaxWidth;
            MenuBar.MaxWidth = MenuBarMaxWidth;
            MenuBar.MinWidth = MenuBarMinWidth;
            this.Loaded += new RoutedEventHandler(HomeWindowLoaded);
            
        }
        private void HomeWindowLoaded(object sender, RoutedEventArgs e)
        {
            window = Window.GetWindow(this);
            window.Closing += window_Closing;
        }

        void window_Closing(object sender, CancelEventArgs e)
        {
            DisposeView();
        }

        private void ShowPopupButton_Click(object sender, RoutedEventArgs e)
        {
            logoutPopup.PopupAnimation = PopupAnimation.Slide;
            logoutPopup.IsOpen = true;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationNode selected = ((sender as TextBlock).DataContext as NavigationNode);
            var content = ((HomeWindowVM)DataContext).Content;
            var viewModel = ((UserControl)content).DataContext as ViewModel.ViewModelBase;
            ((HomeWindowVM)DataContext).NavigateTo(selected);
        }

        /// <summary>
        /// Dispose off all the resources held by previous viewmodels
        /// </summary>
        private void DisposePreviousViewModelReferences(object viewModel)
        {
            GridMultiSelect multiSelect = new GridMultiSelect();

            var type = viewModel.GetType();

            //check if viewmodel implements IDisposable interface
            bool hasIDisposableSupport = typeof(System.IDisposable).IsAssignableFrom(type);

            if (hasIDisposableSupport)
            {
                dynamic obj = viewModel;

                if (obj != null)
                {
                    obj.Dispose();
                }
            }

            var isAnyChildViewModels = ((ViewModel.ViewModelBase)viewModel).GetChildViewModels().Any();

            //if any of the child viewmodels are there
            if (isAnyChildViewModels)
            {
                var childViewModels = ((ViewModel.ViewModelBase)viewModel).GetChildViewModels();

                foreach (var item in childViewModels)
                {
                    if (item != null)
                    {
                        bool hasChildVMIDisposableSupport = typeof(System.IDisposable).IsAssignableFrom(item.GetType());

                        if (hasChildVMIDisposableSupport)
                        {
                            dynamic obj = item;

                            if (obj != null)
                            {
                                obj.Dispose();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if any of the viewmodel has changed since the last operation
        /// </summary>
        /// <param name="viewModels"></param>
        /// <returns></returns>
        private bool CheckIfAnyViewModelChanged(System.Collections.Generic.IEnumerable<IChangeTracking> viewModels,
            ViewModel.ViewModelBase baseViewModel)
        {
            bool isChanged = false;

            foreach (var item in viewModels)
            {
                if (item != null && item.IsChanged)
                { isChanged = true; }
            }

            //if this is false check for another baseViewModel has changed
            if (!isChanged)
            {
                var changeTrackingViewModel = baseViewModel as IChangeTracking;
                if (changeTrackingViewModel != null && changeTrackingViewModel.IsChanged)
                {
                    isChanged = true;                   
                }
            }

            return isChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadTreeView_PreviewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var content = ((HomeWindowVM)DataContext).Content;

            var viewModel = ((UserControl)content).DataContext as ViewModel.ViewModelBase;

            var baseTypeName = ((NavigationNode)e.AddedItems[0]).Type.BaseType.Name.ToString();

            var viewModels = viewModel.GetChildViewModels().Cast<IChangeTracking>();

            if (!string.IsNullOrWhiteSpace(baseTypeName) && baseTypeName.Equals("usercontrol", System.StringComparison.OrdinalIgnoreCase))
            {
                //if not changed then dispose the viewmodel delegates registartions and navigate to another tab
                if (!CheckIfAnyViewModelChanged(viewModels, viewModel))
                {
                    DisposePreviousViewModelReferences(viewModel);
                }
            }

            foreach (var item in viewModels)
            {
                if (item != null && item.IsChanged)
                {
                    ShowConfirmationMessage(e, item, viewModel);
                }
            }

            var changeTrackingViewModel = viewModel as IChangeTracking;
            if (changeTrackingViewModel != null && changeTrackingViewModel.IsChanged)
            {
                ShowConfirmationMessage(e, changeTrackingViewModel);
            }
        }

        private void ShowConfirmationMessage(SelectionChangedEventArgs e, IChangeTracking viewModel, object mainViewModel = null)
        {
            var result = MessageBox.Show("You have unsaved changes. Do you want to continue?", "Unsaved changes", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Handled = true;
            }
            else
            {
                if (mainViewModel != null)
                {
                    DisposePreviousViewModelReferences(mainViewModel);
                }

                viewModel.AcceptChanges();
            }
        }

        private void RadTreeView_Selected(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (radTreeView != null && sender != radTreeView)
                radTreeView.SelectedItem = null;

            radTreeView = sender as RadTreeView;

            FrameworkElement control = e.OriginalSource as FrameworkElement;
            if (control != null)
            {
                NavigationNode node = control.DataContext as NavigationNode;
                if (node != null)
                    ((HomeWindowVM)DataContext).NavigateTo(node);
            }
        }

        private void MenuBar_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            (DataContext as HomeWindowVM).IsAnyExpanded = true;
        }

        private void MenuBar_Collapsed(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            (DataContext as HomeWindowVM).IsAnyExpanded = false;
        }

        private void IsMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as RadToggleButton;
            if (MenuBar == null) return;
            if (toggleButton.IsChecked.Value)
            {
                MenuBar.Width = MenuBarMaxWidth;
                LogoImage.Visibility = Visibility.Visible;
            }
            else
            {
                MenuBar.Width = MenuBarMinWidth;
                LogoImage.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuBar_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)((RadPanelBar)sender).Parent;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void StackPanelFixed_SP_Loaded(object sender, RoutedEventArgs e)
        {
            var treeView = sender as RadTreeView;
            var firstItem = treeView.Items[0] as NavigationNode;
            if (((NavigationNode)firstItem.Parent).Name == "Port Storage")
            {
                treeView.SelectedItem = treeView.Items[0];
            }
        }

        /// <summary>
        /// Dispose UserControl View on window closing
        /// </summary>
        private void DisposeView()
        {
            var content = ((HomeWindowVM)DataContext).Content;
            var viewModel = ((UserControl)content).DataContext as ViewModel.ViewModelBase;
            DisposePreviousViewModelReferences(viewModel);
            window.Closing -= window_Closing;
        }

       
    }
}
