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
                            <div class="card-header" style="background-color: #838583; font-size: 25px; text-align: center; color: white;">
                                Cabinet Meeting Details     
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" DataKeyNames="CabinetId" ID="gridview" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnPageIndexChanging="gridview_PageIndexChanging" 
                                            AllowPaging="true" PageSize="10"  >
                                                <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSno" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblCabinetId" runat="server" Visible="false" Text='<%#Eval("CabinetId").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Meeting Date" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeetingDate" Text='<%# Eval("MeetingDate") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Detail" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCabinetDetail" Text='<%# Eval("CabinetDetail") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Refrence<br /> Documents" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink_ID" runat="server" Text="View" NavigateUrl='<% #"../Legal/CabinetMeetingDoc/" + Eval("CabinetDocument")%>' Enabled='<%# "../Legal/CabinetMeetingDoc/" + Eval("CabinetDocument").ToString() != null ? true : false %>' CssClass="btn-sm label  label-primary" Target="_blank">View</asp:HyperLink>
                                                            <asp:Label ID="lblFileUpload" runat="server" Text='<%# Eval("CabinetDocument") %>' Visible="false"></asp:Label>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
</asp:Content>

