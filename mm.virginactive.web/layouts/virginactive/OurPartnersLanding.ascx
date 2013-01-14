<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OurPartnersLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.OurPartnersLanding" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.OurPartners" %>

			<div id="content" class="layout-panels">
                <asp:ListView ID="OurPartnersListing" runat="server" OnItemDataBound="DetailList_OnItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                    <section class="<%# (Container.DataItem as OurPartnersDetailItem).Abstract.GetPanelCssClass() %>">
                        <div class="section-image-panel">	
                            <a href="<%# (Container.DataItem as OurPartnersDetailItem).PageSummary.Url%>" title="<%= Translate.Text("Read more about")%> <%# (Container.DataItem as OurPartnersDetailItem).PageSummary.NavigationTitle.Rendered %>"><asp:Literal ID="image" runat="server"></asp:Literal></a>
                            <div class="panel-arrow">
                                <a class="arrow" href="<%# (Container.DataItem as OurPartnersDetailItem).PageSummary.Url%>" title="<%= Translate.Text("Read more about")%> <%# (Container.DataItem as OurPartnersDetailItem).PageSummary.NavigationTitle.Rendered %>">
                                <span></span><%= Translate.Text("Read more")%></a>
                            </div>
                        </div>	
                        <div class="fl">
                            <h2><a href="<%# (Container.DataItem as OurPartnersDetailItem).PageSummary.Url%>"><%# (Container.DataItem as OurPartnersDetailItem).PageSummary.NavigationTitle.Rendered%></a></h2>
                            <ul id="ChildLinks" runat="server" class="item">
                            </ul>
                        </div>
                        <div class="summary-text"><%# (Container.DataItem as OurPartnersDetailItem).Abstract.Summary.Text%></div>
                    </section>
                    </ItemTemplate>
                </asp:ListView>
            </div> <!-- /content -->

