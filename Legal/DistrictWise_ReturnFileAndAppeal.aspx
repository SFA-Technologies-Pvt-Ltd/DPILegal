<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DistrictWise_ReturnFileAndAppeal.aspx.cs" Inherits="Legal_DistrictWise_ReturnFileAndAppeal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <div class="content">
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header">
                        District Wise Return File And Appeal
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Return File / Appeal</legend>
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
                                            <asp:ListItem Value="1">Return</asp:ListItem>
                                            <asp:ListItem Value="2">Reply</asp:ListItem>
                                            <asp:ListItem Value="3">Compliance</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 2rem" runat="server" id="divYasOrNo" visible="false">
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" ID="ddlYesOrNo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYesOrNo_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row" runat="server" id="divReason" visible="false">
                                <div class="col-md-8">
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
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Receipt No.</label>
                                            <span style="color: red"><b>*</b></span>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvReceipt" Display="Dynamic" ForeColor="Red"
                                                ErrorMessage="Enter Receipt No." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtReceiptNo" ValidationGroup="a">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" ID="txtReceiptNo" CssClass="form-control" AutoComplete="off" placeholder="Enter Receipt No"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Action Appealed From</label>
                                            <span style="color: red"><b>*</b></span>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvActionAppeal" Display="Dynamic" ForeColor="Red"
                                                ErrorMessage="Enter Action Appealed From." ControlToValidate="txtActionAppealedFrom" ValidationGroup="a"
                                                Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" ID="txtActionAppealedFrom" CssClass="form-control" placeholder="Enter Action Appealed From"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <legend>File Moment</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>HOD</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvHOD" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select HOD." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="a" ControlToValidate="ddlHODName" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlHODName" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Dispatch No.</label>
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
                                                <label>File Forward</label>
                                                <span style="color: red"><b>*</b></span>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvFileForward" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter File Forward." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ValidationGroup="a" ControlToValidate="txtFileForward">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtFileForward" CssClass="form-control" ReadOnly="true" Text="Sasan"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <label>HOD Remark</label>
                                            <span style="color:red"><b> *</b></span>
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
                                                <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" />
                                            </div>
                                            <div class="col-md-6">
                                                <a href="DistrictWise_ReturnFileAndAppeal.aspx" class="btn btn-secondary btn-block">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2" runat="server" id="DisposalStatus" visible="false">
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
                                       <%-- <span style="color: red; font-size: 13px; font-weight: 700;">Only PDF Files Accepted and size 200kb.</span>--%>
                                    </div>
                                </div>
                                <div class="col-md-1" id="HearingDtl_CaseDispose" runat="server" visible="false" style="padding-top: 2rem ! important;">
                                    <asp:Button ID="btnCaseDispose" runat="server" CssClass="btn btn-primary" ValidationGroup="CaseDispose" Text="Disposal" OnClick="btnCaseDispose_Click" />
                                </div>
                            </div>

                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        $('#txtDispatchDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        $('#txtReceiptDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
    </script>
</asp:Content>

