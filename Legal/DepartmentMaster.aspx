<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DepartmentMaster.aspx.cs" Inherits="Legal_DepartmentMaster" %>

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
                        Department Master
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    Department Name<span style="color: red;"><b> *</b></span>
                                                    <asp:RequiredFieldValidator ID="Rfvdept" ValidationGroup="Save"
                                                        ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtDeptName" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator></label>
                                                <asp:TextBox ID="txtDeptName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                        </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSave_Click" />
                                                </div>
                                                <div class="col-md-6">
                                                    <a href="DepartmentMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Report</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrddeptMaster" runat="server" AutoGenerateColumns="false" DataKeyNames="Dept_ID" CssClass="table table-bordered text-center" OnRowCommand="GrddeptMaster_RowCommand" AllowPaging="true" OnPageIndexChanging="GrddeptMaster_PageIndexChanging" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.<br />स. क्र." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lbldeptID" runat="server" Text='<%# Eval("Dept_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeptName" runat="server" Text='<%# Eval("Dept_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action<br />गतिविधि" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Dept_ID") %>' CommandName="EditDtl" ToolTip="Edit" CssClass=""><i class="fa fa-edit"</asp:LinkButton>
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
                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

