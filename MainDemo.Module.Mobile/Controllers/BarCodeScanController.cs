using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using MainDemo.Module.BusinessObjects;

namespace MainDemo.Module.Mobile.Controllers {
    public class BarCodeScanController : ObjectViewController<DetailView, Contact> {
        public const string EnabledInEditMode = "EnabledInEditMode";
        private SimpleAction scanBarCodeAction;
        public BarCodeScanController() {
            scanBarCodeAction = new SimpleAction(this, "Scan", PredefinedCategory.Edit);
            scanBarCodeAction.ImageName = "Photo";
            scanBarCodeAction.RegisterClientScriptOnExecute("ScanScript", GetBarCodeClientScript());
        }
        protected override void OnActivated() {
            base.OnActivated();
            scanBarCodeAction.Enabled[EnabledInEditMode] = View.ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
        }
        protected string GetBarCodeClientScript() {
            return @"if (cordova && cordova.plugins && cordova.plugins.barcodeScanner) { 
                     cordova.plugins.barcodeScanner.scan( 
                        function (scanResult) { 
                            if (!scanResult.cancelled) { 
                                $model.CurrentObject.Email = scanResult.text; 
                            } 
                         }, 
                         function(scanError) { 
                             DevExpress.ui.notify({ closeOnClick: true, message: scanError, type: 'error' }, 'error', 5000); 
                         } 
                     );
                 }";
        }
        public SimpleAction ScanBarCodeAction {
            get { return scanBarCodeAction; }
        }
    }
}
