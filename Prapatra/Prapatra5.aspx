<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Prapatra5.aspx.cs" Inherits="Prapatra_Prapatra5" %>

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
                        प्रपत्र-5 अपील के प्रकरणों की जानकारी 
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <table>

                                    <tr>
                                        <th colspan="9">
                                            <h3>प्रपत्र-5 अपील के प्रकरणों की जानकारी </h3>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th colspan="9">
                                            <h4>WA/RP/SLP की जानकारी</h4>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th rowspan="2">स०क्र०
                                        </th>
                                        <th colspan="3">याचिका क्रमांक एवं वर्ष 
                                        </th>
                                        <th rowspan="2">वादी का नाम
                                        </th>
                                        <th rowspan="2">मूल प्रकरण क्र. एवं वर्ष
                                        </th>
                                        <th rowspan="2">यदि अवमानना प्रकरण है तो उसका क्रमांक एवं वर्ष 
                                        </th>
                                        <th rowspan="2">विषय 
                                        </th>
                                        <th rowspan="2">रिमार्क</th>
                                    </tr>
                                    <tr>
                                        <th>WA</th>
                                        <th>RP</th>
                                        <th>SLP</th>
                                    </tr>

                                    <tr>
                                        <th>1
                                        </th>
                                        <th>2
                                        </th>
                                        <th>3
                                        </th>
                                        <th>4
                                        </th>
                                        <th>5
                                        </th>
                                        <th>6
                                        </th>
                                        <th>7
                                        </th>
                                        <th>8
                                        </th>
                                        <th>9
                                        </th>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>

                                    <tr>
                                        <th colspan="9" style="text-align: left">नोट- मूल प्रकरण मे दिये न्यायालय के आदेश का पालन किस स्तर से होना है का उल्लेख करे। </th>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <fieldset>
                            <legend>WA/RP/SLP Case Report</legend>
                            <p class="Statics">प्रपत्र-5 अपील के प्रकरणों की जानकारी</p>
                            <p class="Statics">WA/RP/SLP की जानकारी</p>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdPrapartaFive" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="सं.कं">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="कुल" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="प्रत्यावर्तन प्रस्तुत" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="प्रत्यावर्तन शेष" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="कुल" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="पालन प्रतिवेदन" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="शेष" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="कुल" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="पालन" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="पालन हेतु शेष" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="WA" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="RP" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="SLP" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="जिला संभाग" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="शासन/संचानालय स्तर" DataField="Petitoner_Name" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>



            <!-- Default box -->



            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

