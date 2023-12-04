<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="EditCaseDetail.aspx.cs" Inherits="Legal_EditCaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
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
    <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="IntrimOd" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="VDSOIC" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="OldOICDTL" ShowMessageBox="true" ShowSummary="false" />
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
                        <div class="float-left">
                            Edit Case Detail
                        </div>
                        <div class="float-right">
                            <asp:LinkButton ID="btnBackPage" runat="server" CssClass="btn-sm label-danger" Text="Back" OnClick="btnBackPage_Click"></asp:LinkButton>
                        </div>
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
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control" ReadOnly="true"></asp:DropDownList>
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
                                                    Case Type</label>
                                                <asp:RequiredFieldValidator ID="rfvCasetype" ValidationGroup="Save"
                                                    ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
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
                                                    Designation<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvpetiDesign" Display="Dynamic" ForeColor="Red"
                                                    SetFocusOnError="true" ControlToValidate="ddlPetiDesigNation" ValidationGroup="Petitioner" ErrorMessage="Select Designation"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPetiDesigNation" runat="server" CssClass="form-control select2"></asp:DropDownList>
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
                                                    Present posting District<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPetiAdd" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="ddlPetitionerPresentDistrict" ValidationGroup="Petitioner" ErrorMessage="Select Petitioner Present District"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlPetitionerPresentDistrict" CssClass="form-control select2" AutoPostBack="true"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtPetiAddRess" runat="server" placeholder="Present posting address" AutoComplete="off" CssClass="form-control" MaxLength="100" onkeyup="javascript:capFirst(this);"></asp:TextBox>--%>
                                                <%-- <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator8" Display="Dynamic" ControlToValidate="txtPetiAddRess"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Petitioner" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Remark</label>
                                                <asp:TextBox ID="txtPetitionerRemark" TextMode="MultiLine" runat="server" CssClass="form-control" MaxLength="200" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1" style="padding-top: 2rem! important;">
                                            <asp:Button ID="btnPetitioner" runat="server" CssClass="btn btn-primary btn-block" Text="Save" ValidationGroup="Petitioner" OnClick="btnPetitioner_Click" />
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
                                                        <asp:TemplateField HeaderText="Present posting District">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblPresentpostDisID" runat="server" Text='<%# Eval("District_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("PetitionerRemark") %>'></asp:Label>
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
                                                                <%--<asp:Label ID="lblHODID" runat="server" Text='<%# Eval("HOD_ID") %>'></asp:Label>--%>
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
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Department</label><%--<span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvDeptname" ValidationGroup="Responder"
                                                    ErrorMessage="Select Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    InitialValue="0" ControlToValidate="ddlResDepartment" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlResDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlResDepartment_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>HOD</label><%--<span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="Responder"
                                                    ErrorMessage="Select HOD." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlHodName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlHodName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Type</label>
                                                <asp:DropDownList ID="ddlResOfficetypeName" runat="server" OnSelectedIndexChanged="ddlResOfficetypeName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Office Location</label>
                                                <asp:DropDownList ID="ddlResOfficeName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Designation</label>
                                                <asp:DropDownList ID="ddlResDesig" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvname" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtResName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtResName" runat="server" CssClass="form-control" onkeypress="return chcode();" onkeyup="javascript:capFirst(this);" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtResName"
                                                    ValidationExpression="^[a-zA-Z]+(([a-zA-Z\s])?[a-zA-Z]*)*$" ValidationGroup="Responder" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Mobile No.</label>
                                                <asp:TextBox ID="txtResMobileNo" onkeypress="return NumberOnly();" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RexNodalOfficerMobileNo" ValidationGroup="Responder" runat="server" Display="Dynamic" ControlToValidate="txtResMobileNo"
                                                    ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Address</label>
                                                <asp:TextBox ID="txtResAddress" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="500"></asp:TextBox>
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
                                                        <asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                                <asp:Label ID="lblDepartment_Id" runat="server" Text='<%# Eval("Department_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HOD Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhodname" runat="server" Text='<%# Eval("HodName") %>'></asp:Label>
                                                                <asp:Label ID="lblHod_Id" runat="server" Text='<%# Eval("HOD_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficetype" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblOfficetype_ID" runat="server" Text='<%# Eval("Officetype_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Location">
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
                                                <asp:Button ID="btnDeptAdvocate" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="DeptADV" OnClick="btnDeptAdvocate_Click" />
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
                                    <legend>Case Details</legend>
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
                                        <%-- <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Belongs To</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Department." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlDepartment" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>HOD Name</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select HOD Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlHOD" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:ListBox ID="ddlHOD" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Belongs To</label><span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvCaseBelongto" Display="Dynamic" ForeColor="Red" ValidationGroup="CaseDtl"
                                                    ErrorMessage="Select Case Belongs To." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseBelongto" InitialValue="0">

                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlCaseBelongto" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseBelogto_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">HO</asp:ListItem>
                                                    <asp:ListItem Value="2">District</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divDistrictLocation" visible="false">
                                            <div class="form-group">
                                                <label>Case District Location</label>
                                                <%-- <span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="CaseDtl"
                                                        ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select District'></i>"
                                                        ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>--%>
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject</label>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Sub Subject</label>
                                                <asp:DropDownList ID="ddlCaseSubSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>High Priority Case</label>
                                                <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>PolicyMatter</label>
                                                <asp:DropDownList ID="ddlPolicyMeter" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
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
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Button ID="btnUpdateCaseDtl" runat="server" CssClass="btn btn-primary" ValidationGroup="CaseDtl" Text="Update" OnClick="btnUpdateCaseDtl_Click" />
                                        </div>
                                    </div>
                                </fieldset>
                                <%--- OIC Field Hide on only Admin Login. --%>
                                <%--- OIC Detail Fieldset. --%>
                                <fieldset id="FieldOicDeteail" runat="server">
                                    <legend>Current OIC Details</legend>
                                    <div runat="server" id="divOIC" class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Name/Nodel Officer</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvOic_Name" ValidationGroup="VDSOIC"
                                                    ErrorMessage="Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOicName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOicName" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Designation</label>
                                                <asp:TextBox runat="server" ID="txtOICDesignation" CssClass="form-control"></asp:TextBox>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="VDSOIC"
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="VDSOIC"
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="VDSOIC"
                                                        ErrorMessage="Uplode OICDocument" Text="<i class='fa fa-exclamation-circle' title='Uplode OICDocument'></i>"
                                                        ControlToValidate="fuOICDocument" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:HyperLink runat="server" ID="hyperlinkOICdoc" Target="_blank" Text="view"></asp:HyperLink>
                                                <asp:FileUpload runat="server" ID="fuOICDocument" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <%--start here case action detail by bhanu 11-04-2023--%>
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
                                        <div class="col-md-3" id="divReplyDate" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Reply Date</label>
                                                <asp:TextBox ID="txtReplyDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="divReplyRemark" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Reply Remark</label>
                                                <asp:TextBox runat="server" ID="txtReplyCaseRemark" CssClass="form-control" placeholder="Case Reply Remark" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="margin-top: 2rem!important;">
                                            <div class="row">
                                                <div class="col-md-6 mt-1">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="VDSOIC" Text="Update" OnClick="btnUpdate_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--End here case action detail by bhanu 11-04-2023--%>
                                </fieldset>
                                <%--- OIC Detail Fieldset. --%>
                                <%-- Old OIC Detail --%>
                                <fieldset id="Fieldset1" runat="server">
                                    <legend>Old OIC Details</legend>
                                    <div runat="server" id="div2" class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Old OIC Name</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ValidationGroup="OldOICDTL"
                                                    ErrorMessage="Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtOldOicName" Display="Dynamic" runat="server" >
                                                </asp:RequiredFieldValidator>
                                                <%--<asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged"></asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtOldOicName" placeholder="Enter Old Oic Name" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Old OIC Designation</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvOICDesignation" ValidationGroup="OldOICDTL"
                                                    ErrorMessage="Select Old OIC Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOICDesignation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlOICDesignation" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Old OIC Mobile Number</label>
                                                <span style="color: red;"><b>*</b></span>
                                                 <asp:RequiredFieldValidator ID="rfvOldOICMobileN" ValidationGroup="OldOICDTL"
                                                    ErrorMessage="Enter Old OIC Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtOldOICMobileNu" Display="Dynamic" runat="server" >
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOldOICMobileNu" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Old OIC Email-ID</label>
                                                <span style="color: red;"><b>*</b></span>
                                                 <asp:RequiredFieldValidator ID="rfvOICEmailID" ValidationGroup="OldOICDTL"
                                                    ErrorMessage="Enter Old OIC Email-ID." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtOldOICEmailID" Display="Dynamic" runat="server" >
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOldOICEmailID" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    District<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="ddlOldOICDistrict" ValidationGroup="OldOICDTL" ErrorMessage="Select District" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !' ></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlOldOICDistrict" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="margin-top: 2rem!important;">
                                            <div class="row">
                                                <div class="col-md-6 mt-1">
                                                    <asp:Button ID="BtnAdd" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="OldOICDTL" Text="Add" OnClick="BtnAdd_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                          <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdOldOCDetail" runat="server" CssClass="table table-bordered text-center" DataKeyNames="Case_ID" AutoGenerateColumns="false"
                                                    EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdOldOCDetail_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Old OIC Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldOICName" runat="server" Text='<%# Eval("OldOICName") %>'></asp:Label>
                                                                <asp:Label ID="lblOldOICNameID" runat="server" Text='<%# Eval("OldOICName") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Old OIC Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation_Name" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignation_Nameid" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Old OIC Mobile Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("OldOICMobileNo") %>'></asp:Label>
                                                                <asp:Label ID="lblOldOICMobileNo" runat="server" Text='<%# Eval("OldOICMobileNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Old OIC Email-ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldOICEmail_ID" runat="server" Text='<%# Eval("OldOICEmailID") %>'></asp:Label>
                                                                <asp:Label ID="lblOldOICEmailID" runat="server" Text='<%# Eval("OldOICEmailID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="District">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDistrict_Name" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDistrict_NameID" runat="server" Text='<%# Eval("District_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>&nbsp;
                                                                <%--<asp:LinkButton ID="lnkDisable" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="DeleteRecord" ToolTip="Delete" CssClass="fa fa-trash" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:LinkButton>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- Old OIC Detail End --%>
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
                                                <%--<span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>--%>
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
                                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" Display="Dynamic" ControlToValidate="txtDocumentName"
                                                    ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Docs" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>--%>
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
                                                <%--<span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-2 mt-3">
                                            <div class="row">
                                                <div class="col-md-6  mt-3">
                                                    <asp:Button ID="btnSaveDoc" runat="server" ValidationGroup="Docs" CssClass="btn btn-primary" Text="Save" OnClick="btnSaveDoc_Click" />
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
                                                                <asp:HyperLink ID="hyperViewDoc" runat="server" Visible="false" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("Doc_Path").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" + Eval("Doc_Path")  %>'></asp:HyperLink>
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
                                <%-- Start Here Intirm Order Detail --%>
                                <fieldset>
                                    <legend>Did this case have any Intrim Order</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlIntrimOrder" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIntrimOrder_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Div_IntrimOrderYesNO" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Order Start Date</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="rfvIntrimODate" ValidationGroup="IntrimOd"
                                                        ErrorMessage="Enter Intrim Order Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtIntirmOrderDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIntirmOrderDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Timeline (In Days)</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="rfvOTimeline" ValidationGroup="IntrimOd"
                                                        ErrorMessage="Enter Order Timeline" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtIntrimTimeline" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIntrimTimeline" runat="server" MaxLength="3" placeholder="Timeline In Days" CssClass="form-control" onkeypress="return NumberOnly();" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Personal Presence</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="rfvintrimPP" ValidationGroup="IntrimOd"
                                                        ErrorMessage="Enter PP Detail" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtIntrimPrevPP" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIntrimPrevPP" runat="server" placeholder="Enter PP" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Order End Date</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="IntrimOd"
                                                        ErrorMessage="Enter PP Detail" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtIntrimPrevPP" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIntrimOrderEnddate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Order Summary</label><span style="color: red;"><b>*</b></span>
                                                    <asp:RequiredFieldValidator ID="rfvIOS" ValidationGroup="IntrimOd"
                                                        ErrorMessage="Enter Intrim Order Summary" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtIntrimOrderSummary" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtIntrimOrderSummary" MaxLength="300" TextMode="MultiLine" runat="server" placeholder="Order Summary" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnIntrimOrder" runat="server" Text="Save" ValidationGroup="IntrimOd" CssClass="btn btn-primary btn-block" OnClick="btnIntrimOrder_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Intirm Order Detail --%>
                                <%-- Start Here District Wise Return File And Appeal --%>
                                <fieldset>
                                    <legend>Return File/Appeal</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Action</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvAction" ValidationGroup="a"
                                                    ErrorMessage="Select Action" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlAction" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlAction" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Return/Reply/Compliance</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="margin-top: 2rem" runat="server" id="divYasOrNo" visible="false">
                                            <div class="form-group">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="a"
                                                    ErrorMessage="Select Action Yes Or No" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlYesOrNo" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlYesOrNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYesOrNo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="margin-top: 2rem" runat="server" id="div_ComplianceYesOrNo" visible="false">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlComlianceYesNo" CssClass="form-control" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_WPRPFiledOrNot" visible="false">
                                            <div class="form-group">
                                                <label>WA/RP Filed Or Not</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfv_WPRP" ValidationGroup="a"
                                                    ErrorMessage="Select WP/RP Filed Or Not" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlWPRPFiledOrNot" InitialValue="0" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlWPRPFiledOrNot" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlWPRPFiledOrNot_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" runat="server" id="Div_CaseNo" visible="false">
                                            <div class="form-group">
                                                <label>Case No.</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCaseNo" ValidationGroup="a"
                                                    ErrorMessage="Enter Case No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtCaseNo" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="div_CaseYear" visible="false">
                                            <div class="form-group">
                                                <label>Case Year</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCaseYear" ValidationGroup="a"
                                                    ErrorMessage="Select Case Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlWPRPCaseYear" InitialValue="0" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlWPRPCaseYear" runat="server" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_WPRPFileUp" visible="false">
                                            <div class="form-group">
                                                <label>Document Upload</label>
                                                <asp:HyperLink runat="server" ID="HPWP_RPFile" Target="_blank" Text="view"></asp:HyperLink>
                                                <asp:FileUpload runat="server" ID="WPRPFileUp" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-12" runat="server" id="div_WPRPRemark" visible="false">
                                            <div class="form-group">
                                                <label>District Remark</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvWPRPRemark" ValidationGroup="a"
                                                    ErrorMessage="Enter Remark." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtWPRPRemark" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtWPRPRemark" CssClass="form-control" TextMode="MultiLine" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divReceiptNo" visible="false">
                                            <div class="form-group">
                                                <label>Receipt Number</label>
                                                <%-- <span style="color: red"><b>*</b></span>
                                               <asp:RequiredFieldValidator runat="server" ID="rfvReceipt" Display="Dynamic" ForeColor="Red"
                                                ErrorMessage="Enter Receipt No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtReceiptNo" ValidationGroup="a">
                                            </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="form-control" AutoComplete="off" placeholder="Enter Receipt No"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="divReceiptDate" visible="false">
                                            <div class="form-group">
                                                <label>Date</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvDate1" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Date." ControlToValidate="txtReceiptDate" ValidationGroup="a"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtReceiptDate" CssClass="form-control" AutoComplete="off" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6" runat="server" id="divActionAppealFrom" visible="false">
                                            <div class="form-group">
                                                <label>Action expected From/Action Taken</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvActionAppeal" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Action expected From." ControlToValidate="txtActionAppealedFrom" ValidationGroup="a"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtActionAppealedFrom" CssClass="form-control" placeholder="Enter Action expected From" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_FileMoment" visible="false">
                                            <div class="form-group">
                                                <label>File Movement</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select File Movement." ControlToValidate="RDFileMoment" ValidationGroup="a"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:RadioButtonList runat="server" ID="RDFileMoment" RepeatDirection="Horizontal" CssClass="form-control" OnSelectedIndexChanged="RDFileMoment_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="divReason" visible="false">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Reason</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvReason" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Reason" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtReason" ValidationGroup="a">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtReason" CssClass="form-control" AutoComplete="off" TextMode="MultiLine" placeholder="Enter Reason"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divReturnAndReply" visible="false">
                                        <fieldset>
                                            <legend>File Moment</legend>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>HOD</label>
                                                        <span style="color: red"><b>*</b></span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvHOD" Display="Dynamic" ForeColor="Red"
                                                            ErrorMessage="Select HOD." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                            ValidationGroup="a" ControlToValidate="ddlNameHod" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:DropDownList runat="server" ID="ddlNameHod" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Dispatch Number</label>
                                                        <span style="color: red"><b>*</b></span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDispatchNo" Display="Dynamic" ForeColor="Red"
                                                            ErrorMessage="Enter Dispatch No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                            ValidationGroup="a" ControlToValidate="txtDispatchNo">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:TextBox runat="server" ID="txtDispatchNo" CssClass="form-control" AutoComplete="off" placeholder="Enter Dispatch No"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Date</label>
                                                        <span style="color: red"><b>*</b></span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDate2" Display="Dynamic" ForeColor="Red"
                                                            ErrorMessage="Enter Date." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                            ValidationGroup="a" ControlToValidate="txtDispatchDate">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:TextBox runat="server" ID="txtDispatchDate" CssClass="form-control" AutoComplete="off" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Document Upload</label>
                                                        <%--<span style="color: red"><b>*</b></span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvFileForward" Display="Dynamic" ForeColor="Red"
                                                            ErrorMessage="Enter File Forward." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                            ValidationGroup="a" ControlToValidate="FileUp">
                                                        </asp:RequiredFieldValidator>--%>
                                                        <asp:HyperLink runat="server" ID="FileHpView" Target="_blank" Text="view"></asp:HyperLink>
                                                        <asp:FileUpload runat="server" ID="FileUp" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>HOD Remark</label>
                                                    <span style="color: red"><b>*</b></span>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvHODRemark" Display="Dynamic" ForeColor="Red"
                                                        ErrorMessage="Enter HOD Remark." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ValidationGroup="a" ControlToValidate="txtHODRemark">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtHODRemark" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" style="padding-top: 2rem! important;" runat="server" id="divBtn" visible="false">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here District Wise Return File And Appeal --%>
                                <%-- Start Here Case Dispose Detail --%>
                                <fieldset id="Fieldset_CaseDispose" runat="server" visible="true">
                                    <legend>Disposal Status</legend>
                                    <div class="row">
                                        <div class="col-md-2" runat="server" id="DisposalStatus">
                                            <div class="form-group">
                                                <label>
                                                    Case Disposal</label><span style="color: red;"><b> *</b></span><br />
                                                <asp:RadioButtonList ID="rdCaseDispose" runat="server" CssClass="rbl form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdCaseDispose_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="caseDisposeYes" runat="server" visible="false">
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
                                        <div class="col-md-3" id="OrderWithDir_Div" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>Order With Direction Post</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:DropDownList runat="server" ID="ddlOrderWith" CssClass="form-control" OnSelectedIndexChanged="ddlOrderWith_SelectedIndexChanged" AutoPostBack="true">
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
                                                <asp:DropDownList ID="ddlCompliaceSt" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompliaceSt_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                    <asp:ListItem Value="3">Pending</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- Start Complianc Status --%>
                                        <div class="col-md-3" runat="server" id="Div_CompliancNo" visible="false">
                                            <div class="form-group">
                                                <label>Compliance Number</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvComplianceNo" ValidationGroup="CaseDispose" Enabled="false"
                                                    ErrorMessage="Enter Compliance No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtComplianceNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtComplianceNo" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_CompliancDate" visible="false">
                                            <div class="form-group">
                                                <label>Compliance Date</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvComplianceDate" ValidationGroup="CaseDispose" Enabled="false"
                                                    ErrorMessage="Enter Compliance Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCompianceDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtCompianceDate" CssClass="form-control" AutoComplete="off" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_CompliancDoc" visible="false">
                                            <div class="form-group">
                                                <label>Document Upload</label>
                                                <asp:FileUpload runat="server" ID="ComplianceDoc" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-6" runat="server" id="Div_ComplianceRemark" visible="false">
                                            <div class="form-group">
                                                <label>Compliance Remark</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvComRemark" ValidationGroup="CaseDispose" Enabled="false"
                                                    ErrorMessage="Enter Remark" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtComplianceRemark" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtComplianceRemark" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- End Complianc Status --%>
                                        <%-- Start Any Rejoinder --%>
                                        <div class="col-md-3" runat="server" id="div_Compliance_AnyRejoinder" visible="false">
                                            <div class="form-group">
                                                <label>Any Rejoinder</label>
                                                <span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvAnyRejoinder" ValidationGroup="CaseDispose"
                                                    ErrorMessage="Select Any Rejoinder" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlAnyRejoinder" Display="Dynamic" InitialValue="0" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlAnyRejoinder" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAnyRejoinder_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_RejoinderNo" visible="false">
                                            <div class="form-group">
                                                <label>Rejoinder Number</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvRejNo" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Rejoinder No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtRejoinderNo">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtRejoinderNo" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_RejoinderDate" visible="false">
                                            <div class="form-group">
                                                <label>Rejoinder Date</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvRejDate" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Rejoinder Date." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtRejoinderDate">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtRejoinderDate" CssClass="form-control" AutoComplete="off" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_FileRejoinder" visible="false">
                                            <div class="form-group">
                                                <label>Upload Documnet</label>
                                                <asp:FileUpload runat="server" ID="RejoinderDoc" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-12" runat="server" id="Div_RejoinderRemark" visible="false">
                                            <div class="form-group">
                                                <label>Rejoinder Remark</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvRejRemark" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Rejoinder Date." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtRejoinderRemark">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtRejoinderRemark" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- End Any Rejoinder --%>
                                        <%-- Start Additional Return --%>
                                        <div class="col-md-3" runat="server" id="divAdditionalReturn" visible="false">
                                            <div class="form-group">
                                                <label>Additional Return</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvAdditionalReturn" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Additional Return." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="ddlAdditionalReturn" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlAdditionalReturn" CssClass="form-control" OnSelectedIndexChanged="ddlAdditionalReturn_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_AdditionalNo" visible="false">
                                            <div class="form-group">
                                                <label>Additional Number</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvAdditionalNo" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Additional No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtAdditionalNo">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtAdditionalNo" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_AdditionalDate" visible="false">
                                            <div class="form-group">
                                                <label>Additional Date</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvAdditionalDate" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Additional Date." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtAdditionalDate">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtAdditionalDate" CssClass="form-control" AutoComplete="off" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/mm/yyyy" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" id="Div_AdditionalDoc" visible="false">
                                            <div class="form-group">
                                                <label>Upload Documnet</label>
                                                <asp:FileUpload runat="server" ID="AdditionalDoc" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-12" runat="server" id="Div_AdditionalRemar" visible="false">
                                            <div class="form-group">
                                                <label>Additional Remark</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvadditionalRemark" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Additional Remark." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="CaseDispose" ControlToValidate="txtAdditionalRemar">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtAdditionalRemar" CssClass="form-control" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- End Additional Return --%>
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
                                                <asp:TextBox ID="txtOrderimpletimeline" runat="server" placeholder="Compliance Timeline" onkeypress="return NumberOnly();" AutoComplete="off" CssClass="form-control">
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
                                                <asp:TextBox ID="txtorderSummary" placeholder="Order Summary" runat="server" MaxLength="500" AutoComplete="off" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                                                <%--<span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-1" id="HearingDtl_CaseDispose" runat="server" visible="false" style="padding-top: 2rem ! important;">
                                            <asp:Button ID="btnCaseDispose" runat="server" CssClass="btn btn-primary" ValidationGroup="CaseDispose" Text="Disposal" OnClick="btnCaseDispose_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDispose" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered text-center" AutoGenerateRows="false" EmptyDataText="NO RECORD FOUND">
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
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order With<br />Direction" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderDirection" runat="server" Text='<%# Eval("OrderWithDirection") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComolianceNo" runat="server" Text='<%# Eval("ComplianceNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComolianceDate" runat="server" Text='<%# Eval("ComplianceDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Document" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblComolianceDoc" runat="server" Text='<%# Eval("ComplianceDoc") %>'></asp:Label>--%>
                                                                <asp:HyperLink ID="HyperlinkComolianceDoc" runat="server" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("ComplianceDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("ComplianceDoc")  %>'></asp:HyperLink>
                                                                <%--<asp:HyperLink ID="HyperlinkComolianceDoc" runat="server" Target="_blank" NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("ComplianceDoc") %>' CssClass="fa fa-eye"></asp:HyperLink>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Compliance<br />Remark" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblComolianceRemark" runat="server" Text='<%# Eval("ComplianceRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Any<br />Rejoinder" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAnyRejoinder" runat="server" Text='<%# Eval("AnyRejoinder") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejoinder<br />Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejoinderNo" runat="server" Text='<%# Eval("RejoinderNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejoinder<br />Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejoinderdate" runat="server" Text='<%# Eval("RejoinderDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejoinder<br />Document" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <%--<asp:HyperLink ID="HyperlinkRejoinderDoc" runat="server" Target="_blank" NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("RejoinderDoc") %>' CssClass="fa fa-eye"></asp:HyperLink>--%>
                                                                <asp:HyperLink ID="HyperlinkRejoinderDoc" runat="server" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("RejoinderDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("RejoinderDoc")  %>'></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rejoinder<br />Remark" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRejoinderRemark" runat="server" Text='<%# Eval("RejoinderRemark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Additional<br />Return" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdditionalReturn" runat="server" Text='<%# Eval("AdditionalReturn") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Additional<br />Number" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdditionalno" runat="server" Text='<%# Eval("AdditionalNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Additional<br />Date" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdditionalDate" runat="server" Text='<%# Eval("AdditionalDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Additional<br />Documnet" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <%--<asp:HyperLink ID="HyperlinkAdditionaldoc" runat="server" Target="_blank" NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("AdditionalDoc") %>' CssClass="fa fa-eye"></asp:HyperLink>--%>
                                                                <asp:HyperLink ID="HyperlinkAdditionaldoc" runat="server" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("AdditionalDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("AdditionalDoc")  %>'></asp:HyperLink>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Additional<br />Remark" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdditionalRemark" runat="server" Text='<%# Eval("AdditionalRemark") %>'></asp:Label>
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
                                                                <asp:HyperLink ID="HyperlinkOrderDoc" runat="server" CssClass="fa fa-eye" Target="_blank" Enabled='<%#  Eval("CaseDisposal_Doc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("CaseDisposal_Doc")  %>'></asp:HyperLink>
                                                                <%--<asp:HyperLink ID="HyperlinkOrderDoc" runat="server" Target="_blank" NavigateUrl='<%# "../Legal/DisposalDocs/" + Eval("CaseDisposal_Doc") %>' CssClass="fa fa-eye"></asp:HyperLink>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <%-- End Here Case Dispose Detail --%>
                                <%--Start Here OldCase Detail --%>
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
                                                    <asp:DropDownList ID="ddloldCaseYear" runat="server" CssClass="form-control select2">
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
                                                    <label>Bench Location</label><span style="color: red;"><b>*</b></span>
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
                                                <label>Description Of Proceedings</label>
                                                <asp:FileUpload ID="FU2" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3" id="Div_Doc3" runat="server">
                                                <label>Decision</label>
                                                <asp:FileUpload ID="FU3" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3" id="Div_Doc4" runat="server">
                                                <label>Other</label><br />
                                                <asp:FileUpload ID="FU4" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Order Date</label>
                                                    <asp:TextBox ID="txtOrderDate_OldCase" runat="server" data-provide="datepicker" CssClass="form-control" AutoComplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
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
                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("OrderDate") %>'></asp:Label>
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
                                <%---End Here For Edit Case Details ---%>
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
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $('[id*=ddlHOD]').multiselect({
            includeSelectAllOption: true,
            includeSelectAllOption: true,
            buttonWidth: '100%'
        });

    </script>
    <script type="text/javascript">
        $('#txtDispatchDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtReceiptDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtAdditionalDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtRejoinderDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtCompianceDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>

