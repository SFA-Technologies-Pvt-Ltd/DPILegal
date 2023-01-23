<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="~/UploadOLDHCCases.aspx.cs" Inherits="UploadOLDHCCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function alertMessage() {
            alert('No Record Found');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                                Running Cases Details 
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court</label>
                                                <%--<span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Case No" Text="<i class='fa fa-exclamation-circle' title='Enter Case No'></i>"
                                                        ControlToValidate="ddlCourt" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Type</label><%--<span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Case No" Text="<i class='fa fa-exclamation-circle' title='Enter Case Type'></i>"
                                                        ControlToValidate="ddlCaseType" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case No</label><%--<span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Enter Case No" Text="<i class='fa fa-exclamation-circle' title='Enter Case No'></i>"
                                                        ControlToValidate="ddlCaseNo" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Year</label><%--<span style="color: red;"> *</span>--%>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                        ErrorMessage="Enter Year" Text="<i class='fa fa-exclamation-circle' title='Enter Year'></i>"
                                                        ControlToValidate="ddlYear" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>

                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label CssClass="text-red" Text="Pls click on clear button before applying any filter" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <%-- <div class ="col-md-4"></div>--%>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="btnSearch_Click1" CssClass="btn btn-primary btn-block" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <a href="UploadOLDHCCases.aspx" class="btn btn-secondary btn-block">Clear </a>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Detail</legend>
                                    <div class="row">
                                        <div class="col-md-8 pos-right">
                                            Page Size :
                                            <asp:DropDownList ID="ddlPageSize" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4 pos-left">
                                            <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">

                                            <asp:GridView ID="grdExcel" runat="server" AutoGenerateColumns="false"
                                                OnDataBound="grdExcel_DataBound"
                                                OnPreRender="grdExcel_PreRender"
                                                CssClass="table" Width="100%"
                                                OnPageIndexChanging="grdExcel_PageIndexChanging"
                                                AllowPaging="true" PageSize="100">
                                                <RowStyle HorizontalAlign="Center" Font-Size="12" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Font-Size="50" />
                                                <Columns>
                                                    <%--    <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:BoundField DataField="FilingNo" HeaderText="Filing No" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Court" HeaderText="Court" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Petitioner" HeaderText="Petitioner" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Respondent" HeaderText="Respondent" ItemStyle-HorizontalAlign="Left" />
                                                    <%--<asp:BoundField DataField="P_R_No" HeaderText="P_R_No"  ItemStyle-HorizontalAlign="Left"/>--%>
                                                    <%--<asp:BoundField DataField="Party_Name" HeaderText="Party_Name"  ItemStyle-HorizontalAlign="Left"/>--%>
                                                    <%--<asp:BoundField DataField="Address" HeaderText="Address"  ItemStyle-HorizontalAlign="Left"/>
                    <asp:BoundField DataField="Department" HeaderText="Department"  ItemStyle-HorizontalAlign="Left"/>--%>
                                                    <%--  <asp:boundfield datafield="status" headertext="status"  itemstyle-horizontalalign="left"/>--%>
                                                    <asp:BoundField DataField="pdf" HeaderText="document name" ItemStyle-HorizontalAlign="left" />
                                                    <asp:TemplateField HeaderText="Document" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <a href="<%#Eval("pdflink") %>" target="_blank"><i class="fa fa-eye"></i></a>
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
</asp:Content>

