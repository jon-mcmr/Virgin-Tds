<%@ Page language="c#" AutoEventWireup="True" Inherits="Sitecore.Login.PasswordRecoveryPage" CodeBehind="passwordrecovery.aspx.cs" %>
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

      <div id="FullPanel" class="password-recovery">
        <div id="FullTopPanel">
          <div class="FullTitle">Forgot Your Password?</div>
          <asp:Literal ID="ForgotPasswordDisabledLabel" runat="server" Visible="False" Text="Forgot Your Password functionality is disabled. Please contact your system administrator." />
         
          <div>
            <asp:PasswordRecovery ID="PasswordRecovery" runat="server"  
              SuccessPageUrl="default.aspx"
              OnVerifyingUser="VerifyingUser"
              OnSendingMail="SendEmail" 
              Font-Names="Verdana">
              <MailDefinition Priority="High" Subject="Sending Per Your Request" From="someone@example.com" />
              <InstructionTextStyle CssClass="hidden" />
              <SuccessTextStyle CssClass="success-text"   />
              <TitleTextStyle  CssClass="hidden" />
              <LabelStyle CssClass="label-text"  />
              <TextBoxStyle CssClass="text-box"  />
              <SubmitButtonStyle CssClass="input-submit" />
            </asp:PasswordRecovery>
            <a href="/sitecore/login" class="btn-back">Back to login</a>
          </div>
        </div>
      </div>
    </div>
  </form>
</body>
</html>

