<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="L_T_Defence.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>L&T Defence Online Exam System</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="bs4/bootstrap.min.css" />
    <script src="js/jquery-3.3.1.slim.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="styles/style.css" />

    <style>
        .form-signin {
            background-color: #000000;
        }
    </style>

     <script type="text/javascript"> 
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="background-carousel">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <div class="carousel-inner">
                    <div class="item active" style="background-image: url(images/1.jpg)"></div>

                </div>
            </div>
        </div>

        <div class="container">
            <div class="wrapper center">
                <div class="form-signin">
                    <img src="images/logo.png" />

                    <div class="col-sm-4 col-lg-12 mar">
                        <asp:TextBox ID="txtUserName" class="form-control" placeholder="UserName" ValidationGroup="WorkOrder" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="WorkOrder" ControlToValidate="txtUserName" ForeColor="Red" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-4 col-lg-12 mar">
                        <asp:TextBox ID="txtPassword" class="form-control" type="Password" placeholder="Password" ValidationGroup="WorkOrder" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ValidationGroup="WorkOrder" ControlToValidate="txtPassword" ForeColor="Red" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                    </div>

                       <div class="btn-group" role="group" aria-label="Basic example">
                   <div class="col-12 mar imgcenter">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:Button ID="btnlogin" class="btn btn-warning" runat="server" ValidationGroup="WorkOrder" Text="Login" OnClick="btnlogin_Click"/>
                            <asp:Button ID="btnCancle" class="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancle_Click"  />
                        </div>
                    </div>

                    <%--<asp:Button ID="btnlogin" class="btn btn-lg btn-warning " Width="100%" runat="server" ValidationGroup="WorkOrder" Text="Login" OnClick="btnlogin_Click" />--%>

                </div>
            </div>
        </div>

    </form>
</body>
</html>
