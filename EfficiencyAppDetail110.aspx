<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EfficiencyAppDetail110.aspx.cs" Inherits="Efficiency_EfficiencyAppDetail110" MasterPageFile="~/_master/MasterPageEPA2.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder_Header">
    <style type="text/css">
        .AlignC {
            text-align: center;
        }

        .CR {
            color: red;
        }

        .CO {
            color: orange;
        }

        .CB {
            color: blue;
        }

        .CG {
            color: green;
        }

        .m-value {
            margin: 0 0.3rem;
        }

        .values {
            color: #0000FF;
        }

        .txtBasic {
            width: 60px;
            background-color: #FFFACD;
        }

        .txtMulti {
            width: 80%;
            min-height: 5em;
            /*min-width: 70%;
            max-width:765px;*/
            max-height: 400px;
            background-color: #FEFFDE;
            font-size: 12px;
        }

        .tableBasic {
            border-color: #777777;
            border-collapse: collapse;
            font-size: 13px;
        }

            .tableBasic th {
                border-color: #777777;
            }

            .tableBasic td {
                border-color: #777777;
                vertical-align: central;
            }

        .divInput {
            border: 1px solid #c2c2c2;
            margin: 0.5rem;
            padding: 0.5rem;
        }

        .subItem {
            /*padding-left: 3rem;*/
            /*text-indent: -2.5rem;*/
        }

        li {
            padding-bottom: 0.5em;
        }

        .Item {
            padding: 0.5rem 1rem;
            vertical-align: top;
        }

            .Item > ol {
                padding-left: 0.5rem;
            }

        .bli {
            counter-reset: list;
            padding-left: 0rem;
        }

            .bli > li {
                list-style: none;
                padding: 0rem;
            }

                .bli > li:before {
                    content: "("counter(list)")";
                    counter-increment: list;
                    display: inline-block;
                    vertical-align: top;
                    padding-top: 0.2rem;
                }

            .bli .lidiv {
                display: inline-block;
                width: 96%;
            }

        .TitleDiv {
            padding: 1rem;
        }
    </style>
    <script type="text/javascript">
        function OpenDateSel(Url, _Name) {
            ShowFrame(Url, _Name);
            return false;
        }
        function SetValue(_ID, _Value) {
            document.getElementById(_ID).innerHTML = _Value;
        }
        function KeepScroll() {
            document.getElementById("btnSubmint").focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div runat="server" id="divMain">
        <span style="font-size: 26px; font-weight: bolder" class="CG">【<%=this.Year%> 年度地方環保機關績效考評指標自評成果 】</span><br />
        <span style="font-size: 18px; font-weight: bolder" class="CO">發證單位：</span>
        <asp:Label runat="server" ID="lbCity" />

        <table class="tableBasic" border="1">
            <colgroup>
                <col style="width: 100px" />
                <col style="width: 45px" />
                <col style="width: 140px" />
                <col style="width: 45px" />
                <col style="width: 45px" />
                <col style="width: 750px" />
                <col style="width: 400px" />
            </colgroup>
            <tr style="height: 35px; background-color: #8FBC8F; text-align: center">
                <th>指標類型</th>
                <th>項次</th>
                <th>考核指標</th>
                <th>權重</th>
                <th>分數</th>
                <th>評分標準</th>
                <th>備註</th>
            </tr>
            <tr>
                <td rowspan="4">&ensp;施政重點(90%)</td>
                <td style="padding-left: 1.5em;">1</td>
                <td class="subItem">
                    <div class="TitleDiv">
                        毒物及關注化學物質勾稽查核<br />
                        <br />
                    </div>
                </td>
                <td class="AlignC">26%</td>
                <td class="AlignC">
                    <asp:Label ID="lbscG1" runat="server" CssClass="m-value CB" Text="-" />
                </td>
                <td class="Item">
                    <ol>
                        <li>毒性及關注化學物質勾稽查核 （26%）
                            <ol class="bli">
                                <li>
                                    <div class="lidiv">
                                        於毒化物管理系統下載之專案稽查名單(上下游流向異常及笑氣運作稽查等)，依清查期程於毒性化學物質管理系統中回報之達成率計分，平均之達成率 100% 者
                                        ，得 10 分；平均之達成率 95~99% 者，得 8 分；平均之達成率 90~95% 者，得 6 分；平均之達成率 85~90% 者，得 4 分；平均之達成率未達 85% 者，以 0 分計。(10%)
                                    </div>
                                    <div class="divInput">
                                        回報達成率：
                                        <asp:Label ID="lbAbnormalReport" runat="server" CssClass="m-value CB" Text="-" />
                                        %
                                    </div>
                                </li>
                                <li>
                                    <div class="lidiv">
                                        列管廠場（商）家數達 300 家以上，稽查率 80% 以上得 11 分， 60% 以下得 0 分， 60%~80% 按比例給分；列管廠場（商）家數 299 家以下，
                                        稽查率 95% 以上得 11 分， 75% 以下得0分， 75%~95% 按比率給分。(11%)
                                    </div>
                                    <div class="divInput">
                                        稽查率：
                                    <asp:Label ID="lbInspectionPercent" runat="server" CssClass="m-value CB" Text="-" />
                                        %<br />
                                    </div>
                                </li>
                                <li>
                                    <div class="lidiv">
                                        於每月 25 日前自行於「環保稽查處分管制系統(EEMS)」建立前一個月之稽查單、告發單、裁處單，稽查成果回傳至毒化物管理系統；
                                        每正確上傳（無再修改）一次得分 0.5 分，本項得分最高以 5 分計。(5%)
                                    </div>
                                    <div class="divInput">
                                        正確上傳共：
                                    <asp:Label ID="lbEEMS" runat="server" CssClass="m-value CB" Text="-" />
                                        次<br />
                                    </div>
                                </li>
                            </ol>
                        </li>
                    </ol>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="padding-left: 1.5em;">2</td>
                <td class="subItem">
                    <div class="TitleDiv">
                        毒化災預防整備、演練及事故應變<br />
                        <br />
                    </div>
                </td>
                <td class="AlignC">25 %</td>
                <td class="AlignC">
                    <asp:Label ID="lbscG2" runat="server" CssClass="m-value CB" Text="-" />
                </td>
                <td class="Item">
                    <ol>
                        <li>辦理毒化災或相關災害防救會議：最高得 4 分。
                            <div class="divInput">
                                辦理
                                <asp:LinkButton ID="lbtnDDis" CssClass="m-value" runat="server" Text="0" />
                                場次。
                                <span class="CR">（請直接點擊連結進行場次的輸入）</span><br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtDDisMemo" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span>
                            </div>
                        </li>
                        <li>辦理毒災通聯測試及通聯資料異動上網更新，最高 2 分。列管總家數通聯測試達 5 %（含第 4 類毒化物），最高得 1 分。
                            <div class="divInput">
                                辦理
                                <asp:LinkButton ID="lbtnCommunication" CssClass="m-value" runat="server" Text="0" />
                                場次。
                                <span class="CR">（請直接點擊連結進行場次的輸入）</span><br />
                                列管總通聯測試家數 共
                                <asp:TextBox ID="txtDDisT" CssClass="txtBasic" runat="server" />
                                家。
                                <%--<asp:LinkButton runat="server" ID="lbtnDDisT" Text="辦理通聯測試組織代碼填寫" />--%>
                                <br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtCommunicationMemo" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span><br />
                            </div>
                        </li>
                        <li>毒災事故報知專線測試，包括接通率（最高得1.5分）及確認率（最高得1.5分）。
                            <div class="divInput">
                                接通率<asp:TextBox ID="TextConnectP" CssClass="txtBasic" runat="server" />%，
                                確認率<asp:TextBox ID="TextConfirmP" CssClass="txtBasic" runat="server" />%。
                                <%--<asp:LinkButton runat="server" ID="lbtnDefC" Text="事故報知專線測試組織代碼填寫" />--%>
                                <br />
                            </div>
                        </li>
                        <li>督導業者更新（或確認）聯防組織線上系統資料達80%以上者，得1分。辦理轄內聯防組織無預警測試，達90％以上，最高得1分。
                            <%--<div class="divInput">
                                更新（或確認）比例：                                 
                                <asp:Label ID="lbDefUpdate" runat="server" CssClass="m-value CB" />
                                %
                            </div>--%>
                            <div class="divInput">
                                更新（或確認）比例：                                 
                                <asp:Label ID="lbDefUpdate" runat="server" CssClass="m-value CB" />
                                %<br />
                                轄內聯防組織無預警測試
                                <asp:TextBox ID="txtDefT" CssClass="txtBasic" runat="server" />
                                組。
                                <%--<asp:LinkButton runat="server" ID="lbtnDefT" Text="辦理無預警測試組織代碼填寫" />--%>
                               
                            </div>
                        </li>
                        <li>辦理毒災應變、相關災防演習或毒災線上模擬演練，最高得5分。
                            <div class="divInput">
                                辦理
                            <asp:LinkButton ID="lbtnDis" CssClass="m-value" runat="server" Text="0" />
                                場次。
                            <span class="CR">（請直接點擊連結進行場次的輸入）</span><br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtDis" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span>
                            </div>
                        </li>
                        <li>毒性化學物質運作場所發生事故（以當年度計），現場應變及處理得宜者且完成善後追蹤改善回報本署達100%，最高得3分。
                            <div class="divInput">
                                <asp:CheckBox ID="cbNoDisaster" runat="server" Text="無發生事故" />
                                <br />
                                <div runat="server" id="divDisHappen">
                                    <span class="CG">◎說明</span>
                                    <span style="font-size: smaller; color: red">（請摘要說明事故發生人、事、時、地、物，並於檔案上傳事件副知環保署初報(3日)及捷報(14日)傳真畫面，僅限毒性化學物質事故災害。）</span><br />
                                    <asp:TextBox ID="txtDisaster" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                    <br />
                                    <span class="CB">(限600字以內)</span>
                                    <br />
                                    <asp:LinkButton ID="lbtnDisUpload" runat="server" Text="檔案上傳" />
                                </div>
                            </div>
                        </li>
                        <li>GPS系統之運送車輛異常名單勾稽查核，異常名單查核率達60%或無異常名單者，最高得3分。<span style="color: red;">
                            <br />
                            <%--（資料更新日期：109年12月20日）</span>--%>
                            <div class="divInput">
                                異常名單查核率：
                                 <asp:Label ID="lbGPS" runat="server" CssClass="m-value CB" Text="-" />
                                %
                            </div>
                        </li>
                        <li>運送車輛勾稽異常抽驗（起運地或迄運地毒性化學物質之車輛查核為主）作業，查核1次0.5分，最高得2分。
                            <br />
                            <%--<span style="color: red;">（資料更新日期：109年12月20日）</span>--%>
                            <div class="divInput">
                                查核次數：
                                 <asp:Label ID="lbTransport" runat="server" CssClass="m-value CB" Text="-" />
                                次<asp:Label ID="lbTransportRemark" runat="server"></asp:Label>
                            </div>
                        </li>
                    </ol>
                </td>
                <td class="Item">
                    <ol>

                        <li>評分標準第1項以辦理會議性質及場次數計（自辦1場2分， 參與1場1分），第2、4項以達成比例計算；第4項含平時對轄內運作廠場臨場輔導工作事項。</li>
                        <br />
                        <li>第2項為辦理縣市毒災或其他轄內相關災防機關間通聯測試者（含通聯資料異動上網更新），如：廠場聯絡資料或政府防救聯絡資料有異動，應分別至「毒性化學物質管理系統」及「毒災防救管理資訊系統」更新。</li>
                        <br />
                        <li>第3項報知專線係指各縣市指定於所屬環保局網頁之通訊號碼，本署將不定期測試接通情形及受話者是否清楚專線之目的。</li>
                        <br />
                        <li>評分標準第5項以辦理場次數計，配合本署辦理演練得5分；配合行政院相關演練或縣市跨局處演練得3分；與業者合辦演練得2分；線上模擬演練1場得1分。第6項另環境事故（非列管場所）請求本署支援出勤案件未到場查處或處理不佳者，每件扣1分，得予以扣分至0分；如當年度無事故，則第6項不計分，第5項以8分計。</li>
                        <br />
                        <li>若該縣市無毒化物運作廠場，則第2、4項不計分，第1項以9分計；第6項不計分，第5項以8分計。</li>
                        <br />
                        <li>轄內無列管GPS 運送車輛或廠商則第7、8項不計分，該項併入1、2、4項計分；勾稽查核無異常名單者或未自行規劃辦理專案性查核，第8項不計分，該項併入1、2、4項計分。欲執行第8項其中之起運地或迄運地抽驗，應首先考量安全性，必須於安全無虞下執行。</li>

                    </ol>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 1.5em;">3</td>
                <td class="subItem">
                    <div class="TitleDiv">
                        具食安風險疑慮化學物質源頭管理<br />
                        <br />
                    </div>
                </td>
                <td class="AlignC">10 %</td>
                <td class="AlignC">
                    <asp:Label ID="lbscG3" runat="server" CssClass="m-value CB" Text="-" />
                </td>
                <td class="Item">
                    <ol>
                        <li>輔導訪查（6%）：訪查對象為化工原（材）料販售業者、相關化工產業。<span style="color: red;"></span>
                            <ol class="bli">
                                <li>清查提報110年訪查名單得1分。
                                    <div class="divInput">
                                        109 年訪查名單：
                                        <asp:Label ID="lbReqList" runat="server" CssClass="m-value CB" Text="-" />
                                        家
                                    </div>
                                </li>
                                <li>110年訪查名單輔導訪查或複查，依完成率給分（4捨5入至小數第1位），最高4分。
                                    <div class="divInput">
                                        完成率：
                                         <asp:Label ID="lbReqInspection" runat="server" CssClass="m-value CB" Text="-" />
                                        %
                                    </div>
                                </li>
                                <li>新增業者訪查，達成110年訪查名單5%或新增10家以上得1分，以下依比率給分。
                                    <div class="divInput">
                                        新增業者訪查：
                                        <asp:Label ID="lbNewInspection" runat="server" CssClass="m-value CB" Text="-" />
                                        家
                                    </div>
                                </li>
                            </ol>
                        </li>
                        <li>配合通知辦理其他食安專案輔導訪查（3%）：依完成率給分（4 捨 5 入至小數第 1位），最高 3分。
                            <div class="divInput">
                                完成率：
                                <asp:Label ID="lbProjectInspection" runat="server" CssClass="m-value CB" Text="-" />
                                %
                            </div>
                        </li>
                        <li>訪查成果回傳：對於具食安風險疑慮化學物質，地方環保局於每月25日前自行將前一個月具食安風險化學物質訪（複）查完整紀錄表填報於「毒性化學物質行動稽查輔助系統」；每正確上傳一次得分 0.2 分，本項最高得 1 分。
                            <div class="divInput">
                                正確上傳共：
                                <asp:Label ID="lbUpHighRisk" runat="server" CssClass="m-value CB" Text="-" />
                                次
                            </div>
                        </li>
                    </ol>
                </td>
                <td class="Item">
                    <ol>
                        <li>考量縣市產業特性，本工作項目分數調整方式如下：
                            <ol style="list-style: square; padding-left: 1rem;">
                                <li>110年無運作業者可提報，第 (1)(2)項分數併入「2. 配合通知辦理其他食安專案輔導訪查」調整為 8 分；新增訪查業者，依完成率給分（4 捨 5 入至小數第 1位）。</li>
                                <li>110年無運作業者可提報且無新增訪查業者，第(1)(2)(3)項分數併入「2. 配合通知辦理其他食安專案輔導訪查」調整為9分。</li>
                            </ol>
                        </li>
                        <br />
                        <li><u>1.(2)</u>目標訪查名單係由地方環保局清查 109年名單並提報，經本局確認之名單。</li>
                        <br />
                        <li><u>2.</u>全年未接獲食安專案縣市，分數得均分調整至<u>1.</u>輔導訪查(1)(2)(3)工作項目。</li>
                    </ol>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 1.5em;">4</td>
                <td class="subItem">
                    <div class="TitleDiv">行政作業完整度</div>
                </td>
                <td class="AlignC">13%</td>
                <td class="AlignC">
                    <asp:Label ID="lbscG4" runat="server" CssClass="m-value CB" Text="-" />
                </td>
                <td class="Item">
                    <ol>
                        <li>完成「檢舉違反毒性及關注化學物質管理法案件獎勵辦法」發布(5%)，已完成發布5分，未完成發布0分。<br />
                            <div class="divInput">
                                <asp:CheckBox ID="cbPublished" runat="server" />
                                已發布<br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtPublish" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span>
                            </div>
                        </li>
                        <li>辦理毒性及關注化學物質管理法規、政策宣導說明會或具食安風險疑慮化學物質源頭管理法規、政策宣導說明會：辦理前2場次各得分3分，辦理第3場得分2分，本項最高得分8分(8%)。<br />
                            <div class="divInput">
                                已辦理
                                <asp:LinkButton ID="lbtnPolicyConvention" CssClass="m-value" runat="server" Text="0" />
                                場次。
                                <span class="CR">（請直接點擊連結進行場次的輸入）</span><br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtPolicyConvention" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span>
                            </div>
                        </li>
                        <%--<li>辦理環境用藥管理法規、政策宣導說明會、記者會或印製文宣、發布新聞、刊登廣告等(7%)
                            <ol class="bli">
                                <li>辦理2種以上不同型態之宣導、推廣（7 分）。</li>
                                <li>僅辦理 1 種型態之宣導、推廣(4 分)。</li>
                                <li>皆未辦理(0 分)。</li>
                            </ol>
                            <div class="divInput">
                                分數：<asp:TextBox ID="txtADKinds" CssClass="txtBasic" runat="server" />
                            </div>
                        </li>--%>
                    </ol>
                </td>
                <td class="Item">
                    <ol>
                        <li><u>2.</u>若轄內無毒性化學物質運作廠場，得以具食安風險疑慮化學物質源頭管理相關法規及政策宣導說明會取代。</li>
                        <br />
                        <li><u>3.(1)</u>「辦理 2 種以上不同型態之宣導、推廣」
                            <ol style="list-style: square; padding-left: 1rem;">
                                <li>係 指 分 別 辦 理 不同 主 題 之 宣 導 活動，例如：環境用藥安全宣導、除草劑 安 全 合 理 使 用宣導、病媒防治業施 藥 人 員 訓 練 及環 境 用 藥 相 關 法規宣導。</li>
                                <li>包 含 臉 書 政 策 宣導。</li>
                            </ol>
                        </li>
                        <%--<li>
                            <u>3.(1)</u>辦理 2 種以上不同型態之宣導、推廣
                            <ol style="list-style: disc; padding-left: 1rem;">
                                <li>係指分別辦理不同主題之宣導活動，例如：環境用藥安全宣導、除草劑安全合理使用宣導、病媒防治業施藥人員訓練及環境用藥相關法規宣導。</li>
                                <li>包含臉書政策宣導。</li>
                                <li>辦理 2 種（含）以上不同型態之宣導。</li>
                            </ol>
                        </li>--%>
                    </ol>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 0.75em;">創新作為(10%)</td>
                <td style="padding-left: 1.5em;">5</td>
                <td class="subItem">
                    <div class="TitleDiv">地方創新作為</div>
                </td>
                <td class="AlignC">10%</td>
                <td class="AlignC">
                    <asp:Label ID="lbscG5" runat="server" CssClass="m-value CB" Text="-" />
                </td>
                <td class="Item">
                    <ol>
                        <li>降低毒物及關注化學物質運作風險、環境用藥管理、食安第一環源頭管理等創新作為。<br />
                            <div class="divInput">
                                辦理
                            <asp:LinkButton ID="lbtnCreative" CssClass="m-value" runat="server" Text="0" />
                                場次。
                            <span class="CR">（請直接點擊連結進行場次的輸入）</span><br />
                                <span class="CG">◎執行效益及特色（請摘要敘述）</span><br />
                                <asp:TextBox ID="txtCreativeMemo" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                <br />
                                <span class="CB">(限600字以內)</span>
                            </div>
                        </li>
                        <li>配合本署推派人員參與專業訓練、演練，最高得1分。
                            <div class="divInput">
                                推派
                            <asp:LinkButton ID="lbtnTrain" CssClass="m-value" runat="server" Text="0" />
                                人
                            <span class="CR">（請直接點擊連結進行人員的輸入）</span>
                            </div>

                        </li>
                        <br />
                        <li>推動毒性化學物質危害預防及應變計畫書上網並更新，最高得分3分。

                            <div class="divInput">
                                <asp:CheckBox ID="cbPre1" runat="server" Text="定期檢視更新毒性化學物質危害預防及應變計畫書網頁" />
                                <%--<asp:LinkButton ID="lbtnPreventionUpload1" runat="server" Text="檔案上傳" />--%>
                                <br />
                                <asp:CheckBox ID="cbPre2" runat="server" Text="公開第三類毒性化學物質危害預防及應變計畫書於網頁摘要達50%列管廠商數" />
                                <%--<asp:LinkButton ID="lbtnPreventionUpload2" runat="server" Text="檔案上傳" />--%>
                                <br />
                                <asp:CheckBox ID="cbPre3" runat="server" Text="將第三類毒性化學物質危害預防及應變計畫書完整公開達50%列管廠商數" />
                                <%--<asp:LinkButton ID="lbtnPreventionUpload3" runat="server" Text="檔案上傳" />--%>
                                <br />
                                <div runat="server" id="div1">
                                    <span class="CG">◎執行效益及特色（請摘要敘述）</span>
                                    <asp:TextBox ID="txtPrevention" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                    <br />
                                    <span class="CB">(限600字以內)</span>
                                    <br />
                                </div>
                            </div>

                        </li>
                        <br />
                        <li>公布非農地環境雜草管理條例或訂定草案預告或其他具體替代方案，最高得1分。

                            <div class="divInput">
                                <asp:CheckBox ID="cbMR1" runat="server" Text="公布非農地環境雜草管理條例" />

                                <%--<asp:LinkButton ID="lbtnManageRrgs1" runat="server" Text="檔案上傳" />--%>
                                <br />
                                <asp:CheckBox ID="cbMR2" runat="server" Text="訂定草案預告或其他具體替代方案" />

                                <%--<asp:LinkButton ID="lbtnManageRrgs2" runat="server" Text="檔案上傳" />--%>
                                <br />
                                <div runat="server" id="div2">
                                    <span class="CG">◎執行效益及特色（請摘要敘述）</span>
                                    <asp:TextBox ID="TextBox1" CssClass="txtMulti" runat="server" TextMode="MultiLine" />
                                    <br />
                                    <span class="CB">(限600字以內)</span>
                                    <br />
                                </div>
                            </div>

                        </li>
                    </ol>
                </td>
                <td class="Item">
                    <ol>

                        <li>請地方環保局自提證明文件，災害預防如辦理細胞廣播演練、疏難避難宣導等。</li>
                        <br />
                        <li>派員參加本署辦理專業訓練得0.7分，參加人員為機關正副首長則可再得0.3分。</li>
                        <br />
                        <li>定期檢視更新毒性化學物質危害預防及應變計畫書網頁得1分；公開第三類毒性化學物質危害預防及應變計畫書於網頁摘要達50%列管廠商數得1分；將第三類毒性化學物質危害預防及應變計畫書完整公開達50%列管廠商數得1分。</li>
                        <br />
                        <li>公布非農地環境雜草管理條例得1分/訂定草案預告或其他具體替代方案得0.8分。</li>

                    </ol>
                </td>
            </tr>
        </table>
        <%--        <div style="margin-top: 10px">
            <asp:Button CssClass="btnOk" runat="server" ID="btnSubmint" OnClientClick="return confirm('確定送出!?')" OnClick="btnSubmint_Click" />
            <asp:Button CssClass="btnClear" runat="server" ID="btnClear" OnClientClick="return confirm('確定清除!?')" OnClick="btnClear_Click" />
        </div>--%>
    </div>
    <div id="dialog" title="" style="display: none">
        <iframe src="about:blank" width="100%" height="100%" scrolling="yes" frameborder="0" id="InfoFrame"></iframe>
    </div>
    <script type="text/javascript">
        $.fx.speeds._default = 1000;
        function ShowFrame(_o, _s) {
            $("#InfoFrame").attr("src", _o);
            $("#dialog").dialog({ height: 720, width: 960, modal: true, title: _s, resizable: false });
            return false;
        }
        function FrameCancel() {
            $("#dialog").dialog("close");
        }
        $(document).ready(function () {
            $('#<%=cbNoDisaster.ClientID%>').change(function () {
                DisHappen(this.checked);
            });
            if (<%=Epa%>) $("[name=divEPA]").show();
            else $("[name=divEPA]").hide();
        });
        function DisHappen(_b) {
            if (_b) $('#<%= divDisHappen.ClientID %>').hide();
            else $('#<%= divDisHappen.ClientID %>').show();

        }

    </script>
</asp:Content>
