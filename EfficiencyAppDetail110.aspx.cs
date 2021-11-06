using ERI.Report.Tox_99.Efficiency.EfficiencyGreadDetail110;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Efficiency_EfficiencyAppDetail110 : DataPage
{
    public string County
    {
        get { return (ViewState["County"] ?? "").ToString(); }
        set { ViewState["County"] = value; }
    }
    public string Epa
    {
        get { return (UserInfo.DEPARTMENT.Contains("署") || UserInfo.DEPARTMENT.Contains("行政院")).ToString().ToLower(); }
    }
    public int Year = 110;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        _master_MasterPageEPA2 _m = this.Page.Master as _master_MasterPageEPA2;
        _m.oCompatibleEdge = true;

        if (Page.IsPostBack) return;

        Init();
        GetData();
    }

    #region Method
    private void Init()
    {
        if (Request["C"] != null && !string.IsNullOrEmpty(Request["C"].ToString()))
            this.County = Request["C"].ToString().ToUpper();

        ClearControl(this.Page);
    }
    private void GetData()
    {
        if (string.IsNullOrEmpty(this.County)) return;
        DataTable dtCity = DBCache.dtCityCode(this.County);
        string sCityName = string.Empty;
        if (dtCity.Rows.Count != 0) sCityName = dtCity.Rows[0]["CityName"].ToString();
        lbCity.Text = sCityName;

        EfficiencyGreadDetail_110_info sInfo = EfficiencyGreadDetail_110_op.GetCityScore(this.County);
        string ID = this.Year + this.County;
        int Type = 0;
        //1_1_1
        string sAbnormalReport = (sInfo.Item1_1_1.Percent.HasValue)
            ? sInfo.Item1_1_1.Percent.Value.ToString("0.#")
            : "-";
        lbAbnormalReport.Text = sAbnormalReport;
        lbAbnormalReport.Text = "-";

        //1_1_2
        string sInspectionPercent = (sInfo.Item1_1_2.Percent.HasValue)
            ? sInfo.Item1_1_2.Percent.Value.ToString("0.#")
            : "-";
        lbInspectionPercent.Text = sInspectionPercent;

        //1_1_3
        lbEEMS.Text = sInfo.Item1_1_3.Times.ToString();

        //2_1
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒化災或相關災害防救會議;
        lbtnDDis.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnDDis.ClientID, this.Year + "-" + Type)
            , " 辦理毒災或相關災害防救會議者、毒災防救等相關議題會議者、廠商運作管理說明會等...");
        //2_2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒災通聯測試及通聯資料異動上網更新;
        lbtnCommunication.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnCommunication.ClientID, this.Year + "-" + Type)
            , " 辦理毒災通聯測試及通聯資料異動上網更新");
        //lbtnDDisT.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
        //    , GetEvaluationDetailUrl("lbtnDDisT", "Defense")
        //    , " 辦理通聯測試組織代碼填寫");

        //2_3
        //lbtnDefC.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
        //     , GetEvaluationDetailUrl("lbtnDefC", "Defense")
        //     , " 辦理無預警測試組織代碼填寫");

        //2_4
        string sDefUpdate = (sInfo.Item2_4.Percent.HasValue)
            ? sInfo.Item2_4.Percent.Value.ToString("0.#")
            : "-";
        lbDefUpdate.Text = sDefUpdate;
        lbDefUpdate.Text = EfficiencyGreadDetail_op.DefenseUnUpdate(County).ToString("0.##");
        //lbtnDefT.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
        //    , GetEvaluationDetailUrl("lbtnDefT", "Defense")
        //    , "  辦理無預警測試組織代碼填寫");

        //2_4
        //lbEquipManageProjectP.Text = sInfo.Item2_4.Percent.ToString();
        //Type = (int)EfficiencyGreadDetail_110_op.eType109.輔導業者系統填列;
        //lbtnSuperviceCom.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
        //    , GetDateSelUrl(Type, lbtnSuperviceCom.ClientID, this.Year + "-" + Type)
        //    , "輔導業者系統填列(如說明會，宣導等)");

        //2_5
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒災應變相關災防演習或毒災線上模擬演練;
        lbtnDis.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnDis.ClientID, this.Year + "-" + Type)
            , "辦理毒災應變或相關災防演習、複合型演習者、毒災線上模擬演練");

        //2_6
        lbtnDisUpload.OnClientClick = string.Format("return OpenDateSel('{0}','發生事故應變處理 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.毒性化學物質運作場所發生事故檔案上傳));

        //2_7
        lbGPS.Text = (sInfo.Item2_7.Percent != null) ? sInfo.Item2_7.Percent.ToString() : "-";

        //2_8
        lbTransport.Text = (sInfo.Item2_8.Count != null) ? sInfo.Item2_8.Count.ToString() : "-";
        lbTransportRemark.Text = (sInfo.Item2_8.Count == null || sInfo.Item2_8.Count == 0) ? "（有車無異常）" : "";
        lbTransport.Text = "-";

        //3_1
        lbReqList.Text = sInfo.Item3_1.RequiredList.ToString();
        string sReqInspection = (sInfo.Item3_1.ReqPercent.HasValue)
            ? sInfo.Item3_1.ReqPercent.Value.ToString("0.#")
            : "-";

        lbReqList.Text = "-";
        lbReqInspection.Text = sReqInspection;
        lbReqInspection.Text = "-";
        lbNewInspection.Text = sInfo.Item3_1.NewInspection.ToString();
        lbNewInspection.Text = "-";

        //3_2
        string sProjectInspection = (sInfo.Item3_2.Percent.HasValue)
            ? sInfo.Item3_2.Percent.Value.ToString("0.#")
            : "-";
        lbProjectInspection.Text = sProjectInspection;
        lbProjectInspection.Text = "-";

        //3_3
        lbUpHighRisk.Text = sInfo.Item3_3.Count.ToString();
        lbUpHighRisk.Text = "-";

        //4_2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒性及關注化學物質管理法規說明會;
        lbtnPolicyConvention.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnPolicyConvention.ClientID, this.Year + "-" + Type)
            , " 法規政策宣導說明會");

        //5_1
        Type = (int)EfficiencyGreadDetail_110_op.eType110.降低毒物及關注化學物質運作風險等地方創新作為;
        lbtnCreative.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnCreative.ClientID, this.Year + "-" + Type)
            , " 地方創新作為");
        //5_2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.配合本署推派人員參與專業訓練;
        lbtnTrain.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnTrain.ClientID, this.Year + "-" + Type)
            , " 配合本署推派人員參與專業訓練、演練");

        //---------------------------------------------------------------------------------------
        PlaceCount();
        List<Efficiency_info> le = Efficiency_op.Query(new Efficiency_cond() { ID = this.Year + this.County });
        if (le.Count == 1)
            ObjMappingControl(le[0], EfficiencyEvaluationDetail_op.Query(new EfficiencyEvaluationDetail_cond() { ID = this.Year + this.County }));
    }
    public void ClearControl(Control _ctl)
    {
        if (!_ctl.HasControls()) return;

        foreach (Control c in _ctl.Controls)
        {
            if (c.Visible)
            {
                if (c is CheckBoxList)
                    (c as CheckBoxList).SelectedIndex = -1;
                if (c is CheckBox)
                {
                    (c as CheckBox).Checked = false;
                    (c as CheckBox).Enabled = false;
                }
                if (c is Label)
                    (c as Label).Text = "-";
                if (c.HasControls())
                    ClearControl(c);
            }
        }
    }
    public void PlaceCount()
    {
        //lbtnSeminar.Text = lbtnHREPA.Text = lbtnDDis.Text = lbtnCommunication.Text = lbtnDis.Text = "0";

        List<EfficiencyPlaceDetail_info> lPlace = EfficiencyPlaceDetail_op.Query(new EfficiencyPlaceDetail_cond() { ID = this.Year + this.County });
        if (lPlace.Count == 0) return;
        //lbtnSeminar.Text = lPlace.Where(x =>
        //    x.Type == ((int)EfficiencyGreadDetail_109_op.eType109.運作廠商說明會).ToString()
        //    ).Count().ToString();
        //lbtnHREPA.Text = lPlace.Where(x =>
        //    x.Type == ((int)EfficiencyGreadDetail_109_op.eType109.食安宣導).ToString()
        //    ).Count().ToString();
        lbtnDDis.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.辦理毒化災或相關災害防救會議).ToString()
            ).Count().ToString();
        lbtnCommunication.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.辦理毒災通聯測試及通聯資料異動上網更新).ToString()
            ).Count().ToString();
        lbtnDis.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.辦理毒災應變相關災防演習或毒災線上模擬演練).ToString()
            ).Count().ToString();
        lbtnCreative.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.降低毒物及關注化學物質運作風險等地方創新作為).ToString()
            ).Count().ToString();
        lbtnPolicyConvention.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.辦理毒性及關注化學物質管理法規說明會).ToString()
            ).Count().ToString();
        //lbtnSuperviceCom.Text = lPlace.Where(x =>
        //    x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.督導業者更新聯防組織線上系統資料).ToString()
        //    ).Count().ToString();
        lbtnTrain.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.配合本署推派人員參與專業訓練).ToString()
            ).Count().ToString();
    }
    private string GetDateSelUrl(int _type, string _ID, string _ExtraControlType)
    {
        return string.Format("EfficiencyDateSel.aspx?J={0}&E=v&Y={1}&C={2}&ID={3}&Control={4}"
            , _type
            , this.Year
            , this.County
            , _ID
            , _ExtraControlType
            );
    }
    private string GetStaff(int _type, string _ID, string _ExtraControlType)
    {
        return string.Format("EfficiencyStaff.aspx?J={0}&E=v&Y={1}&C={2}&ID={3}&Control={4}"
            , _type
            , this.Year
            , this.County
            , _ID
            , _ExtraControlType
            );
    }
    private string GetEvaluationDetailUrl(string _ColumnName, string _UiType)
    {
        return string.Format("EfficiencyEvaluationDetail.aspx?J={0}&E=v&Y={1}&C={2}&U={3}"
            , _ColumnName
            , this.Year
            , this.County
            , _UiType
            );
    }
    private string GetUpFileUrl(int _type)
    {
        return string.Format("EfficiencyUpFile.aspx?T={0}&C={1}&E={2}"
            , StringKey.EncryptSys(_type.ToString())
            , StringKey.EncryptSys(this.County)
            , StringKey.EncryptSys("V")
            );

    }
    private void ObjMappingControl(Efficiency_info _info, List<EfficiencyEvaluationDetail_info> _led)
    {
        bool b;
        #region 輸入資料
        //2_1
        txtDDisMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDDisMemo", _led);

        //2_2
        txtDDisT.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDDisT", _led);
        txtCommunicationMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtCommunicationMemo", _led);

        //2_3
        TextConnectP.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("TextConnectP", _led);
        TextConfirmP.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("TextConfirmP", _led);
        //2_4
        txtDefT.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDefT", _led);

        //2_5
        txtDis.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDis", _led);

        //2_6
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbNoDisaster", _led), out b);
        cbNoDisaster.Checked = b;
        divDisHappen.Style.Add("display", (b) ? "none" : "");
        txtDisaster.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDisaster", _led);

        //4_1
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPublished", _led), out b);
        cbPublished.Checked = b;
        txtPublish.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtPublish", _led);

        //4_2
        txtPolicyConvention.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtPolicyConvention", _led);

        //4_3
        //txtADKinds.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtADKinds", _led);

        //5-1
        txtCreativeMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtCreativeMemo", _led);

        //5-3
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre1", _led), out b);
        cbPre1.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre2", _led), out b);
        cbPre2.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre3", _led), out b);
        cbPre3.Checked = b;
        //5-4
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbMR1", _led), out b);
        cbMR1.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbMR2", _led), out b);
        cbMR2.Checked = b;

        #endregion

        #region 分數
        double dG1 = 0, dG2 = 0, dG3 = 0, dG4 = 0, dG5 = 0;
        double ds = 0;

        string sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG111", _led);
        double.TryParse(sValue, out ds);
        dG1 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG112", _led);
        double.TryParse(sValue, out ds);
        dG1 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG113", _led);
        double.TryParse(sValue, out ds);
        dG1 += ds;
        lbscG1.Text = dG1.ToString("0.##");

        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG21", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG22", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG23", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG24", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG25", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG26", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG27", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG28", _led);
        double.TryParse(sValue, out ds);
        dG2 += ds;
        lbscG2.Text = dG2.ToString("0.##");

        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG311", _led);
        double.TryParse(sValue, out ds);
        dG3 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG312", _led);
        double.TryParse(sValue, out ds);
        dG3 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG313", _led);
        double.TryParse(sValue, out ds);
        dG3 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG32", _led);
        double.TryParse(sValue, out ds);
        dG3 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG33", _led);
        double.TryParse(sValue, out ds);
        dG3 += ds;
        lbscG3.Text = dG3.ToString("0.##");

        //4
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG41", _led);
        double.TryParse(sValue, out ds);
        dG4 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG42", _led);
        double.TryParse(sValue, out ds);
        dG4 += ds;
        //sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG43", _led);
        //double.TryParse(sValue, out ds);
        //dG4 += ds;
        lbscG4.Text = dG4.ToString("0.##");

        //5
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG51", _led);
        double.TryParse(sValue, out ds);
        dG5 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG52", _led);
        double.TryParse(sValue, out ds);
        dG5 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG53", _led);
        double.TryParse(sValue, out ds);
        dG5 += ds;
        sValue = EfficiencyEvaluationDetail_op.GetValueByColumnName("scG54", _led);
        double.TryParse(sValue, out ds);
        dG5 += ds;
        lbscG5.Text = dG5.ToString("0.##");

        #endregion
    }
    #endregion
}