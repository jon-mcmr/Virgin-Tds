<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalorieCalculator.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.tools.CalorieCalculator" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

	            <div id="content" class="layout">	
	
					<div class="full-width-float">
						<div class="tools-title">
							<h2><%= tool.Title.Rendered %></h2>	
                            <p><%= tool.Teaser.Rendered %></p>	
						</div>
						
						<div id="tool-cc" class="tool-main">
							<div class="tool-intro">
								<%= tool.Body.Text %>
							</div>
							<div class="tool-data-entry">
								<p class="intro"><%= Translate.Text("Use the tool below to get an estimate of how many calories you need.")%></p>
								
								<div class="units-switch">
									<a href="#" data-weight-val="0" class="active" data-unit="imperial" data-imperial="in" data-metric="cm"><span><%= Translate.Text("Imperial")%></span></a>
									<a href="#" data-weight-val="0" data-unit="metric" data-imperial="lb" data-metric="kg"><span><%= Translate.Text("Metric") %></span></a>
								</div>
								<div class="gender-select">
									<p><%= Translate.Text("Gender") %>:</p>
									<div class="left">
										<input type="radio" id="gender-male" class="radio" name="gender" checked="checked" value="male" />
										<label for="gender-male"><%= Translate.Text("Male") %></label>
									</div>
									<div class="right">	
										<input type="radio" id="gender-female" class="radio" name="gender" value="female" />
										<label for="gender-female"><%= Translate.Text("Female") %></label>
									</div>
								</div>

								<div class="form-inputs  clearfix">
									<div class="wrap clearfix">
										<label for="label-height"><%= Translate.Text("Height")%>: <span class="unit">in</span></label>
										<input type="number" maxlength="20" id="label-height" class="text" placeholder="Enter" data-type="height" min="0" />
									</div>
										
									<div class="wrap clearfix">
										<label for="label-weight"><%= Translate.Text("Weight")%>: <span class="unit">lbs</span></label>
										<input type="number" maxlength="20" id="label-weight" class="text" placeholder="Enter" data-type="weight" min="0" />
									</div>
										
									<div class="wrap clearfix">
										<label for="label-weight"><%= Translate.Text("Age")%>: </label>
										<input type="number" maxlength="20" id="label-age" class="text" placeholder="Enter" data-type="age" min="0" />
									</div>
									<div class="wrap clearfix">
										<label for="label-activity"><%= Translate.Text("Activity level")%>: </label>	
										<select id="label-activity" data-type="activity level">
											<option value="1.25"><%= Translate.Text("Sedentary")%></option>
											<option value="1.3"><%= Translate.Text("Lightly Active")%></option>
                                            <option value="1.5"><%= Translate.Text("Moderately Active")%></option>
											<option value="1.7"><%= Translate.Text("Very Active")%></option>
											<option value="2.0"><%= Translate.Text("Extremely Active")%></option>
										</select>
									</div>
								</div>
									
								<div class="cta-box">
									<a href="#" class="btn btn-cta-xl"><%= Translate.Text("Submit")%></a>
								</div>
								
							</div>
							
                            <div class="tool-results">
                                <div class="results-intro">
                                    <p><%= Translate.Text("Fill in your details so we can work out your required calorie intake.")%></p>
                                </div>
                                <div class="show-results">
									<div class="result-value">
										<p class="main"><%= Translate.Text("Your calorie intake")%></p>
										<p><%= Translate.Text("To maintain your current weight you need the following amount of calories per day")%></p>								
										<span class="update-result">--</span>
									</div>
								</div>
                                <div class="result-message" data-errormsg="<%= Translate.Text("Please enter correct values for")%> " ></div>
							</div>
							
						</div>
				
					</div>

				</div> <!-- /content -->