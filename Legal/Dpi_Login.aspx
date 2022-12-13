<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dpi_Login.aspx.cs" Inherits="Legal_Dpi_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dpi Legal</title>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-A3rJD856KowSb7dwlZdYEkO39Gagi7vIsF0jrRAoQmDKKtQBHUuLZ9AsSv4jD4Xa"
        crossorigin="anonymous"></script>

</head>
<body>

    <form runat="server">
        <div class="vh-100 d-flex justify-content-center align-items-center ">
            <div class="col-md-5 p-5 shadow-sm border rounded-5 border-primary bg-white">
                <h2 class="text-center mb-4 text-primary">
                    <img runat="server" src="../image/logo/ssmsLogo.png" alt="" /></h2>
                <h5 style="text-align: center;">Legal Login</h5>
                <div class="mb-5">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="mb-3">
                    <label class="form-label">User Name</label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control border-primary" MaxLength="50"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <asp:TextBox runat="server" class="form-control border border-primary" ID="txtPassword" AutoComplete="off" TextMode="Password" MaxLength="7" onpaste="return false"></asp:TextBox>
                </div>
                <%-- <p class="small"><a class="text-primary" href="forget-password.html">Forgot password?</a></p>--%>
                <div class="d-grid pt-3">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" OnClientClick="return ValidatePage();" Text="Login" OnClick="btnLogin_Click" />
                </div>
            </div>
        </div>
    </form>
    <script src="../Main_plugins/plugins/sha512.js"></script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
    </script>
    <script>
        function ValidatePage() {
            debugger;
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Login');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%= txtPassword.ClientID %>').value.length != 128) {
                    document.getElementById('<%= txtPassword.ClientID %>').value =
                    SHA512(SHA512(document.getElementById('<%= txtPassword.ClientID %>').value) +
                    '<%= ViewState["RandomText"].ToString() %>');
                }
            }
            else {
                if (document.getElementById('<%= txtUserName.ClientID %>').value == "") {
                    $("input[name='txtUserName']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserName']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserName']").removeClass('TextBoxError');
                    $("input[name='txtUserName']").addClass('TextBoxSuccess');
                }
                if (document.getElementById('<%= txtPassword.ClientID %>').value == "") {
                    $("input[name='txtUserPassword']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserPassword']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserPassword']").removeClass('TextBoxError');
                    $("input[name='txtUserPassword']").addClass('TextBoxSuccess');
                }
                return false;
            }
        }


    </script>
</body>
</html>
