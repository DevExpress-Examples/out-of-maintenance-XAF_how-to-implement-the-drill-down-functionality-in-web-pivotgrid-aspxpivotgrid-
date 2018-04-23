Imports System
Imports System.Collections
Imports System.Linq
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.PivotGrid.Web
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates
Imports DevExpress.ExpressApp.Web.Utils
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPivotGrid

Namespace PivotGridDrillDown.Module.Web.Controllers
    Public Class PivotDrillDownController
        Inherits ViewController(Of ListView)
        Implements IXafCallbackHandler
        Private ReadOnly handlerId As String
        Protected ReadOnly Property CallbackManager() As XafCallbackManager
            Get
                Return DirectCast(WebWindow.CurrentRequestPage, ICallbackManagerHolder).CallbackManager
            End Get
        End Property
        Protected Overrides Sub OnViewControlsCreated()
            MyBase.OnViewControlsCreated()

            CallbackManager.RegisterHandler(handlerId, Me)

            Dim pivotGridListEditor As ASPxPivotGridListEditor = TryCast(View.Editor, ASPxPivotGridListEditor)
            If pivotGridListEditor IsNot Nothing Then
                Dim pivotGrid As ASPxPivotGrid = pivotGridListEditor.PivotGridControl
                Dim script As String = CallbackManager.GetScript(handlerId, "e.ColumnIndex + ';' + e.RowIndex")
                ClientSideEventsHelper.AssignClientHandlerSafe(pivotGrid, "CellClick", (Convert.ToString("function(s, e) {") & script) + "}", "pivotGrid.CellClick")
            End If
        End Sub
        Public Sub New()
            handlerId = "PivotDrillDownHandler" + GetHashCode().ToString()
        End Sub
        Private Sub IXafCallbackHandler_ProcessAction(parameter As String) Implements IXafCallbackHandler.ProcessAction
            Dim indices As String() = parameter.Split(";"c)
            Dim columnIndex As Integer = Integer.Parse(indices(0))
            Dim rowIndex As Integer = Integer.Parse(indices(1))
            Dim drillDown As PivotDrillDownDataSource = DirectCast(View.Editor, ASPxPivotGridListEditor).PivotGridControl.CreateDrillDownDataSource(columnIndex, rowIndex)
            Dim name = View.ObjectTypeInfo.KeyMember.Name
            Dim keysToShow As IList = drillDown.Cast(Of PivotDrillDownDataRow)().Where(Function(row) row(name) IsNot Nothing).[Select](Function(row) row(name)).ToList()
            If keysToShow.Count > 0 Then
				Dim targetType As Type = View.ObjectTypeInfo.Type
                Dim viewId As String = Application.GetListViewId(targetType)
                Dim collectionSource As CollectionSourceBase = Application.CreateCollectionSource(Application.CreateObjectSpace(targetType), targetType, viewId)
                collectionSource.Criteria("SelectedObjects") = New InOperator(ObjectSpace.GetKeyPropertyName(targetType), keysToShow)
                Dim listView As ListView = Application.CreateListView(viewId, collectionSource, False)
                Application.ShowViewStrategy.ShowViewInPopupWindow(listView)
            End If
        End Sub
    End Class
End Namespace