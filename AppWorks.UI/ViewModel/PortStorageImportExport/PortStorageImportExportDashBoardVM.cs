namespace AppWorks.UI.ViewModel.PortStorageImportExport
{
    class PortStorageImportExportDashBoardVM : ViewModelBase
    {
         public AppWorxCommand ImportVehiclesYMSCommand { get; private set; }
        public AppWorxCommand ImportLocationYMSCommand { get; private set; }

        public PortStorageImportExportDashBoardVM()
        {
            //ImportVehiclesYMSCommand = new AppWorxCommand(OnImportVehiclesYMSCommandRaised);
            ImportLocationYMSCommand = new AppWorxCommand(OnImportLocationYMSCommandRaised);
        }

        //private void OnImportVehiclesYMSCommandRaised(object obj)
        //{
        //    PortStorageVehicleImportYMSWindow window = new PortStorageVehicleImportYMSWindow();
        //    window.ShowDialog();
        //}
        private void OnImportLocationYMSCommandRaised(object obj)
        {
            //PortStorageLocationImportYMSWindow window = new PortStorageLocationImportYMSWindow();
            //window.ShowDialog();
        }
    }
}
