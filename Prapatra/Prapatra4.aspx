<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Prapatra4.aspx.cs" Inherits="Prapatra_Prapatra4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #dddddd;
            text-align: center;
            padding: 8px;
            font-weight: 200;
        }

        td {
            padding: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%-- <section class="content-header">
            <h1></h1>
        </section>--%>
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        प्रपत्र-4 पालन हेतु शेष प्रकरणों की जानकारी 
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <table>
                                    <tr>
                                        <th colspan="9">
                                            <h3>प्रपत्र-4 पालन हेतु शेष प्रकरणों की जानकारी </h3>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th colspan="9">
                                            <h4>जिला ............................ दिनांक की स्थिति मे ................................ </h4>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th rowspan="2">स०क्र०
                                        </th>
                                        <th rowspan="2">याचिका क्रमांक एवं वर्ष 
                                        </th>
                                        <th rowspan="2">याचिकाकर्ता का नाम एवं पद 
                                        </th>
                                        <th rowspan="2">विषय
                                        </th>
                                        <th rowspan="2">निर्णय दिनांक
                                        </th>
                                        <th colspan="3">निराकरण की स्थिति
                                        </th>
                                        <th rowspan="2">पालन न होने का कारण
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>जिला स्तर</td>
                                        <td>संभाग स्तर</td>
                                        <td>संचालनालय स्तर</td>
                                    </tr>

                                    <tr>
                                        <td>1
                                        </td>
                                        <td>2
                                        </td>
                                        <td>3
                                        </td>
                                        <td>4
                                        </td>
                                        <td>5
                                        </td>
                                        <td>6
                                        </td>
                                        <td>7
                                        </td>
                                        <td>8
                                        </td>
                                        <td>9
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>

                                    </tr>
                                    <tr>
                                        <th colspan="9" style="text-align: left">नोट- प्रकरण का पालन यदि संचालनालय स्तर पर लंबित है तो किस कक्ष से संबन्धित है जानकारी दें।
                                        </th>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <!-- Default box -->



            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

