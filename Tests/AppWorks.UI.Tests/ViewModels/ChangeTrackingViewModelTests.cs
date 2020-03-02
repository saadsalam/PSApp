using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppWorks.UI.ViewModel;
using AppWorks.UI.Controls.Attributes;

namespace AppWorks.UI.Tests.ViewModels
{
    [TestClass]
    public class ChangeTrackingViewModelTests
    {
        [TestMethod]
        public void ChangeTrackingViewModel_PropertiesInViewModelNotChanged_ReturnsFalse()
        {
            var viewModel = new TestableViewModel();
            viewModel.AcceptChanges();

            Assert.IsFalse(viewModel.IsChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangeTrackingViewModel_AcceptChangesWasNotCalled_Exception()
        {
            var viewModel = new TestableViewModel();

            Assert.IsFalse(viewModel.IsChanged);
        }
        
        [TestMethod]
        public void ChangeTrackingViewModel_ChangeTrackingPropertyWasModified_ReturnsTrue()
        {
            var viewModel = new TestableViewModel();
            viewModel.AcceptChanges();
            viewModel.TestIntProperty = 1;
            Assert.IsTrue(viewModel.IsChanged);
        }

        [TestMethod]
        public void ChangeTrackingViewModel_ChangeTrackingPropertyWasModifiedAndAcceptChangedWasCalled_ReturnsFalse()
        {
            var viewModel = new TestableViewModel();
            viewModel.AcceptChanges();
            viewModel.TestStrProperty = "text";
            viewModel.AcceptChanges();
            Assert.IsFalse(viewModel.IsChanged);
        }

        [TestMethod]
        public void ChangeTrackingViewModel_NonChangeTrackingPropertyWasModified_ReturnsFalse()
        {
            var viewModel = new TestableViewModel();
            viewModel.AcceptChanges();
            viewModel.NotTrackingProperty = "value";
            Assert.IsFalse(viewModel.IsChanged);
        }

        [TestMethod]
        public void ChangeTrackingViewModel_ChangeTrackingPropertyWasModifiedAndReturnedToPreviousState_ReturnsFalse()
        {
            var viewModel = new TestableViewModel();
            viewModel.AcceptChanges();
            viewModel.TestStrProperty = "value";
            viewModel.TestStrProperty = null;
            Assert.IsFalse(viewModel.IsChanged);
        }
    }

    public class TestableViewModel : ChangeTrackingViewModel
    {
        [ChangeTracking]
        public int TestIntProperty { get; set; }

        [ChangeTracking]
        public string TestStrProperty { get; set; }

        public string NotTrackingProperty { get; set; }
    }
}
