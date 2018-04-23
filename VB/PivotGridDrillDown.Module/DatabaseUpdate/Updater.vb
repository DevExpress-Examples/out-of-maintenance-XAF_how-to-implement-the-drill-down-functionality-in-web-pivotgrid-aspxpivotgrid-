Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports PivotGridDrillDown.Module.BusinessObjects

Namespace PivotGridDrillDown.Module.DatabaseUpdate
    Public Class Updater
        Inherits ModuleUpdater
        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub
        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            CreateDomainObject1("Obj 1", 1, 1, 2)
            CreateDomainObject1("Obj 2", 1, 0, 1)
            CreateDomainObject1("Obj 1", 2, 1, 0)
            CreateDomainObject1("Obj 2", 2, 2, 1)
            CreateDomainObject1("Obj 1", 2, 1, 1)
        End Sub
        Private Sub CreateDomainObject1(ByVal name As String, ByVal value1 As Integer, ByVal value2 As Integer, ByVal value3 As Integer)
            Dim obj = ObjectSpace.CreateObject(Of DomainObject1)()
            obj.Name = name
            obj.Value1 = value1
            obj.Value2 = value2
            obj.Value3 = value3
        End Sub
    End Class
End Namespace
