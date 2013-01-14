<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.ascx.cs" Inherits="mm.virginactive.web.layouts.virginactive.ajax.SearchResult" %>
                <div id="ResultWrap" runat="server">
                <%= ResultImageSrc %>
                    <div class="results-list">
                        <h2><%= ResultCategory%></h2>
                        <h3><%= DisplayName%></h3>
					    <p><%= Description%></p>
                    </div>
                </div>
