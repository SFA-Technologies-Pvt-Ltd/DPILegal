<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Legal_ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PHE Legal</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-A3rJD856KowSb7dwlZdYEkO39Gagi7vIsF0jrRAoQmDKKtQBHUuLZ9AsSv4jD4Xa"
        crossorigin="anonymous"></script>
    <style>
        .bgleft_login {
            display: block;
        }

        .bgleft_login {
            background: url(../image/eCases.jpg) no-repeat center center;
            min-height: 100vh;
            background-size: 120%;
        }

        .bgright_login {
            min-height: 100vh;
            padding: 30px;
            border-left: 1px solid #2e0303;
        }

        .btn-warning1 {
            background: #ff8830;
            font-weight: bold;
        }

        .btn:hover {
            background-color: #ff8830c2;
            border-color: #ff8830;
        }

        .form-control {
            border: 1px solid #2e0303 !important;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
         <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="false" ShowSummary="false" ValidationGroup="Save" />
        <div class="vh-100 d-flex justify-content-center align-items-center ">
            <div class="col-lg-8 bgleft_login">
            </div>
            <div class="col-lg-4 bgright_login">
                <div class="row">
                    <div class="col-lg-12 p-5 shadow-sm border rounded-5 border-primary bg-white" style="border: none !important">
                        <h2 class="text-center mb-4 text-primary" style="margin-top: -12px;">
                            <img runat="server" src="../image/logo/ssmsLogo.png" alt="" style="width: 80px;" /></h2>
                        <h5 style="text-align: center; color: #59afd9; font-size: 25px; font-family: 'Times New Roman';">Public Health Engineering Department</h5>
                        <h5 style="text-align: center; color: #f98023; font-size: 21px;">Legal Cases Monitoring System</h5>
                        <div class="mb-5">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="mb-3">
                            <label class="form-label" style="color: #2e0303; font-weight: bold">User Email</label>
                            <asp:RequiredFieldValidator ID="RfvEmpName" ValidationGroup="Save"
                                                ErrorMessage="Enter User Email" ForeColor="Red" Text="Enter User Email"
                                                ControlToValidate="txtUserEmail" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtUserEmail"
                                ErrorMessage="Invalid User Email"
                                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtUserEmail" runat="server" CssClass="form-control border-primary" min="6"></asp:TextBox>
                        </div>
                        <div class="d-grid pt-3">
                            <div style="text-align: center;">
                                <asp:Button ID="btnForgotPassword" runat="server" CssClass="btn btn-warning1 col-lg-8" ValidationGroup="Save" Text="Forgot Password" OnClick="btnForgotPassword_Click" />
                            </div>
                             <div class="d-grid pt-3">
                            <a href="../Login.aspx"  style="text-align: left; color:#0c59e7; font-size: 18px; font-family: 'Times New Roman';text-decoration:none;width: 50px;">Login</a>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
