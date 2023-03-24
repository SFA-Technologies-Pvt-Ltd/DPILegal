<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="Legal_UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        /*.datepicker tbody {
            background-color: #ecfce6 !important;
            color: black;
        }

        .datepicker th {
            background-color: #608640 !important;
        }*/

        .label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
    <style>
        label {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>

                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">User Registration</h3>
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Fill Details</legend>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Office Type<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvofficetype" ValidationGroup="Save"
                                                ErrorMessage="Select Office Type." InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlofficetype" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlofficetype" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlofficetype_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Office Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvOfficeName" ValidationGroup="Save"
                                                ErrorMessage="Select Office Name." InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Designation Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvUsertype" ValidationGroup="Save"
                                                ErrorMessage="Select Designation." InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlUsertype" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:DropDownList ID="ddlUsertype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>OIC</label><br />
                                        <asp:DropDownList ID="ddlOICYesOrNot" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="checkOic_CheckedChanged">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" id="EmpName_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Employee Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvEmpName" ValidationGroup="Save"
                                                ErrorMessage="Enter Employee Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtEmpployeeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtEmpployeeName" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" runat="server" AutoComplete="off" MaxLength="70" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="OICName_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            OIC<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvOicName" ValidationGroup="Save"
                                                ErrorMessage="Select OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlOICList" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlOICList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOICList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            User Email<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvEmailID" ValidationGroup="Save"
                                                ErrorMessage="Enter User Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtUserEmail" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtUserEmail"
                                            ErrorMessage="Invalid User Email" SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtUserEmail" runat="server" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Mobile No.<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvMobileno" ValidationGroup="Save"
                                                ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtMobileNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:RegularExpressionValidator ID="revMobileNo" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            User Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvUserName" ValidationGroup="Save"
                                                ErrorMessage="Enter User Name" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtUserName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="10" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" runat="server" id="Div_pass">
                                    <div class="form-group">
                                        <label>
                                            Password<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvPassword" ValidationGroup="Save"
                                                ErrorMessage="Enter Password" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtPassword" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" min="6" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" id="Div_Confpass">
                                    <div class="form-group">
                                        <label>
                                            Confirm Password<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvpasswordCon" ValidationGroup="Save"
                                                ErrorMessage="Enter Confirm Password" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtConfirmPassword" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvdPasscon" ErrorMessage="Confirm Password Not Matched" ForeColor="Red" Font-Bold="true" Display="None" ValidationGroup="Save" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" runat="server" />
                                        </label>
                                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" min="6" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 pt-3">
                                    <div class="row">
                                        <div class="col-md-6 pt-3">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-6 pt-3">
                                            <a href="UserRegistration.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </fieldset>
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdUserDetails" runat="server" CssClass="datatable table table-bordered table-hover" AutoGenerateColumns="false" OnRowCommand="grdUserDetails_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.<br />सरल क्र." ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserId") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Active Status <br />सक्रिय स्थिति" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                      <%--  <asp:CheckBox runat="server" ID="cbxactive" CssClass="black" ToolTip="Active" Checked='<%# Eval("Active_Status").ToString() == "True" ? true:false %>' OnCheckedChanged="cbxactive_CheckedChanged" AutoPostBack="true"></asp:CheckBox>--%>
                                                          <asp:LinkButton runat="server" CommandName="btnDelete" 
                                                              OnClientClick='<%# "return confirm(\"Are you sure you want to " + (Eval("IsActive").ToString() == "True" ? "Deactivate" :"Activate ") + " this user ?\");" %>' 
                                                              CommandArgument='<%#Eval("UserId").ToString() %>' CssClass='<%# Eval("IsActive").ToString() == "True" ? "label label-success btn btn-sm" :"label label-danger btn btn-sm" %>' 
                                                              Text='<%# Eval("IsActive").ToString() == "True" ? "Active" :"Deactive" %>'></asp:LinkButton>
                                                     <asp:Label runat="server" ID="lblIsActive" Visible="false" Text='<%#Eval("IsActive").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Type<br />कार्यालय का प्रकार" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficetypeId" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name<br />कार्यालय का नाम" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficeId" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation Name<br />पद का नाम" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("UserType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblDesignationType_ID" runat="server" Text='<%# Eval("UserType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name<br />कर्मचारी का नाम" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EMPName") %>'></asp:Label>
                                                        <asp:Label ID="lblEmployeeNameID" runat="server" Text='<%# Eval("EMPName") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.<br />मोबाइल नंबर" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                        <asp:Label ID="lblMobileNoID" runat="server" Text='<%# Eval("MobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("UserEmail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbluserPass" runat="server" Text='<%# Eval("UserPassword") %>' Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblOicID" Text='<%# Eval("OICMaster_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name<br />उपयोगकर्ता का नाम" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                        <asp:Label ID="lblUserNameID" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="EditDetails" ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Password<br />पासवर्ड">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("UserPassword") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <EmptyDataTemplate>No record found</EmptyDataTemplate>
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
    <script lang="javascript" type="text/javascript"> // First Letter's Capital
        function capFirst(cpt) {
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }

        function chcode() { // for only Character
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }
    </script>
    <%--<script src="../DataTable_CssJs/jquery.js"></script>--%>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            PageLength: 15,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 2, 3, 4, 5, 6, 7]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 2, 3,4,5,6,7]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
</asp:Content>

