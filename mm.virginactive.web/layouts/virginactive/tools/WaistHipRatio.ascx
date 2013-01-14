<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WaistHipRatio.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.tools.WaistHipRatio" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>				
				<div id="content" class="layout">	
					
					<div class="full-width-float">
						<div class="tools-title">
							<h2><%= tool.Title.Rendered %></h2>
							<p><%= tool.Teaser.Rendered %></p>				
						</div>
						
						<div id="tool-whr" class="tool-main">
							<div class="tool-intro">
								<%= tool.Body.Text %>
							</div>
							<div class="tool-data-entry">
								<p class="intro"><%= Translate.Text("Enter your details below to check your Waist-Hip Ratio (WHR).")%></p>
								
								<div class="units-switch">
									<a href="#" class="active" data-unit="imperial" data-imperial="in" data-metric="cm"><span><%= Translate.Text("Imperial") %></span></a>
									<a href="#" data-unit="metric" data-imperial="in" data-metric="cm"><span><%= Translate.Text("Metric")%></span></a>
								</div>
									
								<div class="form-inputs">
									<div class="left">
										<label for="label-waist"><%= Translate.Text("Waist")%>&nbsp;(<span class="unit">in</span>)</label>
										<input type="number" maxlength="20" id="label-waist" class="text" placeholder="Enter" data-type="waist" />
									</div>
									<div class="right">
										<label for="label-hip"><%= Translate.Text("Hip")%>&nbsp;(<span class="unit">in</span>)</label>
										<input type="number" maxlength="20" id="label-hip" class="text" placeholder="Enter" data-type="hip" />
									</div>
								</div>
									
								<div class="cta-box">
									<a href="#" class="btn btn-cta-xl"><%= Translate.Text("Submit")%></a>
								</div>
								
							</div>
							
                            <div class="tool-results">
                                <div class="results-intro">
                                    <p><%= Translate.Text("Fill in your details so we can work out your waist-hip ratio.")%></p>
                                </div>
                                <div class="show-results">
                                    <div class="result-value">
                                	    <p class="main"><%= Translate.Text("Your WHR is")%></p>
                                        <span class="update-result">--</span>
                                    </div>
                                    <div class="result-scale">
                                        <span class="result-marker">
                                    	    <span class="smile"></span>
                                        </span>
                                    </div>
                                </div>
                                <div class="result-message" data-errormsg="<%= Translate.Text("Please enter correct values for")%> "  data-extreme="<%= Translate.Text("extreme")%>" data-high="<%= Translate.Text("high")%>" data-average="<%= Translate.Text("average")%>" data-good="<%= Translate.Text("good")%>" data-excellent="<%= Translate.Text("excellent")%>" data-comment-extreme="<%= Translate.Text("Never mind. We'll soon get you back in shape.")%>" data-comment-high="<%= Translate.Text("Oops. A few more miles on the treadmill for you.")%>" data-comment-average="<%= Translate.Text("Not bad at all. Don't give up now though.")%>" data-comment-good="<%= Translate.Text("Nicely done, keep up the good work.")%>" data-comment-excellent="<%= Translate.Text("Whoo hoo, well done you!")%>"></div>
                            </div>

						</div>
					
						<h3><%= Translate.Text("How to measure")%></h3>
						<div class="half-panel fl">
							<p class="intro"><strong><%= Translate.Text("Waist")%>:</strong></p>
							<p><%= Translate.Text("While standing relaxed, measure the smallest area around your waist.The smallest area is usually around the navel or belly button. After measuring, enter the number in the box labeled 'waist'.")%></p>
						</div>
						<div class="half-panel fr">
							<p class="intro"><strong><%= Translate.Text("Hips")%>:</strong></p>
							<p><%= Translate.Text("Measure the largest area around your hips. The largest area is usually around your buttocks. After measuring, enter the number in the box labeled 'hip'.  *Caution: Do not pull the measuring tape tight around your waist or hips.")%></p>
						</div>
					</div>

				</div> <!-- /content -->