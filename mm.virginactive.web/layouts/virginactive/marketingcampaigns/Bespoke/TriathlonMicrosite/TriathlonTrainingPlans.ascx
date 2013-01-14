<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TriathlonTrainingPlans.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.marketingcampaigns.TriathlonTrainingPlans" %>

<div class="mainContent">
    <section class="full-width">
        
        <div class="intro hero">
            <h3><%= currentItem.Panel1Heading.Rendered %></h3>
            <%= currentItem.Panel1BodyText.Rendered %>           
        </div>

        <img src="/va_campaigns/Bespoke/LondonTriathlonII/img/training-plans.jpg" alt="course map" />
    </section>
    
   
    <section class="full-width static-panel">
        <div class="intro">
            <h3><%= currentItem.Panel2Heading.Rendered %></h3>
            <%= currentItem.Panel2BodyText.Rendered %>           
        </div>
        
        <div class="third-width">
            <h4><%= currentItem.Event1Name.Rendered %></h4>
            <div class="plan-details swim">
                <p>750m<span>400m*</span></p>
                <p class="type">Swim</p>
            </div>
            <div class="plan-details bike">
                <p>20km<span>10km*</span></p>
                <p class="type">Bike</p>
            </div>
            <div class="plan-details run">
                <p>5km<span>2.5km*</span></p>
                <p class="type">Run</p>
            </div>
           <section class="panel third-panel">                        
                <div class="content">
                    <p><a href="<%= currentItem.Event1File.MediaUrl %>"><%= currentItem.Event1LinkText.Rendered %></a></p>
                    <div class="panel-arrow"><a class="arrow" title="People" href="<%= currentItem.Event1File.MediaUrl %>"><span></span>Read<br />more</a></div>
                </div>
            </section>

        </div>
            
        <div class="third-width olympic">
            <h4><%= currentItem.Event2Name.Rendered %></h4>
            <div class="plan-details swim">
                <p>1500m</p>
                <p class="type">Swim</p>
            </div>
            <div class="plan-details bike">
                <p>40km</p>
                <p class="type">Bike</p>
            </div>
            <div class="plan-details run">
                <p>10km</p>
                <p class="type">Run</p>
            </div>
            <section class="panel third-panel">                        
                <div class="content">
                    <p><a href="<%= currentItem.Event2File.MediaUrl %>"><%= currentItem.Event2LinkText.Rendered %></a></p>
                    <div class="panel-arrow"><a class="arrow" title="People" href="<%= currentItem.Event2File.MediaUrl %>"><span></span>Read<br />more</a></div>
                </div>
            </section>
        </div>

        <div class="third-width relay">
            <h4><%= currentItem.Event3Name.Rendered %></h4>
            <div class="plan-details">
                <p class="type">Swim, bike or run</p>
            </div>
            <section class="panel third-panel">                        
                <div class="content">
                    <p><a href="<%= currentItem.Event3File.MediaUrl %>"><%= currentItem.Event3LinkText.Rendered %></a></p>
                    <div class="panel-arrow"><a class="arrow" title="People" href="<%= currentItem.Event3File.MediaUrl %>"><span></span>Read<br />more</a></div>
                </div>
            </section>
        </div>

        <p class="note">* denotes Super Sprint</p>
   
    </section>
    <section class="full-width">
    <div class="listing half-listing">                                              
            <h4><%= currentItem.Panel3Heading.Rendered %></h4>
            <%= currentItem.Panel3BodyText.Rendered %>
    </div>
    <div class="listing half-listing">                                              
            <h4><%= currentItem.Panel4Heading.Rendered %></h4>
            <%= currentItem.Panel4BodyText.Rendered %>
    </div>
    </section>

</div>
            <% if (clubFinder != null)
               {%>
                <div class="cta-panel">
                    <div class="cta-panel-inner">
                        <h3><%= clubFinder.Widget.Heading.Rendered%></h3>
                        <%= clubFinder.Widget.Bodytext.Rendered%>
                        <a href="<%= clubFinder.Widget.Buttonlink.Url%>" class="btn-club" target="_blank"><%= clubFinder.Widget.Buttontext.Rendered%></a>
                    </div>
               </div>
           <%} %>