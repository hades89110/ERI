using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERI.Report.Echarts.EchartsUseGoal;

public partial class Echarts_EchartsUseGoal_Iframe : System.Web.UI.Page
{
    [Serializable]
    public class UseVal
    {
        public string UseName { get; set; }
        public int CP { get; set; }
        public double OperVal { get; set; }
        public string Co { get; set; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (IsPostBack) return;
        DataLoad();
    }

    protected void DataLoad()
    {
        toxicDDL.SetDDL3 = false;
        toxicDDL.Bind(true);
        for (int i = 1; i <= 12; i++)
        {
            ddlMonthS.Items.Add(i.ToString("D2"));
            ddlMonthE.Items.Add(i.ToString("D2"));
        }
        for (int i = DateTime.Today.Year; i >= 1911 + 101; i--)
        {
            ddlYearS.Items.Add(new ListItem(i.ToString(), (i - 1911).ToString()));
            //ddlYearE.Items.Add(new ListItem(i.ToString(), (i - 1911).ToString()));
        }
        ddlYearS.SelectedValue = (DateTime.Today.Year - 1 - 1911).ToString();
        ddlMonthE.SelectedValue = "12";
    }

    public string jsonString = "";
    public string jsonString2 = "";
    public string ToxicName = "";
    //public string Sort = "N";

    public string SumT
    {
        get { return ViewState["SumT"] as string ?? ""; }
        set
        { ViewState["SumT"] = value; }
    }
    public string SumP
    {
        get { return ViewState["SumP"] as string ?? ""; }
        set
        { ViewState["SumP"] = value; }
    }

    public string Sort
    {
        get { return ViewState["Sort"] as string ?? "N"; }
        set { ViewState["Sort"] = value; }
    }
    //public List<UseVal> UseVals = new List<UseVal>();
    public List<UseVal> UseVals
    {
        get { return ViewState["UseVals"] as List<UseVal> ?? new List<UseVal>(); }
        set { ViewState["UseVals"] = value; }
    }

    protected void Show_Data(List<UseVal> UseVals)
    {
        Root mainroot = new Root()
        {
            tooltip = new Tooltip()
            {
                trigger = "item",
                //formatter = @"{a}<br/>{b}  {c} 家"
            },
            legend = new Legend()
            {
                show = false,
                /*
                top = "5%",
                left = "center",
                textStyle = new TextStyle() { fontSize = 16 }
                */
            },
            series = new List<Series>()
            {
                new Series()
                {
                    name="使用目的用途",
                    type="pie",
                    radius= new List<string>()   {  "35%","60%"  },
                    top="8%",
                    //avoidLabelOverlap=false,
                    color= new List<string>()  { "#e6e6e6","#d9d9d9", "#cccccc", "#bfbfbf", "#b3b3b3", "#a6a6a6", "#999999", "#8c8c8c","#808080","#737373","#666666","#595959","#4d4d4d","#404040","#333333","#262626",
                        "#1a1a1a","#0d0d0d","#00000","#0d0d0d","#lalala","#262626"  },
                    itemStyle= new ItemStyle()   {  borderRadius=10,   borderColor="#fff",  borderWidth=2   },
                    label= new ERI.Report.Echarts.EchartsUseGoal.Label()
                    {
                        //show=true,
                        position="outside", fontSize=20, formatter=@"{b}", lineHeight=26
                    },
                    emphasis =new Emphasis()
                    {
                        label =new ERI.Report.Echarts.EchartsUseGoal.Label()
                        { //show=true,
                          fontSize=22,  formatter=@"{b}",  color="rgba(255, 255, 255, 1)",  textBorderColor="rgba(0, 0, 0, 1)", textBorderWidth=3,lineHeight=28
                        }
                    },
                    labelLine =new LabelLine()
                    {
                        //show=true,
                        lineStyle=new LineStyle()  {  width=3  },
                        length=35
                    },
                    data= new List<Datum>()
                    {
                        //Datum
                    }
                }
            }
        };

        Root mainroot2 = new Root()
        {
            legend = new Legend()
            {
                show = true,
                top = "5%",
                left = "center",
                textStyle = new TextStyle() { fontSize = 18 },
                orient = "vertical"
            },
            series = new List<Series>()
            {
                new Series()
                {
                    color= new List<string>()  { "#e6e6e6","#d9d9d9", "#cccccc", "#bfbfbf", "#b3b3b3", "#a6a6a6", "#999999", "#8c8c8c","#808080","#737373","#666666","#595959","#4d4d4d","#404040","#333333","#262626",
                        "#1a1a1a","#0d0d0d","#00000","#0d0d0d","#lalala","#262626" },
                    type="pie",
                    data= new List<Datum>()
                    {
                        //Datum
                    }
                }
            }
        };

        mainroot.series.First().data = new List<Datum>();
        int countcolor = 0;
        List<string> ColorList = new List<string>();

        ColorList.Add("#5470c6");
        ColorList.Add("#91cc75");
        ColorList.Add("#fac858");

        for (int i = 0; i < UseVals.Count; i++)
        {
            if (UseVals[i].CP > 0)
                mainroot.series.First().data.Add(new Datum() { value = UseVals[i].CP, name = UseVals[i].UseName + @"：\n" + Math.Abs(UseVals[i].OperVal / 1000000).ToString("N0") + "噸 " + UseVals[i].CP.ToString("N0") + "家" });
            if (i < 3)
            {
                mainroot.series.First().data.Last().itemStyle = new ItemStyle { };
                mainroot.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                mainroot.series.First().data.Last().itemStyle.borderColor = "#fff";
                mainroot.series.First().data.Last().itemStyle.borderRadius = 10;
                mainroot.series.First().data.Last().itemStyle.borderWidth = 2;
                countcolor++;
            }
        }
        countcolor = 0;
        for (int i = 0; i < UseVals.Count; i++)
        {
            if (UseVals[i].CP > 0)
                mainroot2.series.First().data.Add(new Datum() { name = UseVals[i].UseName });
            if (i < 3)
            {
                mainroot2.series.First().data.Last().itemStyle = new ItemStyle { };
                mainroot2.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                mainroot2.series.First().data.Last().itemStyle.borderColor = "#fff";
                mainroot2.series.First().data.Last().itemStyle.borderRadius = 10;
                mainroot2.series.First().data.Last().itemStyle.borderWidth = 2;
                countcolor++;
            }
        }
        jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(mainroot);
        jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(mainroot2);
    }


    protected void checkData_Click(object sender, EventArgs e)
    {
        if (toxicDDL.SelectedValue == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('請選擇毒化物')", true);
            return;
        }
        var ss = DBCache.ListToxic(toxicDDL.SelectedValue).First().TName;
        ToxicName = toxicDDL.SelectedValue + "<br/>" + DBCache.ListToxic(toxicDDL.SelectedValue).First().TName;

        var lPermitsK = UseGoalKeyWord_op.QueryUseGoalAddKeyWord(new UseGoalKeyWord_cond { ToxicNo = toxicDDL.SelectedValue });

        List<string> uselistK = lPermitsK.Select(s => s.KeyWord).ToList();
        List<string> uselistN = lPermitsK.Select(s => s.Use_Goal).ToList();
        List<string> uselistCo = lPermitsK.Select(s => s.Use_Goal_co).ToList();


        if (int.Parse(ddlMonthS.SelectedValue) > int.Parse(ddlMonthE.SelectedValue))
        {
            string Swap = ddlYearS.SelectedValue;
            Swap = ddlMonthS.SelectedValue;
            ddlMonthS.SelectedValue = ddlMonthE.SelectedValue;
            ddlMonthE.SelectedValue = Swap;
        }

        if (uselistK.Count == 0)
        {
            SystemInfo.ShowMsg(Page, "此毒化物無使用清單");
            return;
        }
        
        var lPermits = TR_OperationMain_op.QueryKeyWordToToxic(new TR_OperationMain_cond
        {
            ToxicNo = toxicDDL.SelectedValue,
            MonthS = ddlMonthS.SelectedValue,
            MonthE = ddlMonthE.SelectedValue,
            YearS = ddlYearS.SelectedValue,
            //YearE = ddlYearE.SelectedValue,
            UseListK = uselistK
        }).ToList();

        var UseVal = new List<UseVal>();
        var Val = lPermits.First();
        if (uselistN.Count > 0) UseVal.Add(new UseVal() { UseName = uselistN[0], CP = Val.P1, OperVal = Val.T1, Co = uselistCo[0] });
        if (uselistN.Count > 1) UseVal.Add(new UseVal() { UseName = uselistN[1], CP = Val.P2, OperVal = Val.T2, Co = uselistCo[1] });
        if (uselistN.Count > 2) UseVal.Add(new UseVal() { UseName = uselistN[2], CP = Val.P3, OperVal = Val.T3, Co = uselistCo[2] });
        if (uselistN.Count > 3) UseVal.Add(new UseVal() { UseName = uselistN[3], CP = Val.P4, OperVal = Val.T4, Co = uselistCo[3] });
        if (uselistN.Count > 4) UseVal.Add(new UseVal() { UseName = uselistN[4], CP = Val.P5, OperVal = Val.T5, Co = uselistCo[4] });
        if (uselistN.Count > 5) UseVal.Add(new UseVal() { UseName = uselistN[5], CP = Val.P6, OperVal = Val.T6, Co = uselistCo[5] });
        if (uselistN.Count > 6) UseVal.Add(new UseVal() { UseName = uselistN[6], CP = Val.P7, OperVal = Val.T7, Co = uselistCo[6] });
        if (uselistN.Count > 7) UseVal.Add(new UseVal() { UseName = uselistN[7], CP = Val.P8, OperVal = Val.T8, Co = uselistCo[7] });
        if (uselistN.Count > 8) UseVal.Add(new UseVal() { UseName = uselistN[8], CP = Val.P9, OperVal = Val.T9, Co = uselistCo[8] });
        if (uselistN.Count > 9) UseVal.Add(new UseVal() { UseName = uselistN[9], CP = Val.P10, OperVal = Val.T10, Co = uselistCo[9] });
        if (uselistN.Count > 10) UseVal.Add(new UseVal() { UseName = uselistN[10], CP = Val.P11, OperVal = Val.T11, Co = uselistCo[10] });
        if (uselistN.Count > 11) UseVal.Add(new UseVal() { UseName = uselistN[11], CP = Val.P12, OperVal = Val.T12, Co = uselistCo[11] });
        if (uselistN.Count > 12) UseVal.Add(new UseVal() { UseName = uselistN[12], CP = Val.P13, OperVal = Val.T13, Co = uselistCo[12] });
        if (uselistN.Count > 13) UseVal.Add(new UseVal() { UseName = uselistN[13], CP = Val.P14, OperVal = Val.T14, Co = uselistCo[13] });
        if (uselistN.Count > 14) UseVal.Add(new UseVal() { UseName = uselistN[14], CP = Val.P15, OperVal = Val.T15, Co = uselistCo[14] });
        if (uselistN.Count > 15) UseVal.Add(new UseVal() { UseName = uselistN[15], CP = Val.P16, OperVal = Val.T16, Co = uselistCo[15] });
        if (uselistN.Count > 16) UseVal.Add(new UseVal() { UseName = uselistN[16], CP = Val.P17, OperVal = Val.T17, Co = uselistCo[16] });
        if (uselistN.Count > 17) UseVal.Add(new UseVal() { UseName = uselistN[17], CP = Val.P18, OperVal = Val.T18, Co = uselistCo[17] });
        if (uselistN.Count > 18) UseVal.Add(new UseVal() { UseName = uselistN[18], CP = Val.P19, OperVal = Val.T19, Co = uselistCo[18] });
        if (uselistN.Count > 19) UseVal.Add(new UseVal() { UseName = uselistN[19], CP = Val.P20, OperVal = Val.T20, Co = uselistCo[19] });
        if (uselistN.Count > 20) UseVal.Add(new UseVal() { UseName = uselistN[20], CP = Val.P21, OperVal = Val.T21, Co = uselistCo[20] });
        if (uselistN.Count > 21) UseVal.Add(new UseVal() { UseName = uselistN[21], CP = Val.P22, OperVal = Val.T22, Co = uselistCo[21] });
        if (uselistN.Count > 22) UseVal.Add(new UseVal() { UseName = uselistN[22], CP = Val.P23, OperVal = Val.T23, Co = uselistCo[22] });
        if (uselistN.Count > 23) UseVal.Add(new UseVal() { UseName = uselistN[23], CP = Val.P24, OperVal = Val.T24, Co = uselistCo[23] });
        if (uselistN.Count > 24) UseVal.Add(new UseVal() { UseName = uselistN[24], CP = Val.P25, OperVal = Val.T25, Co = uselistCo[24] });

        UseVal = UseVal.OrderByDescending(ob => ob.CP).ToList();
        if (lPermits.Select(s => s.P1).First() == 0) return;
        UseVals = UseVal;
        Sort = "P";
        ButtonChangeCSort.Enabled = true;
        ButtonChangePSort.Enabled = false;

        string psum = lPermits.Select(s => s.P1 + s.P2 + s.P3 + s.P4 + s.P5 + s.P6 + s.P7 + s.P8 + s.P9 + s.P10 + s.P11 + s.P12 + s.P13 + s.P14 + s.P15 + s.P16 + s.P17 + s.P18
          + s.P19 + s.P20 + s.P21 + s.P22 + s.P23 + s.P24 + s.P25).First().ToString("N0");
        SumP = "<br/><br/><br/><br/>總廠家數" + psum + "家";
        //double tsum = lPermits.Select(s => s.T1 + s.T2 + s.T3 + s.T4 + s.T5 + s.T6 + s.T7 + s.T8 + s.T9 + s.T10 + s.T11 + s.T12 + s.T13 + s.T14 + s.T15 + s.T16 + s.T17 + s.T18
        //  + s.T19 + s.T20 + s.T21 + s.T22 + s.T23 + s.T24 + s.T25).First();
        //SumT = "<br/><br/>總運作量" + Math.Abs(tsum / 1000000).ToString("N0") + "噸";
        var SumTt = ReportOperationNumber_op.QueryGroupYear(new ReportOperationNumber_cond
        {
            ToxicNo = toxicDDL.SelectedValue,
            MonthS = ddlMonthS.SelectedValue,
            MonthE = ddlMonthE.SelectedValue,
            YearS = ddlYearS.SelectedValue,
            YearE = ddlYearS.SelectedValue+1,
        }).ToList();
        SumT = "<br/><br/>總運作量" + SumTt.Sum(s => s.Use).ToString("N0") + "噸";

        Show_Data(UseVals);
    }

    protected void ChangeSort()
    {
        var UseVal = UseVals;

        Root mainroot = new Root()
        {
            tooltip = new Tooltip()
            {
                trigger = "item",
                //formatter = @"{a}<br/>{b}  {c} 家"
            },
            legend = new Legend()
            {
                show = false,
                /*
                top = "5%",
                left = "center",
                textStyle = new TextStyle() { fontSize = 16 }
                */
            },
            series = new List<Series>()
            {
                new Series()
                {
                    name="使用目的用途",
                    type="pie",
                    radius= new List<string>()   {  "35%","60%"  },
                    top="8%",
                    color= new List<string>()  { "#e6e6e6","#d9d9d9", "#cccccc", "#bfbfbf", "#b3b3b3", "#a6a6a6", "#999999", "#8c8c8c","#808080","#737373","#666666","#595959","#4d4d4d","#404040","#333333","#262626",
                        "#1a1a1a","#0d0d0d","#00000","#0d0d0d","#lalala","#262626"  },
                    //avoidLabelOverlap=false,
                    itemStyle= new ItemStyle()   {  borderRadius=10,   borderColor="#fff",  borderWidth=2   },
                    label= new ERI.Report.Echarts.EchartsUseGoal.Label()
                    {
                        //show=true,
                        position="outside", fontSize=20, formatter=@"{b}", lineHeight=26
                    },
                    emphasis =new Emphasis()
                    {
                        label =new ERI.Report.Echarts.EchartsUseGoal.Label()
                        { //show=true,
                          fontSize=22,  formatter=@"{b}",  color="rgba(255, 255, 255, 1)",  textBorderColor="rgba(0, 0, 0, 1)", textBorderWidth=3,lineHeight=28
                        }
                    },
                    labelLine =new LabelLine()
                    {
                        //show=true,
                        lineStyle=new LineStyle()  {  width=3  },
                        length=35

                    },
                    data= new List<Datum>()
                    {
                        //Datum
                    }
                }
            }
        };

        Root mainroot2 = new Root()
        {
            legend = new Legend()
            {
                show = true,
                top = "5%",
                left = "center",
                textStyle = new TextStyle() { fontSize = 18 },
                orient = "vertical"
            },
            series = new List<Series>()
            {
                new Series()
                {
                    color= new List<string>()  { "#e6e6e6","#d9d9d9", "#cccccc", "#bfbfbf", "#b3b3b3", "#a6a6a6", "#999999", "#8c8c8c","#808080","#737373","#666666","#595959","#4d4d4d","#404040","#333333","#262626",
                        "#1a1a1a","#0d0d0d","#00000","#0d0d0d","#lalala","#262626" },
                    type="pie",
                    data= new List<Datum>()
                    {
                        //Datum
                    }
                }
            }
        };


        mainroot.series.First().data = new List<Datum>();
        int countcolor = 0;
        List<string> ColorList = new List<string>();

        //Random rd = new Random();
        //int a = rd.Next(1, 5);

        ColorList.Add("#5470c6");
        ColorList.Add("#91cc75");
        ColorList.Add("#fac858");

        switch (Sort)
        {
            case "P":
                string Co1 = "", Co2 = "", Co3 = "";
                if (UseVal.Count > 2)
                {
                    Co1 = UseVal[0].Co;
                    Co2 = UseVal[1].Co;
                    Co3 = UseVal[2].Co;
                }
                else if (UseVal.Count == 2)
                {
                    Co1 = UseVal[0].Co;
                    Co2 = UseVal[1].Co;
                }
                else
                {
                    Co1 = UseVal[0].Co;
                }
                UseVals = UseVal.OrderBy(ob => ob.Co).ToList();
                Sort = "C";
                ButtonChangePSort.Enabled = true;
                ButtonChangeCSort.Enabled = false;

                for (int i = 0; i < UseVals.Count; i++)
                {
                    if (UseVals[i].CP > 0)
                        mainroot.series.First().data.Add(new Datum() { value = UseVals[i].CP, name = UseVals[i].UseName + @"：\n" + Math.Abs(UseVals[i].OperVal / 1000000).ToString("N0") + "噸 " + UseVals[i].CP.ToString("N0") + "家" });
                    if (UseVals[i].Co == Co1 || UseVals[i].Co == Co2 || UseVals[i].Co == Co3 )
                    {
                        mainroot.series.First().data.Last().itemStyle = new ItemStyle { };
                        mainroot.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                        mainroot.series.First().data.Last().itemStyle.borderColor = "#fff";
                        mainroot.series.First().data.Last().itemStyle.borderRadius = 10;
                        mainroot.series.First().data.Last().itemStyle.borderWidth = 2;
                        countcolor++;
                    }
                }
                countcolor = 0;
                for (int i = 0; i < UseVals.Count; i++)
                {
                    if (UseVals[i].CP > 0)
                        mainroot2.series.First().data.Add(new Datum() { name = UseVals[i].UseName });
                    if (UseVals[i].Co == Co1 || UseVals[i].Co == Co2 || UseVals[i].Co == Co3 || UseVals[i].Co == "")
                    {
                        mainroot2.series.First().data.Last().itemStyle = new ItemStyle { };
                        mainroot2.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                        mainroot2.series.First().data.Last().itemStyle.borderColor = "#fff";
                        mainroot2.series.First().data.Last().itemStyle.borderRadius = 10;
                        mainroot2.series.First().data.Last().itemStyle.borderWidth = 2;
                        countcolor++;
                    }
                }

                break;
            case "C":
                UseVals = UseVal.OrderByDescending(ob => ob.CP).ToList();
                Sort = "P";
                ButtonChangeCSort.Enabled = true;
                ButtonChangePSort.Enabled = false;

                for (int i = 0; i < UseVals.Count; i++)
                {
                    if (UseVals[i].CP > 0)
                        mainroot.series.First().data.Add(new Datum() { value = UseVals[i].CP, name = UseVals[i].UseName + @"：\n" + Math.Abs(UseVals[i].OperVal / 1000000).ToString("N0") + "噸 " + UseVals[i].CP.ToString("N0") + "家" });
                    if (i < 3)
                    {
                        mainroot.series.First().data.Last().itemStyle = new ItemStyle { };
                        mainroot.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                        mainroot.series.First().data.Last().itemStyle.borderColor = "#fff";
                        mainroot.series.First().data.Last().itemStyle.borderRadius = 10;
                        mainroot.series.First().data.Last().itemStyle.borderWidth = 2;
                        countcolor++;
                    }
                }
                countcolor = 0;
                for (int i = 0; i < UseVals.Count; i++)
                {
                    if (UseVals[i].CP > 0)
                        mainroot2.series.First().data.Add(new Datum() { name = UseVals[i].UseName });
                    if (i < 3)
                    {
                        mainroot2.series.First().data.Last().itemStyle = new ItemStyle { };
                        mainroot2.series.First().data.Last().itemStyle.color = ColorList[countcolor];
                        mainroot2.series.First().data.Last().itemStyle.borderColor = "#fff";
                        mainroot2.series.First().data.Last().itemStyle.borderRadius = 10;
                        mainroot2.series.First().data.Last().itemStyle.borderWidth = 2;
                        countcolor++;
                    }
                }

                break;
        }
        ToxicName = toxicDDL.SelectedValue + "<br/>" + DBCache.ListToxic(toxicDDL.SelectedValue).First().TName;
        jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(mainroot);
        jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(mainroot2);
    }

    protected void ButtonChangePSort_Click(object sender, EventArgs e)
    {
        if (Sort == "N")
        {
            SystemInfo.ShowMsg(Page, "請先查詢物質");
            return;
        }
        ChangeSort();
    }

    protected void ButtonChangeCSort_Click(object sender, EventArgs e)
    {
        if (Sort == "N")
        {
            SystemInfo.ShowMsg(Page, "請先查詢物質");
            return;
        }
        ChangeSort();
    }
}