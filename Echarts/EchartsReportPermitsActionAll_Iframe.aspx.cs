using ERI.Report.Echarts.EchartsReportPermitsActionAll;
using ERI.Report.Tox_99.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Echarts_EchartsReportPermitsActionAll_Iframe : System.Web.UI.Page
{
    public string jsonString = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (IsPostBack) return;

        Showcity();
        if (UserInfo.CityCode != string.Empty)
        {
            foreach (ListItem li in cblCity.Items)
                if (li.Value == UserInfo.CityCode)
                    li.Selected = true;
            //Show_echarts(new List<string>() { UserInfo.CityCode });
            //Show_echarts(new List<string>() { "F" });
        }

        btncheckCity_Click(null, null);
    }

    public Dictionary<string, string> lPermitTxt = new Dictionary<string, string>()
    {
        { "CP", "列管廠家數" },
        { "Per", "證件張數" },
        { "Class1", "許可證" },
        { "Class2", "登記文件" },
        { "Class3", "核可文件" }
    };

    protected void Show_echarts(List<string> citydataS)
    {
        List<string> docdata = new List<string>()
        {
            lPermitTxt["CP"], lPermitTxt["Per"], lPermitTxt["Class1"], lPermitTxt["Class2"], lPermitTxt["Class3"]
        };


        var CitySort = new Dictionary<string, int> {
            { "C", 0 }, { "A", 1 }, { "F", 2 }, { "H", 3 }, { "J", 4 }
            , { "O", 5 }, { "K", 6 }, { "B", 7 }, { "N", 8 }, { "M", 9 }
            , { "P", 10 }, { "Q", 11 }, { "I", 12 }, { "D", 13 }, { "E", 14 }
            , { "T", 15 }, { "G", 16 }, { "U", 17 }, { "V", 18 }, { "X", 19 }
            , { "W", 20 }, { "Z", 21 } };

        var lPermits = ReportPermitsActionAll_op.Query(new ReportPermitsActionAll_cond
        {
            QueryDT = DateTime.Today,
        })
            .Where(w => w.Class >= 1 && w.Class <= 3)
            .GroupBy(g => g.EP_CityCode)
            .Select(s => new
            {
                CityCode = s.Key,
                DBCache.ListCityCode(s.Key).First().CityName,
                CPCount = s.Select(s2 => s2.C_No + s2.Place_No).Distinct().Count(),
                PCount = s.Select(s2 => s2.Per_No).Distinct().Count(),
                Class1 = s.Where(w => w.Class == 1).Select(s2 => s2.Per_No).Distinct().Count(),
                Class2 = s.Where(w => w.Class == 2).Select(s2 => s2.Per_No).Distinct().Count(),
                Class3 = s.Where(w => w.Class == 3).Select(s2 => s2.Per_No).Distinct().Count(),
            })
            .OrderBy(o => CitySort[o.CityCode]).ToList();

        var Data = lPermits.Where(w => citydataS.Contains(w.CityCode));

        var citydata = Data.Select(s => s.CityName).ToList();
        var dataCP = Data.Select(s => s.CPCount).Cast<object>().ToList();
        var dataP = Data.Select(s => s.PCount).Cast<object>().ToList();
        var dataC1 = Data.Select(s => s.Class1).Cast<object>().ToList();
        var dataC2 = Data.Select(s => s.Class2).Cast<object>().ToList();
        var dataC3 = Data.Select(s => s.Class3).Cast<object>().ToList();


        if (citydataS.Contains("ALL"))
        {
            citydata.Insert(0, "全國");
            dataCP.Insert(0, lPermits.Sum(s => s.CPCount));
            dataP.Insert(0, lPermits.Sum(s => s.PCount));
            dataC1.Insert(0, lPermits.Sum(s => s.Class1));
            dataC2.Insert(0, lPermits.Sum(s => s.Class2));
            dataC3.Insert(0, lPermits.Sum(s => s.Class3));
        }

        Root root = new Root()
        {
            tooltip = new Tooltip()
            {
                trigger = "axis",
                axisPointer = new AxisPointer()
                {
                    type = "shadow",
                },
            },
            legend = new Legend()
            {
                top = "3%",
                left = "center",
                textStyle = new TextStyle()
                {
                    fontSize = 14,
                },
                data = docdata
            },
            grid = new Grid()
            {
                left = "7%",
                right = "2%",
                top = "8%",
                bottom = "10%",
            },
            xAxis = new List<Xaxi>()
            {
                new Xaxi()
                {
                    type = "category",
                    data = citydata,
                    axisLabel=new AxisLabel()
                    {
                        fontSize=14,
                        margin=16
                    }
                }
            },
            yAxis = new List<Yaxi>()
            {
                new Yaxi() {
                    type = "value",
                    name = @"證列\n件管\n張廠\n數家\n︵數\n張︵\n︶家\n＼︶",
                    nameLocation="start",
                    nameTextStyle=new NameTextStyle()
                    {
                        fontSize=14,
                        padding=new List<int>(){ -320, 130, 0, 0 }
                    }
                }
            },
            series = new List<Series>()
            {
                new Series()
                {
                    name=lPermitTxt["CP"],
                    type="bar",
                    emphasis=new Emphasis()
                    {
                        focus="series",
                    },
                    data=dataCP,
                    barWidth="25%",
                    label= new Lable()
                    {
                        show=true,
                        fontSize=12
                    }
                },
                new Series()
                {
                    name=lPermitTxt["Per"],
                    type="bar",
                    emphasis=new Emphasis()
                    {
                        focus="series",
                    },
                    data=dataP,
                    barWidth="25%",
                    label= new Lable()
                    {
                        show=true,
                        fontSize=12
                    }
                },
                new Series()
                {
                    name=lPermitTxt["Class1"],
                    type="bar",
                    stack="證件張數",
                    emphasis=new Emphasis()
                    {
                        focus="series",
                    },
                    data=dataC1,
                    barWidth="13%",
                    label= new Lable()
                    {
                        show=false,
                        fontSize=12,
                        offset=new List<int>{ 0,10 }
                    }
                },
                new Series()
                {
                    name=lPermitTxt["Class2"],
                    type="bar",
                    stack="證件張數",
                    emphasis=new Emphasis()
                    {
                        focus="series",
                    },
                    data=dataC2,
                    barWidth="13%",
                    label= new Lable()
                    {
                        show=false,
                        fontSize=12,
                    }
                },
                new Series()
                {
                    name=lPermitTxt["Class3"],
                    type="bar",
                    stack="證件張數",
                    emphasis=new Emphasis()
                    {
                        focus="series",
                    },
                    data=dataC3,
                    barWidth="13%",
                    label= new Lable()
                    {
                        show=false,
                        fontSize=12,
                        offset=new List<int>{ 0,-10 }
                    }
                }
            }
        };
        jsonString = JsonConvert.SerializeObject(root);

    }

    protected void Showcity()
    {
        var CitySort = new Dictionary<string, int> {
            { "C", 0 }, { "A", 1 }, { "F", 2 }, { "H", 3 }, { "J", 4 }
            , { "O", 5 }, { "K", 6 }, { "B", 7 }, { "N", 8 }, { "M", 9 }
            , { "P", 10 }, { "Q", 11 }, { "I", 12 }, { "D", 13 }, { "E", 14 }
            , { "T", 15 }, { "G", 16 }, { "U", 17 }, { "V", 18 }, { "X", 19 }
            , { "W", 20 }, { "Z", 21 } };

        var lPermits = ReportPermitsActionAll_op.Query(new ReportPermitsActionAll_cond { QueryDT = DateTime.Today })
            .Where(w => w.Class >= 1 && w.Class <= 3)
            .GroupBy(g => g.EP_CityCode)
            .Select(s => new
            {
                DBCache.ListCityCode(s.Key).First().CityName,
                lEP_CityCode = s.First().EP_CityCode,
            }).OrderBy(o => CitySort[o.lEP_CityCode]).ToList();
        cblCity.DataSource = lPermits.Select(s => new { s.CityName, s.lEP_CityCode }).ToList();
        cblCity.DataTextField = "CityName";
        cblCity.DataValueField = "lEP_CityCode";
        cblCity.DataBind();

        cblCity.Items.Insert(0, new ListItem("全　國", "ALL"));

    }

    protected void lbtnSelect_Click(object sender, EventArgs e)
    {
        foreach (ListItem chkitem in cblCity.Items)
        {
            chkitem.Selected = true;
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        foreach (ListItem chkitem in cblCity.Items)
        {
            chkitem.Selected = false;
        }
    }

    protected void btncheckCity_Click(object sender, EventArgs e)
    {
        List<string> citydataS = new List<string>();
        List<string> citydataN = new List<string>();
        for (int i = 0; i < cblCity.Items.Count; i++)
        {
            if (cblCity.Items[i].Selected)
            {
                citydataS.Add(cblCity.Items[i].Value.Trim());
            }
            else
                citydataN.Add(cblCity.Items[i].Value.Trim());
        }
        if (citydataN.Count == cblCity.Items.Count)
        {
            citydataN.RemoveAt(0);
        }
        Show_echarts(citydataS.Count > 0 ? citydataS : citydataN);
    }
}