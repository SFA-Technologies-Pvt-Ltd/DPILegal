<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Dashboard_ViewCaseDetail.aspx.cs" Inherits="Legal_Dashboard_ViewCaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header">
                        View Case Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Case Detail</legend>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdCaseDetail" runat="server" CssClass="table table-bordered" EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Court Type" DataField="CourtTypeName" />
                                                <asp:BoundField HeaderText="Case No." DataField="CaseNo" />
                                                <asp:BoundField HeaderText="Petitioner Name" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="Case Subject" DataField="CaseSubject" />
                                                <asp:BoundField HeaderText="Case Detail" DataField="CaseDetail" />
                                                <asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseStatus" runat="server" Text='<%# Eval("CaseStatus") %>' ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green  %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
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

