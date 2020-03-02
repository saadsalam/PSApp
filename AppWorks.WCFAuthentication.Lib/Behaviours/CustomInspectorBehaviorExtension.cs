using System;
using System.ServiceModel.Configuration;

namespace AppWorks.WCFAuthentication.Lib.Behaviours
{
    public class CustomInspectorBehaviorExtension : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new CustomInspectorBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(CustomInspectorBehavior); }

        }
    }
}
