<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="UploadPetition_ExcelFile.aspx.cs" Inherits="Legal_UploadPetition_ExcelFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <style>
        label
        {
            font-size:14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Upload Petition
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Upload Case File</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Upload Excel/ अपलोड एक्सेल</label><span style="color:red;"><b> *</b></span>
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top:3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-block" Text="Upload" OnClick="btnUpload_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="UploadPetition_ExcelFile.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div> 
                                    </div>
                                    
                                </div>
                            </div>
                        </fieldset>
                        <div class="col-md-12">
                            <div class="" id="divExcel" runat="server" ></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" Runat="Server">
</asp:Content>

