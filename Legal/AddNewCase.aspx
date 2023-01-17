<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="AddNewCase.aspx.cs" Inherits="Legal_AddNewCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }

        .pt-4 {
            padding-top: 2rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
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
                        <div class="float-left">
                            Add New Case
                        </div>
                        <div class="float-right">
                            <asp:Button ID="btnAddresponder" runat="server" CssClass="btn-sm label label-warning" Text="Add Responder" OnClick="btnAddresponder_Click" />
                        </div>

                    </div>
                    <div class="card-body">
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
                                        <asp:TextBox ID="txtCaseNo" runat="server" placeholder="Case No." class="form-control" AutoComplete="off" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Old Case No.</label>
                                            <asp:TextBox ID="txtCaseOldRefNo" runat="server" placeholder="Old Case No." AutoComplete="off" class="form-control" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                        </div>
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
                                        <label>Case Year</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                            ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Court Type<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="RfvCourttype" ValidationGroup="Save"
                                                ErrorMessage="Select Court type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCourtType" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3" id="DistrictCourtSelect" runat="server" visible="false">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                District<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="RfvCourtOfDistrict" ValidationGroup="Save"
                                                ErrorMessage="Select District Of Court." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlDistrictCourt" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDistrictCourt" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Case Subject<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="rfvCaseSubject" ValidationGroup="Save"
                                                ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCaseSubject" runat="server" class="form-control select2">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Case Registration Date</label>
                                            <asp:RequiredFieldValidator ID="rfvCaseRegisDate" ValidationGroup="Save"
                                                ErrorMessage="Enter Date Of Case Registration." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtDateOfCaseReg" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtDateOfCaseReg" date-provide="datepicker" runat="server" data-date-end-date="0d" data-date-start-date="0d" AutoComplete="off" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Last Hearing Date</label>
                                        <asp:RequiredFieldValidator ID="rfvLastHearingdate" ValidationGroup="Save"
                                            ErrorMessage="Enter Date Of Last Hearing." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDateOfLastHearing" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDateOfLastHearing" runat="server" date-provide="datepicker" AutoComplete="off" data-date-end-date="0d" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Location</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCourtlocation" ValidationGroup="Save"
                                            ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            High Priority Case</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvHighpriortiy" ValidationGroup="Save"
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
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Subject Detail<span style="color: red;">*</span></label>
                                        <asp:RequiredFieldValidator ID="rfvCasedetail" ValidationGroup="Save"
                                            ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDetail" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
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
                                                        <span style="color: red;"><b>*</b></span></label>
                                                    <asp:RequiredFieldValidator ID="rfvOicName" ValidationGroup="Save"
                                                        ErrorMessage="Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtOICName" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtOICName" runat="server" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label><span style="color: red;"><b> *</b></span>
                                                    <asp:RequiredFieldValidator ID="rfvoicMobile" ValidationGroup="Save"
                                                        ErrorMessage="Enter OIC Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtOICMobileNo" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtOICMobileNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
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
                                                        Advocate Name</label>
                                                    <asp:TextBox ID="txtDeptAdvocateName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                    <asp:TextBox ID="txtDeptAdvocateMobileNo" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="10"></asp:TextBox>
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
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Name</label>
                                                    <asp:TextBox ID="txtPetitionerAppName" runat="server" placeholder="Name" AutoComplete="off" CssClass="form-control" MaxLength="70" onkeypress="return validatename(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                    <asp:TextBox ID="txtPetitionerAppMobileNo" runat="server" placeholder="Mobile No" AutoComplete="off" CssClass="form-control MobileNo1" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>

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
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Name</label>
                                                    <asp:TextBox ID="txtPetitionerAdvName" runat="server" placeholder="Name" CssClass="form-control" MaxLength="50" AutoComplete="off" onkeypress="return validatename(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                    <asp:TextBox ID="txtPetitionerAdvMobileNo" runat="server" placeholder="Mobile" AutoComplete="off" CssClass="form-control MobileNo" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
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
                                                            <asp:Button ID="btnAddDoc" runat="server" Text="Add" CssClass="btn btn-primary btn-block" OnClick="btnAddDoc_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <fieldset>
                                                    <legend>View Doc</legend>

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="">
                                                                <asp:GridView ID="GrdViewDoc" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
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
                                                                                <asp:HyperLink ID="lblDocName" runat="server" NavigateUrl='<%# "../Legal/AddNewCaseDoc/" +  Eval("Document") %>' Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
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
                                    </fieldset>
                                    <fieldset>
                                        <legend>Hearing Detail</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Next Hearing Date</label>
                                                    <asp:TextBox ID="txtHearingDate" runat="server" date-provide="datepicker" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off" onchange="checkHearingDetail();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Hearing Document</label>
                                                    <asp:FileUpload ID="FileHearingDoc" runat="server" CssClass="form-control" />
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
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="Save" runat="server" Text="Save" OnClick="btnSubmit_Click" />
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


        $('#txtDateOfCaseReg').datepicker({
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


            if (msg != "") {
                alert(msg);
                return false;
            }
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

