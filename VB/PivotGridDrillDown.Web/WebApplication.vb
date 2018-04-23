Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo

Namespace PivotGridDrillDown.Web
	Partial Public Class PivotGridDrillDownAspNetApplication
		Inherits WebApplication
		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As PivotGridDrillDown.Module.PivotGridDrillDownModule
		Private module4 As PivotGridDrillDown.Module.Web.PivotGridDrillDownAspNetModule
		Private pivotGridAspNetModule1 As DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule
		Private pivotGridModule1 As DevExpress.ExpressApp.PivotGrid.PivotGridModule
		Private sqlConnection1 As System.Data.SqlClient.SqlConnection

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection, true)
		End Sub

		Private Sub PivotGridDrillDownAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
			e.Updater.Update()
			e.Handled = True
		
		End Sub

		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New PivotGridDrillDown.Module.PivotGridDrillDownModule()
			Me.module4 = New PivotGridDrillDown.Module.Web.PivotGridDrillDownAspNetModule()
			Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
			Me.pivotGridAspNetModule1 = New DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule()
			Me.pivotGridModule1 = New DevExpress.ExpressApp.PivotGrid.PivotGridModule()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' sqlConnection1
			' 
			Me.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.\SQLEXPRESS;Initial Catalog=P" & "ivotGridDrillDown"
			Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
			' 
			' PivotGridDrillDownAspNetApplication
			' 
			Me.ApplicationName = "PivotGridDrillDown"
			Me.Connection = Me.sqlConnection1
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.module4)
			Me.Modules.Add(Me.pivotGridModule1)
			Me.Modules.Add(Me.pivotGridAspNetModule1)
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.PivotGridDrillDownAspNetApplication_DatabaseVersionMismatch);
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace
