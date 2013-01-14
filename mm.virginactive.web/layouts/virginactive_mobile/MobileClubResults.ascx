
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileClubResults.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileClubResults" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ComponentTemplates" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>


		<div class="content clubfinder">
						
			<h1><%= Translate.Text("Club finder search results") %></h1>

            <asp:ListView ID="ClubList" runat="server" OnItemDataBound="ClubList_OnItemDataBound">						
                <LayoutTemplate>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
			        <div class="data_block">
                        <asp:Literal runat="server" ID="ltrNearestHeader"></asp:Literal>
				        <a href="<%# (Container.DataItem as Club).ClubItm.Url %>" class="data_content large_arrow">
					        <p class="address">
                                <strong><%# HtmlRemoval.StripTagsCharArray((Container.DataItem as Club).ClubItm.Clubname.Text) %></strong><br />
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                            </p>
				        </a>
				        <div class="data_footer">
					        <ul class="clearfix">
						        <li class="distance"><asp:literal ID="ltrDistanceLink" runat="server" ></asp:literal></li>                                
						        <li class="phone"><a href="tel:<%# (Container.DataItem as Club).ClubItm.Salestelephonenumber.Text.Replace(" ", "") %>"><%# (Container.DataItem as Club).ClubItm.Salestelephonenumber.Text %></a></li>
					        </ul>
				        </div>
			        </div>                     
                </ItemTemplate>
            </asp:ListView>
		</div>
