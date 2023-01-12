<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="~/UserMgmt/UMMenuFormMap.aspx.cs" Inherits="UserMgmt_UMMenuFormMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

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
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                                <div class="card-header">Menu Form Mapping
                            </div>
                            <div class="card-body">
                                 <div class="row">
                                    
                                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Module Name</label>
                                <asp:DropDownList ID="ddlModule_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlModule_Name_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Menu Name<span style="color: red;"> *</span></label>
                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    ErrorMessage="Select Menu Name" Text="<i class='fa fa-exclamation-circle' title='Select Menu Name !'></i>"
                                                    ControlToValidate="ddlMenu_Name" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                <asp:DropDownList ID="ddlMenu_Name" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlMenu_Name_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                                    </div>
                                <div class="row">
                                    <div class="col-md-12" id="divGrid" runat="server">
                        <asp:GridView ID="GridView1" runat="server" class="table table-hover table-striped table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" ClientIDMode="Static">
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Form_ID").ToString()%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Form_Name_E" HeaderText="Form Name" />
                                <asp:BoundField DataField="Form_Path" HeaderText="Form Path" />
                                <asp:TemplateField ItemStyle-Width="30" HeaderText="Exist Roles">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnForm_ID" runat="server" ClientIDMode="Static" Value='<%# Eval("Form_ID").ToString()%>' />
                                        <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("Form_ID").ToString()%>' ClientIDMode="Static" />
                                    </ItemTemplate>
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="a" CssClass="btn btn-primary btn-block" OnClientClick="return ValidatePage(); " />
                                </div>
                            </div>
                        </div>
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
    <script>

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
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

