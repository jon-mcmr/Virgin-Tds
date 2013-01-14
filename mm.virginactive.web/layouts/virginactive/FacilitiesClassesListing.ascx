<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacilitiesClassesListing.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.FacilitiesLandingListing" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

            <div id="content" class="<%= CssClass%>">
                <asp:PlaceHolder ID="phListing" runat="server" />
            </div>

            <div id="va-overlay-wrap">
                <div id="overlay">
                    <h2><%= Translate.Text("Classes near you")%></h2> 
                    <div id="clubfinderform" class="frm">
                        <p><%= Translate.Text("Find out where your nearest")%> <span class="update-class">zumba</span> <%= Translate.Text("class is")%>.</p>
                        
                        <fieldset class="find">
                            <label class="ffb" for="findclub"><%= Translate.Text("I am looking for a club in")%>:</label>
                            <input id="findclub" maxlength="40" class="searchclubs" type="text" placeholder="<%= Translate.Text("Postcode, city, town or area...")%>">
                            <input type="hidden" id="input-entered" />
                        </fieldset>
                    </div>
                </div>
            </div>