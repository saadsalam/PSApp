using AppWorks.UI.View.Billing;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel.Billing
{
    public class AddEditInvoiceRecordsViewModel : ViewModelBase
    {

        #region Commands

        private ICommand _findCommand;
        public ICommand FindCommand
        {
            get
            {
                return _findCommand ??
                    (_findCommand = new AppWorxCommand(x => FindCommand_Executed()));
            }
        }
        private void FindCommand_Executed()
        {
            InvoiceRecordFind invoiceRecordFind = Application.Current.Windows.OfType<InvoiceRecordFind>().FirstOrDefault();
            if (invoiceRecordFind != null)
            {
                if (invoiceRecordFind.WindowState == WindowState.Minimized)
                {
                    invoiceRecordFind.WindowState = WindowState.Normal;
                }
                invoiceRecordFind.Activate();
            }
            else
            {
                invoiceRecordFind = new InvoiceRecordFind();
                invoiceRecordFind.ShowDialog();
            }
        }


        #endregion Commands

    }
}
