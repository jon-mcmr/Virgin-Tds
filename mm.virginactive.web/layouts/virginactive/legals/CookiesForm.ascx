<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CookiesForm.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.legals.CookiesForm" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Legals" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

            <div id="content" class="layout-inner">
            	<div class="single-article cookies">                    
                    <h2><%= cookieForm.Pagetitle.Rendered %></h2>
                    <%= cookieForm.Bodytext.Rendered %>					
					<h3><%= cookieForm.Subheading.Rendered %></h3>

                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>		
                            <ContentTemplate>    
                	        <div class="cookieType">
                		        <div>
                			        <h4><%= cookieForm.Heading1.Rendered %></h4>
                                    <%= cookieForm.Description1.Rendered %>
                		        </div>
                		        <div class="no_choice">
                			        <span><%= Translate.Text("ALWAYS ON") %></span>
                		        </div>
                	        </div>                                               					
                	        <div class="cookieType choice">
                		        <div>
                			        <h4><%= cookieForm.Heading2.Rendered %></h4>
                                    <%= cookieForm.Description2.Rendered %>
                		        </div>
                		        <ul>
				                    <li>
                				        <label><%= Translate.Text("ON") %></label>
                                        <asp:RadioButton ID="radSocialOn" runat="server" Checked="true" GroupName="Social" name="social"  />
                			        </li>
                			        <li class="last">
                                        <label><%= Translate.Text("OFF") %></label>
                				        <asp:RadioButton ID="radSocialOff" runat="server" GroupName="Social" name="social"  />
                			        </li>
                		        </ul>
                	        </div>
                	
                	        <div class="cookieType choice">
                		        <div>
                			        <h4><%= cookieForm.Heading3.Rendered %></h4>
                                    <%= cookieForm.Description3.Rendered %>
                		        </div>
                		        <ul>
				                    <li>
                				        <label><%= Translate.Text("ON") %></label>
                                        <asp:RadioButton ID="radMetricsOn" runat="server" Checked="true" GroupName="Metrics" name="analytics"  />
                			        </li>
                			        <li class="last">
                				        <label><%= Translate.Text("OFF") %></label>
                				        <asp:RadioButton ID="radMetricsOff" runat="server" GroupName="Metrics" name="analytics"  />
                			        </li>
                		        </ul>
                	        </div>
                	
                	        <div class="cookieType choice">
                		        <div>
                			        <h4><%= cookieForm.Heading4.Rendered %></h4>
                                    <%= cookieForm.Description4.Rendered %>
                		        </div>
                		        <ul>
				                    <li>
                				        <label><%= Translate.Text("ON") %></label>
                                        <asp:RadioButton ID="radPersonalisedOn" runat="server" Checked="true" GroupName="Personalised" name="personalisation" />
                			        </li>
                			        <li class="last">
                				        <label><%= Translate.Text("OFF") %></label>
                				        <asp:RadioButton ID="radPersonalisedOff" runat="server" GroupName="Personalised" name="personalisation" />
                			        </li>
                		        </ul>
                	        </div>
                	
                	        <div class="cookieType choice">
                		        <div>
                			        <h4><%= cookieForm.Heading5.Rendered %></h4>
                                    <%= cookieForm.Description5.Rendered %>
	                	        </div>
                		        <ul>
                			        <li>
                				        <label><%= Translate.Text("ON") %></label>
                                        <asp:RadioButton ID="radMarketingOn" runat="server" Checked="true" GroupName="Marketing" name="marketing"  />
                			        </li>
                			        <li class="last">
                				        <label><%= Translate.Text("OFF") %></label>
                                        <asp:RadioButton ID="radMarketingOff" runat="server" GroupName="Marketing" name="marketing"  />
                			        </li>
                		        </ul>
                	        </div>
                	        <div class="saveCookie choice">
                		        <p class="info" id="pnlFormPrompt" runat="server"><%= cookieForm.Buttondescription.Rendered %></p>
                                <p class="confirmation hidden" id="pnlConfirmation" runat="server"><%= cookieForm.Confirmationtext.Rendered %></p> 
                                <asp:Button class="btn-cta-big btn-submit" ID="btnSubmit" runat="server" onclick="btnSubmit_Click"></asp:Button>
                                <!--<a class="btn btn-cta-big btn-submit" href='#'></a>-->
                	        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                	<%= cookieForm.Footertext.Rendered %>
                	<div class="google_opt_out">
                	    <%= cookieForm.Additionalinfo.Rendered %>	
                	</div>                	
                </div>
            </div> <!-- /content -->


