<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Prapatra1.aspx.cs" Inherits="Prapatra_Prapatra1" %>

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
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Prapatra - 1 Statistical Report
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">                               
                            </div>
                        </div>
                        <fieldset>
                            <legend>Case Detail</legend>

                            <p class="Statics">प्रपत्र - 1 सांख्यकीय</p>
                            <p class="Statics"><span style="text-align:center;">जिला....... &nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="text-align:center;">दिनांक....... की स्तिथि में</span></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdCaseCount" runat="server" CssClass=" table" CellSpacing="5" RowHeaderColumn="प्रपत्र-1" AutoGenerateColumns="false" OnRowCreated="GrdCaseCount_RowCreated">
                                            <HeaderStyle BackColor="#838583" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

