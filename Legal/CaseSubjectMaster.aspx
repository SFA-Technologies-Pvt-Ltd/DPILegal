<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseSubjectMaster.aspx.cs" Inherits="Legal_CaseSubjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
       .btn-danger
        {
            background-color:#cf7b83;
        }
       .btn-info{
           background-color:#548ac5;
       }
       .btn-primary{
           background-color:#548ac5;
       }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
                                Case Subject Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject Code<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv2" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Subject Code" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject Code'></i>"
                                                        ControlToValidate="txtCaseSubjectCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubjectCode" placeholder="Enter Case Subject Code" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Subject" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject'></i>"
                                                        ControlToValidate="txtCaseSubject" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubject" placeholder="Enter Case Subject" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Case Subject Detail<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Tfv3" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Subject Detail" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject Detail'></i>"
                                                        ControlToValidate="txtCaseSubjectDetail" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubjectDetail" placeholder="Enter Case Subject Detail" autocomplete="off"></asp:TextBox>
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
                                                        <a href="CaseSubjectMaster.aspx" class="btn btn-default btn-block">Clear</a>
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
                                            <asp:GridView Width="100%" ID="grdCaseSubject" AutoGenerateColumns="false" runat="server" DataKeyNames="CaseSubjectID" OnRowCommand="grdCaseSubject_RowCommand"
                                                CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdCaseSubject_PageIndexChanging">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnCaseSubjectID" runat="server" Value='<%#Eval("CaseSubjectID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectCode" runat="server" Text='<%# Eval("CaseSubjectCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Detail">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectDetail" runat="server" Text='<%# Eval("CaseSubjectDetail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                 
                                                    <asp:TemplateField HeaderText="Active/Inactive">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActice" runat="server" OnCheckedChanged="chkActice_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Update">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditDetails" CommandArgument='<%# Eval("CaseSubjectID") %>' ToolTip="Edit" CssClass="btn btn-info"><i class="fa fa-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="DeleteDetails"  ToolTip="Delet" CssClass="btn btn-danger"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

