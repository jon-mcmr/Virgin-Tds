<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReciprocalAccessResults.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.ReciprocalAccessResults" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
                        <input type="hidden" id="phErrorMessage" runat="server" class="ph-error-message" value="''" />
                        <!--Confirmation of form submission-->
                        <div id="formCompleted" runat="server" visible="false">
                            <section class="club-access-info">
                                <div class="access-guide-inner">
                                    <div class="close-btn"><a href="#"><%= Translate.Text("Close")%></a></div>
                            
                                    <h3 class="section-header"><%= Translate.Text("Club access guide")%></h3>
                            
                                    <div class="access-details">
                                        <div class="note-your-club">
                                            <p><%= Translate.Text("Your access to")%> <strong><%= ClubName%></strong> <%= Translate.Text("remains unchanged.")%></p>
                                        </div>
                                
                                        <h4><%= Translate.Text("Access Restrictions")%></h4>
                                        <%= currentItem.Accessrestrictions.Rendered%>

                                    </div>
                            
                                    <div class="clubs-locations">
                                        <div class="reciprocal-list" id="UnrestrictedAccess" runat="server" visible="false">
                                            <p class="clubs-locations-header dot-green"><%= Translate.Text("Unrestricted Access")%></p>
                                            <ul class="clubs-locations-list">
                                                <%= unlimitedAccessMarkup%>
                                            </ul>
                                        </div>
                                
                                        <div class="reciprocal-list" id="FourVisitsAccess" runat="server" visible="false">
                                            <p class="clubs-locations-header dot-green"><%= Translate.Text("Access limited to 4 times a month")%></p>
                                            <ul class="clubs-locations-list">
                                                <%= fourVisitsMarkup%>
                                            </ul>
                                        </div>
                                
                                        <div class="reciprocal-list" id="OneVisitAccess" runat="server" visible="false">
                                            <p class="clubs-locations-header dot-yellow"><%= Translate.Text("Access limited to 1 time a month")%></p>
                                            <ul class="clubs-locations-list">
                                                <%= oneVisitMarkup%>
                                            </ul>
                                        </div>
                                
                                        <div class="reciprocal-list" id="GuestFeeAccess" runat="server" visible="false">
                                            <p class="clubs-locations-header dot-green"><%= Translate.Text("Pay the guest fee")%></p>
                                            <ul class="clubs-locations-list">
                                                <%= guestFeeMarkup%>
                                            </ul>
                                        </div>
                                
                                        <div class="reciprocal-list" id="WeekendsAccess" runat="server" visible="false">
                                            <p class="clubs-locations-header dot-green"><%= Translate.Text("Access limited to weekends")%></p>
                                            <ul class="clubs-locations-list">
                                                <%= weekendsMarkup%>
                                            </ul>
                                        </div>
                                        <div class="no-access" id="NoAccessMessage" runat="server" visible="false">
                                            <h3 class="section-header"><%= Translate.Text("Hi")%> <%= MemberName%></h3>
                                            <div class="note-your-club">
                                                <p><%= Translate.Text("We've got you as a member at")%> <strong><%= ClubName%></strong>. <%= Translate.Text("Unfortunately the membership package you have only gives you access to your home club. Talk to the team at your club about upgrading your membership type to enjoy enjoy multi-club access.")%></p>
                                            </div>
                                        </div>                                
                                    </div>

                            
                                </div>
                            </section>                            
                        </div>