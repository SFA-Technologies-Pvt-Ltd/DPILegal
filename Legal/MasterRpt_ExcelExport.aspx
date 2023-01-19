<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="MasterRpt_ExcelExport.aspx.cs" Inherits="Legal_MasterRpt_ExcelExport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .pt-4 {
            padding-top: 2rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Master Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>Year</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvYear" ValidationGroup="Save"
                                            ErrorMessage="Select Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlYear" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>Case Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCasetype" ValidationGroup="Save"
                                            ErrorMessage="Select Case Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="-1">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 pt-4">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" Text="Export" OnClick="btnSearch_Click" ValidationGroup="Save" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="MasterRpt_ExcelExport.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
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

