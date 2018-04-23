using System;
using System.Collections;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotGrid.Web;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Utils;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;

namespace PivotGridDrillDown.Module.Web.Controllers {
    public class PivotDrillDownController : ViewController<ListView>, IXafCallbackHandler {
        private readonly string handlerId;

        protected XafCallbackManager CallbackManager {
            get { return ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager; }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();

            CallbackManager.RegisterHandler(handlerId, this);

            ASPxPivotGridListEditor pivotGridListEditor = View.Editor as ASPxPivotGridListEditor;
            if(pivotGridListEditor != null) {
                ASPxPivotGrid pivotGrid = pivotGridListEditor.PivotGridControl;
                string script = CallbackManager.GetScript(handlerId, "e.ColumnIndex + ';' + e.RowIndex");
                ClientSideEventsHelper.AssignClientHandlerSafe(pivotGrid, "CellClick", "function(s, e) {" + script + "}", "pivotGrid.CellClick");
            }
        }
        public PivotDrillDownController() {
            handlerId = "PivotDrillDownHandler" + GetHashCode();
        }
        void IXafCallbackHandler.ProcessAction(string parameter) {
            string[] indices = parameter.Split(';');
            int columnIndex = Int32.Parse(indices[0]);
            int rowIndex = Int32.Parse(indices[1]);
            PivotDrillDownDataSource drillDown = ((ASPxPivotGridListEditor)View.Editor).PivotGridControl.CreateDrillDownDataSource(columnIndex, rowIndex);
            string name = View.ObjectTypeInfo.KeyMember.Name;
            IList keysToShow = drillDown.Cast<PivotDrillDownDataRow>().Where(row => row[name] != null).Select(row => row[name]).ToList();
            if(keysToShow.Count > 0) {
                Type targetType = View.ObjectTypeInfo.Type;
                string viewId = Application.GetListViewId(targetType);
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(Application.CreateObjectSpace(targetType), targetType, viewId);
                collectionSource.Criteria["SelectedObjects"] = new InOperator(ObjectSpace.GetKeyPropertyName(targetType), keysToShow);
                ListView listView = Application.CreateListView(viewId, collectionSource, false);
                Application.ShowViewStrategy.ShowViewInPopupWindow(listView);
            }
        }
    }
}