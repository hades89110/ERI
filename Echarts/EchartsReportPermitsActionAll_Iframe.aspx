<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EchartsReportPermitsActionAll_Iframe.aspx.cs" Inherits="Echarts_EchartsReportPermitsActionAll_Iframe" %>

<!DOCTYPE html>
<html style="height: 100%">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Report/_img/style.css" rel="stylesheet" />
    <title></title>
</head>
<body style="height: 92%; margin: 0">
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/echarts@5/dist/echarts.min.js"></script>
    <form id="form1" runat="server">

        <div style="font-size: 18px; padding: 0.5em; text-align: center">
            <asp:LinkButton ID="lbtnSelect" runat="server" Text="選擇全部" AutoPostBack="true" OnClick="lbtnSelect_Click" Font-Underline="false" />
            <asp:LinkButton ID="lbtnCancel" runat="server" Text="取消全部" AutoPostBack="true" OnClick="lbtnCancel_Click" Font-Underline="false" />
            <br />
            <asp:CheckBoxList ID="cblCity" runat="server" RepeatColumns="10" RepeatDirection="Horizontal" RepeatLayout="Flow" />
            <asp:Button ID="btncheckCity" runat="server" CssClass="btnQuery" OnClick="btncheckCity_Click" />
        </div>
    </form>

    <div id="container" style="height: 82%"></div>

    <script type="text/javascript">
        var dom = document.getElementById("container");
        var myChart = echarts.init(dom);
        var app = {};

        var option;

        option = JSON.parse('<%=jsonString%>');

        if (option && typeof option === 'object') {
            myChart.setOption(option);
        }
    </script>
    <br />
    <div style="text-align:left;margin-left:10%;font-size:14px;color:#424242;font-family:sans-serif">
        說明：若篩選縣市過多，且瀏覽器未開啟至“最大化”時，將影響查詢結果顯示之縣市別資料。
    </div>
</body>
</html>
