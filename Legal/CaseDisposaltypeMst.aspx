<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseDisposaltypeMst.aspx.cs" Inherits="Legal_CaseDisposaltypeMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Case Disposal Master
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Enter Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Case Disposal Type</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                    ErrorMessage="Enter Case Disposal Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtDisposaltype" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtDisposaltype" runat="server" CssClass="form-control" MaxLength="80" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click"/>
                                        </div>
                                        <div class="col-md-3">
                                            <a href="CaseDisposaltypeMst.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDipose" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case Dispose Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisposetype" runat="server" Text='<%# Eval("CaseDisposeType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Active/Inactive">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Isactive") == "0" ? false : true %>' AutoPostBack="true"></asp:CheckBox>
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

