using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mm.virginactive.wrappers.VirginActive.PageTemplates.YourHealthTemplates;
using Sitecore.Diagnostics;

namespace mm.virginactive.web.layouts.virginactive.tools
{
    public partial class ToolFrame : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ToolItem tool = new ToolItem(Sitecore.Context.Item);

            if (tool != null)
            {
                string toolControl = tool.GetToolType();

                if (!String.IsNullOrEmpty(toolControl))
                {
                    try
                    {
                        Control cntrl = this.Page.LoadControl(toolControl);
                        ToolPh.Controls.Add(cntrl);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(String.Format(
                                "Could not load tool type {0}, check thay the correct tool is defined within /sitecore/content/Components/Your Health/Tool Types, error: {1}"
                                ,toolControl
                                , ex.Message ), this);
                        mm.virginactive.common.EmailMessagingService.ErrorEmailNotification.SendMail(ex);
                    }
                }
            }

            this.Page.FindControl("ScriptPh").Controls.Add(new LiteralControl(@"<script>
		            $(function(){
		                $.va_init.functions.setupTools();
	                });
                </script>"));
        }
    }
}