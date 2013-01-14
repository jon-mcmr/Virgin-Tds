<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobileGenericInfo.ascx.cs" Inherits="mm.virginactive.web.layouts.Mobile.MobileGenericInfo" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
		<div class="content clubfinder">
			<h1><% = Article.Pagetitle.Rendered %></h1>
			
			<div class="data_block">
				 <div class="data_content">
                    <%= Article.Pagebody.Text %>
				 </div>
			</div>
		</div>
