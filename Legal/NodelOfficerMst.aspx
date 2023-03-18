﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="NodelOfficerMst.aspx.cs" Inherits="Legal_NodelOfficerMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnsave_Click" Style="margin-top: 20px; width: 50px;" />
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
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                                Nodel Officer Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>District<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="Save"
                                                        ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division'></i>"
                                                        ControlToValidate="ddldivision" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Nodel Officer Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvNodelOfficerName" ValidationGroup="Save"
                                                        ErrorMessage="Enter Nodel Officer Name" Text="<i class='fa fa-exclamation-circle' title='Enter Nodel Officer Name'></i>"
                                                        ControlToValidate="txtNodelOfficerName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtNodelOfficerName" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter Nodel Officer Name" MaxLength="80" AutoComplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rexNodelOfficerName" runat="server" ErrorMessage="Only Characters Allow" SetFocusOnError="true" ValidationGroup="Save"
                                                        ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtNodelOfficerName" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>

                                        <%-- </div>
                                    <div class="row">--%>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Designation Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Save"
                                                        ErrorMessage="Select Designation" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="ddlDesignation" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Department<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDept" ValidationGroup="Save"
                                                        ErrorMessage="Select Department" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="ddlDepartment" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Mobile No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                        ErrorMessage="Enter Mobile No" Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No'></i>"
                                                        ControlToValidate="txtmobileno" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtmobileno" CssClass="form-control" onkeypress="return NumberOnly();" MaxLength="10" placeholder="Enter Mobile No" AutoComplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ID="Rev_mobno" Display="Dynamic" ForeColor="Red" ControlToValidate="txtmobileno" SetFocusOnError="true"
                                                    ValidationExpression="[6-9]{1}[0-9]{5}[0-9]{4}" ErrorMessage="Mobile No. is Not Valid"
                                                    ValidationGroup="Save"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Email-ID<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RfvEmailId" ValidationGroup="Save"
                                                        ErrorMessage="Enter Email-ID" Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No'></i>"
                                                        ControlToValidate="txtEmailID" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control" MaxLength="50" placeholder="Enter Email-ID" AutoComplete="off"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revEmailid" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtEmailID"
                                                    ErrorMessage="Invalid Email Address" SetFocusOnError="true"
                                                    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnsave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <a href="NodelOfficerMst.aspx" class="btn btn-default btn-block">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Report</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gridNodelOfficer" AutoGenerateColumns="false" runat="server" DataKeyNames="NodelOfficer_ID" OnPageIndexChanging="gridNodelOfficer_PageIndexChanging" OnRowCommand="gridNodelOfficer_RowCommand" EmptyDataText="NO RECORD FOUND"
                                                    CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true">
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblOICID" runat="server" Text='<%# Eval("NodelOfficer_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisionName" runat="server" Text='<%# Eval("DivisionName") %>'></asp:Label>
                                                                <asp:Label ID="lblDivisionID" runat="server" Text='<%# Eval("Division_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nodel Officer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNodelOfficerName" runat="server" Text='<%# Eval("NodelOfficerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignationId" runat="server" Text='<%# Eval("Designation_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("Dept_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDepartmentId" runat="server" Text='<%# Eval("Dept_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("NodelOfficerMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email-ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("NodelOfficerEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("NodelOfficer_ID") %>' CommandName="EditDetails" ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("NodelOfficer_ID") %>'
                                                                OnClientClick="return confirm('Are you sure you want to delete this record?');" ToolTip="Delete" CssClass=""><i class="fa fa-trash"></i></asp:LinkButton>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" Runat="Server">
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
