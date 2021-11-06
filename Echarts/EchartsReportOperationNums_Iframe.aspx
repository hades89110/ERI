<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EchartsReportOperationNums_Iframe.aspx.cs" Inherits="Echarts_EchartsReportOperationNums_Iframe" %>

<%@ Register Src="~/_uc/uc_Toxic.ascx" TagPrefix="eri" TagName="ucToxic" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>歷年運作量查詢</title>
    <link href="/Report/_img/style.css" rel="stylesheet" />
</head>
<body style="height: 98%; margin: 0">
    <form id="form1" runat="server" style="text-align:center">
        毒化物：
                <eri:ucToxic ID="ucToxicData" runat="server" />
        統計期間：<asp:DropDownList ID="ddlYearS" runat="server" />年
                <asp:DropDownList ID="ddlMonthS" runat="server" />月至
                <asp:DropDownList ID="ddlYearE" runat="server" />年
                <asp:DropDownList ID="ddlMonthE" runat="server" />月
                    <asp:Button ID="checkData" runat="server" CssClass="btnQuery" OnClick="checkData_Click" />
        <div style="height: 800px">
            <div id="container" style="height: 50%"></div>
            <div id="container2" style="height: 50%"></div>
        </div>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/echarts@5/dist/echarts.min.js"></script>
        <script type="text/javascript">

            var dom = document.getElementById("container");
            var myChart = echarts.init(dom);

            var dom2 = document.getElementById("container2");
            var myChart2 = echarts.init(dom2);

            var app = {};

            var option = JSON.parse('<%=jsonString%>');
            var option2 = JSON.parse('<%=jsonString2%>');


            if (option && typeof option === 'object') {
                myChart.setOption(option);
            }
            if (option2 && typeof option2 === 'object') {
                myChart2.setOption(option2);
            }

        </script>
    </form>
</body>
</html>
