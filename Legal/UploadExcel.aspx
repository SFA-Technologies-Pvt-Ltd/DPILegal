<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="UploadExcel.aspx.cs" Inherits="Legal_UploadExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
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
    </style>
    <style>
        label {
            font-size: 15px;
        }
    </style>
    <script>
        function MessageAlert(arr) {
            alert("shobhit " + arr);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
                            <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnCancel" ValidationGroup="no" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="No" Style="margin-top: 20px; width: 50px;" />
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
                            </div>
                            <div class="card-body">
                                <div class="row" runat="server" id="divNote">
                                    <div class="col-md-7">
                                        <div class="form-group">
                                            <span style="color: green;"><b>NOTE:- AS this is a sensitive data please confirm the credentials to proceed</b></span>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <span style="color: red;"><b>NOTE: Excel Template to upload cases</b></span>
                                    </div>
                                    <div class="col-md-1" style="left: 86%; position: fixed; display: inline;">
                                        <a href="../Legal/Template.xlsx" class="fa fa-download">Download</a>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="divPassword">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" MaxLength="10" Placeholder="Password" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnPassword" CssClass="btn btn-info btn-block" Text="Check" OnClick="btnPassword_Click" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnReset" CssClass="btn btn-danger btn-block" Text="Reset" OnClick="btnReset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>

                                </div>
                                <div class="row" id="divFill" runat="server">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Upload Excel</label>
                                                    <asp:FileUpload ID="FUExcel" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FUExcel" runat="server" ForeColor="Red"
                                                        Display="Dynamic" ValidationGroup="Chk">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-md-3 mt-4">
                                                <div class="row">
                                                    <div class="col-md-6 mt-2">
                                                        <asp:Button ID="btnCheck" CssClass="btn btn-block btn-success" Text="Check" runat="server" ValidationGroup="Chk" OnClick="btnCheck_Click" />
                                                    </div>
                                                    <div class="col-md-6 mt-2">
                                                        <a href="UploadExcel.aspx" class="btn btn-block btn-secondary">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <fieldset id="Field_ExistRecord" runat="server" visible="false">
                                    <legend>Existing Record</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GrdExistRecord" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" DataKeyNames="UniqueNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <asp:Label ID="lblUniqNo" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filling No." HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFilling" runat="server" Text='<%# Eval("FilingNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No." HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Year" HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseyear" runat="server" Text='<%# Eval("CaseYear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Court Name" HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("CourtName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case type" HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("CasetypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc" HeaderStyle-BackColor="#cc380b" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDocCount" runat="server" CommandName="ViewCount" CommandArgument='<%# Eval("UniqueNo") %>'><%# Eval("pdfCount") %></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Delete" HeaderStyle-BackColor="#cc380b">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRecord" CommandArgument='<%# Eval("UniqueNo") %>' ToolTip="Delete" CssClass="fa fa-trash" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset id="Field_NewRecord" runat="server" visible="false">
                                    <legend>New Record</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdNewRecord" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Filling No." HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFilling" runat="server" Text='<%# Eval("NewFilingNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case No." HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCaseNo" runat="server" Text='<%# Eval("NewCaseNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case Year" HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCaseyear" runat="server" Text='<%# Eval("NewCaseYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Court Name" HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("NewCourtName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case type" HeaderStyle-BackColor="#37bf56">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("NewCasetypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="row">
                                    <div class="col-md-12" id="Div_CaseCount" runat="server" visible="false">
                                        <span style="color: green;"><b>NOTE: Total "
                                            <asp:Label ID="lblCaseCount" runat="server"></asp:Label>
                                            " Cases Going to upload on System.</b></span>
                                    </div>
                                    <div class="col-md-1 mt-1">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Visible="false" Text="Save" OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure you want to Save records?');" />
                                    </div>
                                    <div class="col-md-1 mt-1" id="DivCancelUpload" runat="server" visible="false">
                                        <a href="UploadExcel.aspx" class="btn btn-danger">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script>
        <%-- function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                debugger;
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }--%>
    </script>
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

