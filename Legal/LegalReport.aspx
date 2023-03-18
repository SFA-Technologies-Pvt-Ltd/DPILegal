<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LegalReport.aspx.cs" Inherits="mis_Legal_LegalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
    <style>
        .box {
            min-height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content-header">
         
        </section>

        <section class="content">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

            <div class="row">
                <%-- <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpList.aspx" target="_blank">
                            <span class="info-box-icon bg-aqua"><i class="fa fa-users" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">TOTAL Employee</span>
                                <asp:Label ID="lblTotalEmp" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpPresentList.aspx" target="_blank">
                            <span class="info-box-icon bg-green"><i class="fa fa-user-circle-o" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">PRESENT EMPLOYEE</span>
                                <asp:Label ID="lblTotalPresent" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpTodayOnLeave.aspx" target="_blank">
                            <span class="info-box-icon bg-yellow"><i class="fa fa-user" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">EMPLOYEE ON LEAVE</span>
                                <asp:Label ID="lblTotalOnLeave" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpAbsentList.aspx" target="_blank">
                            <span class="info-box-icon bg-red"><i class="fa fa-user" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">ABSENT EMPLOYEE</span>
                                <asp:Label ID="lblTotalAbsent" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>--%>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <i class="ion ion-clipboard"></i>
                            <h3 class="box-title">All Division/Region Wise Legal Report</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                            <%--<asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="True">--%>
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCommand="GridView1_RowCommand">
                                <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                            <asp:Label ID="lblOfficeID" Text='<%# Eval("Office_ID").ToString()%>' runat="server" Visible="false" />
                                            <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                        </ItemTemplate>

                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                    <asp:TemplateField HeaderText="Total Cases">
                                        <ItemTemplate>
                                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                            <%--<asp:HyperLink ID="lnktotalcases" runat="server" CssClass="label label-info" Text='<%# Eval("TotalCase") %>' NavigateUrl='<%# "AllLegalCasesDetail.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&myparam1=" +APIProcedure.Client_Encrypt("AllCase")%>' Target="_blank"></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnktotalcases" runat="server" CssClass="label label-info" Text='<%# Eval("TotalCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="AllCase"></asp:LinkButton>
                                            <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                            <%--<asp:LinkButton ID="lnktotalcase" runat="server" CssClass="label label-info" Text='<%# Eval("TotalCase") %>' ToolTip='<%# Eval("Office_ID") %>' OnClick="lnktotalcase_Click"></asp:LinkButton>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalCase") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Open Cases">
                                        <ItemTemplate>
                                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                            <%--<asp:HyperLink ID="lnkopencases" runat="server" CssClass="label label-info" Text='<%# Eval("OpenCase") %>' NavigateUrl='<%# "AllLegalCasesDetail.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&myparam1=" +APIProcedure.Client_Encrypt("OpenCase")%>' Target="_blank"></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnkopencases" runat="server" CssClass="label label-info" Text='<%# Eval("OpenCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="OpenCase"></asp:LinkButton>
                                            <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                            <%--<asp:LinkButton ID="lnkopencase" runat="server"  CssClass="label label-warning" Text='<%# Eval("OpenCase") %>' ToolTip='<%# Eval("Office_ID") %>' OnClick="lnkopencase_Click"></asp:LinkButton>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalCase") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="OpenCase" HeaderText="Open Cases" />--%>
                                    <asp:TemplateField HeaderText="Closed Cases">
                                        <ItemTemplate>
                                            <%--CODE CHANGES START BY CHINMAY ON 11-JUL-2019--%>
                                            <%--<asp:HyperLink ID="lnkclosedcases" runat="server" CssClass="label label-info" Text='<%# Eval("CloseCase") %>' NavigateUrl='<%# "AllLegalCasesDetail.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&myparam1=" +APIProcedure.Client_Encrypt("CloseCase")%>' Target="_blank"></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnkclosedcases" runat="server" CssClass="label label-info" Text='<%# Eval("CloseCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="CloseCase"></asp:LinkButton>
                                            <%--CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019--%>
                                            <%-- <asp:LinkButton ID="lnkclosecase" runat="server" CssClass="label label-success" Text='<%# Eval("CloseCase")%>' ToolTip='<%# Eval("Office_ID") %>' OnClick="lnkclosecase_Click"></asp:LinkButton>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalCase") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:BoundField DataField="CloseCase" HeaderText="Closed Cases" />--%>
                                </Columns>
                            </asp:GridView>
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

            "bSort": false,

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

                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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



