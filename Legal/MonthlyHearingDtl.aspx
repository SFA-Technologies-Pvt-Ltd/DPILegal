﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="MonthlyHearingDtl.aspx.cs" Inherits="Legal_MonthlyHearingDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Responder Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Respondent</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case No.</label>
                                        <asp:TextBox ID="txtCaseno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Petitioner Name</label>
                                        <asp:TextBox ID="txtPetitionerName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                        <asp:TextBox ID="txtCasesubject" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Type</label>
                                        <asp:TextBox ID="txtRespondertype" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Court Name</label>
                                        <asp:TextBox ID="txtCourtName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Next Hearing Date</label>
                                        <asp:TextBox ID="txtNextHearingDate" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Case Type</label>
                                        <asp:TextBox ID="txtCasetype" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Name</label>
                                        <asp:TextBox ID="txtRespondentName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <asp:TextBox ID="txtRespondentMobileno" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Email-ID</label>
                                        <asp:TextBox ID="txtRespondentEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Name</label>
                                        <asp:TextBox ID="txtNodalName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Mobile No.</label>
                                        <asp:TextBox ID="txtNodalMobile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Email-ID</label>
                                        <asp:TextBox ID="txtNodalEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Mobile No.</label>
                                        <asp:TextBox ID="txtOICMObile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Email-ID</label>
                                        <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Name</label>
                                        <asp:TextBox ID="txtAdvocatename" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Mobile No</label>
                                        <asp:TextBox ID="txtAdvocatemobile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Email-Id</label>
                                        <asp:TextBox ID="txtAdvocateEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Case Detail</label>
                                        <asp:TextBox ID="txtCaseDtl" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <%-- <asp:Button runat="server" CssClass="btn btn-success" Text="OK" ID="btnYes" Style="margin-top: 20px; width: 80px;" />--%>
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="Close" data-dismiss="modal" Style="margin-top: 20px; width: 60px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Monthly Hearing Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month</label><span style="color: red;"><b> *</b></span>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year</label><span style="color: red;"><b> *</b></span>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RadioButtonList ID="rbWPCOnt" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                                            <asp:ListItem Value="1">&nbsp;WP/Contempt &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;WA/RP</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top: 3%;">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdMonthlyHearingdtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grdMonthlyHearingdtl_RowCommand" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        <asp:Label ID="LabelOICName" runat="server" Text='<%# Eval("OICName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICMObile" runat="server" Text='<%# Eval("OICMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICEmail" runat="server" Text='<%# Eval("OICEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalName" runat="server" Text='<%# Eval("NodalName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalMobile" runat="server" Text='<%# Eval("NodalMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalEmail" runat="server" Text='<%# Eval("NodalEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateName" runat="server" Text='<%# Eval("AdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateMobile" runat="server" Text='<%# Eval("AdvocateMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateEmail" runat="server" Text='<%# Eval("AdvocateEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("Respondertype") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("CaseType") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Court Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-primary" CommandName="ViewDtl" CommandArgument='<%# Eval("ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
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

