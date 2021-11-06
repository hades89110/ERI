<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EchartsUseGoal_Iframe.aspx.cs" Inherits="Echarts_EchartsUseGoal_Iframe" %>

<%@ Register Src="~/_uc/uc_Toxic.ascx" TagPrefix="eri" TagName="ucToxic" %>

<!DOCTYPE html>
<style type="text/css">
    .Center {
        text-align: center;
    }
    .tname {
        position: absolute;
        top: 58%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-family: sans-serif;
        font-size: 28px;
        z-index: 100;
        line-height: 34px;
        text-align: center;
    }

    .tname2 {
        position: absolute;
        top: 62%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-family: sans-serif;
        font-size: 22px;
        z-index: 100;
    }
</style>

<html style="height: 93%">
<head>
    <meta charset="utf-8">
    <link href="/Report/_img/style.css" rel="stylesheet" />
</head>
<body style="height: 100%; margin: 0">

    <div class="tname"><%=ToxicName%></div>
    <div class="tname2"><%=SumP%></div>
    <div class="tname2"><%=SumT%></div>
    <form id="form1" runat="server">
        <div style="text-align: center">
            毒化物：
        <eri:ucToxic ID="toxicDDL" runat="server" />&emsp;
            統計期間：
        <asp:DropDownList ID="ddlYearS" runat="server"></asp:DropDownList>
            年 
        <asp:DropDownList ID="ddlMonthS" runat="server"></asp:DropDownList>月到
        <asp:DropDownList ID="ddlMonthE" runat="server"></asp:DropDownList>月&emsp;
        <asp:Button ID="checkData" runat="server" Text="查詢" OnClick="checkData_Click" />
            <div style="text-align: right">
                <asp:Button ID="ButtonChangePSort" runat="server" Text="以廠家數排序" OnClick="ButtonChangePSort_Click" BackColor="#FCF8E8" Height="32px" />
                <asp:Button ID="ButtonChangeCSort" runat="server" Text=" 以序號排序 " OnClick="ButtonChangeCSort_Click" BackColor="#D5ECC2" Height="32px"/>
                &emsp;&emsp;&emsp;&emsp;&emsp;
            </div>
        </div>

    </form>

    <div id="container2" style="height: 16%"></div>
    <div id="container" style="height: 84%"></div>

    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/echarts@5/dist/echarts.min.js"></script>

    <script type="text/javascript">
        var dom = document.getElementById("container");
        var myChart = echarts.init(dom);
        var dom2 = document.getElementById("container2");
        var myChart2 = echarts.init(dom2);
        var app = {};

        var option;
        option = JSON.parse('<%=jsonString%>');
        var option2;
        option2 = JSON.parse('<%=jsonString2%>');


        if (option && typeof option === 'object') {
            myChart.setOption(option);
        }
        if (option2 && typeof option2 === 'object') {
            myChart2.setOption(option2);
        }

    </script>
</body>
</html>
