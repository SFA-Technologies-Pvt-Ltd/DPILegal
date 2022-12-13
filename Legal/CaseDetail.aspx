<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="CaseDetail.aspx.cs" Inherits="mis_Legal_CaseDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
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
                                                    <asp:BoundField DataField="Case_ClaimAmount" HeaderText="Claim Amount" />
                                                    <asp:BoundField DataField="Case_CourtType" HeaderText="Court Type" />
                                                    <asp:BoundField DataField="District" HeaderText="District" />
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
                                        <asp:GridView ID="GridViewExpense" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Expense_ID"  ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="GridViewExpense_SelectedIndexChanged">
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
                                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expense For</label><br />
                                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Expense_Description</label><br />
                                                    <p id="P1" runat="server"></p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Expense Amount</label><br />
                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
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
 
            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script>
        function ExpenseViewModal() {

            $("#ExpenseViewModal").modal('show');
        }
    </script>
</asp:Content>

