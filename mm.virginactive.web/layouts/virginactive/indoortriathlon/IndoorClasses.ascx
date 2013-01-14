<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorClasses.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorClasses" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

    <div id="content" class="layout-full">
		<div class="indoor-tri full-width-panel-layout">  
                    
            <div class="hero-panel">
                <div class="intro-panel-l">
                    <h2><%= currentItem.MainTitle.Rendered %></h2>                            
                    <p class="intro"><%= currentItem.Subheading.Rendered %></p>
                </div>
                <div class="intro-panel-r">
                    <%= currentItem.Headerbody.Rendered %>
                </div>
            </div>		
            <asp:ListView ID="TrainingModuleListing" runat="server" OnItemDataBound="TrainingModuleListing_OnItemDataBound">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                </LayoutTemplate> 
                <ItemTemplate>
                    <section class="<%# (Container.DataItem as IndoorTrainingClassItem).Cssstyle.Rendered %>">
                        <span class="cal"></span>
                        <img src="<%# (Container.DataItem as IndoorTrainingClassItem).StaticImage.Rendered %>" alt="Placeholder" />
                        <div class="content">
					        <h2><%# (Container.DataItem as IndoorTrainingClassItem).Heading.Rendered %></h2>
	              	        <h3><%# (Container.DataItem as IndoorTrainingClassItem).Subheading.Rendered %></h3>
                            <%# (Container.DataItem as IndoorTrainingClassItem).Bodytext.Rendered %>                            
                            <asp:Literal ID="ltrVideoLink" runat="server"></asp:Literal>
                            <asp:Literal ID="ltrTrainingPDF" runat="server"></asp:Literal>    
                        </div>
                    </section>
                </ItemTemplate>
            </asp:ListView>
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
    </div> <!-- /content -->

        <!-- Overlays -->
        <asp:ListView ID="TrainingVideoOverlays" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
            </LayoutTemplate> 
            <ItemTemplate>
                <div id="overlay-<%# Container.DataItemIndex%>" class="overlay hidden">
    	            <h2><%# (Container.DataItem as IndoorTrainingClassItem).Videotitle.Rendered %></h2>
                    <%# (Container.DataItem as IndoorTrainingClassItem).Videobody.Rendered %>
                    <div class="video">
    	                <iframe width="640" height="400" src="<%# (Container.DataItem as IndoorTrainingClassItem).Videolink.Rendered %>" frameborder="0" allowfullscreen></iframe>
    	            </div>
	            </div>
            </ItemTemplate>
        </asp:ListView>
