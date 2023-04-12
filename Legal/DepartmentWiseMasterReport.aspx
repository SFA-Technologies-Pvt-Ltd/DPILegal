<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DepartmentWiseMasterReport.aspx.cs" Inherits="Legal_DepartmentWiseMasterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Contempt Dispose Case Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Case Year<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Select Case Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Department<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="RfvDept" ValidationGroup="Save"
                                            ErrorMessage="Select Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDepartment" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>
                                            District<span style="color: red;"><b> *</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvDist" ValidationGroup="Save"
                                            ErrorMessage="Select District Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 mt-3">
                                    <div class="row">
                                        <div class="col-md-6 mt-3">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" Text="Export" OnClick="btnSearch_Click"/>
                                        </div>
                                        <div class="col-md-6 mt-3">
                                            <a href="DepartmentWiseMasterReport.aspx" class="btn btn-warning btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div id="fgf" runat="server"></div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

