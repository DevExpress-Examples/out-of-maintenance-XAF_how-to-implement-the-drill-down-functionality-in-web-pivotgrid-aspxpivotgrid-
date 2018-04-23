Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel

Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace PivotGridDrillDown.Module.BusinessObjects
	<DefaultClassOptions> _
	Public Class DomainObject1
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property
		Private _Value1 As Integer
		Public Property Value1() As Integer
			Get
				Return _Value1
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue("Value1", _Value1, value)
			End Set
		End Property
		Private _Value2 As Integer
		Public Property Value2() As Integer
			Get
				Return _Value2
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue("Value2", _Value2, value)
			End Set
		End Property
		Private _Value3 As Integer
		Public Property Value3() As Integer
			Get
				Return _Value3
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue("Value3", _Value3, value)
			End Set
		End Property
	End Class

End Namespace
