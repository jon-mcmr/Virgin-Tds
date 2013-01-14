<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubFinderWidget.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ClubFinderWidget" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>
                                    <h2><%= clubFinder.Widget.Heading.Rendered %></h2>

                                    <p class="bold"><%= clubFinder.Subheading.Rendered %></p>

                                    <p><%= clubFinder.Widget.Bodytext.Rendered %></p>

                                    <p><a href="<%= clubFinder.Widget.Buttonlink.Url %>" class="btn"><%= mm.virginactive.common.Globalization.Translate.Text("Find your club") %></a></p>

                                 
