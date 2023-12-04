<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Library.aspx.cs" Inherits="_Library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                      Case Law Detail
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView Width="100%" ID="grdCaseLibrary" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover ">
                                <RowStyle HorizontalAlign="Center" />
                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CaseNo" HeaderText="Case No." HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="CaseSubject" HeaderText="Case Subject" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="CaseType_Name" HeaderText="Case Type" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PartyName" HeaderText="Petitioner Name" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="RespondentName" HeaderText="Respondent Name" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="RelatedOffice" HeaderText="Related Office" HeaderStyle-HorizontalAlign="Center" />
                                    <%--<asp:BoundField DataField="DecisionDate" HeaderText="Decision Date" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                    <asp:TemplateField HeaderText="Decision Date">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("DecisionDate")).ToString("dd/MM/yyyy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View">
                                        <ItemTemplate>
                                            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%#Eval("PDFViewLink") %>"><i class="fa fa-eye"></i></a></asp:HyperLink>--%>
                                            <a href="<%#Eval("PDFViewLink") %>" target="_blank"><i class="fa fa-eye"></i></a>
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
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

