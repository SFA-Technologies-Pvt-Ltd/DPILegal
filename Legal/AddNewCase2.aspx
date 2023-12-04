<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="AddNewCase2.aspx.cs" Inherits="Legal_AddNewCase2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="Add" ShowMessageBox="true" ShowSummary="false" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Responder" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Petitioner" />
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="Button1" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
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
                        Add New Case
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Case Detail</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Case No.</label><span style="color: red;">*</span>
                                                <asp:RequiredFieldValidator ID="RfvCaseno" ValidationGroup="Save"
                                                    ErrorMessage="Enter Case No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCaseNo" runat="server" placeholder="Case No." class="form-control" AutoComplete="off" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Year</label><span style="color: red;"><b>*</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                                    ErrorMessage="Select Case year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
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
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label>
                                                        Court<span style="color: red;">*</span></label>
                                                    <asp:RequiredFieldValidator ID="RfvCourttype" ValidationGroup="Save"
                                                        ErrorMessage="Select Court type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddlCourtType" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Court Location</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvCourtlocation" ValidationGroup="Save"
                                                    ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCourtLocation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCourtLocation" runat="server" class="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label>
                                                              Case Subject<%--<span style="color: red;">*</span>--%></label>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCaseSubject" ValidationGroup="Save"
                                                        ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList ID="ddlCaseSubject" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Sub Subject</label>
                                                <asp:DropDownList runat="server" ID="ddlSubSubject" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="display: none;">
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label>Case Registration Date</label>
                                                    <asp:TextBox ID="txtDateOfCaseReg" date-provide="datepicker" runat="server" data-date-end-date="0d" AutoComplete="off" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    High Priority Case</label>
                                                <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
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
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case District Location<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="Save"
                                                        ErrorMessage="Select Case District Location" Text="<i class='fa fa-exclamation-circle' title='Select Case District Location'></i>"
                                                        ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="false" >
                                            <div class="form-group">
                                                <label>
                                                    Party Name</label>
                                                <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="false">
                                            <div class="form-group">
                                                <label>
                                                    Last Hearing Date</label>
                                                <asp:TextBox ID="txtDateOfLastHearing" runat="server" date-provide="datepicker" AutoComplete="off" data-date-end-date="0d" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    Case Matter Detail/Remarks<span style="color: red;">*</span></label>
                                                <asp:RequiredFieldValidator ID="rfvCasedetail" ValidationGroup="Save"
                                                    ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtCaseDetail" runat="server" class="form-control" placeholder="Remark" onkeyup="javascript:capFirst(this);" MaxLength="500" TextMode="MultiLine"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
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
                                                <asp:TextBox ID="txtPetiName" runat="server" placeholder="Name" AutoComplete="off" onkeypress="return lettersOnly();" onkeyup="javascript:capFirst(this);" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtPetiName"
                                                    ValidationExpression="^[a-zA-Z]+(([a-zA-Z\s])[a-zA-Z]*)*$" ValidationGroup="Petitioner" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
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
                                                <asp:DropDownList ID="ddlPetiDesigNation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>     
                                                    Mobile No.</label><%--<span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvMoNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtPetiMobileNo" ValidationGroup="Petitioner" ErrorMessage="Enter Mobile No."
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtPetiMobileNo" runat="server" placeholder="Mobile No." AutoComplete="off" CssClass="form-control MobileNo1" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                               <%-- <asp:RegularExpressionValidator ID="rfvPetiadvocatemobile" ValidationGroup="Petitioner" runat="server" Display="Dynamic" ControlToValidate="txtPetiMobileNo"
                                                    ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Present posting address<span style="color: red"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvPetiAdd" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                    ControlToValidate="txtPetiAddRess" ValidationGroup="Petitioner" ErrorMessage="Enter Present posting address"
                                                    Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtPetiAddRess" runat="server" placeholder="Address" AutoComplete="off" CssClass="form-control" TextMode="MultiLine" onkeyup="javascript:capFirst(this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnPetitioner" runat="server" CssClass="btn btn-primary btn-block" Text="Add" ValidationGroup="Petitioner" OnClick="btnPetitioner_Click" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdPetitionerDtl" runat="server" CssClass="table table-bordered " AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionername" runat="server" Text='<%# Eval("PetiName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignationname" runat="server" Text='<%# Eval("Designation_name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignation_ID" runat="server" Text='<%# Eval("Designation_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileno" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("AddRess") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Add Respondent Detail</legend>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Department</label>
                                                <asp:DropDownList ID="ddlResDepartment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlResDepartment_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>HOD</label>
                                                <asp:DropDownList ID="ddlHodName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Type</label><%--<span style="color: red;"><b> *</b></span>--%>
                                                <%--<asp:RequiredFieldValidator ID="rfvOffice" ValidationGroup="Responder"
                                                    ErrorMessage="Select Office type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOfficetypeName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlOfficetypeName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficetypeName_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Office Location</label><%--<span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Responder"
                                                    ErrorMessage="Select Office Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Designation</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Responder"
                                                    ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlDesignation" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Name</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="rfvname" ValidationGroup="Responder"
                                                    ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtResponderName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtResponderName" runat="server" placeholder="Name" onkeyup="javascript:capFirst(this);" CssClass="form-control" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtResponderName"
                                                    ValidationExpression="^[a-zA-Z]+(([a-zA-Z\s])?[a-zA-Z]*)*$" ValidationGroup="Responder" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Mobile No.</label>
                                                <asp:TextBox ID="txtMobileNo" onkeypress="return NumberOnly();" placeholder="Mobile No." runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Address</label>
                                                <asp:TextBox ID="txtAddress" runat="server" placeholder="Address" CssClass="form-control" onkeyup="javascript:capFirst(this);" AutoComplete="off" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1" style="padding-top: 2rem! important;">
                                            <asp:Button runat="server" CssClass="btn btn-primary btn-block" Text="Add" ID="btnYes" ValidationGroup="Responder" OnClick="btnYes_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="GrdRespondent" runat="server" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" CssClass="table table-bordered">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrnO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Dept_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDepartmentname" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="HOD">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHODID" runat="server" Text='<%# Eval("HOD_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblHODname" runat="server" Text='<%# Eval("HodName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficetypeID" runat="server" Text='<%# Eval("OfficeTypeID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblofficetypename" runat="server" Text='<%# Eval("OfficeTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfficeNameID" runat="server" Text='<%# Eval("OfficeNameId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblOfficename" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignationID" runat="server" Text='<%# Eval("DesignationId") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDesignationname" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Respondent Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("RespondentMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>New Case Document</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Document Name</label><%--<span style="color: red"><b> *</b></span>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDocumentName" ForeColor="Red" Display="Dynamic"
                                                            ControlToValidate="txtDocName" ValidationGroup="Add" ErrorMessage="Enter Document Name." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                        </asp:RequiredFieldValidator>--%>
                                                        <asp:TextBox ID="txtDocName" placeholder="Document Name" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" runat="server" MaxLength="50" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Documents</label><%--<span style="color: red"><b> *</b></span>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink4" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvDocument" ForeColor="Red" Display="Dynamic"
                                                            ControlToValidate="FileUpload10" ValidationGroup="Add" ErrorMessage="Select Document" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                        </asp:RequiredFieldValidator>--%>
                                                        <asp:FileUpload ID="FileUpload10" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-2 mt-3">
                                                    <div class="row">
                                                        <div class="col-md-6 mt-3">
                                                            <asp:Button ID="btnAddDoc" runat="server" Text="Add" ValidationGroup="Add" OnClick="btnAddDoc_Click" CssClass="btn btn-primary btn-block" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdViewDoc" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrnO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lblDocName" runat="server" NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" +  Eval("Document") %>' Target="_blank" CssClass="fa fa-eye"></asp:HyperLink>
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
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="Save" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return ValidatePage();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" CssClass="btn btn-block btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>

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
    <script type="text/javascript">
        $('#txtDateOfCaseReg').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#txtDateOfLastHearing').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        function OICDetailModal() {
            $("#OICDetailModal").modal('show');
        }
        function AdvocateDetailModal() {
            $("#AdvocateDetailModal").modal('show');
        }
        function AddRespondent() {
            $("#myModal01").modal('show');
        }
        function myModal() {
            $("#myModal").modal('show');
        }
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


        function checkHearingDetail() {
            debugger;

            var x = document.getElementById("txtDateOfReceipt").value; //This is a STRING, not a Date
            if (x != "") {
                var dateParts = x.split("/");   //Will split in 3 parts: day, month and year
                var xday = dateParts[0];
                var xmonth = dateParts[1];
                var xyear = dateParts[2];
                var xd = new Date(xyear, parseInt(xmonth, 10) - 1, xday);
            }
            else {
                var xd = "";
            }

            var y = document.getElementById("txtHearingDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtDateOfFiling").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];
                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }

            if (xd != "" && zd != "" && yd != "" || xd != "" && yd != "" || zd != " " && yd != " ") {
                if (xd >= yd && zd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt and Date Of Receipt ");
                    document.getElementById("txtDateOfReceipt").value = "";
                    document.getElementById("txtDateOfFiling").value = "";
                }
                else if (xd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt");
                    document.getElementById("txtDateOfReceipt").value = "";
                }
                else if (zd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt ");
                    document.getElementById("txtDateOfFiling").value = "";
                }
            }
        }
    </script>
    <script src="../js/ValidationJs.js"></script>


    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

