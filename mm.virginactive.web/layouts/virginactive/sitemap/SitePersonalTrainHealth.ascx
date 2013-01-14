<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SitePersonalTrainHealth.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.sitemap.SitePersonalTrainHealth" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

                    <%= HeaderIsH2? "<h2>":"<h3>" %><a href="<%= personalTrainingLanding.Url%>"><%= personalTrainingLanding.NavigationTitle.Text%></a><%= HeaderIsH2? "</h2>":"</h3>" %>
                    <ul>
                        <li><a href="<%= personalTrainingLanding.Url%>#achievingyourgoals"><%= Translate.Text("Achieving Your Goals")%></a></li>
                        <li><a href="<%= personalTrainingLanding.Url%>"><%= Translate.Text("Training Packages") %></a></li>
                    </ul>

                    <va:NavSection 
                        ID="KidsSection"
                        runat="server" />    

                    <h3><a href="<%= healthAndBeautyLanding.Url%>"><%= healthAndBeautyLanding.NavigationTitle.Text%></a></h3>
                    <ul>
                        <li><a href="<%= healthAndBeautyLanding.Url%>">Spa Treatments</a></li>
                        <asp:PlaceHolder ID="HealthSection" runat="server" />
                    </ul>
                    