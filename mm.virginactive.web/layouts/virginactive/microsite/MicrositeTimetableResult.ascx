<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubTimetableResult.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ClubTimetableResult" %>
<%@ Import Namespace="mm.virginactive.orm.Timetable" %>
<%@ Import Namespace="mm.virginactive.webservices.virginactive.classtimetable" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

    <asp:Repeater runat="server" ID="rptDay" OnItemDataBound="rptDay_OnItemDataBound">
        <ItemTemplate>
            <div id="<%# GetTimetableClassName(Container.DataItem as Timetable)%>" class="timetable clearfix">
                <p class="calendar">
                    <span class="day"><%# GetTimetableDay(Container.DataItem as Timetable)%></span>
                    <span class="date"><%# GetTimetableDate(Container.DataItem as Timetable)%></span>
                </p>
                <table>
                    <tr>
                        <th scope="col" class="start"><%= Translate.Text("Start")%></th>
                        <th scope="col" class="end"><%= Translate.Text("End")%></th>
                        <th scope="col" class="name"><%= Translate.Text("Class Name")%></th>
                        <th scope="col" class="studio"><%= VenueHeading%></th>
                        <th scope="col" class="instructor"><%= Translate.Text("Instructor")%></th>
                        <th scope="col" class="booking"><%= Translate.Text("Booking")%></th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptClass" OnItemDataBound="rptClass_OnItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td><%# (Container.DataItem as Class).StartTime.Value.ToString("HH:mm")%></td>
                                <td><%# (Container.DataItem as Class).EndTime.Value.ToString("HH:mm")%></td>
                                <td class="classdetails"><span class="classname"><%# (Container.DataItem as Class).Classname%></span><asp:Literal ID="ClassInfoBox" runat="server"></asp:Literal></td>
                                <td><%# (Container.DataItem as Class).Studio%></td>
                                <td><%# (Container.DataItem as Class).Teacher%></td>
                                <td><asp:Literal ID="BookClassOrTokenLink" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>
