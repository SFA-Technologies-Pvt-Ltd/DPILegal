<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="HearingList_ForOperator.aspx.cs" Inherits="mis_Legal_HearingList_ForOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    
    <div class="content-wrapper">

        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div id="loader"></div>
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Case List:&nbsp;&nbsp;</h3>
                    <asp:Label ID="LblMsg" runat="server" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
                    <%--<div class="box-tools">
                        <div class="input-group input-group-sm" style="width: 150px;">
                            <asp:TextBox type="text" name="table_search" runat="server" class="form-control pull-right" placeholder="Search"></asp:TextBox>

                            <div class="input-group-btn">
                                <button type="submit" class="btn btn-default"><i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>--%>
                </div>

                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                        <div class="form-group">
                            <label>Select Office (Supervision By)</label><span class="text-danger">*</span>
                            <asp:DropDownList runat="server" ID="ddloffice" CssClass="form-control select2" ClientIDMode="static" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged" AutoPostBack="true">   
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                            </asp:DropDownList>
                            <small><span id="valddloffice" style="color: red;"></span></small>
                        </div>
                    </div>
                    </div>
                </div>
                <div class="box-body">
                    
                    <div class="row">
                        <div class="col-md-12" id="mydiv" runat="server">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" DataKeyNames="Case_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Case Status" ItemStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:Label ID="CaseStatus" Text='<%# Eval("Case_Status") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:BoundField DataField="Case_PetitionerAppName" HeaderText="Petitioner Name" />
                                    <asp:BoundField DataField="Hearing_Date" HeaderText="Hearing Date" />
                                    <asp:BoundField DataField="Case_No" HeaderText="Case No" />
                                    <asp:BoundField DataField="Case_OldRefNo" HeaderText="Old Cas Ref No" />
                                    <asp:BoundField DataField="Case_ClaimAmount" HeaderText="Claim Amount" />
                                    <asp:BoundField DataField="Case_Description" HeaderText="Subject Of Case" />
                                    <asp:BoundField DataField="Case_CourtType" HeaderText="Court Type" />
                                    <asp:TemplateField HeaderText="View More Detail" ShowHeader="False">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="LblViewMore" runat="server" CssClass="label label-default">View More</asp:Label>--%>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="View More"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
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

                        // javascript: print(),

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

