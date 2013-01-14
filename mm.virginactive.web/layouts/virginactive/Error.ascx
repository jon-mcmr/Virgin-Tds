<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Error.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.Error" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            <div class="container">
            <div id="content" class="layout-error">
            	<img src="/virginactive/images/error.png" width="381" height="383" class="fl" alt="Error" />
				<div class="panel-content">
					<h1><%= ErrorItm.PageSummary.NavigationTitle.Text %></h1>
					<%= ErrorItm.Body.Text %>
					
					<ul>
					<li><a class="btn btn-cta-big" href="<%= clubFinder.Url %>"><%= Translate.Text("Find your club")%></a></li>
						<li><a class="btn btn-cta-big" href="<%= classes.Url %>"><%= Translate.Text("View classes")%></a></li>
						<li><a class="btn btn-cta-big last" href="<%= enqForm.Url + "?sc_trk=enq" %>"><%= Translate.Text("Book a Visit")%></a></li>
					</ul>
					
					<p><%= Translate.Text("None of the above? In that case, maybe it's better if you head to the") + " "%><a href="/"><%= Translate.Text("homepage")%></a></p>
				</div>
    					
            </div> <!-- /content -->
            </div>