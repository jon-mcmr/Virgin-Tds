<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndoorLinks.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.indoortriathlon.IndoorLinks" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.IndoorTriathlon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

            <div id="content" class="layout-inner">                 
                <h2><% = currentItem.Heading.Rendered%></h2>
                <%= currentItem.Headerbody.Text%>

                <!--LINKS-->
                <asp:ListView ID="LinkPanels" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate> 
                    <ItemTemplate>
                        <section class="listing full-listing">
                            <a href="<%#(Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl != ""? (Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl : (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttonlink.Url + "\" target=\"_blank" %>">                                              
                            <%# (Container.DataItem as FileImageLinkWidgetItem).Image.RenderCrop("220x120")%>
                            </a>	
                            <div class="inner">
                                <h4><%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Heading.Rendered%></h4>
                                <%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Bodytext.Rendered%>
                                <p class="more"><a href="<%#(Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl != ""? (Container.DataItem as FileImageLinkWidgetItem).File.MediaUrl : (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttonlink.Url + "\" target=\"_blank" %>"><%# (Container.DataItem as FileImageLinkWidgetItem).Widget.Buttontext.Rendered%></a></p>
                            </div>                                                
                        </section>
                    </ItemTemplate>
                </asp:ListView>

            </div> <!-- /content -->
