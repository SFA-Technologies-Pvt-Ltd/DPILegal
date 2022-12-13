<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="mis_Legal_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Head Office Report</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Division</label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control select2" SelectionMode="Multiple" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office</label>
                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Court Type</label>
                            <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2" >
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Consumer Court">Consumer Court</asp:ListItem>
                                            <asp:ListItem Value="Labour Court">Labour Court</asp:ListItem>
                                            <asp:ListItem Value="District Court">District Court</asp:ListItem>
                                            <asp:ListItem Value="High Court - Jabalpur">High Court - Jabalpur</asp:ListItem>
                                            <asp:ListItem Value="High Court -  Indore">High Court -  Indore</asp:ListItem>
                                            <asp:ListItem Value="High Court - Gwalior">High Court - Gwalior</asp:ListItem>
                                            <asp:ListItem Value="Supreme Court">Supreme Court</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                        </div>
                       <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" style="margin-top:26px;" OnClick="btnSearch_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            <asp:Label ID="lblOfficeID" Text='<%# Eval("Office_ID").ToString()%>' runat="server" Visible="false" />
                                        </ItemTemplate>

                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                    <asp:TemplateField HeaderText="Civil Cases">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lnkCivilCases" CssClass="label label-info" runat="server"  Text='<%# Eval("CivilCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="CivilCase"></asp:LinkButton> 
                                            <%-- <asp:HyperLink ID="lnkCivilCases" CssClass="label label-info" runat="server"  Text='<%# Eval("CivilCase") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("CivilCase")+ "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>'  Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consumer Cases">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkconsumercases" CssClass="label label-info" runat="server"  Text='<%# Eval("ConsumerCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="ConsumerCase"></asp:LinkButton> 
                                         <%-- <asp:HyperLink ID="lnkconsumercases" CssClass="label label-info" runat="server"  Text='<%# Eval("ConsumerCase") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("ConsumerCase") + "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>'  Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Criminal Cases">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkcriminalcases" CssClass="label label-info" runat="server"  Text='<%# Eval("CriminalCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="CriminalCase"></asp:LinkButton> 
                                            <%--<asp:HyperLink ID="lnkcriminalcases" CssClass="label label-info" runat="server"  Text='<%# Eval("CriminalCase") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("CriminalCase") + "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>' Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Income Tax Cases">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkinctxcases" CssClass="label label-info" runat="server"  Text='<%# Eval("IncometaxCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="IncometaxCase"></asp:LinkButton> 
                                            <%--<asp:HyperLink ID="lnkinctxcases" CssClass="label label-info" runat="server" Text='<%# Eval("IncometaxCase") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("IncometaxCase") + "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>' Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST Cases">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkGSTcases" CssClass="label label-info" runat="server"  Text='<%# Eval("GSTCase") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="GSTCase"></asp:LinkButton> 
                                            <%--<asp:HyperLink ID="lnkGSTcases" CssClass="label label-info" runat="server"  Text='<%# Eval("GSTCase") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("GSTCase") + "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>' Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Service Master">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkServiceMaster" CssClass="label label-info" runat="server"  Text='<%# Eval("ServiceMaster") %>' CommandArgument='<%# Container.DataItemIndex %>' CommandName="ServiceMaster"></asp:LinkButton> 
                                            <%--<asp:HyperLink ID="lnkServiceMaster" CssClass="label label-info" runat="server"  Text='<%# Eval("ServiceMaster") %>' NavigateUrl='<%# "CaseDetailReport.aspx?OfficeID=" + APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) + "&casetype=" +APIProcedure.Client_Encrypt("ServiceMaster") + "&courttype=" +APIProcedure.Client_Encrypt(ddlCourtType.SelectedItem.Value)%>' Target="_blank"></asp:HyperLink> --%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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

                        columns: [0, 1, 2, 3, 4, 5,6,7,8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5,6,7,8]
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

