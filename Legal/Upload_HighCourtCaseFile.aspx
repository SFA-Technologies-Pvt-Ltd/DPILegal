<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="Upload_HighCourtCaseFile.aspx.cs" Inherits="Legal_Upload_HighCourtCaseFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        label {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <div class="card">
                    <div class="card-header">
                        Upload Case File
                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Upload Case File</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Court Type<span style="color: red;"><b> *</b></span><br />
                                            कोर्ट का प्रकार</label>
                                        <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Case Type<span style="color: red;"><b> *</b></span><br />
                                            प्रकरण का प्रकार</label>
                                        <asp:DropDownList ID="ddlCaseType" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm">
                                    <div class="form-group">
                                        <label>
                                            Upload Case List<br />
                                            अपलोड केस लिस्ट</label><span style="color: red;"><b> *</b></span>
                                        <asp:FileUpload ID="fileUploadExcel" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3 mt-1">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary btn-block" Text="Upload" OnClick="btnUpload_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <a href="Upload_HighCourtCaseFile.aspx" class="btn btn-default btn-block">Clear</a>
                                        </div>
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

