<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_ReportAll.aspx.cs" Inherits="Legal_Mobile_ReportAll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../image/logo/ssmsLogoremovebg.png" rel="icon" />
    <title>School Edu Legal</title>
    <%--======================================================================================================--%>
    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <!-- Font Awesome -->
    <link href="../Main_plugins/plugins/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <%--====================================================Start Select 2==========================================================--%>
    <link href="../Main_plugins/plugins/select2/css/select2.css" rel="stylesheet" />
    <link href="../Main_plugins/plugins/select2/css/select2.min.css" rel="stylesheet" />
    <link href="Newcss/AdminLTE.css" rel="stylesheet" />
    <%--====================================================End Select 2==========================================================--%>
    <link href="../Main_plugins/plugins/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Tempusdominus Bootstrap 4 -->

    <link rel="stylesheet" href="../Main_plugins/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="../Main_plugins/plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <!-- JQVMap -->
    <link rel="stylesheet" href="../Main_plugins/plugins/jqvmap/jqvmap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../Main_plugins/dist/css/adminlte.min.css" />
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="../Main_plugins/plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <!-- Daterange picker -->
    <%--    <link rel="stylesheet" href="../Main_plugins/plugins/daterangepicker/daterangepicker.css" />--%>
    <!-- summernote -->
    <link href="../Main_plugins/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="../Main_plugins/plugins/bootstrap/bootstrap-datepicker.css" rel="stylesheet" />
    <link rel="stylesheet" href="../Main_plugins/plugins/summernote/summernote-bs4.min.css" />
    <!-- Custom css -->
    <link href="../Main_plugins/custom_css/Custom.css" rel="stylesheet" />
    <style>
        fieldset {
            border: 1px solid #2095a1cc;
            padding: 0.35em 0.625em 0.75em;
            margin: 10px;
            border-radius: 5px;
            padding-left: 20px;
        }

        .card {
            background-color: #e5e5e5;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #2095a1cc;
            font-size: 16px;
            font-weight: 600;
            color: #416a59;
        }

        .box {
            min-height: 500px;
        }

        .box {
            position: relative;
            border-radius: 3px;
            /*background: #ffffff;*/
            background-color: #f5f5f5;
            border-top: 3px solid #d2d6de;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: 0 1px 1px #d6dee8;
        }

        .box-body {
            border-top-left-radius: 0;
            border-top-right-radius: 0;
            border-bottom-right-radius: 3px;
            border-bottom-left-radius: 3px;
            padding: 10px;
        }

        .container-fluid {
            min-height: 250px;
            padding: 15px;
            margin-right: auto;
            margin-left: auto;
            padding-left: 15px;
            padding-right: 15px;
        }

        [class*=sidebar-dark-] .nav-treeview > .nav-item > .nav-link {
            background-color: #e5e5e5;
            color: black;
        }

        spnUsername {
            text-transform: uppercase;
            color: white;
            font-weight: 600;
            font-size: 16px;
            float: right;
        }

        .main-sidebar {
            padding-top: 0px;
        }
    </style>
    <style>
        table {
            border-radius: 10px;
        }

        th {
            color: white;
            background-color: #838583;
            text-align: center;
            font-size: 14px;
        }

        .card-header {
            /*background-color: #68A7AD;*/
            /*background-color: #838583;*/
            background-color: #323a32;
            color: white;
        }

        .wodrcolor {
            color: #906b31;
        }
    </style>
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

        .nav-link:hover {
            background-color: #323a32 !important;
            color: white !important;
            font-weight: bold !important;
        }

        .text-light {
            color: white !important;
        }
    </style>
    <style>
        /* Thise Style For Datatable On Full Page.*/
        .form-inline {
            display: block;
            -ms-flex-flow: row wrap;
            flex-flow: row wrap;
            -ms-flex-align: center;
            align-items: center;
        }
    </style>
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
    </style>
    <style>
        label {
            font-size: 15px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
          
                    <section class="container-fluid">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="card">
                            <div class="card-header">
                                Pending Case Detail
                            </div>
                            <div class="card-body">
                              
                                    <div class="row">
                                        <div class="col-md-3 col-sm" style="display: none;">
                                            <div class="form-group">
                                                <label>
                                                    From Date</label>
                                                <%--  <span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                ErrorMessage="Enter From Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtFromDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm" style="display: none;">
                                            <div class="form-group">
                                                <label>
                                                    To Date</label>
                                                <%--  <span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="RfvEndDate" ValidationGroup="Save"
                                                ErrorMessage="Enter End Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtEndDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                                <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" data-date-end-date="0d" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm">
                                            <div class="form-group">
                                                <label>Case type </label>
                                                <%--<span style="color: red;"><b> *</b></span>
                                            <asp:RequiredFieldValidator ID="rfvcasetype" ValidationGroup="Save"
                                                ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseType" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlCaseType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm">
                                            <div class="form-group">
                                                <label>Court</label>
                                                <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm">
                                            <div class="form-group">
                                                <label>Case No.</label>
                                                <asp:DropDownList ID="ddlCaseNo" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm">
                                            <div class="form-group">
                                                <label>Year</label>
                                                <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                  <%--  </div>
                                    <div class="row">--%>
                                        <div class="col-md-3 col-sm">
                                            <div class="form-group">
                                                <label>Case Status </label>
                                                <%--<span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvcasetype" ValidationGroup="Save"
                                            ErrorMessage="Select Case Sratus." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseStatus" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>--%>
                                                <asp:DropDownList ID="ddlCaseStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Pending</asp:ListItem>
                                                    <asp:ListItem Value="2">Dispose</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-sm mt-3">
                                            <div class="row">
                                                <div class="col-md-6  mt-3">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" ValidationGroup="Save" OnClick="btnSearch_Click" />
                                                </div>
                                                <div class="col-md-6  mt-3">
                                                    <a href="Mobile_ReportAll.aspx" class="btn btn-default btn-block">Clear</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                          
                                 
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GrdCaseDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Case_ID" CssClass="datatable table table-bordered text-center" OnRowCommand="GrdCaseDetails_RowCommand" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("Case_ID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUniqueNo" runat="server" Text='<%# Eval("UniqueNo") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Filling No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCasetype" runat="server" Text='<%# Eval("Casetype_Name") %>'></asp:Label>/
                                                        <asp:Label ID="lblCaseNo" runat="server" Text='<%# Eval("CaseNo") %>'></asp:Label>/
                                               <asp:Label ID="lblCaseYear" runat="server" Text='<%# Eval("CaseYear") %>'></asp:Label>
                                                                -
                                                        <asp:Label ID="lblCourt" runat="server" Text='<%# Eval("CourtTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Petitioner Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("PetitionerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Respondent Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Case Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("CaseStatus") %>' ForeColor='<%# Eval("CaseStatus") == "Pending" ?  System.Drawing.Color.Green :System.Drawing.Color.Red %>' Font-Bold="true"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                      <%--  <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="EditView" ToolTip="Edit/View" CssClass=""><i class="fa fa-edit"</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                          <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Case_ID") %>' CommandName="EditView" ToolTip="Edit/View" CssClass=""><i class="fa fa-eye"</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                           
                            </div>
                        </div>
                    </section>
             
        </div>
        <script src="../Main_plugins/plugins/jquery/jquery.min.js"></script>
        <!-- jQuery UI 1.11.4 -->
        <script src="../Main_plugins/plugins/jquery-ui/jquery-ui.min.js"></script>
        <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
        <script>
            $.widget.bridge('uibutton', $.ui.button)
        </script>
        <!-- Bootstrap 4 -->
        <script src="../Main_plugins/plugins/jquery/jquery.js"></script>
        <script src="../Main_plugins/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
        <!-- ChartJS -->
        <script src="../Main_plugins/plugins/chart.js/Chart.min.js"></script>
        <!-- Sparkline -->
        <script src="../Main_plugins/plugins/sparklines/sparkline.js"></script>
        <!-- JQVMap -->
        <script src="../Main_plugins/plugins/jqvmap/jquery.vmap.min.js"></script>
        <script src="../Main_plugins/plugins/jqvmap/maps/jquery.vmap.usa.js"></script>
        <!-- jQuery Knob Chart -->
        <script src="../Main_plugins/plugins/jquery-knob/jquery.knob.min.js"></script>
        <!-- daterangepicker -->
        <%--    <script src="../Main_plugins/plugins/moment/moment.min.js"></script>
            <script src="../Main_plugins/plugins/daterangepicker/daterangepicker.js"></script>--%>
        <!-- Tempusdominus Bootstrap 4 -->
        <script src="../Main_plugins/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js"></script>
        <!-- Summernote -->
        <script src="../Main_plugins/plugins/summernote/summernote-bs4.min.js"></script>
        <!-- overlayScrollbars -->
        <script src="../Main_plugins/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js"></script>
        <!-- AdminLTE App -->
        <%--====================================================Start Select 2==========================================================--%>
        <script src="../Main_plugins/plugins/select2/js/select2.js"></script>
        <script src="../Main_plugins/plugins/select2/js/select2.min.js"></script>
        <%--====================================================End Select 2==========================================================--%>
        <script src="../Main_plugins/dist/js/adminlte.js"></script>
        <script src="../Main_plugins/plugins/bootstrap/js/bootstrap-datepicker.js"></script>
        <script src="../Main_plugins/plugins/daterangepicker/daterangepicker.js"></script>
        <script>
            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            function lettersOnly() {
                var charCode = event.keyCode;

                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                    return true;
                else
                    return false;
            }
        </script>
        <script type="text/javascript">
            $('.select2').select2()

            $('.DateAdd').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy'
            })
            function myModal() {
                $("#myModal").modal('show');
            }
        </script>

        <script src="../DataTable_CssJs/jquery.js"></script>
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
        
    </form>
</body>
</html>
