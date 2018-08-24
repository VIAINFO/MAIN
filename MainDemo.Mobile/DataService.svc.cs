using System;
using DevExpress.ExpressApp.Mobile.Services;
using DevExpress.ExpressApp;

namespace MainDemo.Mobile {
    public class DataService : MobileDataService<MainDemoMobileApplication> {
        public DataService() {
            Application.ObjectSpaceCreated += application_ObjectSpaceCreated;
        }

        void application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e) {
            e.ObjectSpace.ObjectSaving += ObjectSpace_ObjectSaving;
            e.ObjectSpace.Disposed += ObjectSpace_Disposed;
        }

        void ObjectSpace_Disposed(object sender, EventArgs e) {
            ((IObjectSpace)sender).ObjectSaving -= ObjectSpace_ObjectSaving;
        }
        void ObjectSpace_ObjectSaving(object sender, ObjectManipulatingEventArgs e) {
            string siteMode = System.Configuration.ConfigurationManager.AppSettings["SiteMode"];
            if(siteMode != null && siteMode.ToLower() == "true") {
                throw new InvalidOperationException("Data modifications are not allowed in this demo.");

            }
        }
        public override void Dispose() {
            if(Application != null) {
                Application.ObjectSpaceCreated -= application_ObjectSpaceCreated;
            }
            base.Dispose();
        }
    }
}
