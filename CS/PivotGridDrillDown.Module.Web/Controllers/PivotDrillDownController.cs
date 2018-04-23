using System;
using System.Collections;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.PivotGrid.Web;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;

namespace PivotGridDrillDown.Module.Web.Controllers {

    public class PivotDrillDownController : ViewController<ListView>, IXafCallbackHandler {
        protected override void OnActivated() {
            base.OnActivated();
            WebWindow.CurrentRequestWindow.PagePreRender += new EventHandler(CurrentRequestWindow_PagePreRender);
        }
        void CurrentRequestWindow_PagePreRender(object sender, EventArgs e) {
            if (View != null) {
                ASPxPivotGridListEditor pivotGridListEditor = View.Editor as ASPxPivotGridListEditor;
                if (pivotGridListEditor != null) {
                    ASPxPivotGrid pivotGrid = pivotGridListEditor.PivotGridControl;
                    pivotGrid.ClientSideEvents.CellClick = String.Format("function(s, e){{{0}}}", XafCallbackManager.GetScript("ViewController1", "e.ColumnIndex.toString() + ';' + e.RowIndex.toString()")); ;
                }
            }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            XafCallbackManager.RegisterHandler("ViewController1", this);
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            WebWindow.CurrentRequestWindow.PagePreRender -= new EventHandler(CurrentRequestWindow_PagePreRender);
        }
        protected XafCallbackManager XafCallbackManager {
            get {
                return ((ICallbackManagerHolder)WebWindow.CurrentRequestPage).CallbackManager;
            }
        }

        #region IXafCallbackHandler Members

        public void ProcessAction(string parameter) {
            string[] indices = parameter.Split(';');
            int columnIndex = Int32.Parse(indices[0]);
            int rowIndex = Int32.Parse(indices[1]);
            PivotDrillDownDataSource drillDown = ((ASPxPivotGridListEditor)View.Editor).PivotGridControl.CreateDrillDownDataSource(columnIndex, rowIndex);
            ArrayList keysToShow = new ArrayList();
            foreach(PivotDrillDownDataRow  row in drillDown){
                object key = row[View.ObjectTypeInfo.KeyMember.Name];
                if (key != null) {
                    keysToShow.Add(key);
                }
            }
            if (keysToShow.Count > 0) {
                string viewId = Application.GetListViewId(View.ObjectTypeInfo.Type);
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(Application.CreateObjectSpace(), View.ObjectTypeInfo.Type, viewId);
                collectionSource.Criteria["SelectedObjects"] = new InOperator(ObjectSpace.GetKeyPropertyName(View.ObjectTypeInfo.Type), keysToShow);
                ListView listView = Application.CreateListView(viewId, collectionSource, true);
                ShowViewParameters svp = new ShowViewParameters(listView);
                svp.TargetWindow = TargetWindow.NewModalWindow;
                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(Frame, null));
            }
        }

        #endregion
    }

}
