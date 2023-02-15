<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="EditOld_PendingCases.aspx.cs" Inherits="Legal_EditOld_PendingCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
    <style>
        table tbody tr td a {
            font-size: 17px;
            padding-right: 10px;
        }

        table tbody tr td span {
            font-size: 20px;
            padding-right: 10px;
        }

        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            transform: translate3d(0px, 0px, 0px) !important;
            padding: 2px !important;
            max-height: 18em !important;
            overflow-y: auto !important;
            /*#EC8712*/
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        .form-controlSearchBox {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #CED4DA;
            /*border-radius: 0.25rem;*/
            box-shadow: inset 0 0 0 transparent;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .multiselect-native-select button {
            border-radius: 0.5em;
            border-color: #579FBB !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Edit Old Pending Case
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Edit Detail
                            </legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Filling No</label>
                                        <asp:TextBox ID="txtFilingNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Court</label>
                                        <asp:TextBox ID="txtCourt" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Petitionar</label>
                                        <asp:TextBox ID="txtPetitioner" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Office</label>
                                        <asp:ListBox runat="server" ID="ddlRespondentOffice" showcheckbox="true" SelectionMode="Multiple" ClientIDMode="Static" AppendDataBoundItems="true"
                                            CssClass="form-control multiselect"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <asp:DropDownList ID="ddlOICNameOpen" runat="server" OnSelectedIndexChanged="ddlOICNameOpen_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC Mobile No</label>
                                        <asp:TextBox ID="txtOICMobileNoOpen" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                        <asp:DropDownList ID="ddlCaseSubject" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Sub Subject</label>
                                        <asp:DropDownList ID="ddlCaseSubSubject" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList ID="ddlIsComplaince" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Select" />
                                            <asp:ListItem Text="Yes" />
                                            <asp:ListItem Text="No" />
                                            <asp:ListItem Text="Pending" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Type</label>
                                        <asp:TextBox ID="txtRespondent" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-block" Text="Update" OnClick="btnUpdate_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Old Case No</label>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtCaseNo" Autocomplete="Off" ValidationGroup="Ent" runat="server" ForeColor="Red" />
                                        <asp:TextBox ID="txtCaseNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="ddlYear" ValidationGroup="Ent" runat="server" InitialValue="0" ForeColor="Red" />
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>CaseType</label>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="ddlCaseType" ValidationGroup="Ent" runat="server" InitialValue="0" ForeColor="Red" />
                                        <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Court</label>
                                        <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="ddlCourt" ValidationGroup="Ent" runat="server" InitialValue="0" ForeColor="Red" />
                                        <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label>केस का विवरण</label><br />
                                    <asp:FileUpload ID="FU1" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>कार्यवाही का विवरण</label><br />
                                    <asp:FileUpload ID="FU2" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>निर्णय</label><br />
                                    <asp:FileUpload ID="FU3" runat="server" />
                                </div>
                                <div class="col-md-3">
                                    <label>अन्य</label><br />
                                    <asp:FileUpload ID="FU4" runat="server" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Button Text="Submit" ID="btnSubmit" runat="server" CssClass="btn btn-primary" ValidationGroup="Ent" OnClick="btnSubmit_Click" />
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
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $('[id*=ddlRespondentOffice]').multiselect({
            includeSelectAllOption: true,
            buttonWidth: '100%',
        });
    </script>
</asp:Content>

