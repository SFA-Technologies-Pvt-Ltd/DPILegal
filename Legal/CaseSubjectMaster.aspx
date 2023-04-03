<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseSubjectMaster.aspx.cs" Inherits="Legal_CaseSubjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        .btn-danger {
            background-color: #cf7b83;
        }

        .btn-info {
            background-color: #548ac5;
        }

        .btn-primary {
            background-color: #548ac5;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp;
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
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
                                                    <asp:RequiredFieldValidator ID="Rfv2" ValidationGroup="Save"
                                                        ErrorMessage="Enter Case Subject Code" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject Code'></i>"
                                                        ControlToValidate="txtCaseSubjectCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubjectCode" placeholder="Enter Case Subject Code" onkeypress="return NumberOnly();" MaxLength="5" autocomplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RevCaseSubject" ForeColor="Red" Display="Dynamic" ErrorMessage="Only Number Allow."
                                                    SetFocusOnError="true" ControlToValidate="txtCaseSubjectCode" ValidationExpression="^[0-9]+?$" ValidationGroup="Save">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Case Subject<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                                ErrorMessage="Enter Case Subject" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject'></i>"
                                                                ControlToValidate="txtCaseSubject" ForeColor="Red" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubject" placeholder="Enter Case Subject" MaxLength="300" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Case Subject Detail<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="Tfv3" ValidationGroup="Save"
                                                                ErrorMessage="Enter Case Subject Detail" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject Detail'></i>"
                                                                ControlToValidate="txtCaseSubjectDetail" ForeColor="Red" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubjectDetail" placeholder="Enter Case Subject Detail" MaxLength="400" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <%-- <div class ="col-md-4"></div>--%>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
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
                                                CssClass="table table-bordered table-hover datatable" OnPageIndexChanging="grdCaseSubject_PageIndexChanging">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnCaseSubjectID" runat="server" Value='<%#Eval("CaseSubjectID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Code" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectCode" runat="server" Text='<%# Eval("CaseSubjectCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Detail" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectDetail" runat="server" Text='<%# Eval("CaseSubjectDetail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditDetails" CommandArgument='<%# Eval("CaseSubjectID") %>' ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("CaseSubjectID") %>' ToolTip="Delete" CssClass="" OnClientClick="return confirm('Are you sure you want to delete this record?');"><i class="fa fa-trash"></i></asp:LinkButton>
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
    <%--<script src="../DataTable_CssJs/jquery.js"></script>--%>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../datatable_cssjs/datatables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        function NumberOnly() { //only Numeric required.
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }

        function capFirst(cpt) { //only Capital First.
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }

        function chcode() { // Only English or Hindi Required
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }
    </script>
    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
    <script>
        function Comrfirm() {
            "Are You Sure Delete Record"
            return true;
        }
    </script>
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

