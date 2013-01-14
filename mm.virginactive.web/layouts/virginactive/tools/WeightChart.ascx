<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightChart.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.tools.WeightChart" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>				
				<div id="content" class="layout">
					
					<div class="full-width-float">
						<div class="tools-title">
							<h2><%= tool.Title.Rendered %></h2>
							<p><%= tool.Teaser.Rendered %></p>	
						</div>
						
						<div id="tool-wc" class="tool-main">
							<div class="tool-intro">
								<%= tool.Body.Text %>
							</div>
						    <img src="/virginactive/images/tools/weight-chart.jpg" alt="<%= Translate.Text("This is a rough guide to estimate the healthy weight range for your height")%>" class="wc">
						</div>
						
					</div>

				</div> <!-- /content -->
							