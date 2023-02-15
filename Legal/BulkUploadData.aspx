<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="BulkUploadData.aspx.cs" Inherits="BulkUploadData" %>

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
                                Running Cases Details 
                            </div>
                            <div class="card-body">
                                <div class="row">
                                   <%-- <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Upload Excel</label>
                                            <asp:FileUpload ID="FU1" runat="server" />
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btnbulkSave" CssClass="btn btn-primary btn-block" Text="Bulk Save" runat="server" OnClick="btnbulkSave_Click" />
                                        </div>
                                        <div class="form-group">
                                            <a href="BulkUploadData.aspx" class="btn btn-secondary btn-block" >Clear</a>
                                        </div>
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

