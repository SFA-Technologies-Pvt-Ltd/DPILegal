<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="AddNewCase.aspx.cs" Inherits="Legal_AddNewCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }

        .pt-4
        {
            padding-top:2rem!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal01" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 60%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Add Respondent</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Add Respondent</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Type</label>
                                        <asp:DropDownList ID="ddlRespondertype" runat="server" CssClass="form-control">                                         
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <%--<asp:DropDownList ID="ddlDistrictForRespondent" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlOfficetypeName" runat="server" CssClass="form-control">                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Name</label>
                                        <asp:TextBox ID="txtResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <asp:TextBox ID="txtResponderNo" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <asp:TextBox ID="txtEditRespondentOICName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Mobile No.</label>
                                        <asp:TextBox ID="txtEditRepondentOICMObile" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC Email-ID</label>
                                        <asp:TextBox ID="txtEditRepondentOICEmail" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnYes" Style="margin-top: 20px; width: 80px;" />
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
                        <asp:Label ID="lbl" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Case Detail</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case No.<br />
                                            प्रकरण क्रमांक<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtCaseNo" runat="server" placeholder="Case No." class="form-control" AutoComplete="off" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                        <small><span id="valtxtCaseNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Old Case No.<br />
                                                पुराने प्रकरण का क्रमांक</label>
                                            <asp:TextBox ID="txtCaseOldRefNo" runat="server" placeholder="Old Case No." AutoComplete="off" class="form-control" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type<br />प्रकरण का प्रकार</label><span style="color: red;"><b>*</b></span>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Court Type<br />
                                                कोर्ट का प्रकार <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                            <small><span id="valddlCourtType" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                               
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Case Subject<br />
                                                प्रकरण का विषय<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlCaseSubject" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                            <small><span id="valddlCaseSubject" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Date of Case<br />
                                                प्रकरण की तिथि</label>
                                            <div class="input-group date">
                                               
                                                <asp:TextBox ID="txtDateOfReceipt" date-provide="datepicker" runat="server" AutoComplete="off" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtDateOfReceipt" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Last Hearing Date<br />
                                            पिछली सुनवाई की तिथि</label>
                                        <div class="input-group date">
                                            
                                            <asp:TextBox ID="txtDateOfLastHearing" runat="server" date-provide="datepicker" AutoComplete="off" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3" id="Hearing_Date" runat="server">
                                    <div class="form-group">
                                        <label>Next Hearing Date<br />(अगली सुनवाई की तिथि)</label>
                                        <div class="input-group date">
                                          
                                            <asp:TextBox ID="txtHearingDate" runat="server" date-provide="datepicker" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off" onchange="checkHearingDetail();"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtHearingDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                               
                            </div>
                            <div class="row">
                             <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Location<br />
                                            स्थान</label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control">
                                        </asp:DropDownList>
                                        <small><span id="valddlDistrict" class="text-danger"></span></small>
                                    </div>

                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            High Priority Case<br />
                                            उच्च प्राथमिकता का प्रकरण</label><span style="color: red;"><b>*</b></span>
                                        <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlHighprioritycase" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3 pt-5">
                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-8">
                                                <asp:Button ID="btnAddresponder" runat="server" CssClass="btn btn-info btn-block" Text="Add Responder" OnClick="btnAddresponder_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Subject Detail/
                                            प्रकरण की जानकारी<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtCaseDescription" Rows="4" runat="server"  class="form-control" TextMode="MultiLine"></asp:TextBox>
                                        <small><span id="valtxtCaseDescription" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Appointment of Officer Incharge</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        OIC
                                                        <br />
                                                        प्रभारी अधिकारी<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                    <small><span id="valOICName" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.<br />
                                                        मोबाइल नंबर</label>
                                                    <asp:TextBox ID="txtOICMobileNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <small><span id="valOICMobile" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Email<br />
                                                        ईमेल</label>
                                                    <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    <small><span id="valOICEmailID" class="text-danger"></span></small>
                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Appointment of Advocate</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Advocate Name<br />
                                                        वकील का नाम</label>
                                                    <asp:TextBox ID="txtDeptAdvocateName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                    <small><span id="valAdvocateName" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.<br />
                                                        मोबाइल नंबर</label>
                                                    <asp:TextBox ID="txtDeptAdvocateMobileNo" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <small><span id="valAdvocateMobile" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Email<br />
                                                        ईमेल</label>
                                                    <asp:TextBox ID="txtDeptAdvocateEmail" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    <small><span id="valAdvocateEmailId" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Petitioner / Applicant Details</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Name<br />
                                                        नाम</label>
                                                    <asp:TextBox ID="txtPetitionerAppName" runat="server" placeholder="Name" AutoComplete="off" CssClass="form-control" MaxLength="70" onkeypress="return validatename(event);"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAppName" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.<br />
                                                        मोबाइल नंबर</label>
                                                    <asp:TextBox ID="txtPetitionerAppMobileNo" runat="server" placeholder="Mobile No" AutoComplete="off" CssClass="form-control MobileNo1" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAppMobileNo" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Email<br />
                                                        ईमेल</label>
                                                    <asp:TextBox ID="txtPetitionerAppEmail" runat="server" placeholder="Email" AutoComplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAppEmail" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Address<br />
                                                        पता</label>
                                                    <asp:TextBox ID="txtPetitionerAppAddress" runat="server" placeholder="Address" CssClass="form-control" AutoComplete="off" MaxLength="200"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAppAddress" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Petitioner Advocate Details</legend>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Name<br />
                                                        नाम</label>
                                                    <asp:TextBox ID="txtPetitionerAdvName" runat="server" placeholder="Name" CssClass="form-control" MaxLength="50" AutoComplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAdvName" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.<br />
                                                        मोबाइल नंबर</label>
                                                    <asp:TextBox ID="txtPetitionerAdvMobileNo" runat="server" placeholder="Mobile" AutoComplete="off" CssClass="form-control MobileNo" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAdvMobileNo" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Email<br />
                                                        ईमेल</label>
                                                    <asp:TextBox ID="txtPetitionerAdvEmail" runat="server" placeholder="Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAdvEmail" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>
                                                        Address<br />
                                                        पता</label>
                                                    <asp:TextBox ID="txtPetitionerAdvAddress" runat="server" placeholder="Address" CssClass="form-control" AutoComplete="off" MaxLength="200"></asp:TextBox>
                                                    <small><span id="valtxtPetitionerAdvAddress" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Document Upload</legend>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-3" style="display: none;">
                                                        <div class="form-group">

                                                            <label>Document1</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>

                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" style="display: none;">
                                                        <div class="form-group">

                                                            <label>Document2</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink2" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" style="display: none;">
                                                        <div class="form-group">
                                                            <label>Document3</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink3" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                            <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Document Name</label>
                                                            <asp:TextBox ID="txtDocName" runat="server" MaxLength="50" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Documents</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink4" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                            <asp:FileUpload ID="FileUpload10" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnAddDoc" runat="server" Text="Add" CssClass="btn btn-primary btn-block" OnClick="btnAddDoc_Click"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <fieldset>
                                                    <legend>View Doc</legend>

                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <asp:GridView ID="GrdViewDoc" runat="server" CssClass="table table-bordered table-responsive" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrnO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Document Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="View">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lblDocName" runat="server" NavigateUrl='<%# "../AddNewCaseDoc" +  Eval("Document") %>' Target="_blank" CssClass="label label-primary">View</asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </fieldset>
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
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return validateCaseDetail();" />
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">


        $('#txtDateOfReceipt').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#txtDateOfLastHearing').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#txtHearingDate').datepicker({
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

        $('.MobileNo').blur(function () {
            debugger;
            var Obj = $('.MobileNo').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo').val('');
                    return false;
                }
            }
        });
        $('.MobileNo1').blur(function () {
            debugger;
            var Obj = $('.MobileNo1').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo1').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo1').val('');
                    return false;
                }
            }
        });
        $('.MobileNo2').blur(function () {
            debugger;
            var Obj = $('.MobileNo2').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo2').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo2').val('');
                    return false;
                }
            }
        });

        function validateCaseDetail() {
            debugger;
            var msg = "";
            var Hearing_Date = document.getElementById('<%=txtHearingDate.ClientID%>');
            $("#valtxtCaseNo").html("");
            $("#valddlCourtType").html("");
            $("#valddlDistrict").html("");
            $("#valddlCaseSubject").html("");
            $("#valddloffice").html("");
            $("#valtxtDate").html("");
            $("#valtxtDateOfLastHearing").html("");
            $("#valddlHighprioritycase").html("");
            $("#valOICName").html("");
            $("#valvalOICEmailID").html("");
            $("#valOICMobile").html("");
            $("#valvalAdvocateName").html("");
            $("#valvalAdvocateEmailID").html("");
            $("#valAdvocateMobile").html("");
            $("#valtxtCaseDescription").html("");
            //$("#valddlOIC").html(" ");
            //$("#valddlAdvocate").html("");
            $("#valtxtPetitionerAppName").html("");
            $("#valtxtPetitionerAppMobileNo").html("");
            $("#valtxtPetitionerAppEmail").html("");
            $("#valtxtPetitionerAppAddress").html("");
            $("#valtxtPetitionerAdvName").html("");
            $("#txtPetitionerAdvMobileNo").html("");
            $("#valtxtPetitionerAdvEmail").html("");
            $("#valtxtPetitionerAdvAddress").html("");
          <%--  $("#valtxtHearingDate").html("");
            if (document.getElementById('<%=ddloffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office(Supervision By). \n";
                $("#valddloffice").html("Select Office(Supervision By)");
            }--%>
            if (document.getElementById('<%=txtCaseNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Case Number. \n";
                $("#valtxtCaseNo").html("Enter Case Number");
            }
            if (document.getElementById('<%=txtOICName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter OIC Name. \n";
                $("#valOICName").html("Enter OIC Name");
            }
            if (document.getElementById('<%=txtOICEmail.ClientID%>').value.trim() == "") {
                msg = msg + "Enter OIC Email-ID. \n";
                $("#valvalOICEmailID").html("Enter OIC Email-ID");
            }
            if (document.getElementById('<%=txtOICMobileNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter OIC Mobile No. \n";
                $("#valOICMobile").html("Enter OIC Mobile No.");
            }
            if (document.getElementById('<%=ddlCourtType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Court Type. \n";
                $("#valddlCourtType").html("Select Court Type");
            }

            if (document.getElementById('<%=ddlDistrict.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select District. \n";
                $("#valddlDistrict").html("Select District");
            }
            if (document.getElementById('<%=ddlHighprioritycase.ClientID%>').selectedIndex == 0) {

                msg = msg + "Select High Priority Case Type. \n";
                $("#valddlHighprioritycase").html("Select High Priority Case Type");
            }

            if (document.getElementById('<%=ddlCaseSubject.ClientID%>').selectedIndex == 0) {

                msg = msg + "Select Case Subject. \n";
                $("#valddlCaseSubject").html("Select Case Subject");
            }
            if (document.getElementById('<%=txtDateOfReceipt.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date Of Receipt. \n";
                $("#valtxtDateOfReceipt").html("Enter Date of Case");
            }
            if (document.getElementById('<%=txtDateOfLastHearing.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date of Filling. \n";
                $("#valtxtDate").html("Enter last Hearing Date");
            }
            if (document.getElementById('<%=txtCaseDescription.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject Of Case. \n";
                $("#valtxtCaseDescription").html("Enter Subject Of Case");
            }
            if (document.getElementById('<%=txtPetitionerAppMobileNo.ClientID%>').value.trim() != "") {
                if (document.getElementById('<%=txtPetitionerAppMobileNo.ClientID%>').value.length != 10) {
                    msg += "Enter Correct Petitioner/Applicant  Mobile No. \n";
                    $("#valtxtPetitionerAppMobileNo").html("Enter Correct Petitioner/Applicant  Mobile No ");
                }
            }
            if (document.getElementById('<%=txtPetitionerAppEmail.ClientID%>').value.trim() != "") {
                if (document.getElementById('<%=txtPetitionerAppEmail.ClientID%>').value.trim() != "") {
                    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

                    if (reg.test(document.getElementById('<%=txtPetitionerAppEmail.ClientID%>').value) == false) {
                        msg = msg + "Please Enter Valid Petitioner/Applicant Email Address. \n";
                        $("#valtxtPetitionerAppEmail").html("Please Enter Valid Petitioner/Applicant Email Address");
                    }
                }
            }
            if (document.getElementById('<%=txtPetitionerAdvMobileNo.ClientID%>').value.trim() != "") {
                if (document.getElementById('<%=txtPetitionerAdvMobileNo.ClientID%>').value.length != 10) {
                    msg += "Enter Correct Petitioner/Advocate  Mobile No. \n";
                    $("#valtxtPetitionerAdvMobileNo").html("Enter Correct Petitioner/Advocate  Mobile No ");
                }
            }
            if (document.getElementById('<%=txtPetitionerAdvEmail.ClientID%>').value.trim() != "") {
                if (document.getElementById('<%=txtPetitionerAdvEmail.ClientID%>').value.trim() != "") {
                    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;

                    if (reg.test(document.getElementById('<%=txtPetitionerAdvEmail.ClientID%>').value) == false) {
                        msg = msg + "Please Enter  Valid Petitioner/Advocate Email  Address. \n";
                        $("#valtxtPetitionerAdvEmail").html("Please Enter  Valid Petitioner/Advocate Email  Address ");
                    }
                }
            }
            if (document.getElementById('<%=txtPetitionerAppName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Petitioner/Applicant Name. \n";
                $("#valtxtPetitionerAppName").html("Enter Petitioner/Applicant Name ");
            }

            if (document.getElementById('<%=txtPetitionerAdvName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Petitioner/Advocate Name. \n";
                $("#valtxtPetitionerAdvName").html("Enter Petitioner/Advocate Name ");
            }

            if (document.getElementById('<%=txtPetitionerAdvAddress.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Petitioner/Advocate Address. \n";
                $("#valtxtPetitionerAdvAddress").html("Enter Petitioner/Advocate Address ");
            }

           <%-- if (Hearing_Date != null) {
                if (document.getElementById('<%=txtHearingDate.ClientID%>').value.trim() == "") {
                    msg = msg + "Select  Hearing Date. \n";
                    $("#valtxtHearingDate").html("Select  Hearing Date ");
                }
            }--%>

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        }
        function validateAdvocateDetail() {
            var msg = "";
            $("#valtxtAdvocate_Name").html("");
            $("#valtxtAdvocate_MobileNo").html("");
            $("#txtAdvocate_Email").html("");
            if (document.getElementById('<%=txtDeptAdvocateName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Advocate / CA Name. \n";
                $("#valtxtAdvocate_Name").html("Enter Advocate / CA Name");
            }
            if (document.getElementById('<%=txtDeptAdvocateMobileNo.ClientID%>').value.trim() == "") {
                msg += "Enter Advocate / CA Mobile No. \n";
                $("#valtxtAdvocate_MobileNo").html("Enter Advocate / CA  Mobile No");
            }
            else if (document.getElementById('<%=txtDeptAdvocateMobileNo.ClientID%>').value.length != 10) {
                msg += "Enter  Correct Advocate / CA Mobile No. \n";
                $("#valtxtAdvocate_MobileNo").html("Enter Correct Advocate / CA Mobile No");
            }
            if (document.getElementById('<%=txtDeptAdvocateEmail.ClientID%>').value.trim() != "") {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;

                if (reg.test(document.getElementById('<%=txtDeptAdvocateEmail.ClientID%>').value) == false) {
                    msg = msg + "Please Enter Valid Email Address. \n";
                    $("#valtxtAdvocate_Email").html("Please Enter Valid Email Address");
                }
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
           <%-- else {
                if (document.getElementById('<%=btnAdvocateSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (document.getElementById('<%=btnAdvocateSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }--%>
        }

        function checkHearingDetail() {
            debugger;
            //var DateofReceipt = new Date(document.getElementById("txtDateOfReceipt"));
            ////Dateoffiling = Date.parse(document.getElementById("txtDateOfFiling").value);
            //var HearingDate = new Date(document.getElementById("txtHearingDate"));
            ////var DateofReceipt = document.getElementById("txtDateOfReceipt").value;
            ////var Dateoffiling = document.getElementById("txtDateOfFiling").value;
            ////var HearingDate = document.getElementById("txtHearingDate").value;

            //if (DateofReceipt != "" && HearingDate != "")
            //{


            //    if (DateofReceipt > HearingDate)
            //    {
            //        alert("Hearing Date should be greater than DateofReceipt ");

            //    }
            //    //else if (DOF > HDate) {
            //    //    alert("Hearing Date should be greater than DateofFiling ");
            //    //    return false;
            //    //}
            //}
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
</asp:Content>

