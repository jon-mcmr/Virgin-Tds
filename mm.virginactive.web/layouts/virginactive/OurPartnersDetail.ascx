<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OurPartnersDetail.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.OurPartnersDetail" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.OurPartners" %>

		<div id="content" class="layout-inner-panels">
            <div class="partners-inner">
                <section class="full-panel">
                    <%= currentItem.Maincontent.Rendered%>
                </section>
                <hr />
                <asp:ListView ID="OurPartnersModuleListing" runat="server" OnItemDataBound="ModuleList_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                    <section class="<%# (Container.DataItem as OurPartnersModuleItem).Abstract.GetPanelCssClass() %>">
                        <h3><%# (Container.DataItem as OurPartnersModuleItem).Abstract.Subheading.Rendered%></h3>
                        <asp:Literal ID="image" runat="server"></asp:Literal>
                        <div class="summary-text"><%# (Container.DataItem as OurPartnersModuleItem).Abstract.Summary.Text%></div>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
                <%= OurPartnersModuleListing.Items.Count > 0 ? "<hr />" : "" %>
                <%= currentItem.Footercontent.Rendered%>
            </div>
        </div> <!-- /content -->