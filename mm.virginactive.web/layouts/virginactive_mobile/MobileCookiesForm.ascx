<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileCookiesForm.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileCookiesForm" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Legals" %>
		<div class="content clubfinder">
			<h1><% = cookiesForm.Pagetitle.Rendered%></h1>			
			<div class="data_block">
				 <div class="data_content">
                    <%= cookiesForm.Mobilebodytext.Text%>
				 </div>
			</div>
		</div>

