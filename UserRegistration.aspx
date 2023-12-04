<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="Legal_UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                                ErrorMessage="Select Office type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            कार्यालय का प्रकार</label>
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
                                            </asp:RequiredFieldValidator><br />
                                            कार्यालय का नाम</label>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Designation Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvUsertype" ValidationGroup="Save"
                                                ErrorMessage="Select Designation Name." InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlUsertype" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            पद का नाम</label>
                                        <asp:DropDownList ID="ddlUsertype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Employee Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvEmpName" ValidationGroup="Save"
                                                ErrorMessage="Enter Employee Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtEmpployeeName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            कर्मचारी का नाम</label>
                                        <asp:TextBox ID="txtEmpployeeName" onkeyup="javascript:capFirst(this);" placehorder="Enter Employee Name" onkeypress="return chcode();" runat="server" AutoComplete="off" MaxLength="70" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3" id="UsrName_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            User Name<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvUserName" ValidationGroup="Save"
                                                ErrorMessage="Enter User Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtUserName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            उपयोगकर्ता का नाम
                                        </label>
                                        <asp:TextBox ID="txtUserName" runat="server" MaxLength="10" AutoComplete="off" placeholder="Enter User Name" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="UsrPassword_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Password<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvPassword" ValidationGroup="Save"
                                                ErrorMessage="Enter Password" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtPassword" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            पासवर्ड
                                        </label>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" min="6" AutoComplete="off" placeholder="Enter Password" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="UsrConfirmPass_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Password<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvpasswordCon" ValidationGroup="Save"
                                                ErrorMessage="Enter Confirm Password" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtConfirmPassword" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            पासवर्ड पुष्टि
                                        </label>
                                        <asp:CompareValidator ID="cvdPasscon" ErrorMessage="Confirm Password Not Machted" ForeColor="Red" ValidationGroup="Save" Display="Dynamic" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" runat="server" />
                                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" min="6" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            User Email<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvEmailID" ValidationGroup="Save"
                                                ErrorMessage="Enter User Email" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtUserEmail" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            उपयोगकर्ता ईमेल
                                        </label>
                                        <asp:TextBox ID="txtUserEmail" runat="server" placeholder="Enter User Email" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtUserEmail"
                                            ErrorMessage="Invalid User Email" SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Mobile No.<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvMobileno" ValidationGroup="Save"
                                                ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtMobileNo" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator><br />
                                            मोबाइल नंबर</label>
                                        <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" AutoComplete="off" onkeypress="return NumberOnly();" placeholder="Enter Mobile No." CssClass="form-control"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revMobileNo" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" style="margin-top: 5.5%">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-6">
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
                                        <asp:GridView ID="grdUserDetails" runat="server" CssClass="table table-bordered table-hover text-center" AutoGenerateColumns="false" OnRowCommand="grdUserDetails_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserId") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Type Name<br />कार्यालय का प्रकार" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficetypeId" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name<br />कार्यालय का नाम" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficeId" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation Name<br />पद का नाम" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("UserType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblDesignationType_ID" runat="server" Text='<%# Eval("UserType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name<br />कर्मचारी का नाम" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EMPName") %>'></asp:Label>
                                                        <asp:Label ID="lblEmployeeNameID" runat="server" Text='<%# Eval("EMPName") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No.<br />मोबाइल नंबर" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                        <asp:Label ID="lblMobileNoID" runat="server" Text='<%# Eval("MobileNo") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("UserEmail") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbluserPass" runat="server" Text='<%# Eval("UserPassword") %>' Visible="false"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="User Name<br />उपयोगकर्ता का नाम" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                        <asp:Label ID="lblUserNameID" runat="server" Text='<%# Eval("UserName") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Password<br />पासवर्ड">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("UserPassword") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("UserId") %>' CommandName="EditDetails" ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("UserId") %>' ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"><i class="fa fa-trash"></i></asp:LinkButton>
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
        function NumberOnly() { //only Numeric required.
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }
    </script>
</asp:Content>

