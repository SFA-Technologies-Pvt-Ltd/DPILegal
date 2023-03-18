<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="ZoneMaster.aspx.cs" Inherits="Legal_ZoneMaster" %>

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
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Zone Master
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Zone Name
                                            <span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                    ErrorMessage="Enter Zone Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtZoneName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtZoneName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter Zone Name" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="display: none">
                                            <div class="form-group">
                                                <label>
                                                    Zone Code
                                            <span style="color: red;"><b>*</b></span></label>
                                                <%--<asp:RequiredFieldValidator ID="rfvcode" ValidationGroup="Save"
                                                    ErrorMessage="Enter Zone Code." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtZoneCode" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtZoneCode" runat="server" placeholder="Enter Zone Code" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Office Type<span style="color: red;"><b>*</b></span></label>

                                                <asp:RequiredFieldValidator ID="RfvofficeType" ValidationGroup="Save"
                                                    ErrorMessage="Enter Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlOfficeType" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList runat="server" ID="ddlOfficeType" CssClass="form-control"></asp:DropDownList>
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
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Zone office Location
                                                <span style="color: red;"><b>*</b></span></label>
                                                <asp:RequiredFieldValidator ID="RfvZoneOfficeHQ" ValidationGroup="Save"
                                                    ErrorMessage="Enter Zone office Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtZoneOfficeLocation" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtZoneOfficeLocation" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter Zone office HQ" CssClass="form-control" AutoComplete="off" MaxLength="80"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage();" />
                                        </div>
                                        <div class="col-md-2">
                                            <a href="ZoneMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>
                        </div>
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdZoneMaster" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" DataKeyNames="Zone_ID" EmptyDataText="NO RECORD FOUND" AllowPaging="true" PageIndex="10" OnRowCommand="GrdZoneMaster_RowCommand" OnPageIndexChanging="GrdZoneMaster_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblZoneID" runat="server" Text='<%# Eval("Zone_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneName" runat="server" Text='<%# Eval("ZoneName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Zone Code" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblZoneCode" runat="server" Text='<%# Eval("ZoneCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Type" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblofficetypeID" runat="server" Text='<%# Eval("OfficeType_Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOfficetypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Level" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfficelevelID" runat="server" Text='<%# Eval("OfficeLevel_Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOfficelevelName" runat="server" Text='<%# Eval("OfficeLevelName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Office Location" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("OfficeLocation") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Zone_ID") %>' CommandName="EditDetails" ToolTip="Edit" CssClass=" "><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("Zone_ID") %>' ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"><i class="fa fa-trash"></i></asp:LinkButton>
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

