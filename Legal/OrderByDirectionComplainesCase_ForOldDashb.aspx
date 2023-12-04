<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="OrderByDirectionComplainesCase_ForOldDashb.aspx.cs" Inherits="Legal_OrderByDirectionComplainesCase_ForOldDashb" %>

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
    <div class="content-wrapper">
        <section class="content-header">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div style="display: inline;" class="float-left">
                            <span style="color: #e5e5e5">ORDER BY DIRECTION COMPLAINES CASE</span>
                        </div>
                        <div class="float-right">
                            <a href="OldCaseDashBoard.aspx" class="btn label label-danger">Back</a>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Order By Direction Complaines Case list</legend>
                        <div class="row">
                            <div class="col-lg col-md col-sm">
                                <div class="">

                                    <asp:GridView ID="grvDirectionComplaines" runat="server" CssClass="datatable table-responsive" Width="100%"
                                        AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Filing No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFillingNo" runat="server" Text='<%# Eval("FilingNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Court" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCourt" runat="server" Text='<%# Eval("Court") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Petitioner" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPetitioner" runat="server" Text='<%# Eval("Petitioner") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Respondent" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRespondent" runat="server" Text='<%# Eval("Respondent") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Subject" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField HeaderText="HOD Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhodname" runat="server" Text='<%# Eval("HodName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Complaince Status" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("IsComplaince") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Case Status" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaseStatus" runat="server" Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("CaseStatus") %>'></asp:Label>
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

