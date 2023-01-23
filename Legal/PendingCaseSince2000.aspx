<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="PendingCaseSince2000.aspx.cs" Inherits="Legal_PendingCaseSince2000" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ScriptManager runat="server" id="ScriptManager1"/>
    <asp:UpdatePanel runat="server" ID="UP1">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content-header">
                    <div class="container-fluid">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <a href="OldCaseDashBoard.aspx" title="<<Back" class="btn btn-link"></a>
                        <div class="card">
                            <div class="card-header" style="text-align: center;">
                                <span style="font-size: 18px; color: #e5e5e5" id="spnCaseType" runat="server"></span>
                            </div>
                            <div class="card-body" style="opacity: 1;">
                                <asp:GridView runat="server" ID="grdCaseTypeDetail" EmptyDataText="No Record Found" AutoGenerateColumns="false" 
                                    OnRowEditing="grdCaseTypeDetail_RowEditing" OnRowCommand="grdCaseTypeDetail_RowCommand"
                                    OnRowUpdating="grdCaseTypeDetail_RowUpdating" OnRowCancelingEdit="grdCaseTypeDetail_RowCancelingEdit" OnRowDataBound="grdCaseTypeDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FilingNo" HeaderText="Filing No" ReadOnly="true" />
                                        <asp:BoundField DataField="Court" HeaderText="Court" ReadOnly="true" />
                                        <asp:BoundField DataField="Petitioner" HeaderText="Petitioner" ReadOnly="true" />
                                        <asp:BoundField DataField="Respondent" HeaderText="Respondent" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="Respondent Office">
                                            <ItemTemplate>
                                                <asp:Label ID="txtRespondentOffice" Text='<%#Eval("RespondentOffice") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRespondentOffice" Text='<%#Eval("RespondentOffice") %>' runat="server"  />
                                                <asp:HiddenField ID="hdnUId" runat="server" Value='<%#Eval("UniqueNo") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OIC Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOICName" Text='<%#Eval("OICId") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlOICName" runat="server"  OnTextChanged="ddlOICName_TextChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OIC Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOICMobileNo" Text='<%#Eval("OICMobileNo") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtOICMobileNo" Text='<%#Eval("OICMobileNo") %>' runat="server" ReadOnly="true" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" Text='<%#Eval("Remarks") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%#Eval("Remarks") %>' runat="server"  />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Case Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCaseSubjectId" Text='<%#Eval("CaseSubjectId") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" >
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ShowHeader="true" />
                                        <%-- <asp:TemplateField HeaderText="Edit" >
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Edit" runat="server" CommandName="edit" CommandArgument='<%#Eval("UniqueNo") %>' />
                                         <asp:LinkButton Text="Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("UniqueNo") %>' />
                                         <asp:LinkButton Text="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%#Eval("UniqueNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

