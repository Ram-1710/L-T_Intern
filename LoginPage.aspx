<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="L_T_Defence.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>L&T Defence Online Exam System</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>


    <link rel="stylesheet" href="styles/style.css"/>
    <style>
        .form-signin {
            background-color: #000000;
        }

        .hover08 figure img {
            -webkit-filter: grayscale(0);
            filter: grayscale(0);
        }

        .hover08 figure:hover img {
            -webkit-filter: grayscale(100);
            filter: drop-shadow(4px 4px 4px gray);
            -webkit-transition: .3s ease-in-out;
            transition: .3s ease-in-out;
        }
    </style>


</head>

<body>
    <form runat="server">
        <div id="background-carousel">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <div class="carousel-inner">
                    <div class="item active" style="background-image: url(images/1.jpg)"></div>

                </div>
            </div>
        </div>


        <div class="container-fluid">
            <div class="row">

                <div class="col-sm-10"></div>
                <div class="col-sm-2 hover08">
                    <br/>
                    <a href="#" data-toggle="modal" data-target="#myModal">
                        <figure>
                            <img src="images/books.png"/>
                        </figure>
                    </a>
                </div>
            </div>
        </div>




        <div class="container">
            <div class="wrapper center">
                <div class="form-signin">
                    <img src="images/logo.png" />
                    <asp:Button ID="btnAdministrator" class="btn btn-lg btn-info btn-block" runat="server" Text="Administrator" OnClick="btnAdministrator_Click" />
                    <asp:Button ID="btnRegisterdUser" class="btn btn-lg btn-warning btn-block" runat="server" Text="Registered User" OnClick="btnRegisterdUser_Click" />
                </div>
            </div>
        </div>


        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title text-primary">STUDY MATERIAL</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                           <%-- <input type="text" class="form-control" id="email" placeholder="User ID">--%>
                            <asp:TextBox ID="txtUserName" CssClass="form-control" placeholder="User ID" runat="server"></asp:TextBox>
                            <br/>
                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>



                    </div>
                    <div class="modal-footer">
                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        <asp:Button ID="btnOk" CssClass="btn btn-primary " runat="server" OnClick="btnOk_Click" Text="Ok" />
                    </div>
                </div>

            </div>
        </div>

    </form>
</body>

</html>
