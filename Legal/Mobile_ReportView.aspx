<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mobile_ReportView.aspx.cs" Inherits="Mobile_ReportView" %>

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
            <div>
                <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
                <div class="">
                    <section class="mt-4">
                        <div class="container-fluid">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="card">
                                <div class="card-header">
                                    Case Summery Detail
                                    <span class="float-right" style="text-align:right">
                                        <a href="Mobile_ReportAll.aspx" class="btn btn-info"><i class="fas fa-home"></i></a>
                                    </span>
                                </div>
                                <div class="card-body">
                                   
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Petitioner Name</label>
                                                    <asp:TextBox runat="server" ID="lblPetitionorName" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Respondent Name</label>
                                                    <asp:TextBox runat="server" ID="lblRespondentname" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>OIC Name</label>
                                                    <asp:TextBox runat="server" ID="lblOICName" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                                                </div>
                                            </div>
                                        
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Case Subject</label>
                                                <asp:TextBox runat="server" ID="lblCaseSubject" ReadOnly="true" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Case Summery</label>
                                                <asp:TextBox runat="server" ID="lblCaseSummry" Enabled="false" TextMode="MultiLine" CssClass="form-control" t></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label>Order Summery (If Any)</label>
                                                <asp:TextBox runat="server" ID="lblCaseOrderSummry" Enabled="false" TextMode="MultiLine" CssClass="form-control" t></asp:TextBox>
                                            </div>
                                        </div>
                                            </div>
                                    
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
            <!-- ./wrapper -->
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
			
			<script type="text/javascript">
                $('.select2').select2()
            </script>
            <%-- <script src="../DataTable_CssJs/jquery.js"></script>
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
                                columns: [0, 1, 2, 3]
                            },
                            footer: true,
                            autoPrint: true
                        }, {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: $('h3').text(),
                            exportOptions: {
                                columns: [0, 1, 2, 3]
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
            </script>--%>
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
