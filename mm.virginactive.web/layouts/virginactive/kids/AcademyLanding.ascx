<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AcademyLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.kids.AcademyLanding" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Kids" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.KidsShared" %>

             <div id="content" class="layout-inner-panels">
                <section class="full-panel full-panel-active">	
                    <%= Landing.Abstract.Image.RenderCrop("440x300") %>
                    <div class="panel-inner">
                        <h2 class="active rep"><span></span><%= Landing.PageSummary.NavigationTitle.Rendered %></h2>
                        <%= Landing.Body.Text %>
                    </div>
                </section>
                <div class="saq-panel">
                    <img src="/virginactive/images/saq-international.gif" alt="SAQ International Training Courses" />
                    <div class="panel-inner">
                        <h2><%= Landing.Featureheading.Rendered %></h2>
                         <%= Landing.Featurebody.Text %>
                    </div>
                </div>

                <asp:ListView runat="server" ID="PanelList">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                    </LayoutTemplate>

                    <ItemTemplate>
                    <section class="half-panel">
                    <%# (Container.DataItem as KidsFeatureItem).Abstract.Image.RenderCrop("170x210") %>          
                    <div class="panel-inner">
                        <h3><%# (Container.DataItem as KidsFeatureItem).Abstract.Subheading.Rendered %></h3>
                        
                        <div class="panel-content">
                            <%# (Container.DataItem as KidsFeatureItem).Abstract.Summary.Text %>
						    <ul>
                                <li><%= MoreLink %></li>
                            </ul>
                        </div>
                    </div>    
                    </section>                        
                    </ItemTemplate>
                </asp:ListView>
                
                
                <div class="additional-panel">
                	<h3><%= Landing.Bottomheading.Rendered %></h3>
                    <%= Landing.Bottombody.Text %>
                </div>
                
                <div class="contrast-cta">
                    <p><%= Translate.Text("See our Active Academy facilities by getting in touch today")%></p> 
                    <%= MoreBtnLink  %>
                </div> 
            </div> <!-- /content -->