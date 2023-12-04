<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="MonthlyHearingDtl.aspx.cs" Inherits="Legal_MonthlyHearingDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
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
            .sorting,
        .sorting_asc,
        .sorting_desc,
        .sorting_asc_disabled,
        .sorting_desc_disabled {
            cursor: pointer;
            position: relative;
            &:after;

        {
            position: absolute;
            bottom: 8px;
            right: 8px;
            display: block;
            font-family: 'Glyphicons Halflings';
            opacity: 0.5;
        }

        }

        .sorting:after {
            opacity: 0.2;
            content: "⏭" !important; /* sort */
        }

        .sorting_asc:after {
            content: "⏬" !important; /* sort-by-attributes */
        }

        .sorting_desc:after {
            content: "⏫" !important; /* sort-by-attributes-alt */
        }
    </style>
    <style>
        label {
            font-size: 15px;
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
                        Monthly Hearing Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Select Month Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlMonth" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control select2">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                            ErrorMessage="Select Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlYear" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                            ErrorMessage="Select Case Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <%--<asp:RadioButtonList ID="rbWPCOnt" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                                            <asp:ListItem Value="1">&nbsp;WP/Contempt &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;WA/RP</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                    </div>
                                </div>
                                    <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Court Name <span style="color: red;"><b>*</b></span>
                                            <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                                ErrorMessage="Select Court Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCourtName" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />
                                        </label>
                                        <asp:DropDownList ID="ddlCourtName" runat="server" CssClass="form-control select2" ></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 mt-4">
                                    <div class="row">
                                        <div class="col-md-6 mt-2">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-6 mt-2">
                                            <a href="MonthlyHearingDtl.aspx" class="btn btn-block btn-default">Clear</a>
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
                                        <asp:GridView ID="grdMonthlyHearingdtl" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" OnRowCommand="grdMonthlyHearingdtl_RowCommand" DataKeyNames="Case_ID" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitonerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Respondent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="District Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCasesubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Hearing Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Return File/Appeal">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReplyCompliance" runat="server" Text='<%# Eval("ActionYesOrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCasestatus" runat="server" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green  %>' Font-Bold="true" Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDtl" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
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
    <script src="../DataTable_CssJs/jquery.js"></script>
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
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7]
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
    </script>
</asp:Content>

