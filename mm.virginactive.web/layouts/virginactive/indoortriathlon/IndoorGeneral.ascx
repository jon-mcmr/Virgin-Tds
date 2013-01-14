<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorGeneral.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorGeneral" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

		<div id="content" class="layout-inner-panels">
            <div class="partners-inner">
                <section class="full-panel">
                    <h2><%= currentItem.Subheading.Rendered%></h2>
                    <%= currentItem.Mainimage.RenderCrop("900x310") %>
                    <%= currentItem.Maincontent.Rendered%>
                </section>
                <hr />
                <asp:ListView ID="GeneralModuleListing" runat="server" OnItemDataBound="ModuleList_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                    <section class="<%# (Container.DataItem as IndoorModuleItem).Abstract.GetPanelCssClass() %>">
                        <h3><%# (Container.DataItem as IndoorModuleItem).Abstract.Subheading.Rendered%></h3>
                        <asp:Literal ID="image" runat="server"></asp:Literal>
                        <div class="summary-text"><%# (Container.DataItem as IndoorModuleItem).Abstract.Summary.Text%></div>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
                <%= GeneralModuleListing.Items.Count > 0 ? "<hr />" : ""%>
                <%--<%= currentItem.Footercontent.Rendered%>--%>
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
