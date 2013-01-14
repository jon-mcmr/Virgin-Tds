<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorEvents.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorEvents" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

    <div id="content">
      <div class="layout-block indoor-tri clearfix">
        <p class="already_registered_cta"><%= Translate.Text("Already registered?")%> <a href="http://va.racetimingsystems.com/secure/athlete.aspx"><%= Translate.Text("Manage your entry here.")%></a></p>
        <div class="hero">
            <%= currentItem.Headerimage.RenderCrop("938x310")%>
		    <h2><%= currentItem.Headertitle.Rendered %></h2>	    	
            <p><%= currentItem.Headertitledetail.Rendered %></p>		    
		    <p class="cta">
                <a class="btn btn-cta-big" href="<%= registrationForm.Widget.Buttonlink.Url %>" id="btn-register"><%= registrationForm.Widget.Buttontext.Rendered %></a>
            </p>
	    </div>
    
        <div class="panel date">
            <div class="content-panel">
    		    <h2><%= currentItem.Heading.Rendered %></h2>
                <p class="intro"><%= currentItem.Headingintro.Rendered %></p>
                <%= currentItem.Headerbody.Rendered %>
            </div>
            <div class="event-date">
                <span class="cal"></span>
		        <span class="when"><%= Translate.Text("When was it?")%></span>
			    <span><%= currentItem.Datetext.Rendered %></span>
                <div class="date-b"></div>
		    </div>
	    </div>
		
	    <div class="panel event">	
		    <h3><%= currentItem.Eventheader.Rendered %></h3>
			
		    <div class="event-table">
			    <table id="duathlon">
					    <tr>
						    <th rowspan="2" class="name"><%= currentItem.Event1name.Rendered %></th>
						    <th><%= Translate.Text("Run")%></th>
						    <th><%= Translate.Text("Cycle")%></th>
                            <th rowspan="3" class="content"><%= currentItem.Event1body.Rendered %></th>
                            
					    </tr>
					    <tr class="type">
						    <td class="run"><span></span><%= Translate.Text("run")%></td>
						    <td class="cycle"><span></span><%= Translate.Text("cycle")%></td>
					    </tr>
					    <tr>
						    <th><%= Translate.Text("Distances")%></th>
						    <td>2.5km</td>
						    <td>10km</td>
                            
                        </tr>
			    </table>
		    </div>
		
		    <div class="event-table">
			    <table id="triathlon">
					    <tr>
						    <th rowspan="2" class="name"><%= currentItem.Event2name.Rendered %></th>
						    <th><%= Translate.Text("Run")%></th>
						    <th><%= Translate.Text("Cycle")%></th>
						    <th><%= Translate.Text("Swim")%></th>
                            <th rowspan="4" class="content"><%= currentItem.Event2body.Rendered %></th>
					    </tr>
					    <tr class="type">
						    <td class="run"><span></span><%= Translate.Text("run")%></td>
                            <td class="cycle"><span></span><%= Translate.Text("cycle")%></td>
						    <td class="swim"><span></span><%= Translate.Text("swim")%></td>
					    </tr>
					    <tr>
						    <th class="bb"><%= Translate.Text("Dash")%></th>
						    <td>1.2km</td>
						    <td>5km</td>
						    <td>200m</td>
					    </tr>
					    <tr>
						    <th><%= Translate.Text("Supreme")%></th>
						    <td>2.5km</td>
						    <td>10km</td>
						    <td>400m</td>
					    </tr>
			    </table>
		    </div>
        </div>
			
		    <div class="panel bg" id="panel1" runat="server"> <!-- Classes with bg -->
        	    <div class="content-panel fl">
				    <h2><%= currentItem.Panel1title.Rendered %></h2>
				    <p class="intro"><%= currentItem.Panel1subheading.Rendered %></p>
                    <%= currentItem.Panel1body.Rendered %>
                    <p><a href="<%= currentItem.Panel1link.Url  %>">Find out more</a></p>
			    </div>
                <%= currentItem.Panel1image.RenderCrop("180x120") %>
		    </div>
			
	        <div class="panel date" id="panel2" runat="server"> <!-- Kids -->
			    <div class="content-panel fl">
				    <h2><%= currentItem.Panel2title.Rendered %></h2>
                    <p class="intro"><%= currentItem.Panel2subheading.Rendered %></p>
                    <%= currentItem.Panel2body.Rendered %>
			    </div>	
			    <div class="event-date">
                    <span class="cal"></span>
		            <span class="when"><%= Translate.Text("When was it?")%></span>
                    <span><%= currentItem.KidsDatetext.Rendered %></span>
                    <div class="date-b"></div>
		        </div>
		    </div>
			
		    <div class="panel" id="panel3" runat="server"> <!-- Bring a friend -->
                <%= currentItem.Panel3image.RenderCrop("180x120") %>
			    <div class="content-panel fl">
				    <h3><%= currentItem.Panel3title.Rendered %></h3>
                    <%= currentItem.Panel3body.Rendered %>
			    </div>
		    </div>
		    <div class="panel" id="panel4" runat="server"> <!-- Prizes -->
                <%= currentItem.Panel4image.RenderCrop("180x120") %>
			    <div class="content-panel fl">
				    <h3><%= currentItem.Panel4title.Rendered %></h3>
                    <%= currentItem.Panel4body.Rendered %>
			    </div>
		    </div>
			
		    <div id="club-list">
		        <h2><%= Translate.Text("Participating Clubs")%></h2>
                <%= currentItem.Clubstext.Rendered %>
			    <ul>
                    <asp:Literal id="clubList" runat="server"></asp:Literal>                                
                </ul>
		    </div>
	    </div>
            <% if (registrationForm != null)
               {%>
		<div class="footer-cta indoor">
            <p><%= registrationForm.Widget.Heading.Rendered%></p>
            <p class="cta"> 
                <a class="btn btn-cta-big" href="<%= registrationForm.Widget.Buttonlink.Url %>"><%= registrationForm.Widget.Buttontext.Rendered%></a>
            </p>
        </div>
           <%} %>	
    </div><!-- /content -->
