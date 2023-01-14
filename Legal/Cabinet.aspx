<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Cabinet.aspx.cs" Inherits="Legal_Cabinet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <style>
        /*table {
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #dddddd;
            text-align: center;
            padding: 8px;
            font-weight: 200;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="card">
                   
                    <div class="card-header">
                        Cabinet Meeting Details
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered text-center">
                                            <tr>
                                                <th colspan="4">
                                                    <h3>Cabinet Meeting Details</h3>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th>Sr.No.
                                                </th>
                                                <th>Meeting Date
                                                </th>
                                                <th> Detail 
                                                </th>
                                                <th>Reference Document 
                                                </th>
                                            </tr>
                                            <tr>
                                               <%-- <td colspan="5" style="font-weight:600; text-align:center; height:10%;">NO RECORD FOUND</td>--%>
                                                <td>1</td>
                                                <td>06/12/2022</td>
                                                <td>दैनिक वेतन भोगी लाभ</td>
                                                <td>No Document</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                 <td>13/12/2022</td>
                                                <td>पदोन्नति संबंधी</td>
                                               <td>No Document</td>
                                               
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                 <td>03/01/2023</td>
                                                <td>समान वेतनमान संबंधी</td>
                                                <td>No Document</td>
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

