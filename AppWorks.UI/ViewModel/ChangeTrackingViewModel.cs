using AppWorks.UI.Controls.Attributes;
using AppWorks.UI.Controls.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AppWorks.UI.ViewModel
{
    public abstract class ChangeTrackingViewModel : ViewModelBase, IChangeTracking
    {
        #region Fields

        private readonly Lazy<IReadOnlyCollection<PropertyInfo>> _changeTrackingProperties;
        private Dictionary<string, object> _acceptedSnapshot;

        #endregion

        #region Properties

        public bool IsChanged
        {
            get
            {
                if (_acceptedSnapshot == null)
                {
                    throw new InvalidOperationException("AcceptChanges method should be called.");
                }
                var currentSnapshot = GetSnapshot();
                return !_acceptedSnapshot.DictionaryEquals(currentSnapshot);
            }
        }

        #endregion

        #region Ctor

        protected ChangeTrackingViewModel()
        {
            _changeTrackingProperties = new Lazy<IReadOnlyCollection<PropertyInfo>>(GetChangeTrackingProperties);
        }

        #endregion

        #region Methods

        public void AcceptChanges()
        {
            _acceptedSnapshot = GetSnapshot();
        }

        protected virtual IReadOnlyCollection<PropertyInfo> GetChangeTrackingProperties()
        {
            var attrType = typeof(ChangeTrackingAttribute);
            return GetType().GetProperties().Where(p => p.IsDefined(attrType)).ToArray();
        }

        private Dictionary<string, object> GetSnapshot()
        {
            return _changeTrackingProperties.Value.ToDictionary(p => p.Name, p => p.GetValue(this));
        }

        #endregion
    }
}