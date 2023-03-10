<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="UploadExcel.aspx.cs" Inherits="Legal_UploadExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="card">
                            <div class="card-header">
                            </div>
                           
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Upload Excel</label>
                                            <asp:FileUpload ID="FUExcel" runat="server" CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FUExcel" runat="server" ForeColor="Red"
                                                Display="Dynamic" ValidationGroup="Chk" >

                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Button ID="btnCheck" CssClass="btn btn-success" Text="Check" runat="server" ValidationGroup="Chk" OnClick="btnCheck_Click"/>&nbsp&nbsp;
                                        <a href="UploadExcel.aspx" class="btn btn-secondary">Clear</a>
                                    </div>
                                </div>
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

