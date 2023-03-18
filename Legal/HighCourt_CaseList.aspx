<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="HighCourt_CaseList.aspx.cs" Inherits="Legal_HighCourt_CaseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Case Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Case And Petitioner Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case No.</label>
                                        <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control" Text='<%# Eval("CaseNo") %>' AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Petitioner Name</label>
                                        <asp:TextBox ID="txtPetitionerName" runat="server" CssClass="form-control" Text='<%# Eval("Petitoner_Name") %>' AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>District Name</label>
                                        <asp:DropDownList ID="ddlDistrictName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Transfered To Designation</label>
                                        <asp:DropDownList ID="ddlTransferOfficeName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Responder Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">                                       
                                        <asp:GridView ID="GrdCaseDoc_Details" runat="server" CssClass="table table-bordered table-hover text-center" AutoGenerateColumns="false" AllowPaging="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />सरल क्र." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responder Name<br />प्रतिउत्तर का नाम">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtResponderName" runat="server" Text='<%# Eval("Respondent_Name") %>' CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Responder No.<br />प्रतिउत्तर का नंबर">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtResponderNo" runat="server" CssClass="form-control" Text='<%# Eval("RespondentNo") %>' AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department Name<br />डिपार्टमेंट का नाम">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDepartment" runat="server" Text='<%# Eval("Department") %>' CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address<br />पता">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Text='<%# Eval("responderAddress") %>' AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <legend>Case Document</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">                                      
                                        <asp:GridView ID="GrdDocCase" runat="server" AutoGenerateColumns="false" AllowPaging="true" CssClass="table table-bordered text-center table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />सरल क्र." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documents Name<br />दस्तावेज का नाम">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDocName" runat="server" CssClass="form-control" Text='<%# Eval("Doc_Name") %>' MaxLength="70" AutoComplete="off"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documents Upload<br />दस्तावेज अपलोड">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="fileUploadDoc1" runat="server" CssClass="form-control" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Case Report
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Case Report</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Date<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            दिनांक</label>
                                        <asp:TextBox ID="txtDate" runat="server" data-date-end-date="0d" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3" style="padding-top: 5.5%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSearch_Click1" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="CaseReport.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="GrdCaseDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Case_ID" CssClass="table table-bordered table-hover text-center" OnRowCommand="GrdCaseDetails_RowCommand" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />सरल क्र.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case No.<br />मुक़दमा क्र.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Petitioner Name<br />आवेदक का नाम">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitoner_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status<br />स्थिति">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("CaseStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action<br />गतिविधि">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="EditView" ToolTip="Edit/View" CssClass="btn btn-primary"><i class="fa fa-edit"</asp:LinkButton>
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

