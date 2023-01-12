<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="New_pheLegalRepot.aspx.cs" Inherits="Legal_New_pheLegalRepot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        /*.datepicker tbody {
            background-color: #ecfce6 !important;
            color: black;
        }

        .datepicker th {
            background-color: #608640 !important;
        }*/

        .label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>--%>
    <style>
        .pt-5
        {
            padding-top:3.6rem!important
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
        <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
     <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">View Case Details</span>
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
                                        <label>Justice Name</label>
                                        <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Justice Mobile No.</label>
                                        <asp:TextBox ID="txtOICMObile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Justice Email-ID</label>
                                        <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%--  <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Justice Name</label>
                                        <asp:TextBox ID="txtAdvocatename" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Justice Mobile No</label>
                                        <asp:TextBox ID="txtAdvocatemobile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Justice Email-Id</label>
                                        <asp:TextBox ID="txtAdvocateEmailID" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Type</label>
                                        <asp:TextBox ID="txtOfficeType" runat="server" CssClass="form-control"></asp:TextBox>
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
                       Phe Legal Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label> From Date<br /> प्रारंभिक तिथि<span style="color: red;"><b> *</b></span></label>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                           
                                        <asp:TextBox ID="txtFromDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                         <label>
                                            To Date<br />अंतिम तिथि<span style="color: red;"><b> *</b></span></label>
                                            <asp:RequiredFieldValidator ID="RfvEndDate" ValidationGroup="Save"
                                                ErrorMessage="Enter End Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtEndDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                           
                                        <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" data-date-end-date="0d" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 pt-5">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="New_pheLegalRepot.aspx" class="btn btn-default btn-block">Clear</a>
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
                                      
                                        <asp:GridView ID="grdSubjectWiseCasedtl" runat="server" CssClass=" table table-bordered" AutoGenerateColumns="false" DataKeyNames="Case_ID" OnRowCommand="grdSubjectWiseCasedtl_RowCommand" AllowPaging="true" OnPageIndexChanging="grdSubjectWiseCasedtl_PageIndexChanging" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name" ItemStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("FilingNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitoner_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responder Name" ItemStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("Respondent_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Next Hearing Date (DD/MM/YYYY)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNextHearingdate" Text='<%# Eval ("NextHearingDate") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("Casetype_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Case Detail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="OIC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOicName" runat="server" Text='<%# Eval("OICNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responder Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("RespondertypeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               
                                                <asp:TemplateField HeaderText="Case Reg. Date (DD/MM/YYYY)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldateofCaseUpdt" runat="server" Text='<%# Eval("UpdatedDate") %>'></asp:Label>
                                                        <asp:Label ID="LabelOICName" runat="server" Text='<%# Eval("petiAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICMObile" runat="server" Text='<%# Eval("petiAdvocateMobile") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelOICEmail" runat="server" Text='<%# Eval("PetiAdvocateEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalName" runat="server" Text='<%# Eval("NodalOfficer_Name") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalMobile" runat="server" Text='<%# Eval("NodalOfficerMobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelNodalEmail" runat="server" Text='<%# Eval("NodalOfficerEmailID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateName" runat="server" Text='<%# Eval("DeptAdvocateName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateMobile" runat="server" Text='<%# Eval("DeptAdvocateMobileNO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelAdvocateEmail" runat="server" Text='<%# Eval("DeptAdvocateEmailId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblRespondentMobileNo" runat="server" Text='<%# Eval("RespondentNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseDetail" runat="server" Text='<%# Eval("CaseDetail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtTypeName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="LabelRespondertype" runat="server" Text='<%# Eval("RespondertypeName") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOfficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>' Visible="false"></asp:Label>
                                                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Court Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtTypeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-primary" CommandName="ViewDtl" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
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
    <%--  <script src="../DataTable_CssJs/jquery.js"></script>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            PageLength: 15,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>--%>
</asp:Content>

