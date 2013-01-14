<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Sitecore.Data" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="Sitecore.Data.Items" %>
<%@ Import Namespace="mm.virginactive.wrappers.VirginActive.BaseTemplates" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private List<PageSummaryItem> list = null;
    private TemplateItem psiTemplate = null;
    private List<ProblemItem> problemList = null;

    protected void Page_Load(object sender, EventArgs args)
    {
        if (!Sitecore.Context.User.IsAdministrator)
        {
            Response.End();
        }
    }

    public void ShowFields(object sender, EventArgs args)
    {
        GetProblemItems(StartPath.Text);

        Session["problemList"] = problemList;
        problemList.Sort(new ProblemItemPathSorterAsc());

        ItemGrid.DataSource = problemList;
        ItemGrid.DataBind();
        ExportButton.Visible = true;
    }

    private void GetProblemItems(string startPath)
    {
        ErrorLabel.Visible = false;
        ErrorLabel.Text = "";

        Item startItem = Sitecore.Configuration.Factory.GetDatabase("master").SelectSingleItem(startPath);
        if (startItem == null)
        {
            ErrorLabel.Text = "Start Path not found";
            ErrorLabel.Visible = true;
            return;
        }

        list = new List<PageSummaryItem>();
        problemList = new List<ProblemItem>();
        psiTemplate = new TemplateItem(Sitecore.Configuration.Factory.GetDatabase("master").GetItem(new ID(PageSummaryItem.TemplateId)));

        AddProblemItem(startItem);
    }

    private void AddProblemItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (InheritsTemplate(item, psiTemplate))
        {

            PageSummaryItem psItem = new PageSummaryItem(item);

            if (UnconvertedTokens.Checked && psItem.Pagedescription.Raw.Contains("$name")) // Meta Description has unconverted tokens
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem(){Item = psItem, Message = "Meta Description has unconverted tokens"};
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
            if (UnconvertedTokens.Checked && psItem.NavigationTitle.Raw.Contains("$name")) // Navigation Title has unconverted tokens
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem() { Item = psItem, Message = "Navigation Title has unconverted tokens" };
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
            if (StandardValues.Checked && psItem.Pagedescription.Field.InnerField.ContainsStandardValue) // Meta Description standard value
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem() { Item = psItem, Message = "Meta Description standard value" };
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
            if (StandardValues.Checked && psItem.NavigationTitle.Field.InnerField.ContainsStandardValue) // Navigation Title standard value
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem() { Item = psItem, Message = "Navigation Title standard value" };
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
            if (EmptyFields.Checked && psItem.Pagedescription.Raw == "") // Meta Description empty
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem() { Item = psItem, Message = "Meta Description empty" };
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
            if (EmptyFields.Checked && psItem.NavigationTitle.Raw == "") // Navigation Title empty
            {
                list.Add(psItem);
                ProblemItem pi = new ProblemItem() { Item = psItem, Message = "Navigation Title empty" };
                if (!problemList.Contains(pi))
                {
                    problemList.Add(pi);
                }
            }
        }

        foreach (Item child in item.Children)
        {
            AddProblemItem(child);
        }
    }

    private bool InheritsTemplate(Item item, TemplateItem template)
    {
        if (item.Template.BaseTemplates.Any(baseTemplate => baseTemplate.ID == template.ID))
        {
            return true;
        }

        return
            item.Template.BaseTemplates.Where(baseTemplate => baseTemplate != item.Template).Any(
                baseTemplate => TemplateInheritsTemplate(baseTemplate, template));
    }

    private bool TemplateInheritsTemplate(TemplateItem item, TemplateItem template)
    {
        if (item.BaseTemplates.Any(baseTemplate => baseTemplate.ID == template.ID))
        {
            return item.BaseTemplates.Where(baseTemplate => baseTemplate != item).Any(
                baseTemplate => TemplateInheritsTemplate(baseTemplate, template));
        }

        return false;
    }

    private string GvSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GvSortDirection)
        {
            //If previous sort direction if ascending order then assign new direction as descending order
            case "ASC":
                GvSortDirection = "DESC";
                break;

            //If previous sort direction if descending order then assign new direction as ascending order
            case "DESC":
                GvSortDirection = "ASC";
                break;
        }
        return GvSortDirection;
    }
    
    public class ProblemItem : IEqualityComparer<ProblemItem>
    {
        public PageSummaryItem Item { get; set; }
        public string Message { get; set; }
        
        public string Path
        {
            get { return Item.InnerItem.Paths.FullPath; }
        }
        
        public string Link
        {
            get { return GetInternalItemLink(Item.InnerItem.ID); }
        }

        public ID ID
        {
            get { return Item.InnerItem.ID; }
        }

        public bool Equals(ProblemItem x, ProblemItem y)
        {
            return x.Item == y.Item && x.Message == y.Message;
        }

        public int GetHashCode(ProblemItem obj)
        {
            return(obj.Path + Message).GetHashCode();
        }
    }
    
    public class ProblemItemPathSorterAsc : IComparer<ProblemItem>
    {
        public int Compare(ProblemItem x, ProblemItem y)
        {
            return x.Path.CompareTo(y.Path);
        }
    }

    public class ProblemItemPathSorterDesc : IComparer<ProblemItem>
    {
        public int Compare(ProblemItem x, ProblemItem y)
        {
            return y.Path.CompareTo(x.Path);
        }
    }

    public class ProblemItemMessageSorterAsc : IComparer<ProblemItem>
    {
        public int Compare(ProblemItem x, ProblemItem y)
        {
            return x.Message.CompareTo(y.Message);
        }
    }

    public class ProblemItemMessageSorterDesc : IComparer<ProblemItem>
    {
        public int Compare(ProblemItem x, ProblemItem y)
        {
            return y.Message.CompareTo(x.Message);
        }
    }

    private void ItemGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            problemList = (List<ProblemItem>) Session["problemList"];
            if (problemList == null)
            {
                GetProblemItems(StartPath.Text);
            }
        }
        catch
        {
            GetProblemItems(StartPath.Text);
        }
        
        if (e.SortExpression == "path")
        {
            if (GetSortDirection() == "ASC")
            {
                problemList.Sort(new ProblemItemPathSorterAsc());
            }
            else
            {
                problemList.Sort(new ProblemItemPathSorterDesc());
            }
        }
        else if (e.SortExpression == "message")
        {
            if (GetSortDirection() == "ASC")
            {
                problemList.Sort(new ProblemItemMessageSorterAsc());
            }
            else
            {
                problemList.Sort(new ProblemItemMessageSorterDesc());
            }
        }

        ItemGrid.DataSource = problemList;
        ItemGrid.DataBind();
    }

    protected static string GetInternalItemLink(Sitecore.Data.ID guid)
    {
        return
            string.Format("javascript:scForm.getParentForm().postRequest('','','','item:load(id={0})'); return false;",
                          guid);
    }

    protected void ExportButton_Click(object sender, EventArgs e)
    {
        Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
        Response.Charset = String.Empty;
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
        ItemGrid.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
        //base.VerifyRenderingInServerForm(control); 
    } 

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/sitecore/shell/controls/InternetExplorer.js" type="text/javascript"></script>
    <script src="/sitecore/shell/controls/Sitecore.js" type="text/javascript"></script>
    <style type="text/css">
        .margintop {
            margin-top: 20px;
        }
    </style>
</head>
<body style="background-color: white; padding: 10px;">
    <form id="form1" runat="server">
    <div>
          <asp:TextBox runat="server" ID="StartPath" Text="/sitecore/content"></asp:TextBox>
          Enter the start path.
      </div>
      <div>
          <asp:CheckBox runat="server" ID="EmptyFields" Checked="True" Text="Find all Empty fields" /><br />
          <asp:CheckBox runat="server" ID="StandardValues" Checked="True" Text="Find fields with Standard Values"/><br />
          <asp:CheckBox runat="server" ID="UnconvertedTokens" Checked="True" Text="Find all fields with Unconverted Tokens"/> 
      </div>
      <div>
          Get a report on the problem items<br />
          <asp:Button ID="Button2" runat="server" Text="Report" OnClick="ShowFields"/>
      </div>
      <asp:Label runat="server" ID="ErrorLabel" Visible="False"></asp:Label>
      <asp:Button runat="server" ID="ExportButton" OnClick="ExportButton_Click" Text="Export" Visible="False" CssClass="margintop"/>
      <asp:GridView runat="server" ID="ItemGrid" AutoGenerateColumns="False" AllowSorting="True" OnSorting="ItemGrid_Sorting">
          <Columns>
              <asp:TemplateField runat="server" HeaderText="Path" SortExpression="path">
                  <ItemTemplate>
                      <a href="/sitecore/shell/sitecore/content/Applications/Content Editor.aspx?id=<%# Eval("ID") %>&la=en&fo=<%# Eval("ID") %>" target="_blank"><%# Eval("Path") %></a>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField runat="server" DataField="Message" HeaderText="Problem" SortExpression="message"/>
          </Columns>
      </asp:GridView>
    </form>
</body>
</html>
