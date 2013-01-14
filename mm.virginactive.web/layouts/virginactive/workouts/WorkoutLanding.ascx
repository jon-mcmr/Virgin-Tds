<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorkoutLanding.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.workouts.WorkoutLanding" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Workouts" %>


            <div id="content" class="layout-inner">
           		<h2><%= landing.Heading.Rendered %></h2>
                	<div class="float-container">
                        <div class="intro-panel">
                            <p><%= landing.Intro.Rendered %></p>
                        </div> 
                        <div class="content-panel">
                            <%= landing.Body.Text %>
                        </div>
                    </div>
                    
                    <div id="workout-result"></div>
                    <asp:PlaceHolder ID="TestPh" runat="server" />
                    
                    <div class="contrast-panel">
                    	<div class="panel-left">
                            <h4><%= landing.LeftPanelHeading.Rendered %></h4>
                            <%= landing.LeftPanelBody.Text %>
                        </div>
                    	<div class="panel-right">
                        	<h4><%= landing.RightPanelHeading.Rendered %></h4>
                            <%= landing.RightPanelBody.Text %>
                        </div>
                    </div>
                </div>
                