<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AllLegalCasesDetail.aspx.cs" Inherits="mis_Legal_AllLegalCasesDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title"><asp:Label ID="lblCase" runat="server" Text=""></asp:Label></h3>
                            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                    <%--<asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" DataKeyNames="Case_ID" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">--%>
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" DataKeyNames="Case_ID" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="GridView1_RowCommand">
                                        <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
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
                                                    <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                                    <%--<asp:HyperLink ID="lnkViewMore" runat="server" CssClass="label label-default" NavigateUrl='<%# "CaseDetail.aspx?Case_ID=" + APIProcedure.Client_Encrypt(Eval("Case_ID").ToString())%>' Target="_blank" Text="View More"></asp:HyperLink> --%>
                                                    <asp:LinkButton ID="lnkViewMore" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("Case_ID") %>' CommandName="View">View More</asp:LinkButton>
                                                    <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                                </ItemTemplate>

                                                <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="View More"></asp:LinkButton>--%>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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

                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
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
