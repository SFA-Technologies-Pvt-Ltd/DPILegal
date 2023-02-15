<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="OfficetypeMaster.aspx.cs" Inherits="Legal_OfficetypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                                Office Type Master
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>Enter Details</legend>
                                            <div class="row">
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <label>Office Type Name<span style="color: red;"> *</span></label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="Rfv_OfficeTypeName" ValidationGroup="Save"
                                                                ErrorMessage="Enter Office Type Name" Text="<i class='fa fa-exclamation-circle' title='Select Zone'></i>"
                                                                ControlToValidate="txtOfficeTypeName" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rexOfficetypeName" runat="server" ErrorMessage="Only Characters Allow" ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtOfficeTypeName" Font-Bold="true" ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Mobile No. is Not Valid'></i>" ValidationGroup="Save" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox runat="server" ID="txtOfficeTypeName" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <a href="OfficetypeMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>Report</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdOfficetypeMst" runat="server" CssClass="table table-bordered table-hover" DataKeyNames="OfficeType_Id" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdOfficetypeMst_PageIndexChanging" OnRowCommand="grdOfficetypeMst_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        <asp:Label ID="lblOfficetypeID" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Office type Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOfficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action"  ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("OfficeType_Id") %>' CommandName="EditDetails" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        function capFirst(cpt) { //only Capital First.
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

