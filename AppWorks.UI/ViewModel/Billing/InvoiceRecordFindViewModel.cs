using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel.Billing
{
    public class InvoiceRecordFindViewModel : ViewModelBase
    {

        #region Properties

        public ObservableCollection<object> InvoiceRecords { get; private set; }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                NotifyPropertyChanged("CustomerName");
            }
        }

        private string _invoiceStatus;
        public string InvoiceStatus
        {
            get { return _invoiceStatus; }
            set
            {
                _invoiceStatus = value;
                NotifyPropertyChanged("InvoiceStatus");
            }
        }

        private string _poNumber;
        public string PONumber
        {
            get { return _poNumber; }
            set
            {
                _poNumber = value;
                NotifyPropertyChanged("PONumber");
            }
        }

        private string _invoiceType;
        public string InvocieType
        {
            get { return _invoiceType; }
            set
            {
                _invoiceType = value;
                NotifyPropertyChanged("InvoiceType");
            }
        }

        private string _invoiceNumber;
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                NotifyPropertyChanged("InvoiceNumber");
            }
        }

        private string _orderNumber;
        public string OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                _orderNumber = value;
                NotifyPropertyChanged("OrderNumber");
            }
        }

        private string _vin;
        public string VIN
        {
            get { return _vin;}
            set
            {
                _vin = value;
                NotifyPropertyChanged("VIN");
            }
        }

        private string _customerNumber;
        public string CustomerNumber
        {
            get { return _customerNumber; }
            set
            {
                _customerNumber = value;
                NotifyPropertyChanged("CustomerNumber");
            }
        }

        private string _loadNumber;
        public string LoadNumber
        {
            get { return _loadNumber; }
            set
            {
                _loadNumber = value;
                NotifyPropertyChanged("LoadNumber");
            }
        }

        private string _customerIdentity;
        public string CustomerIdentity
        {
            get { return _customerIdentity; }
            set
            {
                _customerIdentity = value;
                NotifyPropertyChanged("CustomerIdentity");
            }
        }

        private string _createdBetween;
        public string CreatedBetween
        {
            get { return _createdBetween; }
            set
            {
                _createdBetween = value;
                NotifyPropertyChanged("CreatedBetween");
            }
        }

        private string _driver;
        public string Driver
        {
            get { return _driver; }
            set
            {
                _driver = value;
                NotifyPropertyChanged("Driver");
            }
        }

        private DateTime? _invoiceDateBetween;
        public DateTime? InvoiceDateBetween
        {
            get { return _invoiceDateBetween; }
            set
            {
                _invoiceDateBetween = value;
                NotifyPropertyChanged("InvoiceDateBetween");
            }
        }

        private string _outsideCarrier;
        public string OutsideCarrier
        {
            get { return _outsideCarrier; }
            set
            {
                _outsideCarrier = value;
                NotifyPropertyChanged("OutsideCarrier");
            }
        }

        private string _paidBetween;
        public string PaidBetween
        {
            get { return _paidBetween; }
            set
            {
                _paidBetween = value;
                NotifyPropertyChanged("PaidBetween");
            }
        }

        private string _extra1;
        public string Extra1
        {
            get { return _extra1; }
            set
            {
                _extra1 = value;
                NotifyPropertyChanged("Extra1");
            }
        }

        private string _extra2;
        public string Extra2
        {
            get { return _extra2; }
            set
            {
                _extra2 = value;
                NotifyPropertyChanged("Extra2");
            }
        }

        private string _extra3;
        public string Extra3
        {
            get { return _extra3; }
            set
            {
                _extra3 = value;
                NotifyPropertyChanged("Extra3");
            }
        }

        #endregion Properties

        #region Commands

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ??
                    (_searchCommand = new AppWorxCommand(SearchCommand_Executed));
            }
        }
        private void SearchCommand_Executed(object obj)
        {

        }

        private ICommand _clearResultsCommand;
        public ICommand ClearResultsCommand
        {
            get
            {
                return _clearResultsCommand ??
                    (_clearResultsCommand = new AppWorxCommand(ClearResultsCommand_Executed, ClearResultsCommand_CanExecute));
            }
        }
        private bool ClearResultsCommand_CanExecute(object obj)
        {
            return InvoiceRecords.Any();
        }
        private void ClearResultsCommand_Executed(object obj)
        {
            InvoiceRecords.Clear();
        }

        private ICommand _continueCommand;
        public ICommand ContinueCommand
        {
            get
            {
                return _continueCommand ??
                    (_continueCommand = new AppWorxCommand(ContinueCommand_Executed));
            }
        }
        private void ContinueCommand_Executed(object obj)
        {
            OnCloseWindowRequested();
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new AppWorxCommand(CancelCommand_Executed));
            }
        }
        private void CancelCommand_Executed(object obj)
        {
            OnCloseWindowRequested();
        }

        private ICommand _fillDataCommand;
        public ICommand FillDataCommand
        {
            get
            {
                return _fillDataCommand ??
                    (_fillDataCommand = new AppWorxCommand(FillDataCommand_Executed));
            }
        }
        private void FillDataCommand_Executed(object obj)
        {

        }


        #endregion Commands

        #region Events

        public event EventHandler CloseWindowRequested;
        private void OnCloseWindowRequested()
        {
            EventHandler handler = CloseWindowRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion Events

        #region Constructors

        public InvoiceRecordFindViewModel()
        {
            InvoiceRecords = new ObservableCollection<object>();
        }

        #endregion Constructors

    }
}
