<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Case_SubSubjectMaster.aspx.cs" Inherits="Legal_Case_SubSubjectMaster" %>

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
                        Sub Subject Master
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Enter Details</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Subject</label><span style="color: red;"><b> * </b></span>
                                        <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                            ErrorMessage="Selct Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlcaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlcaseSubject" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Sub Subject</label><span style="color: red;"><b> * </b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                            ErrorMessage="Enter Case Sub Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtSubsubject" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSubsubject" runat="server" CssClass="form-control" AutoComplete="off" placeholder="Enter Sub Subject" MaxLength="200" onkeypress="return chcode()();" onkeyup="javascript:capFirst(this);"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top: 2rem! important">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-block" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" OnClientClick="return ValidatePage();" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="Case_SubSubjectMaster.aspx" class="btn btn-default btn-block">Clear</a>
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
                                        <asp:GridView ID="grdSub_Subect" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdSub_Subect_PageIndexChanging" OnRowCommand="grdSub_Subect_RowCommand" DataKeyNames="CaseSubSubj_Id" EmptyDataText="NO RECORD FOUND">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("CaseSubSubj_Id") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSunjectName" runat="server" Text='<%# Eval("caseSubject") %>'></asp:Label>
                                                        <asp:Label ID="lblSubjectID" runat="server" Text='<%# Eval("CaseSubjectID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case Sub Subject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSub_CaseSubject" runat="server" Text='<%# Eval("CaseSubSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-edit" CommandName="EditDetails" ToolTip="Edit" CommandArgument='<%# Eval("CaseSubSubj_Id") %>'></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("CaseSubSubj_Id") %>' ToolTip="Delete" CssClass="" OnClientClick="return confirm('Are you sure you want to delete this record?');"><i class="fa fa-trash"></i></asp:LinkButton>
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

