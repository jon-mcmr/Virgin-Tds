<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupOfferEnquiryForm.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.clubfinder.GroupOfferEnquiryForm" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
<%@ Import Namespace="mm.virginactive.common.Constants.SitecoreConstants" %>
<%@ Import Namespace="mm.virginactive.common.Helpers" %>
<%@ Import Namespace="mm.virginactive.controls.Model" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
             <div id="content" class="layout-block layout-enquiry-form group-enquiry-form">
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
                        <asp:UpdatePanel ID="pnlForm" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                            <ContentTemplate>
                                <!--Uncompleted Form-->
                                <div class="enquiry enquiry-member form-wrap clearfix" id="formToComplete" runat="server">
                                    <div class="panel-l">
                                        <div class="grid1 clearfix">
                                            <h2><%= offerItem.Heading.Rendered %></h2>
                                            <p><%= clubOffer.Teaser.Rendered %></p>
                                            <asp:ListView ID="ClubList" runat="server" OnItemDataBound="ClubList_OnItemDataBound">
                                                <LayoutTemplate>
					                            <ul class="club-list ui-helper-clearfix">
                                                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                                                </ul>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <li>
                                                        <%# (Container.DataItem as ClubItem).Clubimage.RenderCrop("100x65") %>
                                                        <asp:Literal id="ltrClubName" runat="server"></asp:Literal>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="grid2">
                                           <span class="price"><%= offerItem.Price.Rendered %></span> <span class="aside-text"><%= offerItem.Pricedetail.Rendered %></span>
                                        </div>
                                    </div>
                                    <div class="panel-2 clearfix">
                                        <div class="grid1">
                                            <div class="panel-2-content">
                                                <h3><%= clubOffer.Subheading.Rendered %></h3>
                                                <%= clubOffer.Bodytext.Rendered %>
                                                <small><%= offerItem.Additionalinfo.Rendered %></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="enquirypanel">
                                        <h3 class="ffb"><%= Translate.Text("Enquiry form")%></h3>
                                        <p><%= Translate.Text("Please complete all the fields")%></p>
                                        <div class="badge">
                                            <%= offerItem.Additionalpricedetail.Rendered %>
                                        </div>
                                        <fieldset class="find">		
                                            <div class="wrap selectParentWrap wrap-enquiry-member">							
                                                <label for="<%=clubFindSelect.ClientID%>"><%= Translate.Text("Preferred club")%></label>
                                                <%--<select class="chzn-clublist update-inputs select-wrap" id="clubFindSelect" runat="server"></select>--%>
                                                <asp:dropdownlist class="chzn-clublist update-inputs select-wrap" id="clubFindSelect" runat="server"></asp:dropdownlist>	
                                                <asp:RequiredFieldValidator ID="rfvClubName" runat="server" Display="Dynamic" ControlToValidate="clubFindSelect" EnableClientScript="True" CssClass="span1" />
                                                <asp:RegularExpressionValidator ID="revClubName" runat="server" ControlToValidate="clubFindSelect" Display="Dynamic" />
                                                <asp:CustomValidator ID="cvClubName" runat="server" Display="Dynamic" />
                                            </div>
                                        </fieldset>
                                        <fieldset class="aboutyou">
                                            <div class="wrap wrap-name">
                                                <div class="form-wrap">
                                                    <label for="<%=txtName.ClientID%>"><%= Translate.Text("Your name")%></label>
                                                    <input type="text" maxlength="30" id="txtName" class="text" runat="server" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" />
                                            </div>

                                            <div class="wrap wrap-email">
                                                <div class="form-wrap">
                                                    <label for="<%=txtEmail.ClientID%>"><%= Translate.Text("Email address")%></label>
                                                    <input type="text" maxlength="40" id="txtEmail" class="text" runat="server" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmail" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" />
                                            </div>
                            
                                            <div class="wrap wrap-contact">
                                                <div class="form-wrap">
                                                    <label for="<%=txtPhone.ClientID%>"><%= Translate.Text("Contact number")%></label>
                                                    <input type="text" maxlength="20"  id="txtPhone" class="text" runat="server" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" Display="Dynamic" ControlToValidate="txtPhone" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" />
                                            </div>	

                                            <div class="wrap wrap-how">
                                                <div class="form-wrap">
                                                    <label for="<%=drpHowDidYouFindUs.ClientID%>"><%= Translate.Text("How did you find us?")%></label>
                                                    <asp:dropdownlist class="text" runat="server" id="drpHowDidYouFindUs"/>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvHowDidYouFindUs" runat="server" Display="Dynamic" ControlToValidate="drpHowDidYouFindUs" EnableClientScript="True" />
                                                <asp:RegularExpressionValidator ID="revHowDidYouFindUs" runat="server" ControlToValidate="drpHowDidYouFindUs" Display="Dynamic" />
                                            </div>								
                                        </fieldset>
                                        <fieldset class="check">
                                            <div class="wrap">
                                                <input type="checkbox" id="chkSubscribe" class="checkbox" runat="server" checked="checked" /> 
                                                <label for="<%=chkSubscribe.ClientID%>" class="check"><%= Translate.Text("Subscribe to our newsletter for the latest news and offers")%></label>
                                            </div>
                                        </fieldset>                                        
                                        <fieldset id="gaq-Offer-<%=offerItem.Name.Replace(" ", "-") %>" class="button">
                                            <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-cta-xl btn-submit"
                                                onclick="btnSubmit_Click"><%= Translate.Text("Submit")%></asp:LinkButton>
                                        </fieldset>
                                        <span class="enquiry-panel-corner">&nbsp;</span>
                                    </div>
                                </div>
                                <!--Confirmation of form submission-->
                                <div class="enquiry enquiry-member enquiry-member-thanks" id="formCompleted" runat="server" visible="false">
                                    <div class="panel-l">
                                        <div class="grid1 clearfix">
                                                <div id="speech-bubble">
                                                <h2><%= Translate.Text("thanks for your interest")%></h2>
                                            </div>
                                        </div>

                                        <div class="grid2">
                                              <h3><%= Translate.Text("A team member from")%> <%= CurrentClub.Clubname.Rendered%> <%= Translate.Text("will call you shortly.")%></h3>
                                        </div>
                                    </div>

                                    <div class="panel-2 clearfix">
                                        <div class="grid1">
                                            <div class="panel-2-content">
                                                <img class="hero-image" src="/virginactive/images/backgrounds/bg-enquiry-from-thanks.gif" alt="">
                                            </div>
                                        </div>
                                    </div>
                    
                                    <div class="enquirypanel confirmation">
                                        <!--<p class="top"><%= Translate.Text("A team member from")%> <%= CurrentClub.Clubname.Rendered%> <%= Translate.Text("will call you shortly.")%></p>-->
                                            <h3><%= Translate.Text("Details supplied")%></h3>
                                            <dl class="details-preferred">
                                                <dt><%= Translate.Text("Preferred club")%>:</dt>
                                                <dd><%= CurrentClub.Clubname.Rendered%></dd>
                                            </dl>
                                            <dl class="details-name">
                                                <dt><%= Translate.Text("Your name")%>:</dt>
                                                <dd><%=txtName.Value.Trim()%></dd>
                                            </dl>
                                            <dl class="details-email">
                                                <dt><%= Translate.Text("Email address")%>:</dt>
                                                <dd><%=txtEmail.Value.Trim()%></dd>
                                            </dl>
                                                <% if (txtPhone.Value.Trim() != "")
                                                   {%>
                                                <dl class="details-number">
                                                    <dt><%= Translate.Text("Contact number")%>:</dt>
                                                    <dd><%=txtPhone.Value.Trim()%></dd>
                                                </dl>
                                                <% }%>
                                                <% if (drpHowDidYouFindUs.SelectedValue.ToString() != "" && drpHowDidYouFindUs.SelectedValue.ToString() != Translate.Text("Select"))
                                                   {%>
                                                <dl class="details-find">
                                                    <dt><%= Translate.Text("How did you find us")%>:</dt>
                                                    <dd><%=drpHowDidYouFindUs.SelectedValue.ToString()%></dd>
                                                </dl>
                                                <% }%>
                                            <p><%=chkSubscribe.Checked ? Translate.Text("You have opted to receive our newsletter") : Translate.Text("You have opted NOT to receive our newsletter")%></p>
                                            <p><%=chkSubscribe.Checked ? Translate.Text("You can unsubscribe at any time") : ""%></p>
                                            <span class="enquiry-panel-corner">&nbsp;</span>
                                    </div>
                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
            </div> <!-- /content -->