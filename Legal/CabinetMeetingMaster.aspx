<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CabinetMeetingMaster.aspx.cs" Inherits="Legal_CabinetMeetingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../assets/css/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../../assets/css/daterangepicker.css" rel="stylesheet" />
    <style>
        li.header {
            font-size: 16px !important;
        }

        span#ctl00_spnUsername {
            text-transform: uppercase;
            color: white;
            font-weight: 600;
            font-size: 16px;
        }

        li.dropdown.tasks-menu.classhide a {
            padding: 4px 10px 0px 0px;
        }

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                                Cabinet Meeting Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Meeting Date<span style="color: red;"> *</span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" Display="Dynamic" ControlToValidate="txtMeetingdate" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Select Meeting Date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtMeetingdate" data-provide="datepicker" data-date-end-date="0d" placeholder="dd/mm/yyyy" data-date-format="dd/mm/yyyy" autocomplete="off" data-date-autoclose="true" CssClass="form-control " runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label>Detail <span style="color: red;">*</span></label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Display="Dynamic" ControlToValidate="txtDetail" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ErrorMessage="Please Enter Detail" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtDetail" CssClass="form-control" autocomplete="off" onkeyup="javascript:capFirst(this);" onkeypress="return chcode()" Placeholder="Enter Detail" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Refrence Documents</label>
                                            <asp:FileUpload ID="RefrenceDocument" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-md-3" style="padding-top:2rem!important">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnsave_Click" OnClientClick="return ValidatePage();"/>
                                                </div>
                                                <div class="col-md-6">
                                                    <a href="CabinetMeetingMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <fieldset>
                                <legend>Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" DataKeyNames="CabinetId" ID="gridview" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnPageIndexChanging="gridview_PageIndexChanging" AllowPaging="true" PageSize="10" OnRowCommand="gridview_RowCommand" EmptyDataText="NO RECORD FOUND">
                                                <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSno" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblCabinetId" runat="server" Visible="false" Text='<%#Eval("CabinetId").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Meeting Date" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeetingDate" Text='<%# Eval("MeetingDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Detail" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCabinetDetail" Text='<%# Eval("CabinetDetail") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Refrence<br /> Documents" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink_ID" runat="server" Text="View" NavigateUrl='<% #"../Legal/CabinetMeetingDoc/" + Eval("CabinetDocument")%>' CssClass="btn-sm label  label-primary" Target="_blank"><i class="fa fa-eye"></i></asp:HyperLink>
                                                            <asp:Label ID="lblFileUpload" runat="server" Text='<%# Eval("CabinetDocument") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnView" runat="server" CssClass="" CommandName="ViewDtl" CommandArgument='<%# Eval("CabinetId") %>' ToolTip="View"><i class="fa fa-edit"></i></asp:LinkButton>
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

