<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="OfficeMaster.aspx.cs" Inherits="Legal_OfficeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" OnClick="btnSave_Click" />
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
                        Office Master
                    </div>
                    <div class="card-body">

                        <fieldset>
                            <legend>Enter Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Office Type
                                            <span style="color: red;"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvOfficeType" ValidationGroup="Save"
                                            ErrorMessage="Select Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeType" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" ID="ddlOfficeType" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Office Name
                                            <span style="color: red;"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="RfvOfficeName" ValidationGroup="Save"
                                            ErrorMessage="Enter Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOfficeName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter Office Name" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="revDesignationName" Display="Dynamic" ControlToValidate="txtOfficeName"
                                            ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Save" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>
                                            Office Loaction
                                            <span style="color: red;"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvOfficelocation" ValidationGroup="Save"
                                            ErrorMessage="Enter Office Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOfficelocation" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOfficelocation" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter Office Loaction" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                         <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" Display="Dynamic" ControlToValidate="txtOfficelocation"
                                            ValidationExpression="^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$" ValidationGroup="Save" ForeColor="Red" ErrorMessage="Please Enter Valid Text">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClientClick="return ValidatePage();" OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="OfficeMaster.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="GrdOfficeMaster" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" DataKeyNames="Office_Id" AllowPaging="true" OnPageIndexChanging="GrdOfficeMaster_PageIndexChanging" OnRowCommand="GrdOfficeMaster_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficeType" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                        <asp:Label ID="lblOfficeTypeID" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office loaction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOficelocation" runat="server" Text='<%# Eval("Officelocation") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Office_Id") %>' CommandName="EditDetails" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
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
    <script>
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
    <script lang="javascript" type="text/javascript"> // First Letter's Capital
        function capFirst(cpt) {
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }
        function lettersOnly() { // Only English Letter Allow.
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

