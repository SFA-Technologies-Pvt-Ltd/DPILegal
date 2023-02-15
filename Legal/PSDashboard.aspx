<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="PSDashboard.aspx.cs" Inherits="mis_Legal_PSDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .LegalImage {
            background-image: url("../image/Legal%203.png");
            /*background-position: center;
            background-repeat: no-repeat;
            background-size: 70px 70px;*/
            height: 70px;
            width: 70px;
            opacity: 1;
        }

        .Libraimage {
            height: 50px;
            width: 50px;
            display: inline;
            align-content: center;
            margin: 0 0 0 34px;
        }

        .boxshape {
            height: 110px;
            /*width: 66%;*/
            width: 100%;
            border-top-right-radius: 34px;
            /*border-top-left-radius: 44px;
            border-bottom-left-radius: 44px;*/
            border-top-left-radius: 55px;
            border-bottom-left-radius: 55px;
            border-radius: -50px 0px;
            opacity: 1;
        }

        .circle {
            z-index: 50;
            position: absolute;
            border-radius: 100%;
            height: 90px;
            width: 97px;
            border: 3px solid white;
            box-shadow: inset 0 0 8px black;
            margin: 5px 5px 10px 9px;
            background-color: #140d0d;
        }

        .insideBox {
            border-radius: 100%;
            margin: 5px 7px 2px 6px;
            height: 75px;
            width: 80px;
            text-align: center;
            font-weight: 700;
            color: white;
            font-family: sans-serif;
            line-height: 1;
            padding-top: 10px;
        }

        .lableBox {
            /*position: relative;
            height: 35px;
            width: 147%;
            border: 2px solid white;
            border-top-right-radius: 15px;
            margin: -53px 28px 28px 83px;
            background-color: #140d0d;
            border-left-color: white;
            border-left: none;*/
            COLOR: white;
            /*font-size: 20px;*/
            font-weight: 900;
            display: inline;
            position: absolute;
            border: solid 3px white;
            padding: 1px 21px;
            top: 34%;
            right: 44%;
            background-color: black;
            border-top-right-radius: 15px;
        }

        .buttonDiv {
            /*height: 26px;
            width: 110%;
            background-color: white;
            margin: -26px 0px 0px 208px;
            border-radius: 30%;*/
            position: absolute;
            display: inline;
            right: 22px;
            bottom: 9px;
        }

        .Buttons {
            border-radius: 21%;
            background-color: white;
        }

        span {
            font-size: 12px;
        }

        .Heading {
            font-size: 18px;
            font-weight: bold;
            color: blue;
        }

        .Space {
            margin-bottom: 40px;
        }

        .NewCaseCourtImage {
            width: 50%;
            display: inline;
            position: relative;
            left: 100px;
        }
    </style>

    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 80%; position: absolute; left: 10%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left;font-size:20px" id="myModalLabel">High Priority Case Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <fieldset>
                                <legend>High Priority Case</legend>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdHighpriorityCase" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                               <%--  <RowStyle Font-Size="30" />--%>
                                               <HeaderStyle Font-Size="30" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No."  >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>' Font-Size="20"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Case Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseSubject" runat="server" Text='<%# Eval("CaseSubject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Case No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseNO" runat="server" Text='<%# Eval("CaseNo") %>'  Font-Size="20"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Petitioner Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPetitionerName" runat="server" Text='<%# Eval("Petitioner") %>'  Font-Size="20"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Court Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCourtName" runat="server" Text='<%# Eval("Court") %>'  Font-Size="20"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:BoundField HeaderText="High Priority Case" DataField="HighPrirtyCaseSts" />
                                                    <asp:BoundField HeaderText="High Priority Case" DataField="Casetype_Name" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--  <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" />--%>
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content-header">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header" style="text-align: left;">
                        <span style="font-size: 18px; color: #e5e5e5">Personal Presence- Principal Secretary</span>
                    </div>
                    <div class="card-body" style="opacity: 1;">
                        <div class="row Space">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">HIGH PRIORITY CASE :</span>
                                <asp:LinkButton ID="btnHighPriorityCase" runat="server" CssClass="btn-sm label label-warning" OnClick="btnHighPriorityCase_Click">TOTAL Case<span id="spnhighpriorityCase" runat="server" style="font-weight: bold;"></span></asp:LinkButton>
                            </div>
                        </div>

                        <div class="row  form-group">
                            <div class="col-lg-6 col-md-12 col-sm-12 ">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">COURT WISE :
                                      <asp:Label ID="lblCaseCount" runat="server" CssClass="Heading" />
                                </span>
                            </div>
                            <div class="col-md-6">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">CASE WISE :(TOTAL CASE : <span id="CasetypeCountno" runat="server" class="Heading"></span>No's)</span>
                            </div>
                            <div class="col-md-6">
                                <div id="sbid" runat="server"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="CasetypeCountID" runat="server"></div>
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-lg-6 col-md-12 col-sm-12">
                                <img src="../image/Legal%201.png" style="height: 25px;" />&nbsp;&nbsp;<span class="Heading">DEPARTMENT WISE :
                                    <asp:Label ID="lblCaseCount2" runat="server" CssClass="Heading" Style="color: #201f1e;" />
                                </span>
                                <div id="sbid2" runat="server" class="dvpading"></div>
                            </div>

                            <%-- <div class="col-lg-6 col-md-12 col-sm-12">
                                <img src="../image/Legal%201.png" style="height: 30px;" />&nbsp;&nbsp;<span class="Heading">COURT WISE CONTEMPT CASES:
                                    <asp:Label ID="lblConcCount" runat="server" CssClass="Heading" Style="color: #201f1e;" />
                                </span>
                                <div id="cwcc" runat="server" class="dvpading"></div>
                            </div>--%>
                        </div>

                        <div class="row form-group">
                            <div class="card card-body" style="background: linear-gradient(to bottom, #fbfb9d 0%, #ffffc2a3 100%);">
                                <fieldset>
                                    <legend>Live Informations</legend>
                                    <div class="row">

                                        <div class="col-md-12 col-sm-12">
                                            <img src="../image/Legal%201.png" style="height: 25px;" />
                                            &nbsp;&nbsp;<span class="Heading">CASE TYPE & DEPARTMENT WISE :<asp:Label ID="lblCasetypeCountno" runat="server" class="Heading" Style="color: #201f1e;"></asp:Label>
                                            </span>
                                            <div id="CasetypeCountID1" runat="server"></div>
                                        </div>
                                        <%--<div class="col-lg-6 col-md-12 col-sm-12">
                                            <img src="../image/Legal%201.png" style="height: 25px;" />
                                            &nbsp;&nbsp;<span class="Heading">ORDER BY DIRECTION :<asp:Label ID="lblOrderByDirectionalCases" runat="server" class="Heading" Style="color: #201f1e;"></asp:Label>
                                            </span>
                                            <div id="CasetypeCountID2" runat="server" style="50%"></div>
                                        </div>--%>
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
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],

            "bSort": false,

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
                    title: $('h1').text(),
                    exportOptions: {

                        // javascript: print(),

                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
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
</asp:Content>

