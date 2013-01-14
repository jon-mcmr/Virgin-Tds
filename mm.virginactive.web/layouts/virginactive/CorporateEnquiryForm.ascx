<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorporateEnquiryForm.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.CorporateEnquiryForm"%>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
            <div id="content" class="layout-block">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
<%--                        <asp:UpdateProgress runat="server" id="PageUpdateProgress">
                            <ProgressTemplate>
                                <div id="skm_LockBackground" class="LockBackground">
                                </div>
                                <div id="skm_LockPane" class="LockPane">
                                    <div id="skm_LockPaneText">
                                        <span><img id="imgProgress" alt="Processing Image" src="/virginactive/Images/indicator-big.gif" class="ProgressImage" />
                                            <span>Loading...</span>
                                        </span>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>
                        <script type="text/javascript" language="javascript">
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                            function EndRequestHandler(sender, args) { scroll(0, 300); }   //Anchor to show/scroll to acknowledgment part
                        </script> 
                        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                            <ContentTemplate>
                                <!--Uncompleted Form-->                                
                                <div class="enquiry enquiry-corporate form-wrap" id="formToComplete" runat="server">
                                    <div id="enquiry-wrap">
                                        <div class="enquiry-heading">
                                            <p><%= Translate.Text("Please tell us a few things about you & your company")%></p>
                                        </div>
                                        <fieldset class="find selectParentWrap">	
                                            <legend class="title"><%= Translate.Text("Clubs of interest")%></legend>	
                                            <p class="required"><sup>*</sup> <%= Translate.Text("required")%></p>
                                            <div class="wrap">
                                                <label>Club/s<sup> *</sup></label>
                                                <div class="select-row select-row-new">
                                                    <select id="findclub1" class="chzn-clublist chzn-corporate-enquiry" runat="server"></select>
                                                    <ul class="add-club">
                                                        <li class="rep icon-add icon-add-hide"><a href="#"><span></span>Add<span class="visuallyhidden"> another club</span></a></li>
                                                        <li class="rep icon-del icon-del-hide"><a href="#"><span></span>Delete<span class="visuallyhidden"> this club</span></a></li>
                                                    </ul>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvClubName" runat="server" Display="Dynamic" ControlToValidate="findclub1" EnableClientScript="True" CssClass="span1" />
                                                <asp:RegularExpressionValidator ID="revClubName" runat="server" ControlToValidate="findclub1" Display="Dynamic" />
                                            </div>
                                        </fieldset>

                                        <fieldset>
                                            <legend class="title"><%= Translate.Text("About you")%></legend>
                                            <div class="wrap">
                                                <label for="<%=txtName.ClientID%>"><%= Translate.Text("Your name")%><sup> *</sup></label>
                                                <input type="text" maxlength="30" id="txtName" class="text" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" />
                                            </div>
                                            <div class="wrap">
                                                <label for="<%=txtJobTitle.ClientID%>"><%= Translate.Text("Job title")%><sup> *</sup></label>
                                                <input type="text" maxlength="50" id="txtJobTitle" class="text" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvJobTitle" runat="server" Display="Dynamic" ControlToValidate="txtJobTitle" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revJobTitle" runat="server" ControlToValidate="txtJobTitle" Display="Dynamic" />
                                            </div>
                                            <div class="wrap">
                                                <label for="<%=txtEmail.ClientID%>"><%= Translate.Text("Email address")%><sup> *</sup></label>
                                                <input type="text" maxlength="40" id="txtEmail" class="text" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" />
                                            </div>
                                            <div class="wrap">
                                                <label for="<%=txtPhone.ClientID%>"><%= Translate.Text("Contact number")%><sup> *</sup></label>
                                                <input type="text" id="txtPhone" maxlength="20"  class="text" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" />
                                            </div>
                                        </fieldset>
                                        <fieldset>
                                            <legend class="title"><%= Translate.Text("Your company")%></legend>
                                            <div class="wrap">
                                                <label for="<%=txtCompanyName.ClientID%>"><%= Translate.Text("Company name")%><sup> *</sup></label>
                                                <input type="text" id="txtCompanyName" maxlength="50" class="text" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" Display="Dynamic" ControlToValidate="txtCompanyName" EnableClientScript="True" />
                                                <%--<asp:RegularExpressionValidator ID="revCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" />--%>
                                            </div>
                                            <div class="wrap wrap-cols">
                                                <div class="cols-left">
                                                    <label for="<%=txtNoEmployees.ClientID%>"><%= Translate.Text("No of employees")%><sup> *</sup></label>
                                                    <input type="text" id="txtNoEmployees" maxlength="50" class="text" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvNoEmployees" runat="server" Display="Dynamic" ControlToValidate="txtNoEmployees" EnableClientScript="True" />
                                                </div>  
                                                <div class="cols-right">
                                                    <label for="<%=txtExistingMembers.ClientID%>" class="remove-padding"><%= Translate.Text("No of existing members")%><sup> *</sup></label>
                                                    <input type="text" id="txtExistingMembers" maxlength="50" class="text" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvExistingMembers" runat="server" Display="Dynamic" ControlToValidate="txtExistingMembers" EnableClientScript="True" />
                                                    <asp:CompareValidator ID="cmpvNoEmployees" runat="server" Type="Double" ControlToCompare="txtNoEmployees" ControlToValidate="txtExistingMembers" Display="Dynamic" EnableClientScript="true" Operator="LessThanEqual"></asp:CompareValidator>
                                                </div>
                                            </div>
                                            <div class="wrap">
                                                <label for="<%=txtCompanyWebsite.ClientID%>"><%= Translate.Text("Company website")%></label>
                                                <input type="text" id="txtCompanyWebsite" maxlength="40" class="text" runat="server" />
                                            </div>
                                            <div class="wrap">
                                                <label for="<%=txtLocation.ClientID%>"><%= Translate.Text("Location(s)")%></label>
                                                <input type="text" id="txtLocation" maxlength="50" class="text" runat="server" />
                                            </div>
    <%--                                            <div class="wrap">
                                                <label for="<%=drpNoEmployees.ClientID%>"><%= Translate.Text("No of employees")%></label>
                                                <asp:dropdownlist class="text" runat="server" id="drpNoEmployees"/>
                                            </div>--%>                                          	
    <%--                                            <div class="wrap">
                                                <label for="<%=drpExistingMembers.ClientID%>" class="remove-padding"><%= Translate.Text("Existing Virgin Active members")%></label>
                                                <asp:dropdownlist class="text" runat="server" id="drpExistingMembers"/>
                                            </div>--%>                                   								
                                        </fieldset>
                                        <fieldset>
                                            <div class="wrap">
                                                <label for="<%=txtComments.ClientID%>"><%= Translate.Text("Questions/Comments")%></label>
                                                <textarea class="enquiry-textarea" maxlength="500" id="txtComments" runat="server"></textarea>
                                            </div>  
                                        </fieldset>

                                        <fieldset id="gaq-CorporateEnquiry" class="button">
                                               
                                            <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-cta-xl btn-submit"
                                                onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton>
                                        </fieldset>
                                    </div>
                                </div>
                                <!--Confirmation of form submission-->
                                <div class="enquiry enquiry-corporate-thanks" id="formCompleted" runat="server" visible="false">
                                    <div class="panel-top">
                                        <div class="joinus">
                                            <h2><%= Translate.Text("thanks for your interest")%></h2>
                                            <div class="joinus-arrow"></div>
                                        </div>
                                    </div>
                                    <div class="enquirypanel confirmation">
                                        <p class="top"><%= Translate.Text("A team member will call you shortly.")%></p>
                                        <div class="details">
                                            <h3><%= Translate.Text("Details supplied")%></h3>
                                            <dl>
                                                <dt><%= Translate.Text("Your club(s) of interest")%>:</dt>
                                                <dd><%= ClubNames%></dd>
                                                <dt><%= Translate.Text("Your name")%>:</dt>
                                                <dd><%=txtName.Value.Trim()%></dd>
                                                <dt><%= Translate.Text("Job title")%>:</dt>
                                                <dd><%=txtJobTitle.Value.Trim()%></dd>
                                                <dt><%= Translate.Text("Email")%>:</dt>
                                                <dd><%=txtEmail.Value.Trim()%></dd>
                                                <dt><%= Translate.Text("Contact number:")%></dt>
                                                <dd><%=txtPhone.Value.Trim()%></dd>
                                                <% if (txtCompanyName.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Company name")%>:</dt>
                                                <dd><%=txtCompanyName.Value.Trim()%></dd>
                                                <% }%>
                                                <% if (txtNoEmployees.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("No. of employees")%>:</dt>
                                                <dd><%=txtNoEmployees.Value.Trim()%></dd>
                                                <% }%>
                                                <% if (txtExistingMembers.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Existing Virgin Active members")%>:</dt>
                                                <dd><%=txtExistingMembers.Value.Trim()%></dd>
                                                <% }%>
                                                <% if (txtCompanyWebsite.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Company website")%>:</dt>
                                                <dd><%=txtCompanyWebsite.Value.Trim()%></dd>
                                                <% }%>
                                                <% if (txtLocation.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Location(s)")%>:</dt>
                                                <dd><%=txtLocation.Value.Trim()%></dd>
                                                <% }%>
<%--                                                <% if (drpNoEmployees.SelectedValue.ToString() != "" && drpNoEmployees.SelectedValue.ToString() != Translate.Text("Select"))
                                                    {%>
                                                <dt><%= Translate.Text("No. of employees")%>:</dt>
                                                <dd><%=drpNoEmployees.SelectedValue.ToString()%></dd>
                                                <% }%>--%>
<%--                                                <% if (drpExistingMembers.SelectedValue.ToString() != "" && drpExistingMembers.SelectedValue.ToString() != Translate.Text("Select"))
                                                    {%>
                                                <dt><%= Translate.Text("Existing Virgin Active members")%>:</dt>
                                                <dd><%=drpExistingMembers.SelectedValue.ToString()%></dd>
                                                <% }%>--%>
                                                <% if (txtComments.Value.Trim() != "")
                                                   {%>
                                                <dt><%= Translate.Text("Questions/Comments")%>:</dt>
                                                <dd><%=txtComments.Value.Trim()%></dd>
                                                <% }%>
                                            </dl>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </div> <!-- /content -->