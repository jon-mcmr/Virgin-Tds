<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingGetGoing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingGetGoing" %>

<div class="section get_going">
            <div class="container">
                <div class="row">
                    <h2 class="span12"><%= currentItem.Panel4Heading.Rendered %></h2>
                </div>
                <div class="row">
                    <p class="span12 subtitle"><%= currentItem.Panel4Subheading.Rendered %></p>
                    <p class="span12"><a href="#lightbox" class="btn large lightbox cboxElement"><asp:Literal ID="litButtonText" runat="server"></asp:Literal></a></p>
                </div>
            </div>
        </div>