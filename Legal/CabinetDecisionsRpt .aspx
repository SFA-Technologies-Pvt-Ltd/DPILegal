<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CabinetDecisionsRpt .aspx.cs" Inherits="Legal_CabinetDecisionsRpt_" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header">
                       Cabinet Decision Details
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table>
                                            <tr>
                                                <th colspan="4">
                                                    <h3>Cabinet Decision Details</h3>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>Sr.No.
                                                </th>
                                                <th>Candidate Meeting Date
                                                </th>
                                                <th>Design Detail 
                                                </th>
                                                <th>Reference Document 
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>1 </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>2 </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>

                                            </tr>
                                            <tr>
                                                <td>3 </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>

                                            </tr>
                                          
                                            <tr>
                                                <td>4 </td>
                                               
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
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
</asp:Content>

