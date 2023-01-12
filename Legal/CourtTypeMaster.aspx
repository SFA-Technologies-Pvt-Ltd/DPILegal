<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CourtTypeMaster.aspx.cs" Inherits="Legal_CourtTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
        <section class="content">
            <div class="container-fluid">
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                                Court Type Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Court Type" Text="<i class='fa fa-exclamation-circle' title='Enter Court Type'></i>"
                                                        ControlToValidate="txtCourtType" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCourtType" placeholder="Enter Case Type" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <%-- <div class ="col-md-4"></div>--%>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <a href="CourtTypeMaster.aspx" class="btn btn-secondary btn-block">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Detail</legend>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:GridView Width="100%" ID="grdCourtType" AutoGenerateColumns="false" runat="server"
                                                CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdCaseSubject_PageIndexChanging">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <%--<asp:HiddenField ID="hdnCaseSubjectID" runat="server" Value='<%#Eval("CaseSubjectID") %>' />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CourtTypeName" HeaderText="Court Type" HeaderStyle-HorizontalAlign="Center" />
                                                    <%--<asp:TemplateField HeaderText="IsActive">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActice" runat="server" OnCheckedChanged="chkActice_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>

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
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" Runat="Server">
</asp:Content>

