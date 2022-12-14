<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Prapatra3.aspx.cs" Inherits="Prapatra_Prapatra3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #dddddd;
            text-align: center;
            padding: 8px;
            font-weight: 200;
        }

        td {
            padding: 18px;
            width: 15px;
        }

        .Statics {
            border: 1.5px solid #dddddd;
            width: 100%;
            text-align: center;
            margin-bottom: 0px;
            background-color: #838583;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%-- <section class="content-header">
            <h1></h1>
        </section>--%>
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Prapatra - 3 WP Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>WP Case Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <p class="Statics">प्रपत्र -3 लंबित डव्ल्यू पी प्रकरण </p>
                                    <p class="Statics"><span style="text-align: center;">जिला....... &nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-align: center;">दिनांक....... की स्तिथि में</span></p>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdPrapatraThree" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="सं.कं">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="याचिका क्रमांक एवं वर्ष" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="आवेदक का नाम एवं पद" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="प्रकरण का विषय" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="प्रत्यावर्तन प्रस्तुत हाँ/नहीं" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="यदि हाँ तो पप्रत्यावर्तन प्रस्तुत करने की दिनांक" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="यदि नहीं तो कारण" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="ओआईसी का नाम तथा मो. नं." DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="मूल प्रकरण नीतिगत है अथवा नहीं" DataField="Petitoner_Name" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>



