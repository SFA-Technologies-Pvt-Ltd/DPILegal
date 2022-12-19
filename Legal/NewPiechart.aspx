<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="NewPiechart.aspx.cs" Inherits="Legal_NewPiechart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://www.gstatic.com/charts/loader.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header" style="text-align: center;">
                        <span style="font-size: 18px; color: #e5e5e5">Dashboard</span>
                    </div>
                    <div class="card-body">
                        <div id="chart_div" runat="server"></div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" Runat="Server">
</asp:Content>

