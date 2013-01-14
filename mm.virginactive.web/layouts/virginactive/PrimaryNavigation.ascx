<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrimaryNavigation.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.PrimaryNavigation" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/NavLinkSection.ascx" TagName="NavSection" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/FacilitiesClassesNav.ascx" TagName="FavilityClassNav" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/YourHealthNav.ascx" TagName="YourHealth" %>
<%@ Register TagPrefix="va" src="/layouts/virginactive/navigation/membershipsNav.ascx" TagName="MembershipNav" %>


                    <ul id="main-nav" class="ffb">
                        <va:FavilityClassNav ID="FacilityClassNav" runat="server" />
                        <va:YourHealth ID="YourHealth" runat="server" />
                        <va:MembershipNav ID="MemberNav" runat="server" />
                    </ul>
