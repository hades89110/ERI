using ERI.Report.Echarts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Echarts_EchartsReportOperationNums_Iframe : System.Web.UI.Page
{
    public string jsonString = "";
    public string jsonString2 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        dataLoad();
    }
    protected void dataLoad()
    {
        ucToxicData.SetDDL3 = false;
        ucToxicData.Bind(true);
        for (int i = 1; i <= 12; i++)
        {
            ddlMonthS.Items.Add(i.ToString("D2"));
            ddlMonthE.Items.Add(i.ToString("D2"));
        }
        for (int i = DateTime.Today.Year; i >= 1911 + 101; i--)
        {
            ddlYearS.Items.Add(new ListItem(i.ToString(), (i - 1911).ToString()));
            ddlYearE.Items.Add(new ListItem(i.ToString(), (i - 1911).ToString()));
        }
        ddlMonthE.SelectedValue = "12";
        ddlYearS.SelectedValue = (DateTime.Today.Year - 1 - 1911).ToString();
    }
    protected void checkData_Click(object sender, EventArgs e)
    {
        Jsonstr();
    }
    protected void Jsonstr()
    {
        if (ucToxicData.SelectedValue == "") 
        {
            SystemInfo.ShowMsg(Page,"請選擇毒化物");
            return ; 
        }
        if (int.Parse(ddlMonthS.SelectedValue)> int.Parse(ddlMonthE.SelectedValue))
        {
            string Swap = ddlMonthS.SelectedValue;
            ddlMonthS.SelectedValue = ddlMonthE.SelectedValue;
            ddlMonthE.SelectedValue = Swap;
        }
        if (int.Parse(ddlYearS.SelectedValue)> int.Parse(ddlYearE.SelectedValue))
        {
            string Swap= ddlYearS.SelectedValue;
            ddlYearS.SelectedValue = ddlYearE.SelectedValue;
            ddlYearE.SelectedValue = Swap;
        }
        var lPermits = ReportOperationNumber_op.QueryGroupYear(new ReportOperationNumber_cond
        {
            ToxicNo = ucToxicData.SelectedValue,
            MonthS = ddlMonthS.SelectedValue,
            MonthE = ddlMonthE.SelectedValue,
            YearS = ddlYearS.SelectedValue,
            YearE = ddlYearE.SelectedValue,
        }).ToList();

        var dataDate = new List<object>() { "年度" };
        for (int i = int.Parse(ddlYearS.SelectedValue); i <= int.Parse(ddlYearE.SelectedValue); i++)
            dataDate.Add(i.ToString() + "年" + ddlMonthS.SelectedValue + "-" + ddlMonthE.SelectedValue + "月");

        var dataMake = lPermits.Select(s => Math.Round(s.Make, 0, MidpointRounding.AwayFromZero)).Cast<object>().ToList();
        dataMake.Insert(0, "製造");
        var dataImport = lPermits.Select(s => Math.Round(s.Impoert, 0, MidpointRounding.AwayFromZero)).Cast<object>().ToList();
        dataImport.Insert(0, "輸入");
        var dataExport = lPermits.Select(s => Math.Round(s.Export, 0, MidpointRounding.AwayFromZero)).Cast<object>().ToList();
        dataExport.Insert(0, "輸出");
        var dataUse = lPermits.Select(s => Math.Round(s.Use, 0, MidpointRounding.AwayFromZero)).Cast<object>().ToList();
        dataUse.Insert(0, "使用");

        //var dataMake = new List<object>() { "製造" };
        //var dataImport = new List<object>() { "輸入" };
        //var dataExport = new List<object>() { "輸出" };
        //var dataUse = new List<object>() { "使用" };

        /*
        var YearCount = lPermits.Select(s => s.Year).ToList();
        if ( YearCount.Count<=1 && YearCount.First()==null)
        { 
            SystemInfo.ShowMsg(Page, "您查詢的物質運作量為 0 ");
            return;
        }
        */
        
        for (int i = int.Parse(ddlYearS.SelectedValue); i <= int.Parse(ddlYearE.SelectedValue); i++)
        {
            var b = lPermits.Where(w => w.Year == i.ToString()).Count() > 0;
            if (b)
            {
            }
            else
            {
                dataMake.Insert(i-(int.Parse(ddlYearS.SelectedValue))+1,0);
                dataImport.Insert(i - (int.Parse(ddlYearS.SelectedValue)) + 1, 0);
                dataExport.Insert(i - (int.Parse(ddlYearS.SelectedValue)) + 1, 0);
                dataUse.Insert(i - (int.Parse(ddlYearS.SelectedValue)) + 1, 0);
            }
        }
        var sourcedata = new List<List<object>>() { dataDate, dataMake, dataImport, dataExport, dataUse };

        var series1 = new Series()
        {
            type = "line",
            seriesLayoutBy = "row",
            label = new Lable()
            {
                show = true,
                position = "top",
            },
        };
        //年份bar條
        var seriesAxisIndex = new Series()
        {
            type = "bar",
            xAxisIndex = 0,
            yAxisIndex = 0,
            label = new Lable()
            {
                show = true,
                position = "inside",
            },
        };
        var root = new Root()
        {
            legend = new Legend()
            {
                left = "center",
                textStyle=new TextStyle() { fontSize=14 },
                top ="12%"
            },
            tooltip = new Tooltip(),
            dataset = new Dataset()
            {
                source = sourcedata
            },
            xAxis = new List<Xaxi>()
            {
                new Xaxi()
                {
                    type = "category",
                    gridIndex = 0,
                },
            },
            yAxis = new List<Yaxi>()
            {
                new Yaxi()
                {
                    gridIndex = 0,
                    name =@"運\n作\n量\n單\n位\n︵\n公\n噸\n︶",
                    nameLocation="start",
                    nameTextStyle=new NameTextStyle()
                    {
                        fontSize=14,
                        padding=new List<int>(){ -220,160,0,0 }
                    }
                },
            },
            grid = new List<Grid>()
            {
                new Grid()
                {
                    height = "70%",
                    top = "22%",
                    left="8%",
                    right="5%"
                }
            },
            series = new List<Series>()
            {
               series1,
               series1,
               series1,
               series1,
            }
        };

        var root2 = new Root()
        {
            legend = new Legend()
            {
                left = "center",
                top="15%",
                textStyle=new TextStyle() { fontSize=12 }
            },
            tooltip = new Tooltip(),
            dataset = new Dataset()
            {
                source = sourcedata
            },
            xAxis = new List<Xaxi>()
            {
                new Xaxi()
                {
                    type = "category",
                    gridIndex = 0,
                },
            },
            yAxis = new List<Yaxi>()
            {
                new Yaxi()
                {
                    gridIndex = 0,
                    name =@"運\n作\n量\n單\n位\n︵\n公\n噸\n︶",
                    nameLocation="start",
                    nameTextStyle=new NameTextStyle()
                    {
                        fontSize=14,
                        padding=new List<int>(){ -220, 160, 0, 0 }
                    }
                },
            },
            grid = new List<Grid>()
            {
                new Grid()
                {   
                    top = "25%",
                    height = "70%",
                    left="8%",
                    right="5%"
                }
            },
            series = new List<Series>()
            {
                //seriesAxisIndex,
            }
        };

        for (int i = 0; i <= int.Parse(ddlYearE.SelectedValue) - int.Parse(ddlYearS.SelectedValue); i++)
        {
            root2.series.Add(seriesAxisIndex);
        }

        jsonString = JsonConvert.SerializeObject(root);
        jsonString2 = JsonConvert.SerializeObject(root2);
    }
}
