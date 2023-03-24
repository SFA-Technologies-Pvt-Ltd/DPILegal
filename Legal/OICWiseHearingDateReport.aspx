<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="OICWiseHearingDateReport.aspx.cs" Inherits="Legal_OICWiseHearingDateReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function AlertMessage() {
            alert("Please Select Court Type");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />

    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Hearing Detail
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Search Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing From Date</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Enter Hearing Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtHearingFromDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtHearingFromDate" runat="server" CssClass="form-control disableFuturedate" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing To Date</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                            ErrorMessage="Enter Hearing Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtHearingToDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtHearingToDate" runat="server" CssClass="form-control disableFuturedate" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Court Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                            ErrorMessage="Select Court Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCourtType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save"
                                            ErrorMessage="Select OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOICName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOICName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-4">
                                            <a href="OICWiseHearingDateReport.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdTodayHearingdtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                            EmptyDataText="NO RECORD FOUND" AllowPaging="true" PageSize="10">
                                            <RowStyle HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OICName" HeaderText="OIC Name" ItemStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="उक्त समय से लम्बित प्रकरण" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("OICWiseHearingDateShow.aspx?U={0}&F={1}&O={2}&C={3}&HF={4}&HT={5}",
                    Eval("UniqueNo1").ToString(),"1",Eval("OicId"),Eval("Court"),Eval("HearingFrom"),Eval("HearingTo")) %>'
                                                            Target="_blank"><%#Eval("Last30daysPendingCaseCount") %></asp:HyperLink>
                                                        <%--<asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("Last30daysPendingCaseCount") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="उक्त समय में प्राप्त प्रकरण" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("OICWiseHearingDateShow.aspx?U={0}&F={1}&O={2}&C={3}&HF={4}&HT={5}",
                    Eval("UniqueNo2").ToString(),"2",Eval("OicId"),Eval("Court"),Eval("HearingFrom"),Eval("HearingTo")) %>'
                                                            Target="_blank"><%#Eval("Last30daysCaseCount") %></asp:HyperLink>
                                                        <%--<asp:Label ID="lblCourtName" runat="server"  Text='<%# Eval("Last30daysCaseCount") %>' ></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="कुल निराकृत प्रकरण" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("OICWiseHearingDateShow.aspx?U={0}&F={1}&O={2}&C={3}&HF={4}&HT={5}",
                    Eval("UniqueNo3").ToString(),"3",Eval("OicId"),Eval("Court"),Eval("HearingFrom"),Eval("HearingTo")) %>'
                                                            Target="_blank"><%#Eval("SolvedCasesCount") %></asp:HyperLink>
                                                        <%--<asp:Label ID="lblCaseSubject" runat="server" Text='<%#Eval("SolvedCasesCount") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="कुल लम्बित प्रकरण" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("OICWiseHearingDateShow.aspx?U={0}&F={1}&O={2}&C={3}&HF={4}&HT={5}",
                    Eval("UniqueNo4").ToString(),"4",Eval("OicId"),Eval("Court"),Eval("HearingFrom"),Eval("HearingTo")) %>'
                                                            Target="_blank"><%#Eval("RemainingCasesCount") %></asp:HyperLink>
                                                        <%--<asp:Label ID="lblPetitionerName" runat="server"  Text='<%#Eval("RemainingCasesCount") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--   <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="ViewDtl" CommandArgument='<%# Eval("Case_ID") %>' ToolTip="View"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
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
</asp:Content>

