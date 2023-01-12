<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="~/UserMgmt/UMMenuMaster.aspx.cs" Inherits="mis_UserMgmt_UMMenuMaster" %>

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
                            <div class="card-header">
                                Menu Master
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Module<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                    ErrorMessage="Select Module" Text="<i class='fa fa-exclamation-circle' title='Select Module !'></i>"
                                                    ControlToValidate="ddlModule_Name" InitialValue="0" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlModule_Name" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlModule_Name_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Menu Name<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Enter Menu Name" Text="<i class='fa fa-exclamation-circle' title='Enter Menu Name !'></i>"
                                                    ControlToValidate="txtMenu_Name" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtMenu_Name" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Menu Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Menu Name (Hindi)<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Enter Menu Name(Hindi)" Text="<i class='fa fa-exclamation-circle' title='Enter Menu Name(Hindi) !'></i>"
                                                    ControlToValidate="txtMenu_Name_H" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtMenu_Name_H" placeholder="Enter Menu Name(Hindi)" autocomplete="off" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Order By<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Enter Order By" Text="<i class='fa fa-exclamation-circle' title='Enter Order By !'></i>"
                                                    ControlToValidate="txtOrderBy" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" CssClass="form-control Number" ID="txtOrderBy" onkeypress="javascript:tbx_Number(event, this);" placeholder="Enter Order By" autocomplete="off" MaxLength="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Menu Icon<span style="color: red;"> *</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Menu Icon" Text="<i class='fa fa-exclamation-circle' title='Enter Menu Icon !'></i>"
                                                    ControlToValidate="txtMenu_Icon" ForeColor="Red" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtMenu_Icon" runat="server" data-placement="bottomRight" class="form-control icp icp-auto" MaxLength="100" value="nav-icon fas fa-list-ul"></asp:TextBox>
                                                <span class="input-group-addon action-create"></span>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="row">
                                
                              <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Icon<span style="color: red;"> *</span></label>
                                        <asp:Label ID="IconDisplay" runat="server">
                                                <i id="IconDisplay1" runat="server"></i>
                                            
                                        </asp:Label>
                                        <asp:Label runat="server" ID="Label1" placeholder="Enter Menu Icon" autocomplete="off"></asp:Label>
                                    </div>
                                </div>
                            </div>--%>

                                <div class="row col-md-3 pt-4">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClientClick="return ValidatePage()" />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkClear" OnClick="lnkClear_Click" Text="Reset" CssClass="btn btn-default btn-block" runat="server"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                        AutoGenerateColumns="False" DataKeyNames="Menu_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Menu_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Module_Name_E" HeaderText="Module" />
                                            <asp:BoundField DataField="Menu_Name_E" HeaderText="Menu Name" />
                                            <asp:BoundField DataField="Menu_Name_H" HeaderText="Menu Name(Hindi)" />
                                            <asp:BoundField DataField="OrderBy" HeaderText="Order By" />
                                            <asp:TemplateField HeaderText="Menu Icon">
                                                <ItemTemplate>
                                                    <asp:Label ID="IconDisplay" runat="server" Text='<%# Eval("Menu_Icon").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Menu_ID").ToString()%>' Checked='<%# Eval("Menu_IsActive").ToString()=="True" ? true : false %>' AutoPostBack="true" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
    <%--<script src="../assets/js/fontawesome-iconpicker.js"></script>
    <script type="text/javascript">
        $(function () {

            $.iconpicker.batch('.icp.iconpicker-element', 'destroy');

            $(document).on('click', '.action-placement', function (e) {
                $('.action-placement').removeClass('active');
                $(this).addClass('active');
                $('.icp-opts').data('iconpicker').updatePlacement($(this).text());
                e.preventDefault();
                return false;
            });
            $('.action-create').on('click', function () {
                $('.icp-auto').iconpicker();

                $('.icp-dd').iconpicker({
                    //title: 'Dropdown with picker',
                    //component:'.btn > i'
                });

            }).trigger('click');
        });
    </script>--%>
    <script type="text/javascript">
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

