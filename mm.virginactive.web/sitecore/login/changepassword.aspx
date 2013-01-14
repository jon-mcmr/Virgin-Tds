<%@ Page language="c#" AutoEventWireup="True" Inherits="Sitecore.Login.ChangePasswordPage" CodeBehind="ChangePassword.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="none" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
  <title>Sitecore</title>
  <link href="/sitecore/login/default.css" rel="stylesheet" />

</head>
<!--[if lt IE 7 ]>
<body class="ie ie6"> <![endif]-->
<!--[if IE 7 ]>
<body class="ie ie7"> <![endif]-->
<!--[if IE 8 ]>
<body class="ie ie8"> <![endif]-->
<!--[if IE 9 ]>
<body class="ie ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<body> <!--<![endif]-->

 <form id="LoginForm" runat="server">
   <div id="Body">
      <div id="Banner">
        <div id="BannerPartnerLogo">
          <asp:PlaceHolder ID="PartnerLogo" runat="server" />
        </div>
        
        <img id="BannerLogo" src="/sitecore/login/logo.png" alt="Sitecore Logo" border="0" />
      </div>
      
      <div id="Menu">
        &nbsp;
      </div>

      <div id="FullPanel" class="change-password">
        <div id="FullTopPanel">
        
          <div class="FullTitle">Change Your Password.</div>
          <asp:Literal ID="ChangePasswordDisabledLabel" runat="server" Visible="False" Text="Change Password  functionality is disabled. Please contact your system administrator." />
          <div class="Centered">
            <asp:ChangePassword ID="ChangePassword" runat="server" CancelDestinationPageUrl="default.aspx" DisplayUserName="True" InstructionText="Enter your username and old password." Font-Names="verdana" Font-Size="9pt" ContinueDestinationPageUrl="default.aspx">
              <CancelButtonStyle CssClass="hidden" />
              <ChangePasswordButtonStyle CssClass="input-submit" />
              <ContinueButtonStyle CssClass="input-submit" />
              <TitleTextStyle CssClass="hidden" />
              <PasswordHintStyle CssClass="password-hint" />
              <InstructionTextStyle CssClass="hidden" />
              <LabelStyle  CssClass="label-text"  />
              <TextBoxStyle CssClass="text-box" />
            </asp:ChangePassword>
            <a href="/sitecore/login" class="btn-back">Back to login</a>
          </div>
        </div>
      </div>
      
    </div>
  </form>
</body>
</html>

