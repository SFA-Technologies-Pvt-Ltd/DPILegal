<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="EditWACaseDetails.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Legal_EditWACaseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <asp:ValidationSummary ID="VDS2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddResponder" />
    <asp:ValidationSummary ID="VDS3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDtl" />
    <asp:ValidationSummary ID="VDS4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDispose" />
    <asp:ValidationSummary ID="VDS5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Hearing" />
    <%-- Start Here Using POPUP Modal for Edit Responder --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Edit Respondent Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Respondent</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Type</label>
                                        <asp:DropDownList ID="ddlEditRespondertype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Name</label>
                                        <asp:TextBox ID="txtResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <asp:TextBox ID="txtResponderNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnYes" Style="margin-top: 20px; width: 80px;" OnClick="btnAddResponder_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Using POPUP Modal for Edit Responder --%>
    <%-- Start Here Edit Documents  ---%>
    <div class="modal fade" id="MymodalEditDocuments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 50%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left">Edit Documents</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Documents</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Documents Name </label>
                                        
                                        <asp:TextBox ID="txtEditDocumentsName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Document Upload</label>
                                        <asp:FileUpload ID="fileUpload2_EditDoc" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Edit Doc" ID="btnEditDocuments" Style="margin-top: 20px; width: 80px;" OnClick="btnSaveDoc_Click" />
                        <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Edit Documnets --%>

    <%-- Start Here Edit Hearing Dtl --%>
    <div class="modal fade" id="ModalEditHearingDtl" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left">Edit Hearing Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Hearing</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Date </label>
                                        <asp:TextBox ID="txtEditHearingDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-9" style="display: none;">
                                    <div class="form-group">
                                        <label>Hearing Detail</label>
                                        <asp:TextBox ID="txtEditHearingDtl" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Detail</label>
                                        <asp:DropDownList ID="ddleditHearing" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Next Hearing Date</asp:ListItem>
                                            <asp:ListItem Value="2">Not Hearing</asp:ListItem>
                                            <asp:ListItem Value="3">Dispose</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Document</label>
                                        <asp:FileUpload ID="FileUpEditHearigDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnEditHearing" Style="margin-top: 20px; width: 80px;" OnClick="btnSaveHearingDtl_Click" />
                        <asp:Button ID="btnHearing" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Edit Hearing Dtl --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div class="card-title" style="display: inline; float: left;">
                            WA/RP/SLP Case Detail
                        </div>
                        <div class="float-right">
                            <asp:LinkButton ID="lnkEditCaseDtl" runat="server" CssClass="btn-sm label label-success" OnClick="lnkEditCaseDtl_Click">Edit Case Detail</asp:LinkButton>
                            <asp:LinkButton ID="lnkAddResponderDtl" runat="server" CssClass="btn-sm label-info" OnClick="lnkAddResponderDtl_Click">Add Responder</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="lnkAddEditDoc" runat="server" CssClass="btn-sm label-danger" OnClick="lnkAddEditDoc_Click">Back</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body">
                        <%-- Start Here Bind Case && Petitioner Detail --%>
                        <fieldset id="FieldSet_CaseDetail" runat="server" visible="false">
                            <legend>Case Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <th>Particular</th>
                                            <th>WP Case Detail</th>
                                            <th>WA/RP/SLP Case Detail</th>
                                        </tr>
                                        <tr>
                                            <td>Case No.</td>
                                            <td>
                                                <asp:Label ID="lblRefCaseNO" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblRefWPCaseNo" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Type</td>
                                            <td>
                                                <asp:Label ID="lblWPCasetype" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWaCasetype" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Year</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseYear" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseYear" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Court Type</td>
                                            <td>
                                                <asp:Label ID="lblWPCourtType" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACourtType" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Petitioner Name</td>
                                            <td>
                                                <asp:Label ID="lblWPPetionerName" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAPetionerName" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Office Type</td>
                                            <td>
                                                <asp:Label ID="lblWPOfficeType" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOfficeType" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Office Name</td>
                                            <td>
                                                <asp:Label ID="lblWPOfficeName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOfficeName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>WP/WA Case No</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseNo" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseNo" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Nodal Officer Name</td>
                                            <td>
                                                <asp:Label ID="lblWPNOdalOfficerName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWANOdalOfficerName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Nodal Officer Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPNOdalOfficerMObile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWANOdalOfficerMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>OIC Name</td>
                                            <td>
                                                <asp:Label ID="lblWPOICNAme" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOICNAme" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>OIC Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPOICMobile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOICMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Advocate Name</td>
                                            <td>
                                                <asp:Label ID="lblWPAdvocateName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAAdvocateName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Advocate Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPAdvocateMobile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAAdvocateMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Subject</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseSubject" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseSubject" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Details</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseDtl" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseDtl" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Status</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseStatus" Font-Bold="true" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseStatus" Font-Bold="true" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Case && Petitioner Detail --%>
                        <%-- Start Here Bind Documnet Detail --%>
                        <fieldset id="FieldSet_DocumentDetail" runat="server" visible="false">
                            <legend>Case Document</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Document Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvAddDocumnet" ValidationGroup="Save"
                                            ErrorMessage="Enter Document Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDocumentName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Document Upload</label>
                                        <%-- <span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvUploadDoc" ValidationGroup="Save"
                                            ErrorMessage="Upload Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="FileUpload1" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                        <span style="color: red;">Document Should be 200Kb.</span>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSaveDoc" runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" Text="Upload Doc" OnClick="btnSaveDoc_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row pt-4">
                                <div class="col-md-9">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdCaseDoc" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false" DataKeyNames="WADoc_ID" OnRowCommand="GrdCaseDoc_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblWaCaseID" runat="server" Text='<%# Eval("WACase_ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblDocumentID" runat="server" Text='<%# Eval("WADoc_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyperViewDoc" runat="server" CssClass="btn btn-primary" Target="_blank" Enabled='<%#  Eval("DocPath").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/WaCaseDoc/" +  Eval("DocPath") %>'><i class="fa fa-eye"></i></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditDoc" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("WADoc_ID") %>' CommandName="EditDocument" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Document Detail --%>

                        <%-- Start Here Bind Responder Detail --%>
                        <fieldset id="FieldSet_ResponderDetail" runat="server" visible="false">
                            <legend>Responder Details</legend>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GrdResponderDtl" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="Responder_ID" AutoGenerateColumns="false" OnRowCommand="GrdResponderDtl_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblResponderID" runat="server" Text='<%# Eval("Responder_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("WACase_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Responder Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponderTypeName" runat="server" Text='<%# Eval("RespondertypeName") %>'></asp:Label>
                                                    <asp:Label ID="lblrespondertypeID" runat="server" Text='<%# Eval("Respondertype_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Responder Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponderName" runat="server" Text='<%# Eval("ResponderName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Responder Mobile No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponderNo" runat="server" Text='<%# Eval("ResponderNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Departent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartent" runat="server" Text='<%# Eval("ResponderDepartMent") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ResponderAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Responder_ID") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="btn btn-primary"><i class="fa fa-edit"</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Responder Detail --%>

                        <%--Start Here BInd Add add Hearing Dtl --%>
                        <fieldset id="Fieldset_HearingDtl" runat="server">
                            <legend>Hearing Detail</legend>
                            <div class="row float-right" style="margin: -2% 0 0 0;">
                                <asp:LinkButton ID="lnkbtnAddNewHering" runat="server" CssClass="btn-sm label label-success" OnClick="lnkbtnAddNewHering_Click">Add Hearing</asp:LinkButton>
                                  &nbsp;&nbsp;
                                               <asp:LinkButton ID="btnHearingBack" runat="server" CssClass="btn-sm label label-danger" OnClick="btnHearingBack_Click">Back</asp:LinkButton>
                            </div>
                            <div class="" id="AddNewHearing" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Hearing Date </label>
                                            <span style="color: red;"><b>* </b></span>
                                            <asp:RequiredFieldValidator ID="RfvHearingDate" ValidationGroup="Hearing"
                                                ErrorMessage="Enter Hearing Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtHearingDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtHearingDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-9" style="display: none;">
                                        <div class="form-group">
                                           <%-- <label>Hearing Detail</label><span style="color: red;"><b> * </b></span>
                                            <asp:RequiredFieldValidator ID="RfvHearingDtl" ValidationGroup="Hearing"
                                                ErrorMessage="Enter Hearing Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtHearingDtl" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:TextBox ID="txtHearingDtl" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Hearing Detail</label>
                                            <asp:DropDownList ID="ddlHearingDtl" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Next Hearing Date</asp:ListItem>
                                                <asp:ListItem Value="2">Not Hearing</asp:ListItem>
                                                <asp:ListItem Value="3">Dispose</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%--  </div>
                                <div class="row">--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Hearing Document</label>
                                            <%-- <asp:RequiredFieldValidator ID="RfvHearingDoc" ValidationGroup="Hearing"
                                                ErrorMessage="Enter Hearing Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="fileUpload_HearingDoc" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:FileUpload ID="fileUpload_HearingDoc" runat="server" CssClass="form-control" />
                                            <span style="color: red;">Document Should be 200Kb.</span>
                                        </div>
                                    </div>
                                    <div class="col-md-3 mt-5" >
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button ID="btnAddHearing" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Hearing" Text="Add" OnClick="btnAddHearing_Click" />
                                              
                                            </div>
                                            <%-- <div class="col-md-6">
                                            <a href="EditCaseDetail.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>--%>
                                        </div>
                                    </div>
                                </div>

                                <fieldset id="FieldSet_SaveHeringDtl" runat="server" visible="false">
                                    <legend>Hearing Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdHearingDetail" runat="server" AutoGenerateColumns="false" CssClass="table" BorderWidth="2px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowID" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("HearingDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Detail">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDetail" runat="server" Text='<%# Eval("HearingDetail") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Document">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lblHearingDoc" runat="server" Target="_blank" NavigateUrl='<%# "../Legal/WACaseHearingDoc/" + Eval("HearingDoc") %>' CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3">
                                        <div class="col-md-5"></div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnSaveHearingDtl" runat="server" CssClass="btn btn-primary btn-block" Text="Save Hearing" OnClick="btnSaveHearingDtl_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <fieldset id="FiledSet_HearingDBDtl" runat="server">
                                <legend>Hearings</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdHearingDtl" runat="server" CssClass="table" BorderWidth="2px" AutoGenerateColumns="false" DataKeyNames="WaNxtHearing_ID" OnRowCommand="GrdHearingDtl_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblHearingID" runat="server" Text='<%# Eval("WaNxtHearing_ID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblWACaseID" runat="server" Text='<%# Eval("WACase_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("NxtHearingDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Detail">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingDetail" runat="server" Text='<%# Eval("HearingDtl") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Document">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hplnkHearingDocument" runat="server" NavigateUrl='<%# "../Legal/WACaseHearingDoc/" + Eval("HearingDoc") %>' Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnEditHearing" runat="server" CssClass="btn btn-primary" CommandName="EditHearing" CommandArgument='<%# Eval("WaNxtHearing_ID") %>' ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-3">
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
                                <div class="col-md-3" id="OrderNoRemove" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order No.
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOrderNo" ValidationGroup="CaseDispose"
                                            ErrorMessage="Enter Order No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDispose_OrderNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDispose_OrderNo" onkeypress="return NumberOnly();" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="OrderBy1" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Case Disposal Date</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCaseDisposeDate" ValidationGroup="CaseDispose"
                                            ErrorMessage="Enter Case Disposal Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDisposeDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDisposeDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" id="DivOrderTimeline" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order Implement Timeline (In Days)
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="CaseDispose"
                                            ErrorMessage="Enter Case Disposal Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOrderimpletimeline" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <%-- <asp:TextBox ID="txtOrderimpletimeline" runat="server" CssClass="form-control" data-provide="datepicker" data-date-autoclose="true" data-date-format="dd/MM/yyyy" placeholder="DD/MM/YYYY">
                                        </asp:TextBox>--%>
                                        <asp:TextBox ID="txtOrderimpletimeline" runat="server" CssClass="form-control" onkeypress="return NumberOnly();"  AutoComplete="off" MaxLength="3">
                                        </asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3" id="OrderBy2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order Document
                                        </label>
                                        <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCaseDisposeOrderDoc" ValidationGroup="CaseDispose"
                                            ErrorMessage="Upload Order Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="FielUpcaseDisposeOrderDoc" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:FileUpload ID="FielUpcaseDisposeOrderDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                        <span style="color: red;">Document Should be 200Kb.</span>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                                <div class="col-md-3" id="ViewDoc_CaseDipose" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>View Document</label><br />
                                        <asp:HyperLink ID="hyPerlinkViewDisposeDoc" runat="server" Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                    </div>
                                </div>
                                <div class="col-md-3" id="HearingDtl_CaseDispose" runat="server" visible="false" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnCaseDispose" runat="server" CssClass="btn btn-primary" Text="Case Disposal" OnClick="btnCaseDispose_Click" ValidationGroup="CaseDispose" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <%--End Here BInd Add add Hearing Dtl --%>


                        <%-- Start Here Case Dispose Detail --%>
                        <fieldset id="Fieldset_CaseDispose" runat="server">
                            <legend>Case Disposal</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GrdCaseDispose" runat="server" BorderWidth="2px" CssClass="table text-center" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Case Disposal Status" DataField="CaseDisposeStatus" />
                                            <asp:BoundField HeaderText="Case Disposal Type" DataField="CaseDisposeType" />
                                            <asp:BoundField HeaderText=" Order Implement Timeline (in Days)" DataField="CaseDsiposeOrderNo" />
                                            <asp:BoundField HeaderText="Case Disposal Date" DataField="CaseDisposeDate" />
                                            <asp:TemplateField HeaderText="Order Document">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperlinkOrderDoc" runat="server" Target="_blank" NavigateUrl='<%# "WACaseDispose/" + Eval("CaseDisposeOrderDoc") %>' CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Case Dispose Detail --%>


                        <%--- Start Here For Edit Case Details ---%>
                        <fieldset id="Case_EditField" runat="server" visible="false">
                            <legend>Edit WA/RP/SLP Case Details</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>WP Case No.</label>
                                        <asp:Label ID="lblCaseNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>WA/RP/SLP Case No</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtWaCaseNo" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter WaCaseNo." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtWaCaseNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtWaCaseNo" runat="server" CssClass="form-control" onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Case Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCasetype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div> 
                                                             
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Petitioner Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvPetitionerName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Petitioner Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtPetitionerName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtPetitionerName" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Designation Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDesignation" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Court Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="refvddlCourttype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Court Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCourtType" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOfficetype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeType" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficeType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOfficeName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control" MaxLength="70" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Case Year
                                         <span style="color: red;"><b>*</b></span>
                                            <asp:RequiredFieldValidator ID="RfvCaseYear" ValidationGroup="CaseDtl"
                                                ErrorMessage="Select Case Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>                                                              
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Name</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvNOdalOfficerName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Nodal Officer Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtNOdalOfficerName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtNOdalOfficerName" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Mobile No.</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvNodalOfficerMobileNo" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Nodal Officer Mobile No.." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtNodalOfficerMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RexNodalOfficerMobileNo" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtNodalOfficerMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtNodalOfficerMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Email-ID</label>
                                        <asp:TextBox ID="txtNodalOfficerEmail" runat="server"  AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <%--<span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOICNAME" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Enter OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOicName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtOicName" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Mobile No.</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOicMobileNO" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter OIC Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOicMobileNO" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revOICMobileNo" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtOicMobileNO"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtOicMobileNO" runat="server" CssClass="form-control" onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Email-ID</label>
                                        <asp:TextBox ID="txtOICEmailID" runat="server"  AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Name</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvAdvocateName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Enter Advocate Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAdvocateName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtAdvocateName" runat="server" CssClass="form-control" MaxLength="80" AutoComplete="off" onkeypress="return chcode();"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Mobile No.</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvAdvocateMObile" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Advocate Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAdvocateMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="RexAdvocateMObile" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtAdvocateMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtAdvocateMobileNo" runat="server" onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Advocate Email-ID</label>
                                        <asp:TextBox ID="txtNodalEmailID" runat="server"  AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>High Priority Case</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select High  Priority Case." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlHighPriorityCase" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHighPriorityCase" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Case Subject</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control">
                                           <%-- <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">स्थानांतरण</asp:ListItem>
                                            <asp:ListItem Value="2">नियूक्ति</asp:ListItem>
                                            <asp:ListItem Value="3">प्रतिनियुक्ति</asp:ListItem>
                                            <asp:ListItem Value="4">पदोन्नति</asp:ListItem>
                                            <asp:ListItem Value="5">वेतन बृद्धि</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-12" style="display: none;">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                       <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtCaseSubject" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseSubject" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtCaseSubject" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Case Details</label><span style="color: red;"><b> *</b></span><br />
                                        <asp:RequiredFieldValidator ID="RFVActionByDistrict" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDetail" runat="server" TextMode="MultiLine" CssClass="form-control" AutoComplete="off" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-3" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpdateWaDtl" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="CaseDtl" Text="Save" OnClick="btnUpdateWaDtl_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Case Dispose</label><span style="color: red;"><b> *</b></span><br />
                                        <asp:RadioButtonList ID="rdCaseDispose" runat="server" CssClass="rbl form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdCaseDispose_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="caseDisposeYes" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Dispose Type
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvDisposeType" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Dispose Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDisponsType" Display="Dynamic" InitialValue="0" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDisponsType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDisponsType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="OrderBy1" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order No.
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOrderNo" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Order No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDispose_OrderNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDispose_OrderNo" onkeypress="return NumberOnly();" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="OrderBy2" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order Document
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCaseDisposeOrderDoc" ValidationGroup="CaseDtl"
                                            ErrorMessage="Upload Order Document." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="FielUpcaseDisposeOrderDoc" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:FileUpload ID="FielUpcaseDisposeOrderDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                            <%--- <div class="col-md-3" id="ViewDoc_CaseDipose" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>View Document</label><br />
                                        <asp:HyperLink ID="hyPerlinkViewDisposeDoc" runat="server" Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="CaseDtl" Text="Update" OnClick="btnUpdate_Click" />
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>--%>
                        </fieldset>

                    </div>

                    <%---End Here For Edit Case Details ---%>
                    <%---Start Here For Add Responder Details ---%>
                    <fieldset id="Field_AddResponder" runat="server" visible="false">
                        <legend>Add Respondent</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Respondent Type</label>
                                    <span style="color: red;"><b>*</b></span>

                                    <asp:RequiredFieldValidator ID="RFVResponderType" ValidationGroup="AddResponder"
                                        ErrorMessage="Enter Responder Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                        ControlToValidate="ddlResponderType" Display="Dynamic" runat="server" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlResponderType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Respondent Name</label>
                                    <span style="color: red;"><b>*</b></span>

                                    <asp:RequiredFieldValidator ID="RFVtxtAddDocumentName" ValidationGroup="AddResponder"
                                        ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                        ControlToValidate="txtAddResponderName" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Respondent Mobile No.</label>
                                    <span style="color: red;"><b>*</b></span>

                                    <asp:RequiredFieldValidator ID="RfvtxtAddResponderNo" ValidationGroup="AddResponder"
                                        ErrorMessage="Enter Responder No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                        ControlToValidate="txtAddResponderNo" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddResponderNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Department</label>
                                    <span style="color: red;"><b>*</b></span>
                                    <asp:RequiredFieldValidator ID="RfvtxtAddResponderDepartment" ValidationGroup="AddResponder"
                                        ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                        ControlToValidate="txtAddResponderDepartment" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddResponderDepartment" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Address</label>
                                    <span style="color: red;"><b>*</b></span>
                                    <asp:RequiredFieldValidator ID="RfvtxtAddResponderAddress" ValidationGroup="AddResponder"
                                        ErrorMessage="Enter Address Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                        ControlToValidate="txtAddResponderAddress" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtAddResponderAddress" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-top: 3%;">
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Button ID="btnAddResponder" Text="Add" runat="server" ValidationGroup="AddResponder" CssClass="btn btn-primary btn-block" OnClick="btnAddResponder_Click" />
                                    </div>
                                    <%--  <div class="col-md-6">
                                            <a href="EditCaseDetail.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <%---End Here For Add Responder Details ---%>
                    <%---Start Here For Add && Edit Documents ---%>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        function chcode() { // for only Character
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }

        function NumberOnly() {
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }
    </script>
</asp:Content>

