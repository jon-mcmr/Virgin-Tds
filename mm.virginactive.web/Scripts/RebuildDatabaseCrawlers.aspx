﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RebuildDatabaseCrawlers.aspx.cs" Inherits="Sitecore.SharedSource.Search.Scripts.RebuildDatabaseCrawlers" Debug="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Rebuild database crawlers</title>
</head>
<body>
  <form id="Form2" runat="server">
  <div>
    <table>
      <thead>
        <tr>
          <td>Available crawlers</td>
        </tr>
      </thead>
      <tr>
        <td>
          <asp:CheckBoxList ID="cblIndexes" runat="server">
          </asp:CheckBoxList>
        </td>
      </tr>
      <tr>
        <td>
          <asp:Button ID="btnRebuild" runat="server" Text="Rebuild" OnClick="RebuildBtn_Click" />
        </td>
      </tr>
    </table>
  </div>
  </form>
</body>
</html>
