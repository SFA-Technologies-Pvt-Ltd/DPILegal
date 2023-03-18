<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="ViewWPPendingCaseDetail.aspx.cs" Inherits="Legal_ViewWPPendingCaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
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
                        View Case Detail
                        <span class="float-right">
                            <%--<asp:LinkButton ID="lbtBack" runat="server" CssClass="btn btn-danger" Text="Back" Font-Size="Small" OnClick=""></asp:LinkButton>--%>
                            <asp:LinkButton ID="lbkBack" runat="server" CssClass="btn-sm label-danger" OnClick="lbkBack_Click">Back</asp:LinkButton>
                        </span>
                    </div>
                    <div class="card-body">
                        <fieldset id="FieldSet_CaseDetail" runat="server" visible="true">
                            <legend>Case Details</legend>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Case No.</label>
                                        <asp:TextBox ID="lblCaseNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Case Year</label>
                                        <asp:TextBox ID="txtCaseYear" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Court Name</label>
                                        <asp:TextBox ID="txtCourtType" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Court Location</label>
                                        <asp:TextBox ID="txtCourtLocation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Party Name</label>
                                        <asp:TextBox ID="txtParty" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Case Type</label>
                                        <asp:TextBox ID="txtCasetype" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                        <asp:TextBox ID="txtCaseSubject" runat="server" CssClass="form-control" ReadOnly="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Case Sub Subject</label>
                                        <asp:TextBox ID="txtCaseSubSubject" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Oic Name</label>
                                        <asp:TextBox ID="txtOicName" runat="server" CssClass="form-control" ReadOnly="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Oic Mobile Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:TextBox ID="txtOicMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Oic Email-ID</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:TextBox ID="txtOicEmailId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="div1" runat="server">
                                    <div class="form-group">
                                        <label>OIC Order Date<span style="color: red;"> *</span></label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter OIC Order Date" Text="<i class='fa fa-exclamation-circle' title='Enter OIC Order Date'></i>"
                                            ControlToValidate="txtOICDate" ForeColor="Red" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOICDate" runat="server" ReadOnly="true"  placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>High Priority Case</label>
                                        <asp:TextBox ID="txtHighprioritycase" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Case Detail/Remark</label>
                                        <asp:TextBox ID="txtCaseDetail" runat="server" TextMode="MultiLine" ReadOnly="true" onkeyup="javascript:capFirst(this);" CssClass="form-control" AutoComplete="off" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" runat="server" id="dvCasestatus" visible="true">
                                    <div class="form-group">
                                        <label>Case Status</label>
                                        <asp:TextBox runat="server" ID="txtCaseStatus" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" id="dvCaseDisposalType" visible="false">
                                    <div class="form-group">
                                        <label>Case Disposal Type</label>
                                        <asp:TextBox runat="server" ID="txtcasedisposaltype" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" id="Compilance_Div" visible="false">
                                    <div class="form-group">
                                        <label>Compliance Status</label>
                                        <asp:TextBox runat="server" ID="txtComplianceStatus" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" runat="server" id="dvOrderSummary" visible="false">
                                    <div class="form-group">
                                        <label>Order Summary</label>
                                        <asp:TextBox ID="txtOrderSummary" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Petitioner Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdPetitioner" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Name" DataField="PetitionerName" />
                                                <asp:BoundField HeaderText="Designation" DataField="Designation_Name" />
                                                <asp:BoundField HeaderText="Mobile No." DataField="PetitionerMobileNo" />
                                                <asp:BoundField HeaderText="Address" DataField="PetitionerAddress" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Advocate Details</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Petitioner Advocate Detail</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GrdPetiAdv" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Name" DataField="PetiAdv_Name" />
                                                            <asp:BoundField HeaderText="Mobile No." DataField="PetiAdv_MobileNo" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Department Advocate Detail</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GrdDeptAdv" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Name" DataField="DeptAdvName" />
                                                            <asp:BoundField HeaderText="Mobile No." DataField="DeptAdvMobileNo" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Respondent Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdResponderDtl" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Office type" DataField="OfficeType_Name" />
                                                <asp:BoundField HeaderText="Office Name" DataField="OfficeName" />
                                                <asp:BoundField HeaderText="Responder Name" DataField="RespondentName" />
                                                <asp:BoundField HeaderText="Designation" DataField="Designation_Name" />
                                                <asp:BoundField HeaderText="Mobile No." DataField="RespondentMobileNo" />
                                                <asp:BoundField HeaderText="Departent" DataField="Department" />
                                                <asp:BoundField HeaderText="Address" DataField="Address" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="row">
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Hearing Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdHearingDtl" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Hearing Date" DataField="NextDate" />
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkHearingDoc" runat="server" Target="_blank" CssClass="fa fa-eye" Enabled='<%# Eval("HearingDoc").ToString() == "" ? false:true %>' NavigateUrl='<%# "../Legal/HearingDoc/" + Eval("HearingDoc") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Document Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdDocument" runat="server" CssClass="table" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" OnRowDataBound="GrdDocument_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Document Name" DataField="Doc_Name" />
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkDocPath" runat="server" Visible="false" Target="_blank" Enabled='<%# Eval("Doc_Path").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" +  Eval("Doc_Path") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                                <asp:Label ID="lbldoc" runat="server" Text='<%# Eval("Doc_Path") %>' Visible="false"></asp:Label>
                                                                <asp:HyperLink ID="hyperLink" runat="server" Visible="false" Target="_blank" Enabled='<%# Eval("Doc_Path").ToString() == "" ? false : true %>' NavigateUrl='<%# Eval("Doc_Path") %>' CssClass="fa fa-eye"></asp:HyperLink>
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

