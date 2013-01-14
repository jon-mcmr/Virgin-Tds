<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClubPersonalMembership.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.club.ClubPersonalMembership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Clubs.Membership" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.PageTemplates.Membership" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>

                	<div class="membership-pers">
						<h2><%= SharedLanding.Title.Raw %></h2>

                        <asp:ListView ID="MemberSectionList" runat="server" OnItemDataBound="MemberSectionList_OnItemDataBound">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
                            </LayoutTemplate>

                            <ItemTemplate>                            
                            <section class="membership-type <%# (Container.DataItem as MembershipStaticPageItem).GetPanelCssClass() %>">
							<div class="content-panel">
								<div class="title-note">
									<h3><%# new PageSummaryItem( (Container.DataItem as MembershipStaticPageItem).InnerItem ).NavigationTitle.Raw %></h3>
								</div>
								<h3><%# (Container.DataItem as MembershipStaticPageItem).Heading.Raw %></h3>
								<p class="memb-intro"><%# (Container.DataItem as MembershipStaticPageItem).Body.Raw %></p>
                                
                                <asp:ListView ID="FeatureList" ItemPlaceholderID="FeatureListPh" DataSource="<%# (Container.DataItem as MembershipStaticPageItem).InnerItem.Children %>" runat="server">
                                    <LayoutTemplate>
                                    <ul class="features">
                                        <asp:PlaceHolder ID="FeatureListPh" runat="server" />
                                    </ul>
                                    </LayoutTemplate>

                                    <ItemTemplate>                                    
                                        <li><strong><%# new MembershipProductItem((Container.DataItem as Item)).Productname.Raw %> </strong><%# new MembershipProductItem((Container.DataItem as Item)).Productbody.Raw %></li>
                                    </ItemTemplate>
                                </asp:ListView>			            	   	
							</div>
	
							<div class="info-panel">
								<h3 class="price"><span class="price-value"><%# (Container.DataItem as MembershipStaticPageItem).Price %></span><span class="per-month"><%= Translate.Text("per month") %></span></h3>
                                <asp:literal id="ltrFormLink" runat="server"></asp:literal>
								<!--<a href="" class="btn btn-cta-big gaqTag" data-gaqcategory="CTA" data-gaqaction="EnquireNow" data-gaqlabel="Membership"><%= Translate.Text("Enquire now") %></a>-->
							</div>
							
						</section>
                            </ItemTemplate>
                        </asp:ListView>
                    	
						
	            
                    <section class="notes-bottom-panel">
						<h3><%= Translate.Text("Ready to join?") %></h3>
                        <ul class="membership-steps">
                        	<li><span>1</span> <%= Translate.Text("Come see us") %></li>
							<li><span>2</span> <%= Translate.Text("Choose a membership") %></li>
							<li class="last"><span>3</span> <%= Translate.Text("Get active!") %></li>
                        </ul>
						<%= SharedLanding.Questions.Raw %>
                    </section>

					<div class="contrast-cta">
						<p><%= Translate.Text("Ready to join?") %> <%= Translate.Text("Let us show you around...") %></p> 
						<a href="<%= enqForm.Url + "?sc_trk=enq&c=" + currentClub.InnerItem.ID.ToShortID() %>"class="btn btn-cta-big gaqTag" data-gaqcategory="CTA" data-gaqaction="MembershipEnquiry" data-gaqlabel="Membership"><%= Translate.Text("Membership Enquiry") %></a>
					</div>
				</div>