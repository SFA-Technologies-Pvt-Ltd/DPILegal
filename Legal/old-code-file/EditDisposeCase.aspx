<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="EditDisposeCase.aspx.cs" Inherits="Legal_EditDisposeCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }

        hr {
            border: 1px solid #2095a1cc;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <asp:ValidationSummary ID="VDS2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Responder" />
    <asp:ValidationSummary ID="VDS3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDtl" />
    <asp:ValidationSummary ID="VDS4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDispose" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Petitioner" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="DeptADV" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Docs" />
    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Hearing" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="PetiAdv" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="OldCase" ShowMessageBox="true" ShowSummary="false" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp;
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnYes_Click" Style="margin-top: 20px; width: 50px;" />
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
                        Edit Case Detail
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%-- Start Here Bind Case && Petitioner Detail --%>
                                <fieldset id="FieldSet_CaseDetail" runat="server" visible="true">
                                    <legend>Case Info</legend>
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
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Name</label>
                                                <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Court Location</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvDistrict" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCourtLocation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCourtLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Party Name</label>
                                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Case Type</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCasetype" ValidationGroup="Save"
                                                    ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>
                                <%-- Start Here Case Dispose Detail --%>
                                <fieldset id="Fieldset_CaseDispose" runat="server" visible="true">
                                    <legend>Disposal Status</legend>
                                    <div class="row">
                                        <div class="col-md-2" runat="server" id="DisposalStatus">
                                            <div class="form-group">
                                                <%-- <label>
                                                    Case Disposal</label><span style="color: red;"><b> *</b></span><br />
                                                <asp:RadioButtonList ID="rdCaseDispose" runat="server" CssClass="rbl form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdCaseDispose_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="caseDisposeYes" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Disposal Type
                                                </label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvDisposeType" ValidationGroup="CaseDispose"
                                                    ErrorMessage="Select Case Disposal Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlDisponsType" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDisponsType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDisponsType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="CimplianceSt_Div" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Compliance Status
                                                </label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="CaseDispose" Enabled="false"
                                                    ErrorMessage="Select Compliance" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCompliaceSt" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCompliaceSt" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                    <asp:ListItem Value="3">Pending</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="OrderBy1" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Disposal Date
                                                </label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvCaseDisposeDate" ValidationGroup="CaseDispose"
                                                    ErrorMessage="Enter Case Dispose Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseDisposeDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCaseDisposeDate" runat="server" data-provide="datepicker" CssClass="form-control" AutoComplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" placeholder="DD/MM/YYYY"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3" id="DivOrderTimeline" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Compliance Timeline(In Days)
                                                </label>
                                                <asp:TextBox ID="txtOrderimpletimeline" runat="server" onkeypress="return NumberOnly();" AutoComplete="off" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" id="OrderSummary_Div" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Order Summary</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="CaseDispose"
                                                    ErrorMessage="Enter Order Summary" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtorderSummary" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtorderSummary" runat="server" MaxLength="500" AutoComplete="off" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator6" Display="Dynamic" ControlToValidate="txtorderSummary"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="CaseDispose" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4" id="OrderBy2" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Order Document
                                                </label>
                                                <asp:FileUpload ID="FielUpcaseDisposeOrderDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                <span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>
                                            </div>
                                        </div>
                                        <div class="col-md-1" id="HearingDtl_CaseDispose" runat="server" visible="false" style="padding-top: 0.5rem ! important;">
                                            <asp:Button ID="btnCaseDispose" runat="server" CssClass="btn btn-primary" ValidationGroup="CaseDispose" Text="Disposal" OnClick="btnCaseDispose_Click" />
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDispose" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center" OnRowCommand="GrdCaseDispose_RowCommand" DataKeyNames="Case_ID" AutoGenerateRows="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Disposal<br />Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisposalStatus" runat="server" Text='<%# Eval("CaseDisposal_Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Disposal<br />type" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisposaltype" runat="server" Text='<%# Eval("CaseDisposeType") %>'></asp:Label>
                                                                <asp:Label ID="lblDisposaltype_Id" runat="server" Text='<%# Eval("CaseDisposalType_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompliceSts" runat="server" Text='<%# Eval("Compliance_Status") %>'></asp:Label>
                                                                <asp:Label ID="lblComplianseSts_ID" runat="server" Text='<%# Eval("ComplianseSts_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Timeline(In Days)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltimeline" runat="server" Text='<%# Eval("CaseDisposal_Timeline") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order<br />Summary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblorderSummary" runat="server" Text='<%# Eval("OrderSummary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperlinkOrderDoc" runat="server" Target="_blank" Enabled='<%# Eval("CaseDisposal_Doc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("CaseDisposal_Doc") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditBtn" runat="server" Enabled='<%# Eval("CaseDisposalType_Id").ToString() == "2" ? true : false %>' CommandName="EditCase" ToolTip="Edit" CommandArgument='<%#  Eval("Case_ID") %>' CssClass="fa fa-edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Case Dispose Detail --%>


                                <%--Start Here Petitioner Detail --%>
                                <fieldset id="FieldViewPetiDtl" runat="server" visible="true">
                                    <legend>Petitioner Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Name<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPetitionerName" Display="Dynamic" ForeColor="Red"
                                                    SetFocusOnError="true" ControlToValidate="txtPetiName" ValidationGroup="Petitioner" ErrorMessage="Enter Name"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPetiName" runat="server" placeholder="Name" AutoComplete="off" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" Display="Dynamic" ControlToValidate="txtPetiName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Petitioner" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Desigantion<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvpetiDesign" Display="Dynamic" ForeColor="Red"
                                                    SetFocusOnError="true" ControlToValidate="ddlPetiDesigNation" ValidationGroup="Petitioner" ErrorMessage="Select Designation"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPetiDesigNation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Mobile No.</label>
                                                <%-- <asp:RequiredFieldValidator runat="server" ID="rfvMoNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtPetiMobileNo" ValidationGroup="Petitioner" ErrorMessage="Enter Mobile No."
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtPetiMobileNo" runat="server" placeholder="Mobile No" AutoComplete="off" CssClass="form-control MobileNo1" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfvPetiadvocatemobile" ValidationGroup="Petitioner" runat="server" Display="Dynamic" ControlToValidate="txtPetiMobileNo"
                                                    ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Present posting address<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPetiAdd" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtPetiAddRess" ValidationGroup="Petitioner" ErrorMessage="Enter Petitioner Present posting address"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPetiAddRess" runat="server" placeholder="Present posting address" AutoComplete="off" CssClass="form-control" MaxLength="100" onkeyup="javascript:capFirst(this);"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator8" Display="Dynamic" ControlToValidate="txtPetiAddRess"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Petitioner" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnPetitioner" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="Petitioner" OnClick="btnPetitioner_Click" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdPetiDtl" runat="server" CssClass="table table-bordered" DataKeyNames="Petitioner_Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdPetiDtl_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblPetitionerID" runat="server" Text='<%# Eval("Petitioner_Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUniqNO" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignation_Id" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionermobileNo" runat="server" Text='<%# Eval("PetitionerMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("PetitionerAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Petitioner_Id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                               <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("Petitioner_Id") %>' CommandName="DeleteRecord" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="fa fa-trash"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here Petitioner Detail --%>
                                <%--Start Here Petitioner Advocate Dtl --%>
                                <fieldset>
                                    <legend>Petitioner Advocate Details</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Name</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvPetiAdvName" ValidationGroup="PetiAdv"
                                                    ErrorMessage="Enter Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtPetiAdvocateName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPetiAdvocateName" runat="server" placeholder="Name" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtPetiAdvocateName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="PetiAdv" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Mobile No.</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvMObile" ValidationGroup="PetiAdv"
                                                    ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtPetiAdvocateMobileNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPetiAdvocateMobileNo" runat="server" placeholder="Mobile" AutoComplete="off" CssClass="form-control MobileNo" onkeypress="return NumberOnly();" MaxLength="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfv" ValidationGroup="PetiAdv" runat="server" Display="Dynamic" ControlToValidate="txtPetiAdvocateMobileNo"
                                                    ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2 mt-3">
                                            <div class="row">
                                                <div class="col-md-6 mt-3">
                                                    <asp:Button ID="btnPetiAdvSave" runat="server" Text="Save" OnClick="btnPetiAdvSave_Click" ValidationGroup="PetiAdv" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdPetiAdv" runat="server" CssClass="table table-bordered " AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" DataKeyNames="PetiAdv_Id" OnRowCommand="GrdPetiAdv_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advocate Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetiAdvocatename" runat="server" Text='<%# Eval("PetiAdv_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetiAdvocatMObile" runat="server" Text='<%# Eval("PetiAdv_MobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("PetiAdv_Id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("PetiAdv_Id") %>' CommandName="DeleteRecord" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="fa fa-trash"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here Petitioner Advocate Dtl --%>
                                <%-- Start Here Bind Responder Detail --%>
                                <fieldset id="FieldViewRespondent" runat="server" visible="true">
                                    <legend>Respondent Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Type</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvOffice" ValidationGroup="Responder"
                                                    ErrorMessage="Select Office type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlResOfficetypeName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlResOfficetypeName" runat="server" OnSelectedIndexChanged="ddlResOfficetypeName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Responder"
                                                    ErrorMessage="Select Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlResOfficeName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlResOfficeName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Designation</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Responder"
                                                    ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlResDesig" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlResDesig" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvname" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtResName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtResName" runat="server" CssClass="form-control" onkeypress="return chcode();" onkeyup="javascript:capFirst(this);" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtResName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Responder" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Mobile No.</label>

                                                <%--<asp:RequiredFieldValidator ID="rfvmobileno" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtResMobileNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtResMobileNo" onkeypress="return NumberOnly();" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RexNodalOfficerMobileNo" ValidationGroup="Responder" runat="server" Display="Dynamic" ControlToValidate="txtResMobileNo"
                                                    ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Department/HOD</label><span style="color: red;"><b> *</b></span>
                                                <%--<asp:RequiredFieldValidator ID="rfvDeptname" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                   InitialValue="0" ControlToValidate="ddlResDepartment" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlResDepartment" runat="server" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Address</label><span style="color: red;"><b> *</b></span>
                                                <%-- <asp:RequiredFieldValidator ID="rfvAdd" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Address." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtResAddress" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtResAddress" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                <%-- <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator7" Display="Dynamic" ControlToValidate="txtResAddress"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Responder" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-1" style="padding-top: 2rem! important;">
                                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Save" ID="btnRespondent" ValidationGroup="Responder" OnClick="btnRespondent_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdRespondentDtl" runat="server" CssClass="table table-bordered" DataKeyNames="Respondent_ID" AutoGenerateColumns="false" OnRowCommand="GrdRespondentDtl_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblResponderID" runat="server" Text='<%# Eval("Respondent_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUniqNO" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficetype" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblOfficetype_ID" runat="server" Text='<%# Eval("Officetype_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                                <asp:Label ID="lblOffice_Id" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignation_Id" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Respondent Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Responder Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRespondentNo" runat="server" Text='<%# Eval("RespondentMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Respondent_ID") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("Respondent_ID") %>' CommandName="DeleteRecord" ToolTip="Delete" CssClass="fa fa-trash" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Bind Responder Detail --%>
                                <%--Start Here DeptAdv Detail --%>
                                <fieldset id="FieldViewDeptAdvDtl" runat="server" visible="true">
                                    <legend>Respondent Advocate Details</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Advocate Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtDeptAdvocateName" ValidationGroup="DeptADV" ErrorMessage="Enter Name."
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtDeptAdvocateName" runat="server" AutoComplete="off" placeholder="Advocate Name" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="revDesignationName" Display="Dynamic" ControlToValidate="txtDeptAdvocateName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="DeptADV" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Mobile No.</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvAdvmovile" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtDeptAdvocateMobileNo" ValidationGroup="DeptADV" ErrorMessage="Enter Mobile No."
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtDeptAdvocateMobileNo" runat="server" AutoComplete="off" placeholder="Mobile No." CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="revAppointmentMobileNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtDeptAdvocateMobileNo" ValidationExpression="^([6-9]{1}[0-9]{9})$" ValidationGroup="DeptADV" ErrorMessage="Invalid Mobile No.">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-1" style="margin-top: 2rem ! important;">
                                            <div class="form-group">
                                                <asp:Button ID="btnDeptAdvocate" runat="server" CssClass="btn btn-primary " Text="Save" ValidationGroup="DeptADV" OnClick="btnDeptAdvocate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdDeptAdvDtl" runat="server" CssClass="table table-bordered text-center" DataKeyNames="DeptAdv_Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdDeptAdvDtl_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblDeptAdvID" runat="server" Text='<%# Eval("DeptAdv_Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUniqNO" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advocate Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdvocateName" runat="server" Text='<%# Eval("DeptAdvName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("DeptAdvMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("DeptAdv_Id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("DeptAdv_Id") %>' CommandName="DeleteRecord" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="fa fa-trash"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here DeptAdv Detail --%>
                                <fieldset>

                                    <hr />
                                    <p style="text-align: justify; color: red"><span>Note :- </span>In the "Case Belongs To" option, if "District Location" is selected, then the case will be handled by respective districts DM/DEO. OIC will also be appointed by the respective districts DM/DEO.</p>
                                    <div class="row">
                                        <div class="col-md-3" runat="server" id="divRealtion">
                                            <div class="form-group">
                                                <label style="color: blue">Related to School Edu Dep</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Related" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaserRelated" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlCaserRelated" OnSelectedIndexChanged="ddlCaserRelated_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divCaseRelate">
                                            <div class="form-group">
                                                <label style="color: #f86e15">Do you want to delete case</label>
                                                <asp:DropDownList runat="server" ID="ddlDeleteCase" OnSelectedIndexChanged="ddlDeleteCase_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Belongs To</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Department." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlDepartment" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case District Location<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="Save"
                                                        ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select District'></i>"
                                                        ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <legend>Case Details</legend>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Case Subject</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvCaseSubject" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group">
                                                <label>Case Sub Subject</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RfvtxtOrder" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Case Sub Subject" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseSubSubject" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseSubSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divOIC" class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Name/Nodel Officer</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvOic_Name" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOicName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOicName" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Mobile Number</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:TextBox ID="txtOicMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Email-ID</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:TextBox ID="txtOicEmailId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Order Number<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="Save"
                                                        ErrorMessage="Enter OIC case number" Text="<i class='fa fa-exclamation-circle' title='Enter OIC case number'></i>"
                                                        ControlToValidate="txtOICcaseNumber" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtOICcaseNumber" CssClass="form-control" MaxLength="45"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="div1" runat="server">
                                            <div class="form-group">
                                                <label>OIC Order Date<span style="color: red;"> *</span></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Enter OIC Order Date" Text="<i class='fa fa-exclamation-circle' title='Enter OIC Order Date'></i>"
                                                    ControlToValidate="txtOICDate" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOICDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Order Documnet<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="Save"
                                                        ErrorMessage="Uplode OICDocument" Text="<i class='fa fa-exclamation-circle' title='Uplode OICDocument'></i>"
                                                        ControlToValidate="fuOICDocument" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:HyperLink runat="server" ID="hyperlinkOICdoc" Target="_blank" Text="view"></asp:HyperLink>
                                                <asp:FileUpload runat="server" ID="fuOICDocument" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>High Priority Case</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvHighpriortiy" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select High Priority Case." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlHighprioritycase" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--start here case action detail by raghav 02-03-2023--%>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Case Reply</label>
                                                <asp:DropDownList runat="server" ID="ddlCaseReply" CssClass="form-control" OnSelectedIndexChanged="ddlCaseReply_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="divReplyDate" runat="server">
                                            <div class="form-group">
                                                <label>Reply Date</label>
                                                <asp:TextBox ID="txtReplyDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-5" id="divReplyRemark" runat="server">
                                            <div class="form-group">
                                                <label>Reply Remark</label>
                                                <asp:TextBox runat="server" ID="txtReplyCaseRemark" CssClass="form-control" placeholder="Case Reply Remark" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <%--End here case action detail by raghav 02-03-2023--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Case Matter Detail/Remark</label><span style="color: red;"><b> *</b></span><br />
                                                <asp:RequiredFieldValidator ID="RFVActionByDistrict" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCaseDetail" runat="server" TextMode="MultiLine" onkeyup="javascript:capFirst(this);" CssClass="form-control" AutoComplete="off" MaxLength="250"></asp:TextBox>
                                                <%-- <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator9" Display="Dynamic" ControlToValidate="txtCaseDetail"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="CaseDtl" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-3 mt-1">
                                            <div class="row">
                                                <div class="col-md-6 mt-1">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary " ValidationGroup="CaseDtl" Text="Update" OnClick="btnUpdate_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--Start Here Hearing Detail --%>
                                <fieldset id="FieldViewHearingDtl" runat="server" visible="true">
                                    <legend>Hearing Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Next Hearing Date</label><span style="color: red;"><b>* </b></span>
                                                <asp:RequiredFieldValidator ID="rfvHearingDate" ValidationGroup="Hearing"
                                                    ErrorMessage="Enter Next Hearing Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtNextHearingDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtNextHearingDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Hearing Document</label>
                                                <%--<asp:RequiredFieldValidator ID="rfvhearingFile" ValidationGroup="Hearing"
                                                    ErrorMessage="Upload Hearing Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="FileHearingDoc" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:FileUpload ID="FileHearingDoc" runat="server" CssClass="form-control" />
                                                <span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>
                                            </div>
                                        </div>
                                        <div class="col-md-2 mt-3">
                                            <div class="row">
                                                <div class="col-md-6 mt-3">
                                                    <asp:Button ID="btnAddHeairng" runat="server" CssClass="btn btn-primary" ValidationGroup="Hearing" Text="Save" OnClick="btnAddHeairng_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdHearingDtl" runat="server" CssClass="table table-bordered text-center" DataKeyNames="NextHearing_ID" AutoGenerateColumns="false"
                                                    EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdHearingDtl_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblPetitionerID" runat="server" Text='<%# Eval("NextHearing_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUniqNO" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("NextHearingDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyperHearingDoc" runat="server" Target="_blank" Enabled='<%# Eval("HearingDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/HearingDoc/" + Eval("HearingDoc") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                                <asp:Label ID="lblHearingDocPath" runat="server" Visible="false" Text='<%# Eval("HearingDoc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("NextHearing_ID") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("NextHearing_ID") %>' CommandName="DeleteRecord" ToolTip="Delete" CssClass="fa fa-trash" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here Hearing Detail --%>

                                <%-- Start Here Bind Documnet Detail --%>
                                <fieldset id="FieldViewDocument" runat="server" visible="true">
                                    <legend>Case Document</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Document Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RfvAddDocumnet" ValidationGroup="Docs"
                                                    ErrorMessage="Enter Document Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtDocumentName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                                <asp:TextBox ID="txtDocumentName" placeholder="Enter Document Name" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtDocumentName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Docs" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Document Upload</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RfvUploadDoc" ValidationGroup="Docs"
                                                    ErrorMessage="Upload Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="FileCaseDoc" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:FileUpload ID="FileCaseDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                                <span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>
                                            </div>
                                        </div>
                                        <div class="col-md-2 mt-3">
                                            <div class="row">
                                                <div class="col-md-6  mt-3">
                                                    <asp:Button ID="btnSaveDoc" runat="server" ValidationGroup="Docs" CssClass="btn btn-primary " Text="Save" OnClick="btnSaveDoc_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-4">
                                        <div class="col-md-9" style="overflow: scroll; max-height: 400px;">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDocument" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false"
                                                    DataKeyNames="CaseDoc_ID" OnRowCommand="GrdCaseDocument_RowCommand" EmptyDataText="NO RECORD FOUND" OnRowDataBound="GrdCaseDocument_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDocumentID" runat="server" Text='<%# Eval("CaseDoc_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("Doc_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyperViewDoc" runat="server" Visible="false" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("Doc_Path").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" +  Eval("Doc_Path") %>'></asp:HyperLink>
                                                                <asp:Label ID="lblDocPath" runat="server" Text='<%# Eval("Doc_Path") %>' Visible="false"></asp:Label>
                                                                <asp:HyperLink ID="hyperViewLink" runat="server" CssClass="fa fa-eye" Visible="false" Target="_blank" NavigateUrl='<%# Eval("Doc_Path") %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditDoc" runat="server" CssClass="fa fa-edit" CommandName="EditRecord" CommandArgument='<%# Eval("CaseDoc_ID") %>' ToolTip="Edit"></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkDisable" runat="server" CssClass="fa fa-trash" CommandName="DeleteRecord" CommandArgument='<%# Eval("CaseDoc_ID") %>' ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Bind Document Detail --%>

                                <fieldset id="fildAskForOldCase" runat="server">
                                    <legend>Did this case have any old case detail</legend>
                                    <div class=" row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlAskForOldCase" CssClass="form-control" OnSelectedIndexChanged="ddlAskForOldCase_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Start Here OldCase Detail --%>
                                    <div runat="server" id="FieldViewOldCaseDtl">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Old Case No</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="OldCase"
                                                        ErrorMessage="Enter Old Case No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtoldCaseNo" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtoldCaseNo" runat="server" CssClass="form-control" MaxLength="10" AutoComplete="off" onkeypress="return NumberOnly();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Year</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="OldCase"
                                                        ErrorMessage="Select Case Year" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddloldCaseYear" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddloldCaseYear" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>CaseType</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="OldCase"
                                                        ErrorMessage="Select Case type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddloldCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddloldCasetype" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Court</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="OldCase"
                                                        ErrorMessage="Select Court" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddloldCaseCourt" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddloldCaseCourt" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddloldCaseCourt_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Court Location</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="OldCase"
                                                        ErrorMessage="Select Court Location" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddloldCourtLoca_Id" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddloldCourtLoca_Id" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3" id="Div_Doc1" runat="server">
                                                <label>Case Details</label><br />
                                                <asp:FileUpload ID="FU1" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3" id="Div_Doc2" runat="server">
                                                <label>Description Of Proceedings</label><br />
                                                <asp:FileUpload ID="FU2" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3" id="Div_Doc3" runat="server">
                                                <label>Decision</label><br />
                                                <asp:FileUpload ID="FU3" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3" id="Div_Doc4" runat="server">
                                                <label>Other</label><br />
                                                <asp:FileUpload ID="FU4" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-1 mt-3">
                                                <asp:Button ID="btnOldCase" runat="server" Text="Save" OnClick="btnOldCase_Click" ValidationGroup="OldCase" CssClass="btn btn-primary mt-3" />
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GrdOldCaseDtl" runat="server" CssClass="table table-bordered text-center" DataKeyNames="Id" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdOldCaseDtl_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    <asp:Label ID="lbloldCaseID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Old Case No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Case Year">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldCaseYear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Case type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldCasetype" runat="server" Text='<%# Eval("CaseType") %>'></asp:Label>
                                                                    <asp:Label ID="lblOldCasetype_Id" runat="server" Text='<%# Eval("Casetype_Id") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Court">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldCourt" runat="server" Text='<%# Eval("Court") %>'></asp:Label>
                                                                    <asp:Label ID="lblOldCourt_Id" runat="server" Text='<%# Eval("CourtType_Id") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Court Location">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldCourtLoca" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                                    <asp:Label ID="lblOldCourtLoca_Id" runat="server" Text='<%# Eval("CourtDistLoca_Id") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doc Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="hypOldCaseDtl" runat="server" Enabled='<%# Eval("DocLink").ToString() == "" ? false : true %>' Target="_blank" NavigateUrl='<%# "~/Legal/OldCaseDocument/" +  Eval("DocLink") %>' CssClass="fa fa-eye"></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                 <asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteRecord" ToolTip="Delete" CssClass="fa fa-trash" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%--End Here DeptAdv Detail --%>
                                <%---End Here For Edit Case Details ---%>


                                <fieldset id="Fieldset1" runat="server">
                                    <legend>Want To File WA/RP/SLP </legend>

                                    <div class=" row">
                                        <p style="font-weight: bold"><span style="color:red; font-weight: bold">Note:- </span> Time limit for Filing WA/RP/SLP is 90 days from this oder date.</p>
                                        <br />
                                    </div>
                                    <div class="row">
                                        <p style="color: red; font-weight: bold">
                                            Disposed date is :
                                                   <asp:Label Style="font-weight: BOLD; color: black" runat="server" ID="lblcasedisposeldate"></asp:Label>
                                            <br />
                                            Disposal due date is :
                                            <asp:Label Style="font-weight: BOLD; color: black" runat="server" ID="lblremainingdays"></asp:Label><br />
                                            Remaining days for appeal is :
                                            <asp:Label Style="font-weight: BOLD; color: black" runat="server" ID="lblTotalDays"></asp:Label>
                                        </p>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <asp:HiddenField ID="hdnUniqueNo" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        function NumberOnly() { //only Numeric required.
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }

        function capFirst(cpt) { //only Capital First.
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }

        function chcode() { // Only English or Hindi Required
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }

        function lettersOnly() { // Only English Letter Allow.
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }
    </script>
    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Delete this case?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

