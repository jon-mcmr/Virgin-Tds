<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookComments.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.FacebookComments" %>

<div id="fb-root"></div>
<script src="<%#ScriptSourceURL%>"></script>
<!--colorscheme options are 'light' or 'dark' -->
<fb:comments href="<%#SiteURL%>" num_posts="<%#NumberOfPosts%>" width="500" colorscheme="light"></fb:comments>


