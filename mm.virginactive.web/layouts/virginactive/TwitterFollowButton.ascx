<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterFollowButton.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.TwitterFollowButton" %>

<!--
twitter parameter notes:
data-show-count - Followers count display
data-button - Button color
data-text-color - Button color
data-link-color - Link color
data-lang - Language
-->

<script src="<%= ScriptSourceURL%>" type="text/javascript"></script>
<a href="http://twitter.com/<%= twitterFollowButton.Twitterusername.Rendered%>" class="twitter-follow-button" data-show-count="false" ></a>

