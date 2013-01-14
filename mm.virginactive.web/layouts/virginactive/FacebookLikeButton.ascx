<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacebookLikeButton.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.FacebookLikeButton" %>

<!--<div id="fb-root"></div>-->
<script src="<%= ScriptSourceURL%>"></script>
<fb:like href="<%= ScriptSourceURL%>" send="false" layout="button_count" show_faces="false" colorscheme="light" font="arial"></fb:like>
<!--
facebook parameter notes:
send - specifies whether to include a Send button with the Like button
layout - there are three options:
            standard - displays social text to the right of the button and friends' profile photos below. Minimum width: 225 pixels. Default width: 450 pixels. Height: 35 pixels (without photos) or 80 pixels (with photos).
            button_count - displays the total number of likes to the right of the button. Minimum width: 90 pixels. Default width: 90 pixels. Height: 20 pixels.
            box_count - displays the total number of likes above the button. Minimum width: 55 pixels. Default width: 55 pixels. Height: 65 pixels.
show_faces - specifies whether to display profile photos below the button (standard layout only)
font - the font to display in the button. Options: 'arial', 'lucida grande', 'segoe ui', 'tahoma', 'trebuchet ms', 'verdana'
-->

