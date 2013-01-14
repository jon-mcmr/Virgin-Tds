<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubLanding" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<sc:FieldRenderer runat="server" fieldname="club image" Parameters="crop=940x333" />
<sc:FieldRenderer runat="server" fieldname="club image" Parameters="crop=240x308" />
<sc:FieldRenderer runat="server" fieldname="club image" Parameters="crop=300x180" />
<sc:FieldRenderer runat="server" fieldname="club image" Parameters="crop=460x210&class=test" />

<%= context.Clubimage.RenderCrop("280x180") %>
BLAH!