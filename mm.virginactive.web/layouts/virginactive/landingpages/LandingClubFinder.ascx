<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingClubFinder.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.landingpages.LandingClubFinder" %>
 <div class="section lightbox">
            <div class="container club_finder" id="lightbox">
                <h2>Club Finder</h2>

                <div class="columns clearfix">
                    <div class="col places_search">
                        <h3>Find your nearest club</h3>
                        <input placeholder="City, town, postcode..." id="searchTextField" />
                    </div>
                    <p class="or">Or</p>
                    <div class="col club_dropdown">
                        <h3>Choose a club</h3>
                        <asp:dropdownlist id="clubFindSelect" runat="server"></asp:dropdownlist>

                    </div>
                </div>
            </div>
        </div>