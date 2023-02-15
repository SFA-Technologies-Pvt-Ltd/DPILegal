<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HearingDetails_ForOperator.aspx.cs" Inherits="mis_Legal_HearingDetails_ForOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--  <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%-- <section class="content-header">
            <h1></h1>
        </section>--%>
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->    
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                           

                            <div class="col-md-3">
                                <asp:Button ID="btnEditCaseDetail" CssClass="btn btn-default" OnClick="btnEditCaseDetail_Click" runat="server" Text="Edit Case Detail" />
                            </div>
                             <div class="col-md-3">
                                <asp:Button ID="btnAddExpenses" CssClass="btn btn-default" runat="server" OnClick="btnAddExpenses_Click" Text="Add Expenses" />
                            </div>
                        </div>
                        <%--<div class="pull-right">


                            <%--<div class="col-md-4"><a class="btn btn-default" data-toggle="modal" data-target="#ExpenseModal">Add Expenses</a></div>--%>
                        <%--
                        </div>
                    </div>--%>
                        <div class="box-body">
                            <fieldset>
                                <legend>Case Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        <asp:DetailsView ID="DetailsView1" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Case Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblCaseStatus" runat="server" Text='<%# Eval("Case_Status_Label").ToString() %>'></asp:Label>&nbsp;&nbsp;<asp:Label ID="LblCaseResult" runat="server" Text='<%# Eval("Case_Result").ToString() %>'></asp:Label> 
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Case_No" HeaderText="Case No" />
                                                <asp:BoundField DataField="Case_OldRefNo" HeaderText="Old Case Ref No" />
                                                <asp:BoundField DataField="Case_CourtType" HeaderText="Court Type" />
                                                <asp:BoundField DataField="District" HeaderText="District" />
                                                <asp:BoundField DataField="Case_Type" HeaderText="Case Type" />
                                                <asp:BoundField DataField="Case_DateOfReceipt" HeaderText="Receipt Date" />
                                                <asp:BoundField DataField="Case_DateOfFiling" HeaderText="Filing Date" />
                                                <asp:BoundField DataField="Case_InterimOrder" HeaderText="InterimOrder" />
                                                <asp:BoundField DataField="Case_FinalOrder" HeaderText="FinalOrder" />
                                                <asp:BoundField DataField="Case_DepartmentConcerned" HeaderText="Department Concerned" />

                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                   <div class="form-group">
                                                        <label>Subject Of Case</label>
                                                        <div id="Subjectofcase" runat="server" style="word-wrap: break-word; text-align: justify"></div>
                                                        <div class="pull-right">
                                                            <div class="form-group">
                                                                <asp:HyperLink ID="Case_UploadedDoc1" Visible="true" runat="server" Text="Attachment 1" Target="_blank"></asp:HyperLink>
                                                                &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="Case_UploadedDoc2" Visible="true" runat="server" Text="Attachment 2" Target="_blank"></asp:HyperLink>
                                                                 &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="Case_UploadedDoc3" Visible="true" runat="server" Text="Attachment 3" Target="_blank"></asp:HyperLink>
                                                            </div>
                                                        </div>
                                                    </div>
                                </div>
                                </div>
                            </fieldset>
                            
                            <fieldset>
                                <legend>OIC Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView2" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="OIC Name" />
                                                <asp:BoundField DataField="Department_Name" HeaderText="OIC Department" />
                                                <asp:BoundField DataField="Designation_Name" HeaderText="OIC Designation" />
                                                <asp:BoundField DataField="Emp_MobileNo" HeaderText="OIC MobileNo" />
                                                <asp:BoundField DataField="Emp_Email" HeaderText="OIC Email" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Advocate Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView3" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="Advocate_Name" HeaderText="Advocate Name" />
                                                <asp:BoundField DataField="Advocate_MobileNo" HeaderText="Advocate MobileNo" />
                                                <asp:BoundField DataField="Advocate_Email" HeaderText="Advocate Email" />
                                                <asp:BoundField DataField="Advocate_Address" HeaderText="Advocate Address" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Petitioner Applicant Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView4" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="Case_PetitionerAppName" HeaderText="Applicant Name" />
                                                <asp:BoundField DataField="Case_PetitionerAppMobileNo" HeaderText="Applicant Mobile No" />
                                                <asp:BoundField DataField="Case_PetitionerAppEmail" HeaderText="Applicant Email" />
                                                <asp:BoundField DataField="Case_PetitionerAppAddress" HeaderText="Applicant Address" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Petitioner Advocate Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView5" runat="server" class="table table-striped table-bordered table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="Case_PetitionerAdvName" HeaderText="Advocate Name" />
                                                <asp:BoundField DataField="Case_PetitionerAdvMobileNo" HeaderText="Avocate Mobile No" />
                                                <asp:BoundField DataField="Case_PetitionerAdvEmail" HeaderText="Advocate Email" />
                                                <asp:BoundField DataField="Case_PetitionerAdvAddress" HeaderText="Advocate Address" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </fieldset>
                            <div id ="divExpense" runat="server">
                            <fieldset>
                                <legend>Expense Detail</legend>
                                
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblGridExpenseMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                        <asp:GridView ID="GridViewExpense" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Expense_ID" OnSelectedIndexChanged="GridViewExpense_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Expense_Date" HeaderText="Expense Date" />
                                                <asp:BoundField DataField="Expenses_Amount" HeaderText="Expense Amount" />
                                                <asp:TemplateField HeaderText="View More Detail" ShowHeader="False">
                                                    <ItemTemplate>
                                                        
                                                        <asp:LinkButton ID="lnlViewemore" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="View More"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" ToolTip='<%# Eval("Expense_ID").ToString() %>' OnClick="lnkEdit_Click" CausesValidation="False" Text="Edit"></asp:LinkButton>
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
                <div class="col-md-6">
                    <div class="box box-success">

                        <div class="box-header">
                            <h3 class="box-title">Hearing Detail</h3>
                            
                            <asp:LinkButton ID="lnkBtnReopen" class="btn btn-default pull-right" runat="server"  OnClick="lnkBtnReopen_Click">Reopen Case</asp:LinkButton>
                            <asp:Label ID="LblHearingMsg" runat="server" Text=""></asp:Label>
                            <%--<a href="AddNewCase.aspx" id="btnReopen" runat="server" class="btn btn-default pull-right">Reopen Case</a>--%>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                        <!-- Conversations are loaded here -->
                                        <div class="direct-chat-messages" style="height: 100%;">
                                                <div id="dvChat" runat="server"></div>  <!--For Chat-->
                                        </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <span id="lblNextHearingDate" class="text-bold text-danger" runat="server"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="divHearingDetail" runat="server">
                                <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                <legend>Hearing Detail of &nbsp;<span class="text" runat="server" id="HearingDate"></span></legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Hearing Conclusion<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" ID="txtHearingConclusion" CssClass="form-control" ClientIDMode="Static" placeholder="Remark" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        <small><span id="valtxtHearingConclusion" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                

                                <div class="row">
                                   
                                            <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCase_Status" ClientIDMode="Static" OnSelectedIndexChanged="ddlCase_Status_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem Value="Open">Still Open</asp:ListItem>
                                                <asp:ListItem Value="Close">Closed</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlCase_Status" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                        
                                    <div class="col-md-6" id="DivHearingDate" runat="server">
                                                <div class="form-group">
                                                    <label>Next Hearing Date<span style="color: red;">*</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtHearingDate" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off" ></asp:TextBox>
                                                    </div>
                                                    <small><span id="valtxtHearingDate" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        <div class="col-md-6">
                                                <div class="form-group" id="DivResult" runat="server">
                                                    <label>Result<span style="color: red;">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlCaseResult" CssClass="form-control">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Win</asp:ListItem>
                                                        <asp:ListItem>Loss</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlCaseResult" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                           
                                </div>
                                        <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Upload Document</label>
                                            <asp:FileUpload ID="Hearing_Doc1" runat="server" CssClass="form-control" ClientIDMode="Static"  />
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Upload Document</label>
                                            <asp:FileUpload ID="Hearing_Doc2" runat="server" CssClass="form-control" ClientIDMode="Static"  />
                                       </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" runat="server" class="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="return HearingDetail();" />
                                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                                </div>
                            </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Hearing_ID" class="table table-hover table-striped table-bordered pagination-ys" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>    
                                            <asp:BoundField DataField="Hearing_Date" HeaderText="Hearing Date"/>
                                            <asp:BoundField DataField="Case_Status" Visible="false" HeaderText="Case Status" />
                                            <asp:TemplateField HeaderText="View More" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnAction" runat="server" OnClick="lnkbtnAction_Click" CssClass="label label-default" CausesValidation="False" Text="Action" CommandName="select" ToolTip='<%# Eval("Hearing_ID").ToString()%>' Visible='<%# Eval("Hearing_CaseStatus").ToString()=="" ? true : false %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnView" runat="server" CssClass="label label-default" CausesValidation="False" Text="View" CommandName="select" ToolTip='<%# Eval("Hearing_ID").ToString()%>' Visible='<%# Eval("Hearing_CaseStatus").ToString()!= "" ? true : false %>' OnClick="lnkbtnView_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="label label-default" CausesValidation="False" Text="Edit" CommandName="select" ToolTip='<%# Eval("Hearing_ID").ToString()%>' Visible='<%# Eval("Case_Status").ToString()== "Open" && Eval("Hearing_CaseStatus").ToString()!= "" ? true : false %>' OnClick="lnkbtnEdit_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>--%>
                        </div>
                        <%--<div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Add Hearing Details</h4>
                                        <asp:Label ID="lblHearingMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Case Status<span style="color: red;">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlCaseStatus" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCaseStatus_SelectedIndexChanged">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Open</asp:ListItem>
                                                        <asp:ListItem>Closed</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlCaseStatus" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6" id="DivCelender" runat="server">
                                                <div class="form-group">
                                                    <label>Next Hearing Date<span style="color: red;">*</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtHearingDate" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off" data-date-start-date="0d" OnTextChanged="txtHearingDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <small><span id="valtxtHearingDate" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group" id="DivResult" runat="server">
                                                    <label>Result<span style="color: red;">*</span></label>
                                                    <asp:DropDownList runat="server" ID="ddlCaseResult" CssClass="form-control">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Win</asp:ListItem>
                                                        <asp:ListItem>Loss</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlCaseResult" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Conclusion of Hearing<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtConclusionofHearing" TextMode="MultiLine" Rows="4" CssClass="form-control" runat="server"></asp:TextBox>
                                                 <small><span id="valtxtConclusionofHearing" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Document Upload1</label>
                                                <asp:FileUpload ID="FU_Upload1" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document Upload2</label>
                                                    <asp:FileUpload ID="FU_Upload2" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3"></div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-success btn-block" Style="margin-top: 23px;" Text="Save" OnClick="btnSubmit_Click1" OnClientClick="return HearingDetail();" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnClear" runat="server" class="btn btn-default btn-block" Style="margin-top: 23px;" Text="Clear" OnClick="btnClear_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="HearingViewModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Hearing Detail View Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Hearing Detail</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Hearing Date</label><br />
                                                    <asp:Label ID="lblHearingDate" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Case Status</label><br />
                                                    <asp:Label ID="lblCaseStatus" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Conclusion of hearing</label><br />
                                                    <p id="HearingConclusion" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document1</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:HyperLink ID="Hearing_Doc1" Target="_blank" runat="server" Text="" CssClass="label label-default"></asp:HyperLink>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document2</label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:HyperLink ID="Hearing_Doc2" Target="_blank" runat="server" Text="" CssClass="label label-default"></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div id="EditHearingDetModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!--Edit Hearing Detail Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Hearing Details</h4>
                                        <asp:Label ID="lblEditHearingMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Case_Status</label><br/>
                                                    <asp:Label ID="Case_Status" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Hearing Date</label><br/>
                                                    <asp:Label ID="HearingDate" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Conclusion of Hearing<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtEditHearingConclusion" TextMode="MultiLine" Rows="4" CssClass="form-control" runat="server"></asp:TextBox>
                                                <small><span id ="valtxtEditHearingConclusion" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document Upload1</label>
                                                    <asp:HyperLink ID="HearingDoc1" runat="server" CssClass="label label-default" Text="View"></asp:HyperLink>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document Upload2</label>
                                                    <asp:HyperLink ID="HearingDoc2" runat="server" CssClass="label label-default" Text="View"></asp:HyperLink>
                                                    <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3"></div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnUpdate" runat="server" class="btn btn-success btn-block" Style="margin-top: 23px;" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return EditHearingDetail();" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnUpdateClear" runat="server" class="btn btn-default btn-block" Style="margin-top: 23px;" Text="Clear" OnClick="btnUpdateClear_Click"/>
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div id="ExpenseModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Add Expenses</h4>
                                        <asp:Label ID="LblExpenseMsg" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6" id="DivExpenseFor" runat="server">
                                                <div class="form-group">
                                                    <label>Expense For<span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlExpenseFor" CssClass="form-control" runat="server" MaxLength="50" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseFor_SelectedIndexChanged">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="0">Other</asp:ListItem>
                                                        <asp:ListItem Value="1">Advocate</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id = "valddlExpenseFor" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6" id="DivAdvocate" runat="server">
                                                <div class="form-group">
                                                    <label>Advocate</label>
                                                    <asp:TextBox ID="txtAdvocate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group" runat="server">
                                                    <label>Date<span style="color: red;">*</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <small><span id = "valtxtDate" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expenses (Amount in Rs.)<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtExpenses" runat="server" CssClass="form-control Amount" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                <small><span id = "valtxtExpenses" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Description of Expenses<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtExpenseDetail" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                                                <small><span id = "valtxtExpenseDetail" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3"></div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSaveExpenses" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSaveExpenses_Click" OnClientClick="return ExpenseDetail();" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="btnExpensesClear" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnExpensesClear_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="ExpenseViewModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Expenses Detail View Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Expense Detail</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Date Of Expense</label><br />
                                                    <asp:Label ID="lblExpDate" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expense For</label><br />
                                                    <asp:Label ID="lblExpFor" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Expense_Description</label><br />
                                                    <p id="ExpDescription" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expense Amount</label><br />
                                                    <asp:Label ID="lblExpAmnt" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <%--<div id ="divExpenses" runat ="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <div class="box-title">
                                <h3 class="box-title">Expenses Detail</h3>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <fieldset>
                                    <legend>Expenses Detail</legend>
                                            <div class="row">
                                            <div class="col-md-6" id="DivExpenseFor" runat="server">
                                                <div class="form-group">
                                                    <label>Expense For<span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlExpenseFor" CssClass="form-control" runat="server" MaxLength="50" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseFor_SelectedIndexChanged">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="0">Other</asp:ListItem>
                                                        <asp:ListItem Value="1">Advocate</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id = "valddlExpenseFor" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6" id="DivAdvocate" runat="server">
                                                <div class="form-group">
                                                    <label>Advocate</label>
                                                    <asp:TextBox ID="txtAdvocate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group" runat="server">
                                                    <label>Date<span style="color: red;">*</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" date-provide="datepicker" runat="server" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <small><span id = "valtxtDate" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expenses (Amount in Rs.)<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtExpenses" runat="server" CssClass="form-control Amount" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                <small><span id = "valtxtExpenses" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Description of Expenses<span style="color: red;">*</span></label>
                                                    <asp:TextBox ID="txtExpenseDetail" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                                                <small><span id = "valtxtExpenseDetail" class="text-danger"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="row">
                                            
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Button ID="btnSaveExpenses" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSaveExpenses_Click" OnClientClick="return ExpenseDetail();" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Button ID="btnExpensesClear" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnExpensesClear_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-4"></div>
                                        </div>
                                </fieldset>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>--%>
    <!-- /.box -->
    </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/jQuery-2.2.0.min.js"></script>
    <script src="../js/bootstrap-datepicker.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script>
        function callalert() {
            $("#myModal").modal('show');
        }
        function ExpenseModal() {
            $("#ExpenseModal").modal('show');
        }
        function HearingViewModal() {
            $("#HearingViewModal").modal('show');
        }
        function EditHearingDetModal() {
            $("#EditHearingDetModal").modal('show');
        }
        function ExpenseViewModal() {

            $("#ExpenseViewModal").modal('show');
        }

        $('#txtHearingDate').datepicker({
            autoclose: true,
            format: 'd/m/yyyy'
        });

        $('#txtDate').datepicker({
            autoclose: true,
            format: 'd/m/yyyy'
        });
        $('#txtHearingDate').change(function () {
            debugger;
            var start = document.getElementById('HearingDate').innerHTML;
            var end = $('#txtHearingDate').datepicker('getDate');

            if (start >= end) {

                if ($('#txtHearingDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtHearingDate').val("");
                }

            }
        });
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        <%--function EditHearingDetail() {
            var msg = "";
            $("#valtxtEditHearingConclusion").html("");
            if (document.getElementById('<%=txtEditHearingConclusion.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Conclusion of Hearing. \n";
                $("#valtxtEditHearingConclusion").html("Enter Conclusion of Hearing");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                if (confirm("Do you really want to Update Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }--%>
        function ExpenseDetail() {
            var msg = "";
            $("#valddlExpenseFor").html("");
            $("#valtxtDate").html("");
            $("#valtxtExpenses").html("");
            $("#valtxtExpenseDetail").html("");
            var Expensefor = document.getElementById('<%=ddlExpenseFor.ClientID%>')
            if (Expensefor != null) {
                if (document.getElementById('<%=ddlExpenseFor.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Expense For. \n";
                    $("#valddlExpenseFor").html("Select Expense for");
                }
            }
            if (document.getElementById('<%=txtDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Expense Date. \n";
                $("#valtxtDate").html("Select Expense Date");
            }
            if (document.getElementById('<%=txtExpenses.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Expenses Amount. \n";
                $("#valtxtExpenses").html("Enter Expenses Amount");
            }
            if (document.getElementById('<%=txtExpenseDetail.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Expenses Description. \n";
                $("#valtxtExpenseDetail").html("Enter Expenses Description");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSaveExpenses.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (document.getElementById('<%=btnSaveExpenses.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

            }
        }
        function HearingDetail() {
            debugger;
            var msg = "";
            $("#valddlCase_Status").html("");
            $("#valtxtHearingDate").html("");
            $("#valddlCaseResult").html("");
            $("#valtxtHearingConclusion").html("");
            if (document.getElementById('<%=txtHearingConclusion.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Conclusion of Hearing. \n";
                $("#valtxtHearingConclusion").html("Enter Conclusion of Hearing");
            }
            if (document.getElementById('<%=ddlCase_Status.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Case Status. \n";
                $("#valddlCase_Status").html("Select Case Status");
            }
            if (document.getElementById('<%=ddlCase_Status.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddlCase_Status.ClientID%>').value.trim() == "Open") {
                    if (document.getElementById('<%=txtHearingDate.ClientID%>').value.trim() == "") {
                        msg = msg + "Select Next Hearing Date. \n";
                        $("#valtxtHearingDate").html("Select Next Hearing Date");
                    }
                }
                else if (document.getElementById('<%=ddlCase_Status.ClientID%>').value.trim() == "Close") {
                    if (document.getElementById('<%=ddlCaseResult.ClientID%>').selectedIndex == 0) {
                        msg = msg + "Select Case Result. \n";
                        $("#valddlCaseResult").html("Select Case Result");
                    }

                }
        }

        if (msg != "") {
            alert(msg);
            return false;
        }
        else {

            if (confirm("Do you really want to Save Details ?")) {
                return true;
            }
            else {
                return false;
            }

        }
    }

    </script>
    
    <script src="../js/ValidationJs.js"></script>
</asp:Content>


