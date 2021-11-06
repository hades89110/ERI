using ERI.Report.Tox_99.Efficiency.EfficiencyGreadDetail110;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Efficiency_EfficiencyAppNew110 : DataPage
{
    public string County
    {
        get { return ddlCounty.SelectedValue.ToUpper(); }
        set { ddlCounty.SelectedValue = value; }
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

        //LoadRequest();
        //InitialSetting();
    }

    #region Method
    private void Init()
    {
        if (Request["C"] != null && !string.IsNullOrEmpty(Request["C"].ToString()))
            this.County = Request["C"].ToString().ToUpper();

        DateTime EndTime = new DateTime(2023, 01, 27);
        if (DateTime.Now > EndTime && !string.IsNullOrEmpty(UserInfo.CityCode))
        {
            divMain.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('績效考評已結束填寫');location.href='../Main.aspx';", true);
        }

        ClearControl(this.Page);
        ddlCountyBind();

    }

    private void GetData()
    {
        if (string.IsNullOrEmpty(this.County)) return;

        EfficiencyGreadDetail_110_info sInfo = EfficiencyGreadDetail_110_op.GetCityScore(this.County);

        string ID = this.Year + this.County;
        int Type = 0;


        //1_1_1
        lbAbnormalReport.Text = "-";

        //1_1_2
        string sInspectionPercent = (sInfo.Item1_1_2.Percent.HasValue)
            ? sInfo.Item1_1_2.Percent.Value.ToString("0.#")
            : "-";
        lbInspectionPercent.Text = sInspectionPercent;
        //lbInspectionPercent.Text = "-";

        //1_1_3
        lbEEMS.Text = sInfo.Item1_1_3.Times.ToString("0.#");

        //110 2-1
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒化災或相關災害防救會議;
        lbtnDDis.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnDDis.ClientID, this.Year + "-" + Type)
            , " 辦理毒災或相關災害防救會議者、毒災防救等相關議題會議者、廠商運作管理說明會等...");

        //110 2-2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒災通聯測試及通聯資料異動上網更新;
        lbtnCommunication.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnCommunication.ClientID, this.Year + "-" + Type)
            , " 辦理毒災通聯測試及通聯資料異動上網更新");

        //var lPermits = EfficiencyGreadDetail_110_op.DefenseGroup_op.GetGroupNo(new EfficiencyGreadDetail_110_op.DefenseGroup_cond
        //{
        //    ID = ID,
        //    ColumnName = "lbtnDDisT"
        //}).ToList();

        var lPermits = EfficiencyGreadDetail_110_op.DefenseGroup_op.GetGroupNoNums(new EfficiencyGreadDetail_110_op.DefenseGroup_cond
        {
            ID = ID
        }).ToList();

        List<string> ls = new List<string>();
        List<string> ln = new List<string>();
        //if (lPermits.Count != 0)
        //{
        //    string[] ss = lPermits.Select(s => s.Value).First().ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //    //string[] ss = { "A000001", "A000002" };
        //    Array.ForEach(ss, x => ls.Add(x));
        //    ls.OrderBy(x => x).ToList();
        //}

        //var IPermits = EfficiencyGreadDetail_110_op.DefenseGroup_op.CountSingleNumbers(new EfficiencyGreadDetail_110_op.DefenseGroup_cond
        //{
        //    GroupNoList = ls
        //}).ToList();
        //ln = IPermits.Select(s => s.Nums).ToList();
        ls = lPermits.Select(s => s.GroupNums).ToList();
        ln = lPermits.Select(s => s.Nums).ToList();
        if (ls.Count > 0)
        {
            rpDefenceDataBind(ls,ln);
        }

        //110 2-4
        string sDefUpdate = (sInfo.Item2_4.Percent.HasValue)
            ? sInfo.Item2_4.Percent.Value.ToString("0.#")
            : "-";
        lbDefUpdate.Text = sDefUpdate;
        lbDefUpdate.Text = EfficiencyGreadDetail_op.DefenseUnUpdate(County).ToString("0.##");
        //lbtnDefT.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
        //    , GetEvaluationDetailUrl("lbtnDefT", "Defense")
        //    , "  轄內聯防組織無預警測試");

        if (ls.Count > 0)
        {
            rpDefenceNWDataBind(ls,ln);
        }

        //110 2-5
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒災應變相關災防演習或毒災線上模擬演練;
        lbtnDis.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnDis.ClientID, this.Year + "-" + Type)
            , "辦理毒災應變或相關災防演習、複合型演習者、毒災線上模擬演練");

        //2_6
        lbtnDisUpload.OnClientClick = string.Format("return OpenDateSel('{0}','發生事故應變處理 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.毒性化學物質運作場所發生事故檔案上傳));

        //2_7
        lbGPS.Text = (sInfo.Item2_7.Percent != null)
            ? sInfo.Item2_7.Percent.Value.ToString("0.##")
            : "-";

        //2_8
        //lbTransport.Text = (sInfo.Item2_8.Count != null)
        //    ? sInfo.Item2_8.Count.Value.ToString("0.##")
        //    : "-";
        lbTransport.Text = "-";
        lbTransportRemark.Text = (sInfo.Item2_8.Count == null || sInfo.Item2_8.Count == 0) ? "（有車無異常）" : "";
        lbTransportRemark.Text = "-";

        //3_1
        //lbReqList.Text = sInfo.Item3_1.RequiredList.ToString();
        //string sReqInspection = (sInfo.Item3_1.ReqPercent.HasValue)
        //    ? sInfo.Item3_1.ReqPercent.Value.ToString("0.#")
        //    : "-";
        //lbReqInspection.Text = sReqInspection;
        //lbNewInspection.Text = sInfo.Item3_1.NewInspection.ToString();
        lbReqList.Text = "-";   
        lbReqInspection.Text = "-";
        lbNewInspection.Text = "-";
        //3_2
        string sProjectInspection = (sInfo.Item3_2.Percent.HasValue)
            ? sInfo.Item3_2.Percent.Value.ToString("0.#")
            : "-";
        lbProjectInspection.Text = sProjectInspection;
        lbProjectInspection.Text = "-";

        //3_3
        //lbUpHighRisk.Text = sInfo.Item3_3.Count.ToString();
        lbUpHighRisk.Text = "-";

        //110 4-2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.辦理毒性及關注化學物質管理法規說明會;
        lbtnPolicyConvention.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnPolicyConvention.ClientID, this.Year + "-" + Type)
            , " 法規政策宣導說明會");

        //110 5
        Type = (int)EfficiencyGreadDetail_110_op.eType110.降低毒物及關注化學物質運作風險等地方創新作為;
        lbtnCreative.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetDateSelUrl(Type, lbtnCreative.ClientID, this.Year + "-" + Type)
            , " 地方創新作為");

        //110 5-2
        Type = (int)EfficiencyGreadDetail_110_op.eType110.配合本署推派人員參與專業訓練;
        lbtnTrain.OnClientClick = string.Format("return OpenDateSel('{0}','{1}')"
            , GetStaffUrl(Type, lbtnTrain.ClientID, this.Year + "-" + Type)
            , " 配合本署推派人員參與專業訓練、演練");

        //110 5-3
        lbtnPreventionUpload1.OnClientClick = string.Format("return OpenDateSel('{0}','定期檢視更新毒性化學物質危害預防及應變計畫書網頁 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.定期檢視更新毒性化學物質危害預防及應變計畫書網頁));
        lbtnPreventionUpload2.OnClientClick = string.Format("return OpenDateSel('{0}','公開第三類毒性化學物質危害預防及應變計畫書於網頁摘要達50%列管廠商數 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.公開第三類毒性化學物質危害預防及應變計畫書於網頁摘要));
        lbtnPreventionUpload3.OnClientClick = string.Format("return OpenDateSel('{0}','將第三類毒性化學物質危害預防及應變計畫書完整公開達50%列管廠商數 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.將第三類毒性化學物質危害預防及應變計畫書));

        //110 5-4
        lbtnManageRrgs1.OnClientClick = string.Format("return OpenDateSel('{0}','公布非農地環境雜草管理條例 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.公布非農地環境雜草管理條例));
        lbtnManageRrgs2.OnClientClick = string.Format("return OpenDateSel('{0}','訂定草案預告或其他具體替代方案 檔案上傳')"
            , GetUpFileUrl((int)EfficiencyGreadDetail_110_op.eType110.訂定草案預告或其他具體替代方案));

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
                    (c as CheckBox).Checked = false;
                if (c is TextBox)
                    (c as TextBox).Text = "";
                if (c.HasControls())
                    ClearControl(c);
            }
        }
    }
    private void ddlCountyBind()
    {
        ddlCounty.DataSource = DBCache.dtCityCode(string.Empty);
        ddlCounty.DataTextField = "CityName";
        ddlCounty.DataValueField = "CityCode";
        ddlCounty.DataBind();

        if (UserInfo.COUNTY_NA != "行政院")
        {
            ListItem li = ddlCounty.Items.FindByValue(UserInfo.CityCode);
            if (li != null) ddlCounty.SelectedValue = UserInfo.CityCode;
            ddlCounty.Enabled = false;
        }

    }
    public void PlaceCount()
    {

        List<EfficiencyPlaceDetail_info> lPlace = EfficiencyPlaceDetail_op.Query(new EfficiencyPlaceDetail_cond() { ID = this.Year + this.County });
        if (lPlace.Count == 0) return;
        //lbtnSuperviceCom.Text = lPlace.Where(x =>
        //    x.Type == ((int)EfficiencyGreadDetail_110_op.eType109.輔導業者系統填列).ToString()
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
        lbtnTrain.Text = lPlace.Where(x =>
            x.Type == ((int)EfficiencyGreadDetail_110_op.eType110.配合本署推派人員參與專業訓練).ToString()
            ).Count().ToString();


    }
    private string GetDateSelUrl(int _type, string _ID, string _ExtraControlType)
    {
        return string.Format("EfficiencyDateSel.aspx?J={0}&E=e&Y={1}&C={2}&ID={3}&Control={4}"
            , _type
            , this.Year
            , this.County
            , _ID
            , _ExtraControlType
            );
    }
    private string GetStaffUrl(int _type, string _ID, string _ExtraControlType)
    {
        return string.Format("EfficiencyStaff.aspx?J={0}&E=e&Y={1}&C={2}&ID={3}&Control={4}"
            , _type
            , this.Year
            , this.County
            , _ID
            , _ExtraControlType
            );
    }
    private string GetEvaluationDetailUrl(string _ColumnName, string _UiType)
    {
        return string.Format("EfficiencyEvaluationDetail.aspx?J={0}&E=e&Y={1}&C={2}&U={3}"
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
            , StringKey.EncryptSys("E")
            );
    }
    private void ObjMappingControl(Efficiency_info _info, List<EfficiencyEvaluationDetail_info> _led)
    {
        bool b;
        string ID = this.Year + this.County;

        //21
        txtDDisMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDDisMemo", _led);

        //22
        //txtDDisT.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDDisT", _led);
        txtCommunicationMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtCommunicationMemo", _led);

        //var lPermits = EfficiencyGreadDetail_110_op.DefenseGroup_op.GetGroupNo(new EfficiencyGreadDetail_110_op.DefenseGroup_cond
        //{
        //    ID = ID,
        //    ColumnName = "lbtnDDisT"
        //}).ToList();
        List<string> ln = new List<string>();
        var strGroupNumDiv = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDDisT", _led);
        if (strGroupNumDiv.Length > 0)
        {
            string[] ss = strGroupNumDiv.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ForEach(ss, x => ln.Add(x));
            ln.OrderBy(x => x).ToList();
        }
        if (ln.Count == rpDefence.Items.Count)
        {
            for (int i = 0; i < rpDefence.Items.Count; i++)
            {
                ((TextBox)rpDefence.Items[i].FindControl("txtGroupNums")).Text = ln[i];
            }
        }

        //23
        TextConnectP.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("TextConnectP", _led);
        TextConfirmP.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("TextConfirmP", _led);

        //24
        //txtDefT.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDefT", _led);

        ln = new List<string>();
        strGroupNumDiv = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDefT", _led);
        if (strGroupNumDiv.Length > 0)
        {
            string[] ss = strGroupNumDiv.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Array.ForEach(ss, x => ln.Add(x));
            ln.OrderBy(x => x).ToList();
        }
        if (ln.Count == rpDefenceNW.Items.Count)
        {
            for (int i = 0; i < rpDefenceNW.Items.Count; i++)
            {
                ((TextBox)rpDefenceNW.Items[i].FindControl("txtGroupNums")).Text = ln[i];
            }
        }

        //25
        txtDis.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDis", _led);

        //26
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbNoDisaster", _led), out b);
        cbNoDisaster.Checked = b;
        divDisHappen.Style.Add("display", (b) ? "none" : "");
        txtDisaster.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtDisaster", _led);

        //41
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPublished", _led), out b);
        cbPublished.Checked = b;
        txtPublish.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtPublish", _led);

        //42
        txtPolicyConvention.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtPolicyConvention", _led);

        //43
        //txtADKinds.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtADKinds", _led);

        //51
        txtCreativeMemo.Text = EfficiencyEvaluationDetail_op.GetValueByColumnName("txtCreativeMemo", _led);

        //53
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre1", _led), out b);
        cbPre1.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre2", _led), out b);
        cbPre2.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbPre3", _led), out b);
        cbPre3.Checked = b;

        //54
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbMR1", _led), out b);
        cbMR1.Checked = b;
        bool.TryParse(EfficiencyEvaluationDetail_op.GetValueByColumnName("cbMR2", _led), out b);
        cbMR2.Checked = b;
    }
    private void ControlMappingObj(ref Efficiency_info _info, ref List<EfficiencyEvaluationDetail_info> _le)
    {
        _info.ID = this.Year + this.County;
        _info.Mod_Name = Efficiency_op.GetAppName(UserInfo.UserName);
        if (string.IsNullOrEmpty(_info.App_Name)) _info.App_Name = _info.Mod_Name;

        //二、(一)、1.
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDDisMemo", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtDDisMemo.Text.Trim() });

        //二、(一)、2.
        //_le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDDisT", DataType = EfficiencyEvaluationDetail_op.DataType._Int, Value = string.IsNullOrEmpty(txtDDisT.Text.Trim()) ? "0" : txtDDisT.Text.Trim() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtCommunicationMemo", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtCommunicationMemo.Text.Trim() });

        string strDefGroupNoS = "";
        for (int i = 0; i < rpDefence.Items.Count; i++)
        {
            strDefGroupNoS += ((TextBox)rpDefence.Items[i].FindControl("txtGroupNums")).Text.Trim();
            strDefGroupNoS += "|";
        }
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDDisT", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = strDefGroupNoS });


        //二、(一)、3.
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "TextConnectP", DataType = EfficiencyEvaluationDetail_op.DataType._Int, Value = string.IsNullOrEmpty(TextConnectP.Text.Trim()) ? "0" : TextConnectP.Text.Trim() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "TextConfirmP", DataType = EfficiencyEvaluationDetail_op.DataType._Int, Value = string.IsNullOrEmpty(TextConfirmP.Text.Trim()) ? "0" : TextConfirmP.Text.Trim() });

        //二、(一)、4.

        //_le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDefT", DataType = EfficiencyEvaluationDetail_op.DataType._Int, Value = string.IsNullOrEmpty(txtDefT.Text.Trim()) ? "0" : txtDefT.Text.Trim() });
        strDefGroupNoS = "";
        for (int i = 0; i < rpDefenceNW.Items.Count; i++)
        {
            strDefGroupNoS += ((TextBox)rpDefenceNW.Items[i].FindControl("txtGroupNums")).Text.Trim();
            strDefGroupNoS += "|";
        }
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDefT", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = strDefGroupNoS });

        //二、(二)、1.
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDis", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtDis.Text.Trim() });

        //二、(二)、2.
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbNoDisaster", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbNoDisaster.Checked.ToString() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtDisaster", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtDisaster.Text.Trim() });


        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbPublished", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbPublished.Checked.ToString() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtPublish", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtPublish.Text.Trim() });

        //42
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtPolicyConvention", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtPolicyConvention.Text.Trim() });

        ////43
        //_le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtADKinds", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtADKinds.Text.Trim() });

        //51
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "txtCreativeMemo", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = txtCreativeMemo.Text.Trim() });

        //53
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbPre1", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbPre1.Checked.ToString() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbPre2", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbPre2.Checked.ToString() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbPre3", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbPre3.Checked.ToString() });
        //54
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbMR1", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbMR1.Checked.ToString() });
        _le.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "cbMR2", DataType = EfficiencyEvaluationDetail_op.DataType._bool, Value = cbMR2.Checked.ToString() });
    }
    private bool CheckControl()
    {
        string sEnter = @"\n\r";
        string sErrmsg = string.Empty;
        int a = 0;



        for (int i = 0; i < rpDefence.Items.Count; i++)
        {
            if (((TextBox)rpDefence.Items[i].FindControl("txtGroupNums")).Text=="")
            {
                ((TextBox)rpDefence.Items[i].FindControl("txtGroupNums")).Text = "0";
            }
            if (((TextBox)rpDefenceNW.Items[i].FindControl("txtGroupNums")).Text == "")
            {
                ((TextBox)rpDefenceNW.Items[i].FindControl("txtGroupNums")).Text = "0";
            }
        }
        //二、(一)、2.
        //if (!string.IsNullOrEmpty(txtDDisT.Text.Trim()) && (!int.TryParse(txtDDisT.Text.Trim(), out i) || i < 0)) sErrmsg += "二、(一)、2. 辦理通聯測試總家數 請填寫正整數" + sEnter;

        //二、(一)、3.
        //if (!string.IsNullOrEmpty(txtDefT.Text.Trim()) && (!int.TryParse(txtDefT.Text.Trim(), out i) || i < 0)) sErrmsg += "二、(一)、3. 辦理無預警測試組數 請填寫正整數" + sEnter;

        //------------------------------------------------------------------------------------------------
        List<EfficiencyEvaluationDetail_info> linfo = EfficiencyEvaluationDetail_op.Query(new EfficiencyEvaluationDetail_cond() { ID = this.Year + this.County, ColumnName = "lbtnDDisT" });
        //if (!string.IsNullOrEmpty(txtDDisT.Text.Trim()) && txtDDisT.Text.Trim() != "0" && string.IsNullOrEmpty(EfficiencyEvaluationDetail_op.GetValueByColumnName("lbtnDDisT", linfo))) sErrmsg += "通聯測試組織代碼 " + " 未填寫" + sEnter;
        linfo = EfficiencyEvaluationDetail_op.Query(new EfficiencyEvaluationDetail_cond() { ID = this.Year + this.County, ColumnName = "lbtnDefT" });
        //if (!string.IsNullOrEmpty(txtDefT.Text.Trim()) && txtDefT.Text.Trim() != "0" && string.IsNullOrEmpty(EfficiencyEvaluationDetail_op.GetValueByColumnName("lbtnDefT", linfo))) sErrmsg += "無預警測試組織代碼 " + " 未填寫" + sEnter;

        //linfo = EfficiencyEvaluationDetail_op.Query(new EfficiencyEvaluationDetail_cond() { ID = this.Year + this.County, ColumnName = "lbtnDDis" });

        var lPermits = EfficiencyGreadDetail_110_op.DefenseGroup_op.GetGroupNoNums(new EfficiencyGreadDetail_110_op.DefenseGroup_cond
        {
            ID = this.Year + this.County
        }).ToList();

        List<string> ln = new List<string>();
        ln = lPermits.Select(s => s.Nums).ToList();
        for (int i = 0; i < rpDefence.Items.Count; i++)
        {
            if (int.Parse(((TextBox)rpDefence.Items[i].FindControl("txtGroupNums")).Text.Trim()) > int.Parse(ln[i]))
            {
                sErrmsg = "列管總家數通聯測試不得超過該組織家數";
            }
        }
        for (int i = 0; i < rpDefence.Items.Count; i++)
        {
            if (int.Parse(((TextBox)rpDefenceNW.Items[i].FindControl("txtGroupNums")).Text.Trim()) > int.Parse(ln[i]))
            {
                sErrmsg = "聯防組織無預警測試不得超過該組織家數";
            }
        }
        //QQQQQQQQQQQ
        //SystemInfo.ShowMsg(this.Page, sErrmsg=ln[0].ToString() + ((TextBox)rpDefence.Items[0].FindControl("txtGroupNums")).Text.Trim().ToString());

        if (!string.IsNullOrEmpty(sErrmsg))
            SystemInfo.ShowMsg(this.Page, sErrmsg);

        return string.IsNullOrEmpty(sErrmsg);
    }
    private void InsertScore()
    {
        EfficiencyGreadDetail_110_info dInfo = EfficiencyGreadDetail_110_op.GetCityScore(this.County);
        List<EfficiencyEvaluationDetail_info> lScore = new List<EfficiencyEvaluationDetail_info>();
        double ds = 0;

        //ITEMS SCORE
        ds = dInfo.Item1_1_1.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG111", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item1_1_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG112", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item1_1_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG113", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_1.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG21", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG22", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG23", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_4.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG24", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_5.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG25", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_6.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG26", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_7.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG27", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item2_8.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG28", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item3_1.Score1;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG311", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item3_1.Score2;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG312", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item3_1.Score3;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG313", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item3_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG32", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item3_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG33", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item4_1.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG41", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item4_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG42", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item5_1.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG51", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item5_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG52", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item5_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG53", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });
        ds = dInfo.Item5_4.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG54", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });


        //SUM SCORE
        ds = dInfo.Item1_1_1.Score + dInfo.Item1_1_2.Score + dInfo.Item1_1_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG1", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });

        ds = dInfo.Item2_1.Score + dInfo.Item2_2.Score + dInfo.Item2_3.Score + dInfo.Item2_4.Score + dInfo.Item2_5.Score + dInfo.Item2_6.Score + dInfo.Item2_7.Score + dInfo.Item2_8.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG2", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });

        ds = dInfo.Item3_1.Score1 + dInfo.Item3_1.Score2 + dInfo.Item3_1.Score3 + dInfo.Item3_2.Score + dInfo.Item3_3.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG3", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });

        ds = dInfo.Item4_1.Score + dInfo.Item4_2.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG4", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });

        ds = dInfo.Item5_1.Score + dInfo.Item5_2.Score + dInfo.Item5_3.Score + dInfo.Item5_4.Score;
        lScore.Add(new EfficiencyEvaluationDetail_info() { ID = this.Year + this.County, ColumnName = "scG5", DataType = EfficiencyEvaluationDetail_op.DataType._String, Value = ds.ToString("0.##") });

        foreach (EfficiencyEvaluationDetail_info item in lScore)
            EfficiencyEvaluationDetail_op.DBInsertUpdate(item);
    }

    #endregion

    #region Event

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearControl(this);
        GetData();
    }
    protected void btnSubmint_Click(object sender, EventArgs e)
    {
        if (!CheckControl()) return;

        List<Efficiency_info> infos = Efficiency_op.Query(new Efficiency_cond() { ID = this.Year + this.County });
        Efficiency_info info = infos.Count > 0 ? infos[0] : new Efficiency_info();
        List<EfficiencyEvaluationDetail_info> le = EfficiencyEvaluationDetail_op.Query(new EfficiencyEvaluationDetail_cond() { ID = this.Year + this.County });

        ControlMappingObj(ref info, ref le);
        Efficiency_op.DBInsertUpdate(info);

        foreach (EfficiencyEvaluationDetail_info item in le)
            EfficiencyEvaluationDetail_op.DBInsertUpdate(item);
        InsertScore();
        GetData();

        EfficiencyGreadDetail_op.DBInsertUpdate(new EfficiencyGreadDetail_info() { ID = this.Year + this.County });
        Response.Redirect(string.Format("EfficiencyAppDetail{0}.aspx?C={1}", this.Year, this.County));
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearControl(this.Page);
    }

    #endregion


    private void rpDefenceDataBind(List<string> ls, List<string> ln)
    {
        var GroupList = new List<DefGroupdata>();
        for (int i = 0; i < ls.Count; i++)
        {
            GroupList.Add(new DefGroupdata() { GroupNo = ls[i], GroupNoSums = ln[i] });
        }
        rpDefence.DataSource = GroupList;
        rpDefence.DataBind();
    }

    private void rpDefenceNWDataBind(List<string> ls, List<string> ln)
    {
        var GroupList = new List<DefGroupdata>();
        for (int i = 0; i < ls.Count; i++)
        {
            GroupList.Add(new DefGroupdata() { GroupNo = ls[i], GroupNoSums = ln[i] });
        }
        rpDefenceNW.DataSource = GroupList;
        rpDefenceNW.DataBind();
    }


    protected void View109Detail_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("EfficiencyAppDetail108.aspx?C={0}", this.County));
        Response.Write(string.Format("<script>window.open ('EfficiencyAppDetail110.aspx?C={0}','_blank');</script>", this.County));
    }

    protected void rpDefence_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                var G = (DefGroupdata)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbGroup1")).Text = G.GroupNo;
                ((TextBox)e.Item.FindControl("txtGroupNums")).Text = G.GroupNums;
                ((Label)e.Item.FindControl("lbGroupNoNum")).Text = G.GroupNoSums;
                break;
        }
    }

    public class DefGroupdata
    {
        public string GroupNo { get; set; }
        public string GroupNums { get; set; }
        public string GroupNoSums { get; set; }
    }

    //public List<DefGroupdata> defGroupdatas {get;set;}

    protected void rpDefenceNW_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                var G = (DefGroupdata)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbGroup2")).Text = G.GroupNo;
                ((TextBox)e.Item.FindControl("txtGroupNums")).Text = G.GroupNums;
                ((Label)e.Item.FindControl("lbGroupNoNum")).Text = G.GroupNoSums;
                break;
        }
    }
}