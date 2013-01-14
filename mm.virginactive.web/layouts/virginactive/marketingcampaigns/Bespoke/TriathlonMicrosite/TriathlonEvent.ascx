<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonEvent.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonEvent" %>

<div class="mainContent event">
    <section class="full-width">
        
        <div class="intro hero">
        <h3><%= currentItem.Panel1Heading.Rendered %></h3>
            <%= currentItem.Panel1BodyText.Rendered %>
            <a class="btn-club" href="<%= currentItem.Panel1File.MediaUrl %>"><%= currentItem.Panel1LinkText.Rendered %></a>
        </div>
        <%= currentItem.Panel1Image.RenderCrop("440x300")%>
    </section>    
   
    <section class="full-width static-panel">
        <div class="intro">
                <h3><%= currentItem.Panel2Heading.Rendered %></h3>
                <%= currentItem.Panel2BodyText.Rendered %>
        </div>
        
        <div class="half-width">
            <h4>Saturday Distances</h4>
            <table id="table1">
            <caption>Saturday Distances</caption>
                <thead>
                    <th class="empty"></th>
                    <th>Swim</th>
                    <th>Bike</th>
                    <th>Run</th>
                </thead>
                <tbody>
                    <tr class="row">
                        <th>Super Sprint</th>
                        <td>1 lap</td>
                        <td>1 lap</td>
                        <td>1 lap</td>
                    </tr>
                   <tr class="row">
                        <th>Sprint</th>
                        <td>1 lap</td>
                        <td>2 laps</td>
                        <td>2 laps</td>
                    </tr>
                    <tr class="row last-row">
                        <th>Olympic Team</th>
                        <td>2 laps</td>
                        <td>4 laps</td>
                        <td>4 laps</td>
                    </tr>
                </tbody>
            </table>

        </div>
            
        <div class="half-width">
            <h4>Sunday Bike Route Details</h4>
            <table id="table2">
            <caption>Sunday Bike Route Details</caption>
                <tbody>
                    <tr class="row">
                        <th>Sunday Morning Westminster Route</th>
                        <td>1st lap 14km (turning at Billingsgate) 2nd lap 26km (turning at Westminster)</td>
                    </tr>
                   <tr class="row">
                        <th>Senior Elite Race</th>
                        <td>1st lap 26km (turning at Westminster) 2nd lap 14km (turning at Billingsgate)</td>
                    </tr>
                    <tr class="row last-row">
                        <th>Sunday Afternoon Tower Bridge Route</th>
                        <td>2 laps (turning at Tower Bridge)</td>
                    </tr>
                </tbody>
            </table>

        </div>
   
    </section>

    <section class="listing full-listing">
        <%= currentItem.Panel3Image.RenderCrop("220x120")%>                                              
        <div class="inner">
            <h4><%= currentItem.Panel3Heading.Rendered %></h4>
            <%= currentItem.Panel3BodyText.Rendered %>
            <p class="more"><%= panel3link %></p>
        </div>                                                
    </section>

</div>
            <% if (clubFinder != null)
               {%>
                <div class="cta-panel">
                    <div class="cta-panel-inner">
                        <h3><%= clubFinder.Widget.Heading.Rendered%></h3>
                        <%= clubFinder.Widget.Bodytext.Rendered%>
                        <a href="<%= clubFinder.Widget.Buttonlink.Url%>" class="btn-club" target="_blank"><%= clubFinder.Widget.Buttontext.Rendered%></a>
                    </div>
               </div>
           <%} %>
