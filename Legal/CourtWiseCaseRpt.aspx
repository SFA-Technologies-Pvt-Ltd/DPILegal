<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CourtWiseCaseRpt.aspx.cs" Inherits="Legal_CourtWiseCaseRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        ​ .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        ​ .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            transform: translate3d(0px, 0px, 0px) !important;
            padding: 2px !important;
            max-height: 18em !important;
            overflow-y: auto !important;
            /*#ec8712*/
        }

        ​ .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ​ .form-controlSearchBox {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            /*border-radius: 0.25rem;*/
            box-shadow: inset 0 0 0 transparent;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        ​ .multiselect-native-select button {
            border-radius: 0.5em; 
            border-color: #579fbb !important;
        }

        ​ .dropdown-item {
            display: block !important;
            width: 100% !important;
            padding: 0.25rem !important;
            clear: both !important;
            font-weight: 400 !important;
            color: #212529 !important;
            text-align: inherit !important;
            white-space: nowrap !important;
            background-color: transparent;
            border: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">View Case Detail</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Case Detail</legend>
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
                                <%-- <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Next Hearing Date</label>
                                        <asp:TextBox ID="txtNextHearingDate" runat="server" CssClass="form-control" AutoComplete="off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>--%>
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
                                        <label>Justice Name</label>
                                        <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Name</label>
                                        <asp:TextBox ID="txtOICMObile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Mobile No.</label>
                                        <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
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
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="Close" data-dismiss="modal" Style="margin-top: 20px; width: 60px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:ValidationSummary ID="vds" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Court Wise Case Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Court Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Select Court Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCourtType" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:ListBox ID="ddlCourtType" runat="server" ClientIDMode="Static" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                            ErrorMessage="Select Case Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding: 3.5% 0 0 0">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-4">
                                            <a href="CourtWiseCaseRpt.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdSubjectWiseCasedtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="grdSubjectWiseCasedtl_RowCommand" DataKeyNames="Case_ID" EmptyDataText="NO RECORD FOUND" AllowPaging="true" OnPageIndexChanging="grdSubjectWiseCasedtl_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("FilingNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        <asp:Label ID="LabelOICName" runat="server" Text='<%# Eval("petiAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICMObile" runat="server" Text='<%# Eval("petiAdvocateMobile") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICEmail" runat="server" Text='<%# Eval("PetiAdvocateEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalName" runat="server" Text='<%# Eval("NodalOfficer_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalMobile" runat="server" Text='<%# Eval("NodalOfficerMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalEmail" runat="server" Text='<%# Eval("NodalOfficerEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateName" runat="server" Text='<%# Eval("DeptAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateMobile" runat="server" Text='<%# Eval("DeptAdvocateMobileNO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateEmail" runat="server" Text='<%# Eval("DeptAdvocateEmailId") %>' Visible="false"></asp:Label>
                                                        <%-- <asp:Label ID="LabelHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>' Visible="false"></asp:Label>--%>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("RespondertypeName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("Casetype_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("Respondent_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRespondentMobileNo" runat="server" Text='<%# Eval("RespondentNo") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitoner_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Court Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtTypeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server"  CommandName="ViewDtl" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
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
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $('[id*=ddlCourtType]').multiselect({
            includeSelectAllOption: true,
            buttonWidth: '100%',
        });
    </script>
</asp:Content>

