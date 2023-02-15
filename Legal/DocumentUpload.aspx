<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DocumentUpload.aspx.cs" Inherits="Legal_DocumentUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 635px;">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <h3></h3>
                <a href="CaseList.aspx" class="btn btn-success pull-right"><i class='fa fa-arrow-circle-left'></i>&nbsp;Back</a>

                <div class="box-body">
                    <fieldset>
                        <legend style="font-size: 16px;">Appointment of Advocate Details</legend>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>CASE DOCUMENT TYPE <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlDocumentType" CssClass="form-control">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Initial Document</asp:ListItem>
                                        <asp:ListItem>Case Report</asp:ListItem>
                                        <asp:ListItem>Other</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>UPLOAD CASE DOCUMENT<span style="color: red;"> *</span></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label>UPLOAD CASE DOCUMENT<span style="color: red;"> *</span></label>
                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label>UPLOAD CASE DOCUMENT<span style="color: red;"> *</span></label>
                                    <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-block btn-success" Text="Reset" />
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                <%--  <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div style="overflow: auto;">
                                <h4>Total Case List</h4>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" >
                                        <HeaderStyle BackColor="#1ca79a" Font-Italic="false" ForeColor="Snow" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CaseNo" HeaderText="Case No" />
                                            <asp:BoundField DataField="OldCaseRefNo" HeaderText="Old Refrence Number" />
                                            <asp:BoundField DataField="ReceiptDate" HeaderText="Date Of Receipt" />
                                            <asp:BoundField DataField="CaseFillby" HeaderText="Case Fill By" />
                                            <asp:BoundField DataField="InterimOrder" HeaderText="Interim Order" />
                                            <asp:BoundField DataField="FinalOrder" HeaderText="Final Order" />
                                            <asp:BoundField DataField="CourtType" HeaderText="Department of Court" />
                                            <asp:BoundField DataField="AdvocateName" HeaderText="Petitioner Name" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="lbUpload" runat="server" CausesValidation="False" CommandName="Select"
                                                Text="<span class='btn btn-success'>Upload</span>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>--%>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Detail ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>

