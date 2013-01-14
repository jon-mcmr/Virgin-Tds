<!-- Control Language="C#" AutoEventWireup="true" CodeBehind="CookieRibbon.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.CookieRibbon" -->
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CookieRibbon.ascx.cs" Inherits="Sitecore.Modules.Blog.layouts.CookieRibbon" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.ModuleTemplates" %>

            <% if (cookieRibbon != null)
               {%>
<div id="cookie-ribbon">
	<section class="container">
	    <div>
            <%= cookieRibbon.Bodytext.Rendered%>
	    </div>
	    <!--<a class="btn disable-cookies" href="#"><%= mm.virginactive.common.Globalization.Translate.Text("Leave cookies off")%></a>-->
        <a class="btn" href="<%= CookieFormUrl %>"><%= cookieRibbon.Buttontext.Rendered%></a>
	</section>
</div>
           <%} %>

