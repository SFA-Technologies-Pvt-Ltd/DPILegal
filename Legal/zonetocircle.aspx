<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="~/Legal/zonetocircle.aspx.cs" Inherits="Legal_zonetocircle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
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
                                Circle Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Zone<span style="color: red;"><b> *</b></span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvZone" ValidationGroup="Save"
                                                        ErrorMessage="Select Zone" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="ddlzone" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlzone" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Circle Name<span style="color: red;"><b> *</b></span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvCirlce" ValidationGroup="Save"
                                                        ErrorMessage="Enter Circle Name" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="txtCircleName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox ID="txtCircleName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" CssClass="form-control" AutoComplete="off" placeholder="Enter Circle Name" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Level<span style="color: red;"><b>*</b></span> </label>
                                                <asp:RequiredFieldValidator ID="rfvOfficeLevel" ValidationGroup="Save"
                                                    ErrorMessage="Select Office Level." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOfficeLevel" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOfficeLevel" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOfficeLevel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Type<span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="rfvofficetype" ValidationGroup="Save"
                                                    ErrorMessage="Select Office type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOfficetype" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOfficetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Location<span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    ErrorMessage="Enter location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtlocation" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtlocation" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <a href="zonetocircle.aspx" class="btn btn-default btn-block">Clear</a>
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
                                                <asp:GridView ID="GrdZoneCircle" AutoGenerateColumns="false" runat="server" DataKeyNames="Circle_ID"
                                                    CssClass="table table-bordered table-hover" PageSize="10" AllowPaging="true" EmptyDataText="NO RECORD FOUND" OnRowCommand="GrdZoneCircle_RowCommand" OnPageIndexChanging="GrdZoneCircle_PageIndexChanging">
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblCircleID" runat="server" Text='<%# Eval("Circle_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zone Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblZoneName" runat="server" Text='<%# Eval("ZoneName") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Text='<%# Eval("Zone_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Circle Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCircleName" runat="server" Text='<%# Eval("CirlceName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Circle Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCircleCode" runat="server" Text='<%# Eval("CircleCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Level Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblofficelevel" Text='<%# Eval("OfficeLevelName") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblofficelevel_ID" Text='<%# Eval("Officelevel_Id") %>' Visible="false" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Type Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblofficetype" Text='<%# Eval("OfficeType_Name") %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblofficetype_ID" Text='<%# Eval("Officetype_Id") %>' Visible="false" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocation" Text='<%# Eval("HOLocation") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Circle_ID") %>' CommandName="EditDetails" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("Circle_ID") %>' ToolTip="Delete" CssClass="" OnClientClick="return confirm('Are you sure you want to delete this record?');"><i class="fa fa-trash"></i></asp:LinkButton>
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

