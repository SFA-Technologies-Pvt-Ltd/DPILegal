<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Oicmaster.aspx.cs" Inherits="Legal_Oicmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />
    <style>
        /*.datepicker tbody {
            background-color: #ecfce6 !important;
            color: black;
        }

        .datepicker th {
            background-color: #608640 !important;
        }*/

        .label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        .sorting,
        .sorting_asc,
        .sorting_desc,
        .sorting_asc_disabled,
        .sorting_desc_disabled {
            cursor: pointer;
            position: relative;
            &:after;

        {
            position: absolute;
            bottom: 8px;
            right: 8px;
            display: block;
            font-family: 'Glyphicons Halflings';
            opacity: 0.5;
        }

        }

        .sorting:after {
            opacity: 0.2;
            content: "⏭" !important; /* sort */
        }

        .sorting_asc:after {
            content: "⏬" !important; /* sort-by-attributes */
        }

        .sorting_desc:after {
            content: "⏫" !important; /* sort-by-attributes-alt */
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
                                OIC Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3" style="display: none;">
                                            <div class="form-group">
                                                <label>Zone<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <%-- <asp:RequiredFieldValidator ID="Rfv_zone" ValidationGroup="Save"
                                                        ErrorMessage="Select Zone" Text="<i class='fa fa-exclamation-circle' title='Select Zone'></i>"
                                                        ControlToValidate="ddlzone" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlzone" CssClass="form-control" OnSelectedIndexChanged="ddlzone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="display: none;">
                                            <div class="form-group">
                                                <label>Circle<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <%--  <asp:RequiredFieldValidator ID="rfv_circle" ValidationGroup="Save"
                                                        ErrorMessage="Select Circle" Text="<i class='fa fa-exclamation-circle' title='Select Circle'></i>"
                                                        ControlToValidate="ddlcircle" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlcircle" CssClass="form-control" OnSelectedIndexChanged="ddlcircle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>District<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="Rfv_division" ValidationGroup="Save"
                                                        ErrorMessage="Select Division" Text="<i class='fa fa-exclamation-circle' title='Select Division'></i>"
                                                        ControlToValidate="ddldivision" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddldivision" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>OIC Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RfvOICName" ValidationGroup="Save"
                                                        ErrorMessage="Enter OIC Name" Text="<i class='fa fa-exclamation-circle' title='Enter OIC Name'></i>"
                                                        ControlToValidate="txtoicnme" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rexOfficetypeName" runat="server" ErrorMessage="Only Characters Allow" SetFocusOnError="true"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Valid Text'></i>" ValidationGroup="Save"
                                                        ValidationExpression="^[a-zA-Z ]*$" ControlToValidate="txtoicnme" Font-Bold="true" ForeColor="Red"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtoicnme" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" placeholder="Enter OIC Name" MaxLength="80" AutoComplete="off"></asp:TextBox>

                                            </div>
                                        </div>

                                        <%-- </div>
                                    <div class="row">--%>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Designation Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Save"
                                                        ErrorMessage="Select Designation" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="ddlDesignation" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Department<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvDept" ValidationGroup="Save"
                                                        ErrorMessage="Select Department" Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                        ControlToValidate="ddlDepartment" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlDepartment" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Mobile No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                        ErrorMessage="Enter Mobile No" Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No'></i>"
                                                        ControlToValidate="txtmobileno" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:RegularExpressionValidator runat="server" ID="Rev_mobno" Display="Dynamic" ForeColor="Red" ControlToValidate="txtmobileno" SetFocusOnError="true"
                                                    ValidationExpression="[6-9]{1}[0-9]{5}[0-9]{4}" ErrorMessage="Mobile No. is Not Valid"
                                                    ValidationGroup="Save"></asp:RegularExpressionValidator>
                                                <asp:TextBox runat="server" ID="txtmobileno" CssClass="form-control" onkeypress="return NumberOnly();" MaxLength="10" placeholder="Enter Mobile No" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Email-ID<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RfvEmailId" ValidationGroup="Save"
                                                        ErrorMessage="Enter Email-ID" Text="<i class='fa fa-exclamation-circle' title='Enter Mobile No'></i>"
                                                        ControlToValidate="txtEmailID" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revEmailid" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtEmailID"
                                                        ErrorMessage="Invalid Email Address" SetFocusOnError="true"
                                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control" MaxLength="50" placeholder="Enter Email-ID" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 pt-3">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnsave_Click" OnClientClick="return ValidatePage();" />
                                                    </div>
                                                    <div class="col-md-6" style="margin-top: 1rem;">
                                                        <a href="Oicmaster.aspx" class="btn btn-default btn-block">Clear</a>
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
                                                <asp:GridView ID="gridoicmaster" AutoGenerateColumns="false" runat="server" DataKeyNames="OICMaster_ID" OnPageIndexChanging="gridoicmaster_PageIndexChanging" OnRowCommand="gridoicmaster_RowCommand" EmptyDataText="NO RECORD FOUND"
                                                    CssClass="datatable table table-bordered table-hover">
                                                    <RowStyle HorizontalAlign="Center" />
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblOICID" runat="server" Text='<%# Eval("OICMaster_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Zone Name" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblZoneName" runat="server" Text='<%# Eval("ZoneName") %>'></asp:Label>
                                                                <asp:Label ID="lblZoneID" runat="server" Text='<%# Eval("Zone_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Circle Name" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCircleName" runat="server" Text='<%# Eval("CircleName") %>'></asp:Label>
                                                                <asp:Label ID="lblCircleID" runat="server" Text='<%# Eval("Circle_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="District Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDivisionName" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDivisionID" runat="server" Text='<%# Eval("District_Id") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OIC Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOICName" runat="server" Text='<%# Eval("OICName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDesignationId" runat="server" Text='<%# Eval("Designation_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("Dept_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblDepartmentId" runat="server" Text='<%# Eval("Dept_ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("OICMobileNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Email-ID" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("OICEmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("OICMaster_ID") %>' CommandName="EditDetails" ToolTip="Edit" CssClass=""><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkbtndelete" runat="server" CommandName="DeleteDetails" CommandArgument='<%# Eval("OICMaster_ID") %>'
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
                </div>
            </div>
        </section>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <%--<script src="../DataTable_CssJs/jquery.js"></script>--%>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>
    <script type="text/javascript">
        $('.datatable').DataTable({
            paging: true,
            PageLength: 15,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
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

