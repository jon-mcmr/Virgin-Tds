<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonPeopleOverlay.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonPeopleOverlay" %>

<div id="people-content<%= profile.Name.Replace(" ", "") %>" class="overlay hidden people-content">
    <a href="#" class="button-close">CLOSE <span>[X]</span></a>
    <h2><%= profile.Person.Firstname.Rendered%> <%= profile.Person.Lastname.Rendered%></h2>
                
    <div class="inner">
        <%= profile.Person.Profileimage.RenderCrop("300x180")%>
        <blockquote>"<%= profile.Person.Quote.Rendered%>"</blockquote>
        <p><strong>Time:</strong> <%= profile.Time.Rendered%></p>
        <%= profile.Person.Biotext.Rendered%>
    </div>
</div>
