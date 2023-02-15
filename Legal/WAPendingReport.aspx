<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="WAPendingReport.aspx.cs" Inherits="Legal_WAPendingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <style>
        label
        {
            font-size:15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div class="float-left" style="display: inline;">
                            Pending WA/RP Report
                        </div>
                        <div class="float-right" style="display: inline;">
                            <asp:LinkButton  ID="btnBackToDiv" runat="server" CssClass="btn-sm label label-default" OnClick="btnBackToDiv_Click" Text="Back"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body">
                        <fieldset id="FieldControl" runat="server">
                            <legend>Search Case List</legend>

                            <div class="row" >
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            From Date<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromdate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            प्रारंभिक तिथि</label>
                                        <asp:TextBox ID="txtFromdate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            To Date<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvEndDate" ValidationGroup="Save"
                                                ErrorMessage="Enter End Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtTodate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            अंतिम तिथि</label>
                                        <asp:TextBox ID="txtTodate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-end-date="0d" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 " style="padding: 5.5% 0 0 0">
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                        <div class="col-md-5">
                                            <a href="WAPendingReport.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset id="FieldAllRecordGrid" runat="server">
                            <legend>Report</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdWAPendingReport" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="WACase_ID" OnRowCommand="GrdWAPendingReport_RowCommand" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblCaseId" runat="server" Text='<%# Eval("WACase_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("WACaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseYear" runat="server" Text='<%# Eval("WACaseYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("WAPetitionerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCasesubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseStatus" runat="server" Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="BtnViewDtl" runat="server" CommandArgument='<%# Eval("WACase_ID") %>' ToolTip="View" CommandName="ViewDetail"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                         <div class="" id="ViewAllDtl" runat="server" visible="false">
                            <fieldset id="ViewCaseDtl" runat="server" visible="false">
                                <legend>Case Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="">
                                            <asp:DetailsView ID="dtlCaseDetail" runat="server" AutoGenerateRows="false" CssClass="table" BorderWidth="2px">
                                                <Fields>
                                                    <asp:BoundField HeaderText="Refrence WP Case No." DataField="WPCaseNo" />
                                                    <asp:BoundField HeaderText="WA Case No." DataField="WACaseNo" />
                                                    <asp:BoundField HeaderText="Case Year" DataField="WACaseYear" />
                                                    <asp:BoundField HeaderText="Petitioner Name" DataField="WAPetitionerName" />
                                                    <asp:BoundField HeaderText="Office Type" DataField="OfficeType_Name" />
                                                    <asp:BoundField HeaderText="Office Name" DataField="OfficeName" />
                                                    <asp:BoundField HeaderText="Nodal Officer Name" DataField="WANodalOfficer_Name" />
                                                    <asp:BoundField HeaderText="Nodal Officer Mobile No." DataField="WANodalOfficer_MobileNo" />
                                                    <asp:BoundField HeaderText="OIC Name" DataField="WAOICName" />
                                                    <asp:BoundField HeaderText="OIC Mobile No." DataField="WAOICMobileNo" />
                                                    <asp:BoundField HeaderText="Advocate Name" DataField="WAAdvocateName" />
                                                    <asp:BoundField HeaderText="Advocate Mobile No." DataField="WAAdvocateMobileNo" />
                                                    <asp:BoundField HeaderText="Case Subject" DataField="CaseSubject" />
                                                    <asp:BoundField HeaderText="Case Details" DataField="WACaseDetail" />
                                                    <asp:TemplateField HeaderText="Case Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseStatus" runat="server" Font-Bold="true" ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Responder Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdResponderDtl" runat="server" AutoGenerateColumns="false" CssClass="table">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Responder Name" DataField="ResponderName" />
                                                    <asp:BoundField HeaderText="Responder No." DataField="ResponderNo" />
                                                    <asp:BoundField HeaderText="Departent" DataField="ResponderDepartMent" />
                                                    <asp:BoundField HeaderText="Address" DataField="ResponderAddress" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset id="FieldHearingDtl" runat="server" visible="false">
                                <legend>Hearing Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdHearingDtl" runat="server" AutoGenerateColumns="false" CssClass="table">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Hearing Date" DataField="HearingDate" />
                                                    <asp:BoundField HeaderText="Hearing Detail" DataField="HearingDtl" />
                                                    <asp:TemplateField HeaderText="Hearing Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hplnkHearingDocument" runat="server" NavigateUrl='<%# "../Legal/WACaseHearingDoc/" + Eval("HearingDoc") %>' Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

