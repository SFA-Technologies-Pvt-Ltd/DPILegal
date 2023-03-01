<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseSubjectMaster.aspx.cs" Inherits="Legal_CaseSubjectMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .btn-danger {
            background-color: #cf7b83;
        }

        .btn-info {
            background-color: #548ac5;
        }

        .btn-primary {
            background-color: #548ac5;
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
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                                Case Subject Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="Save"
                                                        ErrorMessage="Enter Case Subject" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject'></i>"
                                                        ControlToValidate="txtCaseSubject" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubject" placeholder="Enter Case Subject" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Case Subject Detail</label>
                                               <%-- <span style="color: red;"> *</span>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Tfv3" ValidationGroup="Save"
                                                        ErrorMessage="Enter Case Subject Detail" Text="<i class='fa fa-exclamation-circle' title='Enter Case Subject Detail'></i>"
                                                        ControlToValidate="txtCaseSubjectDetail" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>--%>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseSubjectDetail" placeholder="Enter Case Subject Detail" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <%-- <div class ="col-md-4"></div>--%>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <a href="CaseSubjectMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Detail</legend>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:GridView Width="100%" ID="grdCaseSubject" AutoGenerateColumns="false" runat="server" DataKeyNames="CaseSubjectID" OnRowCommand="grdCaseSubject_RowCommand"
                                                CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdCaseSubject_PageIndexChanging">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            <asp:HiddenField ID="hdnCaseSubjectID" runat="server" Value='<%#Eval("CaseSubjectID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectCode" runat="server" Text='<%# Eval("CaseSubjectCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case Subject Detail">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubjectDetail" runat="server" Text='<%# Eval("CaseSubjectDetail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Update" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditDetails" CommandArgument='<%# Eval("CaseSubjectID") %>' ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("CaseSubjectID") %>'
                                                                OnClientClick="return confirm('Are you sure you want to delete this record?');" ToolTip="Delete" CssClass=""><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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

