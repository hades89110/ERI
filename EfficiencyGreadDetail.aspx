<%@ Page Title="" Language="C#" MasterPageFile="~/_master/MasterPageEPA.master" AutoEventWireup="true" CodeFile="EfficiencyGreadDetail.aspx.cs" Inherits="Efficiency_EfficiencyGreadDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Header" Runat="Server">
    <link href="css/base.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>績效考評 【 分數查詢 】</h2><hr />
    年度：<asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />
    <div class="w-80 fix-float" id="downloadOpeartion">
        <div class="float-left">
            <input type="button" onclick="GetDownLoad(this);" id="GetWord" value="列印"/>
            <input type="button" onclick="GetDownLoad(this);" id="GetExcel" value="Excel下載"/>
        </div>
        <div class="float-right">
            <input type="button" value="地方環保機關績效考核分數表下載" onclick="GetGTotal();"/>
        </div>
    </div>
    <div class="w-80 noData" id="noData">查無資料</div>
    
    <asp:GridView runat="server" ID="gv" AutoGenerateColumns="false" CssClass="gvHeader c" Width="80%">
    </asp:GridView>
    <script type="text/javascript">
        var table = document.getElementById('<%=gv.ClientID%>');
        var inputs = table.getElementsByTagName("input");
        var cbCitys = [];
        var hfCitys = [];
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].id.indexOf("cbCity") != -1)
                cbCitys.push(inputs[i]);
            if (inputs[i].id.indexOf("hfCity") != -1)
                hfCitys.push(inputs[i]);
        }
        if (cbCitys.length == 0) {
            document.getElementById("downloadOpeartion").style.display = "none";
            document.getElementById("noData").style.display = "";
        } else {
            document.getElementById("downloadOpeartion").style.display = "";
            document.getElementById("noData").style.display = "none";
        }

        function GetDownLoad(obj) {
            var Cou = '';
            for (var i = 0; i < cbCitys.length; i++)
                if (cbCitys[i].checked)
                    Cou += hfCitys[i].value;

            if (Cou.length == 0) {
                alert("請選取縣市");
            } else {
                var page = "";
                switch (obj.id) {
                    case "GetWord": page = "EfficiencyReport"; break;
                    case "GetExcel": page = "EfficiencyExcel"; break;
                }
                var year = document.getElementById('<%=ddlYear.ClientID%>').value;
                if (page == "EfficiencyReport" && year == "108") { alert('功能尚未開放!!!'); return false;}
                var url = page + ".ashx?T=REPORT&Y=" + year + "&Cou=" + Cou;
                window.open(url);

                //var url = page + ".ashx";
                //$.ajax({
                //    url: url,
                //    type: "GET",
                //    data: { "T": "REPORT", "Y": year, "Cou": Cou },
                //    success: function () {
                //        alert("操作成功")
                //    },
                //    error: function (xhr) {
                //        alert("操作失敗")
                //    }
                //});
            }
        }

        function GetAllCheckOrNot(_bool) {
            var bool = Boolean(_bool);
            for (var i = 0; i < inputs.length; i++)
                inputs[i].checked = bool;
        }

        function GetGTotal() {
            var year = document.getElementById('<%=ddlYear.ClientID%>').value;
            var url = "EfficiencyReport.ashx?V=GTotal&Y=" + year + "&E=<%=StringKey.EncryptSys("GetGTotal")%>";
            window.open(url);

            <%--$.ajax({
                url: "EfficiencyReport.ashx",
                type: "GET",
                data: { "V": "GTotal", "Y": year, "E": '<%=StringKey.EncryptSys("GetGTotal")%>' },
                success: function () {
                    alert("操作成功");
                },
                error: function (xhr) {
                    alert("操作失敗")
                }
            });--%>
        }
    </script>
</asp:Content>

