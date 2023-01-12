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
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Petitioner Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="">
                                        <asp:DetailsView ID="grdPetitionerDtl" runat="server" AutoGenerateRows="false" CssClass="table" BorderWidth="2px">
                                            <Fields>

                                                <asp:BoundField HeaderText="Case NO." DataField="Caseno" />
                                                <asp:BoundField HeaderText="Petitioner Name" DataField="Petitoner_Name" />
                                                <asp:BoundField HeaderText="Court Type" DataField="CourtTypeName" />
                                                <asp:BoundField HeaderText="Office Type" DataField="OfficeType_Name" />
                                                <asp:BoundField HeaderText="Office Name" DataField="OfficeName" />
                                                <asp:BoundField HeaderText="WP Case No" DataField="WPCaseNo" />
                                               <asp:BoundField HeaderText="Updated Status" DataField="CurrentOfficeStatus" />
                                                <asp:BoundField HeaderText="Nodal Officer Name" DataField="NodalOfficer_Name" />
                                                <asp:BoundField HeaderText="Nodal Officer Mobile No." DataField="NodalOfficerMobileNo" />
                                                <asp:BoundField HeaderText="Petitioner Advocate Name" DataField="petiAdvocateName" />
                                                <asp:BoundField HeaderText="Petitioner Advocate Mobile No." DataField="petiAdvocateMobile" />
                                                <asp:BoundField HeaderText="Petitioner Advocate Email-Id" DataField="PetiAdvocateEmailID" />
                                                <asp:BoundField HeaderText="Case Subject" DataField="CaseSubject" />
                                                <asp:BoundField HeaderText="Case Details" DataField="CaseDetail" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("CaseStatus") %>' ForeColor='<%# Eval("CaseStatus").ToString() == "Pending" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' Font-Bold="true"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Responder Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdResponderDtl" runat="server" CssClass="table" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Responder Name" DataField="Respondent_Name" />
                                                <asp:BoundField HeaderText="Responder No." DataField="RespondentNo" />
                                                <asp:BoundField HeaderText="Departent" DataField="Department" />
                                                 <asp:BoundField HeaderText="OIC Name" DataField="OICNAME" />
                                                <asp:BoundField HeaderText="OIC Mobile No." DataField="OICMobileNO" />
                                                <asp:BoundField HeaderText="OIC Email-ID" DataField="OICEailID" />
                                                <asp:BoundField HeaderText="Address" DataField="ResponderAddress" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset id="FieldHearingDtl" runat="server" visible="false">
                            <legend>Hearing Detail</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdHearingDtl" runat="server" CssClass="table" AutoGenerateColumns="false">
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
                                                        <asp:HyperLink ID="lnkHearingDoc" runat="server" NavigateUrl='<%# "../Legal/HearingDoc/" +  Eval("HearingDoc") %>'></asp:HyperLink>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

