using AppWorks.UI.ViewModel.WebPortal;
using System.Windows;
using System.Windows.Controls;

namespace AppWorks.UI.View.WebPortal
{
    public partial class WebPortalAdministration : UserControl
    {
        public WebPortalAdministration()
        {
            InitializeComponent();
            this.DataContext = new WebPortalVM();
            //double width = SystemParameters.PrimaryScreenWidth;
            //double height= SystemParameters.PrimaryScreenHeight;
            //UserGridView.Height = 300;
            //gridUsers.Width=width-300;
            //brUser.Height = height-530;
            //brUser.Width = width - 300;
            //brGrid.Width = width - 300;

            Application.Current.Properties["FindUserGridLastScrollOffset"] = 0;
            //brUser.Width = width - 300;
            //string Ud = Application.Current.Properties["LoggedInUserName"].ToString(); 
        }

        //private void rdPgPending_PageIndexChanged(object sender, PageIndexChangedEventArgs e)
        //{
        //    int NewPageSize = e.NewPageIndex;
        //    if (NewPageSize >= 0)
        //        WebPortalDelegate.SetValueMethodPageNumber(NewPageSize);

        //}       

        //private void RadButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var button = sender as RadButton;
        //    var row = button.ParentOfType<GridViewRow>();
        //    if (row != null)
        //    {

        //        row.IsSelected = true;
        //        row.Background = new SolidColorBrush(Colors.Tomato);
        //        var item = row.Item;
        //        //or set its background:
        //        //row.Background = new SolidColorBrush(Colors.Tomato);      
        //    }
        //}
    }
}
