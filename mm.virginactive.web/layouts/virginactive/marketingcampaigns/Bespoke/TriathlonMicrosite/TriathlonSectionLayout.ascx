<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonSectionLayout.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonSectionLayout" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Campaigns" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
        <div class="wrapper-contain">
        <div class="wrapper-inner">&nbsp;</div>
        
            <div id="content" class="container">
                <h1><%= currentItem.NavigationTitle.Rendered %></h1>
                <asp:Literal ID="ltrParentSectionLink" runat="server" ></asp:Literal>
                <sc:Placeholder ID="Placeholder1" Key="sectionContent" runat="server" />
                <div class="ContentFooter">
                    <div class="childSections">
                        <asp:ListView ID="ChildSectionListing" runat="server" OnItemDataBound="SectionList_OnItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate> 
                            <ItemTemplate>
                            <section class="panel <%# (Container.DataItem as AbstractItem).GetPanelCssClass() %>">                                                                                
                                <h2><a href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%></a></h2>
                                <a href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><asp:Literal ID="image" runat="server"></asp:Literal></a>									
                                <div class="content">
                                    <%# (Container.DataItem as AbstractItem).Summary.Rendered%>
                                    <div class="panel-arrow"><a class="arrow" href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><span></span>Read<br />more</a></div>
                                </div>
                                <div class="opacity">&nbsp;</div>
                            </section>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <div class="otherSections">
                        <asp:ListView ID="OtherSectionListing" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate> 
                            <ItemTemplate>
                            <section class="panel third-panel">                        
                                <h2><a href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).NavigationTitle.Raw%></a></h2>
                                <a href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><%# (Container.DataItem as AbstractItem).Image.RenderCrop("300x180")%></a>
                                <div class="content">
                                    <p><a href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>">Find out more</a></p>
                                    <div class="panel-arrow"><a class="arrow" href="<%# new PageSummaryItem((Container.DataItem as AbstractItem).InnerItem).Url%>"><span></span>Read<br />more</a></div>
                                </div>
                                <div class="opacity">&nbsp;</div>
                            </section>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
