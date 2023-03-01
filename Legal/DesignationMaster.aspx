<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="DesignationMaster.aspx.cs" Inherits="Legal_DesignationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .pt-4 {
            padding-top: 1.9rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
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
                        Designation Master
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Enter Details</legend>
                            <div class="row">
                                <div class="col-md-3 col-sm"  style="display:none;">
                                    <div class="form-group">
                                        <label>Office Type Name</label><span style="color: red;"><b> *</b></span>
                                       <%-- <asp:RequiredFieldValidator ID="rfvofficetype" ValidationGroup="Save"
                                            ErrorMessage="Select Office type Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficetypename" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:DropDownList ID="ddlOfficetypename" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOfficetypename_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm" style="display:none;">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red;"><b> *</b></span>
                                        <%--<asp:RequiredFieldValidator ID="rfvofficeName" ValidationGroup="Save"
                                            ErrorMessage="Select Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm">
                                    <div class="form-group">
                                        <label>Designation Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvAuthorityName" ValidationGroup="Save"
                                            ErrorMessage="Enter Designation Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDeDesignation" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDeDesignation" runat="server" CssClass="form-control" placeholder="Enter Designation Name" AutoComplete="off" MaxLength="50" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="revDesignationName" Display="Dynamic" ControlToValidate="txtDeDesignation"
                                            ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Save" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Office Level
                                         <span style="color: red;"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="RfvOfficeLevel" ValidationGroup="Save"
                                            ErrorMessage="Enter Office Level." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeLevel" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlOfficeLevel" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                              <div class="col-md-3 pt-4">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" OnClientClick="return ValidatePage();" ValidationGroup="Save" OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="DesignationMaster.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="GrdDesignation" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="GrdDesignation_PageIndexChanging" OnRowCommand="GrdDesignation_RowCommand" DataKeyNames="Designation_Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office type Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblofficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficetypeID" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblofficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Level" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblOfficelevelID" runat="server" Text='<%# Eval("OfficeLevel_Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOfficelevelName" runat="server" Text='<%# Eval("OfficeLevelName") %>'></asp:Label>
                                          
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditDetails" CommandArgument='<%# Eval("Designation_Id") %>' ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("Designation_Id") %>'
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

