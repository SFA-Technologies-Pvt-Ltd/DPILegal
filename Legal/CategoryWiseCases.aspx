<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="CategoryWiseCases.aspx.cs" Inherits="mis_Legal_CategoryWiseCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Category Wise Case Report</h3>
                            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Case Category</label>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" class="form-control select2">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="Civil Case">Civil Case</asp:ListItem>
                                            <asp:ListItem Value="Consumer Case">Consumer Case</asp:ListItem>
                                            <asp:ListItem Value="Criminal Case">Criminal Case</asp:ListItem>
                                            <asp:ListItem Value="Income tax Case">Income tax Case</asp:ListItem>
                                            <asp:ListItem Value="GST Case">GST Case</asp:ListItem>
                                            <asp:ListItem Value="Service Master">Service Master</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlCaseType" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Status</label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control select2">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                            <asp:ListItem Value="Close">Close</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-success btn-block" style="margin-top: 6px;" OnClick="btnSearch_Click"/>
                                </div>
                            </div>
                        </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" DataKeyNames="Case_ID"  AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="true">
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
                                    <asp:BoundField DataField="Hearing_Date" HeaderText="Hearing Date" />
                                    <asp:BoundField DataField="Case_No" HeaderText="Case No" />
                                    <asp:BoundField DataField="Case_OldRefNo" HeaderText="Case_OldRefNo" />
                                    <asp:BoundField DataField="Case_ClaimAmount" HeaderText="Claim Amount" />
                                    <asp:BoundField DataField="Case_Description" HeaderText="Subject Of Case" />
                                    <asp:BoundField DataField="Case_CourtType" HeaderText="Court Type" />
                                     <asp:BoundField DataField="Case_Type" HeaderText="Case Type" />
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
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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
                    title: $('h1').text(),
                    exportOptions: {

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
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
       <%-- function validateform() {
            var msg = "";
            $("#valddlCaseType").html("");
            if (document.getElementById('<%=ddlCaseType.ClientID%>').selectedIndex == 0) {
                msg += "Select Case Type."
                $("#valddlCaseType").html("Select Case Type ");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }--%>
    </script>
</asp:Content>