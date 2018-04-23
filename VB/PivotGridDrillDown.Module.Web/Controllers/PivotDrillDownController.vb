Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.PivotGrid.Web
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPivotGrid

Namespace PivotGridDrillDown.Module.Web.Controllers

	Public Class PivotDrillDownController
		Inherits ViewController(Of ListView)
		Implements IXafCallbackHandler
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			AddHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
		End Sub
		Private Sub CurrentRequestWindow_PagePreRender(ByVal sender As Object, ByVal e As EventArgs)
			Dim pivotGridListEditor As ASPxPivotGridListEditor = TryCast(View.Editor, ASPxPivotGridListEditor)
			If pivotGridListEditor IsNot Nothing Then
				Dim pivotGrid As ASPxPivotGrid = pivotGridListEditor.PivotGridControl
				pivotGrid.ClientSideEvents.CellClick = String.Format("function(s, e){{{0}}}", XafCallbackManager.GetScript("ViewController1", "e.ColumnIndex.toString() + ';' + e.RowIndex.toString()"))

			End If
		End Sub
		Protected Overrides Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			XafCallbackManager.RegisterHandler("ViewController1", Me)
		End Sub
		Protected Overrides Sub OnDeactivated()
			MyBase.OnDeactivated()
			RemoveHandler WebWindow.CurrentRequestWindow.PagePreRender, AddressOf CurrentRequestWindow_PagePreRender
		End Sub
		Protected ReadOnly Property XafCallbackManager() As XafCallbackManager
			Get
				Return (CType(WebWindow.CurrentRequestPage, ICallbackManagerHolder)).CallbackManager
			End Get
		End Property

		#Region "IXafCallbackHandler Members"

        Public Sub ProcessAction(ByVal parameter As String) Implements IXafCallbackHandler.ProcessAction
            Dim indices() As String = parameter.Split(";"c)
            Dim columnIndex As Integer = Int32.Parse(indices(0))
            Dim rowIndex As Integer = Int32.Parse(indices(1))
            Dim drillDown As PivotDrillDownDataSource = (CType(View.Editor, ASPxPivotGridListEditor)).PivotGridControl.CreateDrillDownDataSource(columnIndex, rowIndex)
            Dim keysToShow As New ArrayList()
            For i As Integer = 0 To drillDown.RowCount - 1
                Dim obj As Object = drillDown(i)(0)
                If obj IsNot Nothing Then
                    keysToShow.Add(ObjectSpace.GetKeyValue(obj))
                End If
            Next i
            Dim viewId As String = Application.GetListViewId(View.ObjectTypeInfo.Type)
            Dim collectionSource As CollectionSourceBase = Application.CreateCollectionSource(Application.CreateObjectSpace(), View.ObjectTypeInfo.Type, viewId)
            collectionSource.Criteria("SelectedObjects") = New InOperator(ObjectSpace.GetKeyPropertyName(View.ObjectTypeInfo.Type), keysToShow)
            Dim listView As ListView = Application.CreateListView(viewId, collectionSource, True)
            Dim svp As New ShowViewParameters(listView)
            svp.TargetWindow = TargetWindow.NewModalWindow
            Application.ShowViewStrategy.ShowView(svp, New ShowViewSource(Frame, Nothing))
        End Sub

		#End Region
	End Class

End Namespace
