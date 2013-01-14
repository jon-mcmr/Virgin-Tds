<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BmiCalculator.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.tools.BmiCalculator" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

				<div id="content" class="layout">
					<div class="full-width-float">
						<div class="tools-title">
							<h2><%= tool.Title.Rendered %></h2>
							<p><%= tool.Teaser.Rendered %></p>
						</div>
						
						<div id="tool-bmi" class="tool-main">
							<div class="tool-intro">
								<%= tool.Body.Text %>
							</div>
							<div class="tool-data-entry">
								<p class="intro"><%= Translate.Text("Enter your details below to check your Body Mass Index (BMI).") %></p>
								
								<div class="units-switch">
									<a href="#" class="active" data-unit="imperial" data-imperial="in" data-metric="cm"><span><%= Translate.Text("Imperial") %></span></a>
									<a href="#" data-unit="metric" data-imperial="lb" data-metric="kg"><span><%= Translate.Text("Metric") %></span></a>
								</div>
									
								<div class="gender-select">
                                    <p><%= Translate.Text("Gender") %>:</p>
                                    <div class="wrap">
                                        <div class="left">
                                            <input type="radio" id="gender-male" class="radio" name="gender" checked="checked" />
                                            <label for="gender-male"><%= Translate.Text("Male") %></label>
                                        </div>
                                        <div class="right">	
                                            <input type="radio" id="gender-female" class="radio" name="gender" />
                                            <label for="gender-female"><%= Translate.Text("Female") %></label>
                                        </div>
                                    </div>
								</div>
									
								<div class="form-inputs">
									<div class="left">
										<label class="label-height"><%= Translate.Text("Height") %> (<span class="unit">in</span>)</label>
										<input type="number" maxlength="20" id="label-height" class="text" placeholder="Enter" data-type="height" />
									</div>
									<div class="right">
										<label class="label-weight"><%= Translate.Text("Weight") %> (<span class="unit">lbs</span>)</label>
										<input type="number" maxlength="20" id="label-weight" class="text" placeholder="Enter" data-type="weight" />
									</div>
								</div>
									
								<div class="cta-box">
									<a href="#" class="btn btn-cta-xl"><%= Translate.Text("Submit") %></a>
								</div>
								
							</div>
							
							<div class="tool-results">
								<div class="results-intro">
                                    <p><%= Translate.Text("Fill in your details so we can work out your BMI.")%></p>
								</div>
                                <div class="show-results">
                                    <div class="result-value">
                                        <p class="main">Your BMI is</p>
                                        <span class="update-result">--</span>
                                    </div>
                                    <div class="result-scale">
                                        <span class="result-marker">
                                            <span class="smiley"></span>
                                        </span>
                                	</div>
                                </div>
                                <div class="result-message" data-errormsg="<%= Translate.Text("Please enter correct values for")%> " data-comment1="<%= Translate.Text("Your BMI result indicates that you are")%> "  data-under="<%= Translate.Text("underweight")%>" data-comment-under="<%= Translate.Text("That doesn't mean tucking into cream cakes and chips though, sorry. Better to work on building your muscle mass!")%>"  data-optimum="<%= Translate.Text("just right in every way")%>" data-comment-optimum="<%= Translate.Text("Aah isn't that a lovely thought")%>" data-over="<%= Translate.Text("over weight")%>" data-comment-over="<%= Translate.Text("Remember, BMI is a rough guide only. Pop in to our club and talk to a fitness professional for more advise.")%>" data-obese="<%= Translate.Text("obese")%>" data-comment-obese="<%= Translate.Text("Remember, BMI is a rough guide only. Pop in to our club and talk to a fitness professional for more advise.")%>"></div>
							</div>
						</div>
						
					</div>

				</div> <!-- /content -->