<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralPage.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.GeneralPage" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>

            <div id="content" class="layout-inner">
            	<div class="single-article">                    
                    <h2><% = Article.Pagetitle.Rendered %></h2>
                    <%= Article.Pagebody.Text %>
                </div>
            </div> <!-- /content -->