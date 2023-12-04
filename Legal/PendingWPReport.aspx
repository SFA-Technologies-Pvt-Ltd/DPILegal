<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="PendingWPReport.aspx.cs" Inherits="mis_Legal_PendingWPReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
    <style>
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
            &:after

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

        .aspNetDisabled {
            pointer-events: none;
            opacity: 0.6;
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
                        Pending Case Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search Case List</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Court Name <span style="color: red;"><b>*</b></span>
                                            <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                                ErrorMessage="Select Court Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCourtName" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />
                                        </label>
                                        <asp:DropDownList ID="ddlCourtName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourtName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>District</label><%--<span style="color: red;"> *</span>--%>
                                        <span class="pull-right">
                                          <%--  <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="Save"
                                                ErrorMessage="Select District" Text="<i class='fa fa-exclamation-circle' title='Select Division'></i>"
                                                ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
                                        </span>
                                        <%--<asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2"></asp:DropDownList>--%>
                                        <asp:ListBox runat="server" ID="ddlDistrict" CssClass="form-control" SelectionMode="Multiple" ClientIDMode="Static"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Case type<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator><br />
                                        </label>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Case No.
                                        </label>
                                        <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case Year
                                        </label>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                              <%--  <div class="col-md-3">
                                    <div class="form-group">
                                        <label>High Priority Case</label>
                                        <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PolicyMatter</label>
                                        <asp:DropDownList ID="ddlPolicyMeter" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            From Date<%--<span style="color: red;"><b> *</b></span>--%>
                                            <%--       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />--%>
                                        </label>
                                        <asp:TextBox ID="txtFromDate" runat="server" date-provide="datepicker" AutoComplete="off" data-date-end-date="0d" placeholder="DD/MM/YYYY" class="form-control DateAdd" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            To Date<%--<span style="color: red;"><b> *</b></span>--%>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                ErrorMessage="Enter To Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txttodate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />--%>
                                        </label>
                                        <asp:TextBox ID="txttodate" runat="server" date-provide="datepicker" AutoComplete="off" data-date-end-date="0d" placeholder="DD/MM/YYYY" class="form-control DateAdd" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top: 2rem">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-5">
                                            <a href="PendingWPReport.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GrdPendingReport" runat="server" CssClass="datatable table table-bordered" EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="false" DataKeyNames="Case_ID" OnRowCommand="GrdPendingReport_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblCaseId" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
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
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Respondent Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HOD Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhod" runat="server" Text='<%# Eval("HodName1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseStatus" runat="server" Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Case Subject">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Next Hearing Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNextHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return File/Compliance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReplyCompliance" runat="server" Text='<%# Eval("ActionYesOrNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Action expected From">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActionAppealedFrom" runat="server" Text='<%# Eval("ActionAppealedFrom") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="File Movement">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHodName" runat="server" Text='<%# Eval("FileMoment") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="OIC/Nodal Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOicName" runat="server" Text='<%# Eval("OICName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="High Priority Case Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHighPriorityCase" runat="server" Text='<%# Eval("HighPriorityCase_Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PolicyMatter Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPolicyMatter" runat="server" Text='<%# Eval("PolicyMeterStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnView" runat="server" CommandName="ViewDetail" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>No record Found</EmptyDataTemplate>
                                    </asp:GridView>

                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:HiddenField ID="hdnDistrict_Id" runat="server" />
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
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <%--<script type="text/javascript">
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
                    title: 'Pending Case Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
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
                    title: 'Pending Case Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: 'Pending Case Report',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
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
    <script type="text/javascript">
        $('[id*=ddlDistrict]').multiselect({
            includeSelectAllOption: true,
            includeSelectAllOption: true,
            buttonWidth: '100%'
        });
    </script>
</asp:Content>
