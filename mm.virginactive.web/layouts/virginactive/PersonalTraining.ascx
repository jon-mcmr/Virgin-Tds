<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalTraining.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.PersonalTraining" %>
<%@ Import Namespace="mm.virginactive.common.Globalization" %>
            
        <div id="content">	            
	 			<div class="layout-inner">
                <div class="facilities-pt">
                    <section>	
                        <%= currentItem.Abstract.Image.RenderCrop("440x360")%>
                        <div class="panel-content">
                            <h2><%= currentItem.PageSummary.NavigationTitle.Raw %></h2>
                            <p class="intro"><%= currentItem.Abstract.Subheading.Rendered %></p>
                            <%= currentItem.Abstract.Summary.Rendered %>
                        
                            <div class="pt-getfit pt-getfit1">
                                <p><%= Translate.Text("Faster")%></p>
                            </div>
                            <div class="pt-getfit pt-getfit2">
                                <p><%= Translate.Text("Safer")%></p>
                            </div>
                            <div class="pt-getfit pt-getfit3">
                                <p><%= Translate.Text("Effective")%></p>
                            </div>
                        </div>
                    </section>
					<hr>
                    	<div class="facilities-quarter" id="achievingyourgoals">
                            <h3><%= Translate.Text("Making your goals happen")%></h3>
                            <section class="width-quarter first">
                                <div class="step">
                                    <h4><strong>1</strong> <span><%= currentItem.Achieveyourgoalsoneheading.Rendered %></span></h4>
                                    <p><%= currentItem.Achieveyourgoalsonetext.Rendered %></p>		
                                </div>
                            </section>
                                        
                            <section class="width-quarter">
                                <div class="step">
									<h4><strong>2</strong> <span><%= currentItem.Achieveyourgoalstwoheading.Rendered %></span></h4>
                                    <p><%= currentItem.Achieveyourgoalstwotext.Rendered %></p>	</div>				
                            </section>
                            <section class="width-quarter">
                                <div class="step">
									<h4><strong>3</strong> <span><%= currentItem.Achieveyourgoalsthreeheading.Rendered %></span></h4>
                                	<p><%= currentItem.Achieveyourgoalsthreetext.Rendered %></p>	
								</div>
                            </section>
                            <section class="width-quarter">
								<div class="step">
									<h4><strong>4</strong> <span><%= currentItem.Achieveyourgoalsfourheading.Rendered %></span></h4>
                                	<p><%= currentItem.Achieveyourgoalsfourtext.Rendered %></p>	
								</div>
                            </section>
                        </div> <!-- / quarter layout -->
                    </div>
				</div>
				<div class="footer-cta">
					<p><%= Translate.Text("See for yourself how a P.T. can tailor your fitness")%></p> 
					<p class="cta"><a class="btn btn-cta-big" href="<%= enqForm.Url + "?sc_trk=enq&Title=Book+a+Visit" %>"><%= Translate.Text("Book a Visit")%></a></p>
				</div>	
		</div> <!-- /content -->



