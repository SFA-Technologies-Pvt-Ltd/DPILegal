<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Legal/Order_By_Direction_Pending_Cases.aspx.cs" Inherits="Legal_Order_By_Drection_Pending_Cases" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../image/logo/ssmsLogoremovebg.png" rel="icon" />
    <title>Legal-Admin</title>
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

        table tbody tr td a {
            font-size: 17px;
            padding-right: 10px;
        }

        table tbody tr td span {
            font-size: 20px;
            padding-right: 10px;
        }

        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            transform: translate3d(0px, 0px, 0px) !important;
            padding: 2px !important;
            max-height: 18em !important;
            overflow-y: auto !important;
            /*#EC8712*/
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        .form-controlSearchBox {
            display: block;
            width: 100%;
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #CED4DA;
            /*border-radius: 0.25rem;*/
            box-shadow: inset 0 0 0 transparent;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .multiselect-native-select button {
            border-radius: 0.5em;
            border-color: #579FBB !important;
        }
    </style>

    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />



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
    <script src='../Main_plugins/plugins/daterangepicker/daterangepicker.js'></script>
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

        function alertMessage() {
            alert('Emailid Not Found')
        }
    </script>

    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
            function endRequestHandle(sender, Args) {
                $('[id*=ddlRespondentOffice]').multiselect({
                    includeSelectAllOption: true,
                    buttonWidth: '100%',
                });
            }
        });
        function OpenModal() {
            $('#EditModal').modal('show');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:ScriptManager runat="server" ID="ScriptManager1" />

            <%--<asp:ListBox runat="server" ID="ddlRespondentOffice1" SelectionMode="Multiple" ClientIDMode="Static" CssClass="multiselect"></asp:ListBox>--%>
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <a href="OldCaseDashBoard.aspx" title="<<Back" class="btn btn-link"></a>
                <div class="card">
                    <div class="card-header" style="text-align: center;">
                        <span style="font-size: 18px; color: #e5e5e5" id="spnCaseType" runat="server"></span>
                        <div class="row mt-2">
                            <div class="col-md-3">
                                <asp:TextBox ID="txtSearch" AutoComplete="Off" runat="server" Font-Size="20px" onkeyup="Search_Gridview(this, 'grdCaseTypeDetail')" CssClass="form-control" placeholder="Case No Search"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Button Text="Search" runat="server" ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                                <asp:Button Text="Clear Search" runat="server" ID="btnClearSearch" OnClick="btnClearSearch_Click" CssClass="btn btn-secondary" />
                            </div>
                        </div>
                    </div>
                    <div class="card-body" style="opacity: 1;">
                        <%--<asp:UpdatePanel runat="server" ID="UP1">
                            <ContentTemplate>--%>
                        <asp:GridView runat="server" ID="grdCaseTypeDetail" EmptyDataText="No Record Found"
                            AutoGenerateColumns="false" CssClass="table-responsive" Width="100%"
                            OnRowEditing="grdCaseTypeDetail_RowEditing" OnRowCommand="grdCaseTypeDetail_RowCommand"
                            OnRowUpdating="grdCaseTypeDetail_RowUpdating" OnRowCancelingEdit="grdCaseTypeDetail_RowCancelingEdit"
                            OnRowDataBound="grdCaseTypeDetail_RowDataBound">
                            <PagerSettings Mode="Numeric" Position="Bottom" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Order By Direction">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsOrderByDirection" Text='<%#Eval("IsOrderByDirection") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlIsOrderByDirection" runat="server">
                                            <asp:ListItem Text="Select" />
                                            <asp:ListItem Text="Yes" />
                                            <asp:ListItem Text="No" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FilingNo" HeaderText="Filing No" ReadOnly="true" />
                                <asp:BoundField DataField="Court" HeaderText="Court" ReadOnly="true" />
                                <asp:BoundField DataField="Petitioner" HeaderText="Petitioner" ReadOnly="true" />

                                <asp:TemplateField HeaderText="Respondent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRespondent" Text='<%#Eval("Respondent") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRespondent" TextMode="MultiLine" Text='<%#Eval("Respondent") %>' runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Respondent Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRespondentOfficeId" Text='<%#Eval("RespondentOffice") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%--<asp:ListBox runat="server" ID="ddlRespondentOffice" showcheckbox="true" SelectionMode="Multiple" CssClass="multiselect-all"></asp:ListBox>--%>
                                        <asp:TextBox ID="txtRespondentOffice" TextMode="MultiLine" Text='<%#Eval("RespondentOffice") %>' runat="server" />
                                        <asp:HiddenField ID="hdnUId" runat="server" Value='<%#Eval("UniqueNo") %>' />
                                        <asp:HiddenField ID="hdnCaseNo" runat="server" Value='<%#Eval("CaseNo") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Case Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaseSubjectId" Text='<%#Eval("CaseSubject") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCaseSubject" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Case Sub Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCaseSubSubjectId" Text='<%#Eval("CaseSubSubject") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtCaseSubSubjectId" Text="1" ReadOnly="true" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OIC Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOICName" Text='<%#Eval("OICId") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hdnOICId" Value='<%#Eval("OICId")%>' runat="server" />
                                        <asp:DropDownList ID="ddlOICName" runat="server" OnTextChanged="ddlOICName_TextChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OIC Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOICMobileNo" Text='<%#Eval("OICMobileNo") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOICMobileNo" Text='<%#Eval("OICMobileNo") %>' runat="server" ReadOnly="true" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Hearing Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHearingDate" Text='<%#Eval("HearingDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtHearingDate" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off" Text='<%#Eval("HearingDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Order Compliance Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderComplianceDate" Text='<%#Eval("OrderComplianceDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtOrderComplianceDate" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off" Text='<%#Eval("OrderComplianceDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" Text='<%#Eval("Remarks") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%#Eval("Remarks") %>' runat="server" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Complaince">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsComplaince" Text='<%#Eval("IsComplaince") %>' runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlIsComplaince" runat="server">
                                            <asp:ListItem Text="Select" />
                                            <asp:ListItem Text="Yes" />
                                            <asp:ListItem Text="No" />
                                            <asp:ListItem Text="Pending" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:CommandField ShowEditButton="true" ShowCancelButton="true" ShowHeader="true" />--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" Value='<%#Eval("UniqueNo") %>'
                                            ID="hdnUniqueNo" />
                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("OrderByDirectionEdit.aspx?U={0}&CT={1}",
                    Eval("UniqueNo").ToString(), Eval("CaseType").ToString()) %>'>Edit</asp:HyperLink>
                                        <%-- <a href="OrderByDirectionEdit.aspx?U=<%#Eval("UniqueNo")&CT=Eval("caseype") %>" target="_parent">Edit</a>--%>
                                        <%-- <asp:LinkButton Text="Edit" runat="server" CommandArgument='<%#Eval("UniqueNo") %>'
                                                    ID="lnkEdit" OnClick="lnkEdit_Click" />--%>
                                        <%-- <asp:LinkButton Text="Update" runat="server" CommandName="Update" CommandArgument='<%#Eval("UniqueNo") %>' />
                                         <asp:LinkButton Text="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%#Eval("UniqueNo") %>' />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <%--</ContentTemplate>

                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>



        </div>


        <div>
            <asp:UpdatePanel runat="server" ID="uuup1">
                <ContentTemplate>
                    <div class="modal fade" id="EditModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div style="display: table; height: 100%; width: 100%;">
                            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                                    <div class="modal-header" style="background-color: #D9D9D9;">
                                        <span class="modal-title" style="float: left" id="myModalLabel">Edit Pending Case Details</span>
                                        <button type="button" class="close" data-dismiss="modal">
                                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                                        </button>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="modal-body">
                                        <fieldset>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Is Order By Direction</label>
                                                        <asp:DropDownList ID="ddlIsOrderByDirection" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Select" />
                                                            <asp:ListItem Text="Yes" />
                                                            <asp:ListItem Text="No" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Filing No</label>
                                                        <asp:TextBox ID="txtFilingNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Court</label>
                                                        <asp:TextBox ID="txtCourt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Petitionar</label>
                                                        <asp:TextBox ID="txtPetitioner" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Case Type</label>
                                                        <asp:TextBox ID="txtRespondent" TextMode="MultiLine" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Respondent Office</label>

                                                        <asp:ListBox runat="server" ID="ddlRespondentOffice" showcheckbox="true" SelectionMode="Multiple" ClientIDMode="Static" AppendDataBoundItems="true"
                                                            CssClass="form-control multiselect"></asp:ListBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Case Subject</label>
                                                        <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>OIC Name</label>
                                                        <asp:DropDownList ID="ddlOICNameOpen" runat="server" OnSelectedIndexChanged="ddlOICNameOpen_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>OIC Mobile No</label>
                                                        <asp:TextBox ID="txtOICMobileNoOpen" runat="server" ReadOnly="true" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Case Sub Subject</label>
                                                        <asp:TextBox runat="server" ID="txtCaseSubSubject" ReadOnly="true" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Order Compliance Date</label>
                                                        <asp:TextBox ID="txtOrderComplianceDate" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="disableFuturedate form-control" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off" Text='<%#Eval("OrderComplianceDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Is Complaince</label>
                                                        <asp:DropDownList ID="ddlIsComplaince" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Select" />
                                                            <asp:ListItem Text="Yes" />
                                                            <asp:ListItem Text="No" />
                                                            <asp:ListItem Text="Pending" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Remarks</label>
                                                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%#Eval("Remarks") %>' runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnUpdate" Style="margin-top: 20px; width: 80px;" />
                                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="Close" data-dismiss="modal" Style="margin-top: 20px; width: 60px;" />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlIsOrderByDirection" />
                    <asp:AsyncPostBackTrigger ControlID="ddlRespondentOffice" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCaseSubject" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOICNameOpen" />
                    <asp:AsyncPostBackTrigger ControlID="ddlIsComplaince" />
                    <asp:AsyncPostBackTrigger ControlID="ddlOICNameOpen" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
    <link href="../Main_plugins/bootstrap/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../Main_plugins/bootstrap/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $('[id*=ddlRespondentOffice]').multiselect({
            includeSelectAllOption: true,
            buttonWidth: '100%',
        });
    </script>

</body>

</html>
