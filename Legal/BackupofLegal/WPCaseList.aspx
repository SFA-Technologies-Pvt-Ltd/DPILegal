<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="WPCaseList.aspx.cs" Inherits="Legal_WPCaseList" %>

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
                        Case Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search Details</legend>
                            <div class="row">
                                <div class="col-md-3 col-sm" style="display: none;">
                                    <div class="form-group">
                                        <label>
                                            From Date</label>
                                        <%--  <span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtFromDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm" style="display: none;">
                                    <div class="form-group">
                                        <label>
                                            To Date</label>
                                        <%--  <span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvEndDate" ValidationGroup="Save"
                                                ErrorMessage="Enter End Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtEndDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" data-date-end-date="0d" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Court</label>
                                          <span style="color: red;"><b> *</b></span>                                        
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save" CssClass="fa fa-pull-right"
                                            ErrorMessage="Select Court Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCourt" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourt_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>Case type </label>
                                        <%--<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvcasetype" ValidationGroup="Save"
                                                ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case No.</label>
                                        <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>Case Status </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvcasetype" ValidationGroup="Save" CssClass="fa fa-pull-right"
                                            ErrorMessage="Select Case Status." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseStatus" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="2">Disposed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm mt-3">
                                    <div class="row">
                                        <div class="col-md-6  mt-3">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-6  mt-3">
                                            <a href="WPCaseList.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdCaseDetails" runat="server" EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="false" DataKeyNames="Case_ID" CssClass="datatable table table-bordered text-center" OnRowCommand="GrdCaseDetails_RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblUniqueNo" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFilingNo" runat="server" Text='<%# Eval("FilingNo") %>' Font-Bold='<%# Eval("CaseSubject_Id").ToString() != "" ? true : false %>' BackColor='<%# Eval("CaseSubject_Id").ToString() != "" ? System.Drawing.Color.LawnGreen : System.Drawing.Color.White  %>'>'></asp:Label>/                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Respondent Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("CaseStatus") %>' ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ?  System.Drawing.Color.Red :System.Drawing.Color.Green %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="EditView" ToolTip="Edit/View" CssClass=""><i class="fa fa-edit"</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                            <EmptyDataTemplate>No record found</EmptyDataTemplate>
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
    </script>
</asp:Content>

