<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4"
    Namespace="DevExpress.Web" TagPrefix="dxge" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.0.0, Culture=neutral, PublicKeyToken=79868b8147b5eae4"
    Namespace="DevExpress.XtraScheduler" TagPrefix="dxschsc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>

<script type="text/javascript"><!--
    function ShowAppointmentInfo(apt){
             var id = apt.GetId();
             var subj = apt.GetSubject();
         alert('This appointment has the ID ' + id + ' and the subject is ' + subj); 
    }
    
//--></script>

<body>
    <form id="form1" runat="server">
        
        <asp:ObjectDataSource ID="appointmentDataSource" runat="server" DataObjectTypeName="CustomEvent"
            TypeName="CustomEventDataSource" DeleteMethod="DeleteMethodHandler" SelectMethod="SelectMethodHandler"
            InsertMethod="InsertMethodHandler" UpdateMethod="UpdateMethodHandler" OnObjectCreated="appointmentsDataSource_ObjectCreated">
        </asp:ObjectDataSource>
        <div>
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" Width="400px" OnAppointmentInserting="ASPxScheduler1_AppointmentInserting" Start="2009-11-05" ClientInstanceName="scheduler">
            <Views>
                <DayView>
                    <TimeRulers>
                        <dxschsc:TimeRuler>
                        </dxschsc:TimeRuler>
                    </TimeRulers>
                    <DayViewStyles ScrollAreaHeight="450px">
                    </DayViewStyles>
                </DayView>
                <WorkWeekView>
                    <TimeRulers>
                        <dxschsc:TimeRuler>
                        </dxschsc:TimeRuler>
                    </TimeRulers>
                </WorkWeekView>
            </Views>
            <ClientSideEvents AppointmentDoubleClick="function(s, e) {
                    var apt = scheduler.GetAppointmentById(e.appointmentId);         
                    scheduler.RefreshClientAppointmentProperties(apt,AppointmentPropertyNames.Normal,ShowAppointmentInfo);
                    e.handled = true;
            }" 
            Init="function(s, e) {
	scheduler.SetTopRowTime(60*60*9*1000);
            }"/>
            <OptionsCustomization AllowInplaceEditor="None" />
            <OptionsToolTips ShowAppointmentToolTip="False" />
        </dxwschs:ASPxScheduler>
        </div>
    </form>
</body>
</html>
