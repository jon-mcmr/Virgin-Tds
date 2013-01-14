<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AboutUsDetail.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.AboutUsDetail" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.AboutUs" %>


		<div id="content" class="layout-inner-panels">

            <div class="about-us-inner">
                <section class="full-panel">
                    <h2><%= currentItem.Abstract.Subheading.Rendered%></h2>		
                    <%= currentItem.Maincontent.Rendered%>
                </section>

				<asp:ListView ID="AboutUsModuleListing" runat="server" OnItemDataBound="ModuleList_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                    <section class="<%# (Container.DataItem as AboutUsModuleItem).Abstract.GetPanelCssClass() %>">
                        <h3><%# (Container.DataItem as AboutUsModuleItem).Abstract.Subheading.Rendered%></h3>
                        <asp:Literal ID="image" runat="server"></asp:Literal>
                        <div class="summary-text"><%# (Container.DataItem as AboutUsModuleItem).Abstract.Summary.Text%></div>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div> <!-- /content -->
