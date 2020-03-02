using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWorks.UI.ViewModel.Vehicle
{
    public class VehicleInfoViewModel : ViewModelBase
    {

        #region Fields

        private ICollection<VehicleInfoViewModel> _parentCollection;

        #endregion Fields

        #region Properties

        public VehicleLocatorVM VehicleLocatorViewModel { get; private set; }

        public string Make
        {
            get
            {
                return VehicleLocatorViewModel.Make;
            }
        }

        public string Model
        {
            get
            {
                return VehicleLocatorViewModel.Model;
            }
        }

        public string CustomerName
        {
            get
            {
                return VehicleLocatorViewModel.CustomerName;
            }
        }

        public int VehicleId
        {
            get
            {
                return VehicleLocatorViewModel.VehicleId;
            }
        }

        public string VIN
        {
            get
            {
                return VehicleLocatorViewModel.Vin;
            }
        }

        #endregion Properties

        #region Commands

        private ICommand _removeFromSelectedCommand;
        public ICommand RemoveFromSelectedCommand
        {
            get
            {
                return _removeFromSelectedCommand ??
                    (_removeFromSelectedCommand = new AppWorxCommand(RemoveFromSelectedCommand_Executed));
            }
        }
        private void RemoveFromSelectedCommand_Executed(object obj)
        {
            _parentCollection.Remove(this);
        }

        #endregion Commands

        #region Constructors

        public VehicleInfoViewModel(VehicleLocatorVM vehicleLocatorViewModel, ICollection<VehicleInfoViewModel> parentCollection)
        {
            if (vehicleLocatorViewModel == null)
                throw new ArgumentNullException("vehicleLocatorViewModel");
            if (parentCollection == null)
                throw new ArgumentNullException("parentCollection");

            VehicleLocatorViewModel = vehicleLocatorViewModel;
            _parentCollection = parentCollection;
        }

        #endregion Constructors

    }
}
