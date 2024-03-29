﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="AddNewCase.aspx.cs" Inherits="Legal_AddNewCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-primary">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Case Registration</h3>
                    <asp:Label ID="lbl" runat="server" Text="" CssClass="text-danger"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Select Office (Supervision By)</label><span class="text-danger">*</span>
                                <asp:DropDownList runat="server" ID="ddloffice" CssClass="form-control select2" ClientIDMode="static" OnSelectedIndexChanged="ddloffice_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddloffice" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>Case Detail</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Case No.(प्रकरण क्रमांक)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtCaseNo" runat="server" placeholder="Case No." class="form-control" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                    <small><span id="valtxtCaseNo" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Old Case No. ( पुराने प्रकरण का क्रमांक)</label>
                                        <asp:TextBox ID="txtCaseOldRefNo" runat="server" placeholder="Old Case No." class="form-control" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Court Type (कोर्ट का प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Consumer Court</asp:ListItem>
                                            <asp:ListItem Value="2">Labour Court</asp:ListItem>
                                            <asp:ListItem Value="3">District Court</asp:ListItem>
                                            <asp:ListItem Value="4">High Court - Jabalpur</asp:ListItem>
                                            <asp:ListItem Value="5">High Court -  Indore</asp:ListItem>
                                            <asp:ListItem Value="6">High Court - Gwalior</asp:ListItem>
                                            <asp:ListItem Value="7">Supreme Court</asp:ListItem>
											<asp:ListItem Value="8">Industrial Court</asp:ListItem>
											<asp:ListItem Value="9">Labour Office</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlCourtType" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>District (जिला)</label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control select2">
                                        </asp:DropDownList>
                                        <small><span id="valddlDistrict" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>Case Category (प्रकरण का प्रकार)<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" class="form-control select2">
                                            <asp:ListItem >Select</asp:ListItem>
                                            <asp:ListItem>Civil Case</asp:ListItem>
                                            <asp:ListItem>Consumer Case</asp:ListItem>
                                            <asp:ListItem>Criminal Case</asp:ListItem>
                                            <asp:ListItem>Income tax Case</asp:ListItem>
                                            <asp:ListItem>GST Case</asp:ListItem>
                                            <asp:ListItem>Service Master</asp:ListItem>
											<asp:ListItem>Others</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlCaseType" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" visible="false">
                                <div class="form-group">
                                    <label>Subject of Case (प्रकरण के विषय)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtSubjectOfCase" runat="server" Text="SubjectOfCase" placeholder="Subject of Case" class="form-control" MaxLength="50" Visible="false"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Department Concerned (विभाग से संबंधित)</label>
                                    <asp:TextBox ID="txtDepartmentConcerned" runat="server" placeholder="Department Concerned" MaxLength="200" class="form-control"></asp:TextBox>
                                    <small><span id="valtxtDepartmentConcerned" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label>
                                            Date of Receiving<br />
                                            (प्राप्ति की तारीख)</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateOfReceipt" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" class="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtDateOfReceipt" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date of Filing & Reply (फाइलिंग की तारीख)</label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDateOfFiling" runat="server" date-provide="datepicker" placeholder="DD/MM/YYYY" class="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <small><span id="valtxtDateOfFiling" class="text-danger"></span></small>
                                </div>
                            </div>
                           
                        </div>
                        <div class="row">
                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Interim Order
                                        <br />
                                        (अंतरिम आदेश)</label>
                                    <asp:TextBox ID="txtInterimOrder" runat="server" placeholder="Interim Order" class="form-control" MaxLength="50"></asp:TextBox>
                                    <small><span id="valtxtInterimOrder" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Final Order<br />
                                        (अंतिम आदेश)</label>
                                    <asp:TextBox ID="txtFinalOrder" runat="server" placeholder="Final Order" class="form-control" MaxLength="50"></asp:TextBox>
                                    <small><span id="valtxtFinalOrder" class="text-danger"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Claim Amount/Pray For Relief (दावा राशि/राहत के लिए प्रार्थना)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtClaimAmount" runat="server" MaxLength="50" placeholder="Claim Amount/Pray For Relief" CssClass="form-control"></asp:TextBox>
                                    <small><span id="valtxtClaimAmount" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Case FiledBy<br />
                                        (मामले द्वारा दायर)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtCaseFiledBy" runat="server" placeholder="Case FiledBy" class="form-control" MaxLength="50"></asp:TextBox>
                                <small><span id ="valtxtCaseFiledBy" class="text-danger"></span></small>
                                </div>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Subject Of Case (प्रकरण के विषय)<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtCaseDescription" Rows="4" runat="server" placeholder="Subject Of Case" class="form-control" TextMode="MultiLine"></asp:TextBox>
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
                                                <%--<label>OIC&nbsp;&nbsp;&nbsp;[<a href="OICRagistration.aspx">Add OIC</a>]<span style="color: red;">*</span></label>--%>
                                                <label>OIC (प्रभारी अधिकारी)<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlOIC" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlOIC_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlOIC" class="text-danger"></span></small>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Department(विभाग)</label>
                                                <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Designation (पद)</label>
                                                <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Email (ईमेल)</label>
                                                <asp:TextBox ID="txtOICEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Mobile No. (मोबाइल नंबर)</label>
                                                <asp:TextBox ID="txtOICMobileNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                        </div>
                        <div class="col-md-6">
                            <div class="box-body">
                                <fieldset>
                                    <legend>Appointment of Advocate / CA Details</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Advocate/CA (अधिवक्ता / सीए) &nbsp;&nbsp;[<asp:LinkButton ID="lnkbtnAdvocate" runat="server" OnClick="lnkbtnAdvocate_Click">Add Advocate/CA]</asp:LinkButton><span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlAdvocate" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlAdvocate_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlAdvocate" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Name (नाम)</label>
                                                <asp:TextBox ID="txtAdvocateName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Mobile No. (मोबाइल नंबर)</label>
                                                <asp:TextBox ID="txtAdvocateMobileNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Email (ईमेल)</label>
                                                <asp:TextBox ID="txtAdvocateEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Address (पता)</label>
                                                <asp:TextBox ID="txtAdvocateAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
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
                                                <label>Name (नाम)</label>
                                                <asp:TextBox ID="txtPetitionerAppName" runat="server" placeholder="Name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                                <small><span id="valtxtPetitionerAppName" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Mobile No. (मोबाइल नंबर)</label>
                                                <asp:TextBox ID="txtPetitionerAppMobileNo" runat="server" placeholder="Mobile No" CssClass="form-control MobileNo1" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
                                                <small><span id="valtxtPetitionerAppMobileNo" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Email (ईमेल)</label>
                                                <asp:TextBox ID="txtPetitionerAppEmail" runat="server" placeholder="Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                <small><span id="valtxtPetitionerAppEmail" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Address (पता)</label>
                                                <asp:TextBox ID="txtPetitionerAppAddress" runat="server" placeholder="Address" CssClass="form-control" MaxLength="200"></asp:TextBox>
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
                                                <label>Name(नाम)</label>
                                                <asp:TextBox ID="txtPetitionerAdvName" runat="server" placeholder="Name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                                <small><span id="valtxtPetitionerAdvName" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Mobile No. (मोबाइल नंबर)</label>
                                                <asp:TextBox ID="txtPetitionerAdvMobileNo" runat="server" placeholder="Mobile" CssClass="form-control MobileNo" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
                                                <small><span id="valtxtPetitionerAdvMobileNo" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Email (ईमेल)</label>
                                                <asp:TextBox ID="txtPetitionerAdvEmail" runat="server" placeholder="Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                <small><span id="valtxtPetitionerAdvEmail" class="text-danger"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Address (पता)</label>
                                                <asp:TextBox ID="txtPetitionerAdvAddress" runat="server" placeholder="Address" CssClass="form-control" MaxLength="200"></asp:TextBox>
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
                                        <div class="col-md-3">
                                            <div class="form-group">

                                                <label>Document1</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>

                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">

                                                <label>Document2</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink2" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Document3</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink3" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3" id="Hearing_Date" runat="server">
                                            <div class="form-group">
                                                <label>Hearing Date (सुनवाई तिथि)</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtHearingDate" runat="server" date-provide="datepicker" placeholder="DD/MM/YYYY"  class="form-control" ClientIDMode="Static" autocomplete="off" onchange="checkHearingDetail();"></asp:TextBox>
                                                </div>
                                                <small><span id="valtxtHearingDate" class="text-danger"></span></small>
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
                                <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSubmit_Click" OnClientClick="return validateCaseDetail();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" CssClass="btn btn-block btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                        <div class="col-md-4"></div>
                        <!--OIC Detail Modal-->

                        <!--Advocate / CA Detail Modal-->
                        <div class="row">
                            <div class="col-md-12">
                                <div class="modal fade" id="AdvocateDetailModal" role="dialog">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Advocate/CA Detail</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="box box-success">
                                                            <div class="box-header">
                                                                <h3 class="box-title">Advocate / CA Detail</h3>
                                                                <asp:Label runat="server" ID="lbladvocatemsg" Text=""></asp:Label>
                                                            </div>
                                                            <div class="box-body">
                                                                <div id="Div1" runat="server">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label>Advocate / CA Name<span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtAdvocate_Name" runat="server" placeholder="Advocate / CA Name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                                                                <small><span id="valtxtAdvocate_Name" class="text-danger"></span></small>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label>Advocate / CA Mobile No.<span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtAdvocate_MobileNo" runat="server" placeholder="Advocate / CA Mobile No" ClientIDMode="Static" CssClass="form-control MobileNo2" MaxLength="10" onkeypress='javascript:tbx_fnNumeric(event, this);'></asp:TextBox>
                                                                                <small><span id="valtxtAdvocate_MobileNo" class="text-danger"></span></small>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label>Advocate / CA Email</label>
                                                                                <asp:TextBox ID="txtAdvocate_Email" runat="server" placeholder="Advocate / CA Email" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                                <small><span id="valtxtAdvocate_Email" class="text-danger"></span></small>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label>Advocate / CA Address</label>
                                                                                <asp:TextBox ID="txtAdvocate_Address" runat="server" placeholder="Advocate / CA Address" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-4"></div>
                                                                        <div class="col-md-2">
                                                                            <div class="form-group">
                                                                                <asp:Button ID="btnAdvocateSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnAdvocateSave_Click" OnClientClick="return validateAdvocateDetail();" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <div class="form-group">
                                                                                <asp:Button ID="btnAdvocateClear" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnAdvocateClear_Click" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4"></div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:Label runat="server" ID="lblGridAdvocateMsg" ForeColor="Red" Font-Size="Medium" Text=""></asp:Label>
                                                                        <asp:GridView ID="GridViewAdvocateDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Advocate_ID" OnSelectedIndexChanged="GridViewAdvocateDetail_SelectedIndexChanged" OnRowDeleting="GridViewAdvocateDetail_RowDeleting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Advocate_Name" HeaderText="Advocate Name" />
                                                                                <asp:BoundField DataField="Advocate_MobileNo" HeaderText="Advocate MobileNo" />
                                                                                <asp:BoundField DataField="Advocate_Email" HeaderText="Advocate Email" />
                                                                                <asp:BoundField DataField="Advocate_Address" HeaderText="Advocate Address" />
                                                                                <asp:TemplateField HeaderText="Edit">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-info" CommandName="Select">Edit</asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" CssClass="label label-danger" CommandName="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="modal-footer">

                                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Modal Header</h4>
                                    </div>
                                    <div class="modal-body">
                                        <p>Some text in the modal.</p>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
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

        $('#txtDateOfFiling').datepicker({
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
            $("#valddlCaseType").html("");
            $("#valddloffice").html("");
            //$("#valtxtDateOfReceipt").html("");
            //$("#valtxtDateOfFiling").html("");
            //$("#valtxtInterimOrder").html("");
            //$("#valtxtFinalOrder").html("");
            $("#valtxtClaimAmount").html("");
            $("#valtxtCaseDescription").html("");
            $("#valddlOIC").html(" ");
            $("#valddlAdvocate").html("");
            //$("#valtxtPetitionerAppName").html("");
            //$("#valtxtPetitionerAppMobileNo").html("");
            //$("#valtxtPetitionerAppEmail").html("");
            //$("#valtxtPetitionerAppAddress").html("");
            //$("#valtxtPetitionerAdvName").html("");
            //$("#txtPetitionerAdvMobileNo").html("");
            //$("#valtxtPetitionerAdvEmail").html("");
            //$("#valtxtPetitionerAdvAddress").html("");
            $("#valtxtHearingDate").html("");
            if (document.getElementById('<%=ddloffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office(Supervision By). \n";
                $("#valddloffice").html("Select Office(Supervision By)");
            }
            if (document.getElementById('<%=txtCaseNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Case Number. \n";
                $("#valtxtCaseNo").html("Enter Case Number");
            }
            if (document.getElementById('<%=ddlCourtType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Court Type. \n";
                $("#valddlCourtType").html("Select Court Type");
            }
            if (document.getElementById('<%=ddlCourtType.ClientID%>').selectedIndex != 0)
            {
                var CourtType = document.getElementById('<%=ddlCourtType.ClientID%>').selectedIndex;
                if (CourtType == 3)
                {
                    if (document.getElementById('<%=ddlDistrict.ClientID%>').selectedIndex == 0) {
                        msg = msg + "Select District. \n";
                        $("#valddlDistrict").html("Select District");
                    }
                }
            }
           
            if (document.getElementById('<%=ddlCaseType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Case Category. \n";
                $("#valddlCaseType").html("Select Case Category");
            }
            <%--if (document.getElementById('<%=txtSubjectOfCase.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject of Case. \n";
            }--%>
            <%-- if (document.getElementById('<%=txtDepartmentConcerned.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Department Concerned. \n";
                $("#valtxtDepartmentConcerned").html("Enter Department Concerned");
            }--%>
            <%--if (document.getElementById('<%=txtDateOfReceipt.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date Of Receipt. \n";
                $("#valtxtDateOfReceipt").html("Select Date Of Receipt");
            }
            if (document.getElementById('<%=txtDateOfFiling.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date of Filling. \n";
                $("#valtxtDateOfFiling").html("Select Date of Filling and Reply");
            }
            if (document.getElementById('<%=txtInterimOrder.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Interim Order. \n";
                $("#valtxtInterimOrder").html("Enter Interim Order");

            }

            if (document.getElementById('<%=txtFinalOrder.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Final order. \n";
                $("#valtxtFinalOrder").html("Enter Final order");
            }--%>
            if (document.getElementById('<%=txtClaimAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Claim Amount/Pray For Relief. \n";
                $("#valtxtClaimAmount").html("Enter Claim Amount/Pray For Relief");
            }

            if (document.getElementById('<%=txtCaseDescription.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject Of Case. \n";
                $("#valtxtCaseDescription").html("Enter Subject Of Case");
            }
           <%-- if (document.getElementById('<%=ddlOIC.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select OIC . \n";
                $("#valddlOIC").html("Select OIC ");
            }--%>
            if (document.getElementById('<%=ddlAdvocate.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Advocate/CA. \n";
                $("#valddlAdvocate").html("Select Advocate/CA ");
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

           <%-- if (document.getElementById('<%=txtPetitionerAppName.ClientID%>').value.trim() == "") {
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
            }--%>

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
            if (document.getElementById('<%=txtAdvocate_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Advocate / CA Name. \n";
                $("#valtxtAdvocate_Name").html("Enter Advocate / CA Name");
            }
            if (document.getElementById('<%=txtAdvocate_MobileNo.ClientID%>').value.trim() == "") {
                msg += "Enter Advocate / CA Mobile No. \n";
                $("#valtxtAdvocate_MobileNo").html("Enter Advocate / CA  Mobile No");
            }
            else if (document.getElementById('<%=txtAdvocate_MobileNo.ClientID%>').value.length != 10) {
                msg += "Enter  Correct Advocate / CA Mobile No. \n";
                $("#valtxtAdvocate_MobileNo").html("Enter Correct Advocate / CA Mobile No");
            }
            if (document.getElementById('<%=txtAdvocate_Email.ClientID%>').value.trim() != "") {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;

                if (reg.test(document.getElementById('<%=txtAdvocate_Email.ClientID%>').value) == false) {
                    msg = msg + "Please Enter Valid Email Address. \n";
                    $("#valtxtAdvocate_Email").html("Please Enter Valid Email Address");
                }
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
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

