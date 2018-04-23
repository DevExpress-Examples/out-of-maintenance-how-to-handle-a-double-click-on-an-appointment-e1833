Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private ReadOnly Property Storage() As ASPxSchedulerStorage
		Get
			Return ASPxScheduler1.Storage
		End Get
	End Property

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		SetupMappings()
		ResourceFiller.FillResources(Me.ASPxScheduler1.Storage, 3)

		ASPxScheduler1.AppointmentDataSource = appointmentDataSource
		ASPxScheduler1.DataBind()

		If (Not Page.IsCallback) Then
			ASPxScheduler1.Start = DateTime.Today
			ASPxScheduler1.GroupType = SchedulerGroupType.Resource
			AddTestAppointment()
		End If

	End Sub


	Private Sub SetupMappings()
		Dim mappings As ASPxAppointmentMappingInfo = Storage.Appointments.Mappings
		Storage.BeginUpdate()
		Try
			mappings.AppointmentId = "Id"
			mappings.Start = "StartTime"
			mappings.End = "EndTime"
			mappings.Subject = "Subject"
			mappings.AllDay = "AllDay"
			mappings.Description = "Description"
			mappings.Label = "Label"
			mappings.Location = "Location"
			mappings.RecurrenceInfo = "RecurrenceInfo"
			mappings.ReminderInfo = "ReminderInfo"
			mappings.ResourceId = "OwnerId"
			mappings.Status = "Status"
			mappings.Type = "EventType"
		Finally
			Storage.EndUpdate()
		End Try
	End Sub

	Protected Sub appointmentsDataSource_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
		e.ObjectInstance = New CustomEventDataSource(GetCustomEvents())
	End Sub
	Private Function GetCustomEvents() As CustomEventList
		Dim events As CustomEventList = TryCast(Session("ListBoundModeObjects"), CustomEventList)
		If events Is Nothing Then
			events = New CustomEventList()
			Session("ListBoundModeObjects") = events
		End If
		Return events
	End Function

	Protected Sub ASPxScheduler1_AppointmentInserting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
		SetAppointmentId(sender, e)
	End Sub

	Private Sub SetAppointmentId(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
		Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
		Dim apt As Appointment = CType(e.Object, Appointment)
		storage.SetAppointmentId(apt, apt.GetHashCode())
	End Sub
	Private Sub AddTestAppointment()
		ASPxScheduler1.Storage.Appointments.Clear()
		Dim apt As Appointment = ASPxScheduler1.Storage.CreateAppointment(DevExpress.XtraScheduler.AppointmentType.Normal)
		apt.BeginUpdate()
		apt.Start = DateTime.Now
		apt.End = apt.Start.AddMinutes(90)
		apt.ResourceId = "sbrighton"
		apt.Subject = "Test"
		apt.Description = "Double-click this appointment to display information in a dialog box."
		apt.EndUpdate()
		ASPxScheduler1.Storage.Appointments.Add(apt)
	End Sub
End Class
