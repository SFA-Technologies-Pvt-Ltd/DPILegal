<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="NewDashbord.aspx.cs" Inherits="Legal_NewDashbord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Styles -->
    <style>
        #chartdiv {
            width: 100%;
            height: 450px;
        }
    </style>




    <!-- Resources -->

    <script src="Chart/Animated.js"></script>
    <script src="Chart/hierarchy.js"></script>
    <script src="Chart/index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header">
                        <h5 style="text-align: center;">Court Wise Pending Cases</h5>

                    </div>
                    <div class="card-body">
                        <div class="row">
                    <div class="col-md-12 justify-content-center">
                        <%--<div id="chartdiv" ></div>--%>
                        <div id="chart" runat="server"></div>
                    </div>
                    
                </div>
                    </div>
                </div>
                

            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

