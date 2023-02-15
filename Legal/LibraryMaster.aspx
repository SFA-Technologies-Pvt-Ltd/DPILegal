<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="~/Legal/LibraryMaster.aspx.cs" Inherits="Legal_LibraryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="content-wrapper">
        <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                                Case Law Master
                            </div>
                            <div class="card-body">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Case No" Text="<i class='fa fa-exclamation-circle' title='Enter Case No'></i>"
                                                        ControlToValidate="txtCaseNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseNo" placeholder="Enter Case No" autocomplete="off" MaxLength="100"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Subject</label><span style="color: red;"><b> *</b></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="Save"
                                                    ErrorMessage="Select Case Subject" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Type<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Type" Text="<i class='fa fa-exclamation-circle' title='Enter Case Type'></i>"
                                                        ControlToValidate="txtCasetype" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCasetype" placeholder="Enter Case Type" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Petitioner Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Petitioner Name" Text="<i class='fa fa-exclamation-circle' title='Enter Party Name'></i>"
                                                        ControlToValidate="txtPartyName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPartyName" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Party Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Respondent Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                        ErrorMessage="Enter Respondent Name" Text="<i class='fa fa-exclamation-circle' title='Enter Party Name'></i>"
                                                        ControlToValidate="txtrespondentName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtrespondentName" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Party Name" autocomplete="off" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Related Office<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Enter Related Office" Text="<i class='fa fa-exclamation-circle' title='Enter Related Office !'></i>"
                                                        ControlToValidate="txtRelatedOffice" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control Number" ID="txtRelatedOffice" placeholder="Enter Related Office" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Decision Date<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                        ErrorMessage="Enter Decision Date" Text="<i class='fa fa-exclamation-circle' title='Enter Decision Date'></i>"
                                                        ControlToValidate="txtDecisionDate" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <%-- <asp:TextBox runat="server" CssClass="form-control" ID="txtDecisionDate" placeholder="Enter Form Path" autocomplete="off"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtDecisionDate" runat="server" data-date-end-date="0d" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Case Year<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Year" Text="<i class='fa fa-exclamation-circle' title='Enter Case Year'></i>"
                                                        ControlToValidate="txtCaseYear" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCaseYear" min="4" MaxLength="4" placeholder="Enter Form Path" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Decision in favour of</label><span style="color: red;"> *</span>
                                                <asp:RequiredFieldValidator ID="rfvDecisionfavour" ValidationGroup="a"
                                                    ErrorMessage="Select Decision in favour." Text="<i class='fa fa-exclamation-circle' title='Required'></i>"
                                                    ControlToValidate="ddlDecisionFavourin" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlDecisionFavourin" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Petitioner</asp:ListItem>
                                                    <asp:ListItem Value="2">Department</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Upload File<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                        ErrorMessage="Enter Case Year" Text="<i class='fa fa-exclamation-circle' title='Enter Upload File'></i>"
                                                        ControlToValidate="FU1" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:FileUpload ID="FU1" runat="server" CssClass="form-control" />
                                                <%--<asp:TextBox runat="server" CssClass="form-control" ID="TextBox1" min="4" MaxLength="4" placeholder="Enter Form Path" autocomplete="off"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3" style="padding-top: 2rem! important;">
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button runat="server" ValidationGroup="a" CssClass="btn btn-primary btn-block" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <a href="LibraryMaster.aspx" class="btn btn-secondary btn-block">Clear</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <%-- <div class ="col-md-4"></div>--%>
                                    </div>
                                </fieldset>
                                <fieldset>
                                    <legend>Detail</legend>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <asp:GridView Width="100%" ID="grdCaseLibrary" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered table-hover ">
                                                <RowStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CaseNo" HeaderText="Case No." HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="CaseSubject" HeaderText="Case Subject" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="CaseType" HeaderText="Case Type" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="PartyName" HeaderText="Petitioner Name" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="RespondentName" HeaderText="Respondent Name" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="RelatedOffice" HeaderText="Related Office" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Case_Infavourof" HeaderText="Decision in Favour of" HeaderStyle-HorizontalAlign="Center"  />
                                                    <asp:TemplateField HeaderText="Decision Date">
                                                        <ItemTemplate>
                                                            <%# Convert.ToDateTime(Eval("DecisionDate")).ToString("dd/MM/yyyy") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View">
                                                        <ItemTemplate>
                                                            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="<%#Eval("PDFViewLink") %>"><i class="fa fa-eye"></i></a></asp:HyperLink>--%>
                                                            <a href="<%#Eval("PDFViewLink") %>" target="_blank"><i class="fa fa-eye"></i></a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </fieldset>
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

