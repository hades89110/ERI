using ERI.Report.Tox_99.Efficiency.EfficiencyGreadDetail;
using ERI.Report.Tox_99.Efficiency.EfficiencyGreadDetail109;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Efficiency_EfficiencyGreadDetail : DataPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Init();
        GetData();
    }
    private void Init()
    {
        ddlYear.Items.Clear();
        int iYear = DateTime.Now.Year - 1911;
        if ((DateTime.Now.Month < 6)) iYear--;
        for (int i = iYear; i >= 107; i--)
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    }
    private void GetData()
    {
        switch (ddlYear.SelectedValue)
        {
            case "107":
                Get107();
                break;
            case "108":
                Get108();
                break;
            case "109":
                Get109();
                break;
        }
    }
    private void Get107()
    {
        gv.Columns.Clear();
        gv.Columns.Add(gvAddColumn("選取<br/>", "CheckBox"));
        gv.Columns.Add(gvAddColumn("名次", "SN"));
        gv.Columns.Add(gvAddColumn("縣市", "CityN"));
        gv.Columns.Add(gvAddColumn("年度績效考核<br/>指標總分", "Sum"));
        gv.Columns.Add(gvAddColumn("毒性化學物質", "G11"));
        gv.Columns.Add(gvAddColumn("食安暨風險疑慮<br/>化學物質專案", "G12"));
        gv.Columns.Add(gvAddColumn("與局配合度", "G13"));
        gv.Columns.Add(gvAddColumn("危害預防整備工作", "G21"));
        gv.Columns.Add(gvAddColumn("變及事故處理、<br/>辦理模擬演練", "G22"));
        gv.Columns.Add(gvAddColumn("", "Link"));

        gv.DataSource = EfficiencyGreadDetail_op.GetScore("");
        gv.DataBind();
    }
    private void Get108()
    {
        gv.Columns.Clear();
        gv.Columns.Add(gvAddColumn("選取<br/>", "CheckBox"));
        gv.Columns.Add(gvAddColumn("縣市", "CityN"));
        gv.Columns.Add(gvAddColumn("年度績效考核<br/>指標總分", "Sum"));
        gv.Columns.Add(gvAddColumn("毒性化學物質", "G11"));
        gv.Columns.Add(gvAddColumn("食安暨風險疑慮<br/>化學物質專案", "G12"));
        gv.Columns.Add(gvAddColumn("與局配合度<br/>毒化物勾稽查核<br/>及食安項目", "G131"));
        gv.Columns.Add(gvAddColumn("與局配合度<br/>毒性化學物質<br/>危害控制項目", "G132"));
        gv.Columns.Add(gvAddColumn("與局配合度<br/>環境用藥項目", "G133"));
        gv.Columns.Add(gvAddColumn("與局配合度<br/>專業技術人員兼任<br/>其他法令所定專業人員<br/>查核作業項目", "G134"));
        gv.Columns.Add(gvAddColumn("危害預防整備工作", "G21"));
        gv.Columns.Add(gvAddColumn("變及事故處理、<br/>辦理模擬演練", "G22"));
        gv.Columns.Add(gvAddColumn("", "Link"));
        gv.DataSource = EfficiencyGreadDetail_108_op.GetScore("");
        gv.DataBind();
    }

    private void Get109()
    {
        gv.Columns.Clear();
        gv.Columns.Add(gvAddColumn("選取<br/>", "CheckBox"));
        gv.Columns.Add(gvAddColumn("縣市", "CityN"));
        gv.Columns.Add(gvAddColumn("年度績效考核<br/>指標總分", "Sum"));
        gv.Columns.Add(gvAddColumn("屬關鍵指標毒物及關注化學物質<br/>勾稽查核及環境用藥查核", "G1"));
        gv.Columns.Add(gvAddColumn("屬關鍵指標毒化災預防整備、<br/>演練及事故應變", "G2"));
        gv.Columns.Add(gvAddColumn("屬關鍵指標具食安風險疑慮<br/>化學物質源頭管理", "G3"));
        gv.Columns.Add(gvAddColumn("毒物及關注化學物質<br/>相關業務推動配合度", "G4"));
        gv.Columns.Add(gvAddColumn("地方創新作為", "G5"));
        gv.Columns.Add(gvAddColumn("", "Link"));
        gv.DataSource = EfficiencyGreadDetail_109_op.GetScore("");
        gv.DataBind();
    }

    private TemplateField gvAddColumn(string _Head, string _Data)
    {
        TemplateField tf = new TemplateField();
        tf = new TemplateField();
        tf.HeaderTemplate = new GridViewTemplate(DataControlRowType.Header, _Head, ddlYear.SelectedValue);
        tf.ItemTemplate = new GridViewTemplate(DataControlRowType.DataRow, _Data, ddlYear.SelectedValue);
        return tf;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }

    #region ITemplate
    public void InstantiateIn(Control container)
    {
        throw new NotImplementedException();
    }
    public class GridViewTemplate : ITemplate
    {
        private DataControlRowType u_Type;
        private string column_title;
        private bool delZero;
        private string year;

        public GridViewTemplate(DataControlRowType type, string colname, string _year, bool _delZero = false)
        {
            u_Type = type;
            column_title = colname;
            year = _year;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {  // ITemplate只有一個 InstantiateIn()方法，此方法需要輸入一個控制項
           // 當實作Class時，定義子控制項和樣板所屬的 Control 物件。這些子控制項依次定義在內嵌樣板內。
            switch (u_Type)
            {
                case DataControlRowType.Header:  // GridView表頭
                    Literal literal1 = new Literal();
                    literal1.Text = column_title;
                    container.Controls.Add(literal1);
                    if (column_title.Contains("選取"))
                    {
                        HtmlInputButton GetAll = new HtmlInputButton();
                        GetAll.Value = "全選";
                        GetAll.Attributes.Add("onclick", "GetAllCheckOrNot(1);");
                        HtmlInputButton NoGet = new HtmlInputButton();
                        NoGet.Value = "反選";
                        NoGet.Attributes.Add("onclick", "GetAllCheckOrNot(0);");
                        container.Controls.Add(GetAll);
                        container.Controls.Add(NoGet);
                    }
                    break;
                case DataControlRowType.DataRow:  // Gridview資料列
                    if (column_title == "Link")
                    {
                        LinkButton lbtnUpdate = new LinkButton();
                        lbtnUpdate.ID = "lbtnUpdate" + column_title;
                        lbtnUpdate.DataBinding += new EventHandler(gvControl_DataBind);
                        container.Controls.Add(lbtnUpdate);
                    }
                    else if (column_title == "CheckBox")
                    {
                        CheckBox cbCity = new CheckBox();
                        cbCity.ID = "cbCity" + column_title;
                        HiddenField hfCity = new HiddenField();
                        hfCity.ID = "hfCity" + column_title;
                        hfCity.DataBinding += new EventHandler(gvControl_DataBind);

                        container.Controls.Add(cbCity);
                        container.Controls.Add(hfCity);
                    }
                    else if (column_title == "SN")
                    {
                        Label lb = new Label();
                        lb.ID = "lb" + column_title;
                        lb.DataBinding += new EventHandler(gvControl_DataBind);
                        container.Controls.Add(lb);
                    }
                    else
                    {
                        Label lb = new Label();
                        lb.ID = "lb" + column_title;
                        lb.DataBinding += new EventHandler(gvControl_DataBind);
                        container.Controls.Add(lb);
                    }
                    break;
                default:
                    break;
            }
        }
        private void gvControl_DataBind(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label lb = sender as Label;
                GridViewRow gvr = lb.NamingContainer as GridViewRow;
                if (lb.ID.Contains("SN") || lb.ID.Contains("CityN"))
                    lb.Text = DataBinder.Eval(gvr.DataItem, column_title).ToString();
                else
                {
                    decimal dno = 0;
                    if (!decimal.TryParse(DataBinder.Eval(gvr.DataItem, column_title).ToString(), out dno))
                        lb.Text = "0.00";
                    else
                        lb.Text = SystemInfo.DelZero(dno.ToString("0.##"));
                }
            }
            if (sender is HiddenField)
            {
                HiddenField hfCity = sender as HiddenField;
                GridViewRow gvr = hfCity.NamingContainer as GridViewRow;
                hfCity.Value = DataBinder.Eval(gvr.DataItem, "City").ToString();
            }
            if (sender is LinkButton)
            {
                LinkButton lbtn = sender as LinkButton;
                if (lbtn.ID.Contains("lbtnUpdate"))
                {
                    GridViewRow gvr = lbtn.NamingContainer as GridViewRow;
                    lbtn.Text = "修改&nbsp;";
                    lbtn.PostBackUrl = "EfficiencyAppNew" + this.year + ".aspx?C=" + DataBinder.Eval(gvr.DataItem, "City").ToString();
                }
            }
        }
    }
    #endregion
}