<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ExamScreen.aspx.cs" Inherits="L_T_Defence.ExamScreen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <link href="css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="js/jquery-3.3.1.js" rel="stylesheet"></script>
    <script src="js/jquery.dataTables.min.js" rel="stylesheet"></script>
    <script src="js/dataTables.bootstrap4.min.js" rel="stylesheet"></script>

    <link href="css/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <script src="js/dataTables.fixedHeader.min.js" type="text/javascript"></script>


    <div class="row ">
        <div class="col-sm">
            <h3><strong>Exam Screen</strong></h3>
        </div>
    </div>

    <br>
    <div class="col-sm-12 col-md-12 ">
        <div class="alert tablebg1">
            <div class="row">

                <div class="col-sm-4 col-lg-4 mar">
                    <label>Name :</label>
                    <asp:Label ID="lblName" runat="server" Text=" "></asp:Label>
                </div>
                <div class="col-sm-4 col-lg-4 mar"></div>
                <div class="col-sm-4 col-lg-4 mar"></div>

                <div class="col-sm-3 col-lg-3 mar">
                    <label>Department:</label>
                    <asp:Label ID="lblDepartment" runat="server" Text=" "></asp:Label>
                </div>

                <div class="col-sm-1 col-lg-1 mar" id="category" runat="server">Category </div>
                <div class="col-sm-2 col-lg-2 mar">
                    <asp:DropDownList ID="ddlcategory" CssClass="form-control" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlcategory" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Select the Category">
                        </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar" id="Module" runat="server">Module </div>
                <div class="col-sm-3 col-lg-2 mar">
                    <asp:DropDownList ID="ddlmodule" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlmodule" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Select the module">
                        </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-3 col-lg-3 mar">
                    <asp:Button ID="btnExamStart" CssClass="btn btn-primary" runat="server" Text="Start" ValidationGroup="Department" OnClick="btnExamStart_Click1" />
                </div>

            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 ">
        <div class="row" id="panel" runat="server">
            <div class="col">
                <div class="alert tablebg1">

                    <div class="row">
                        <div class="col-sm-4">
                        </div>

                    </div>
                    <br>

                    <div class="row">


                        <div class="col-sm-3">
                            <div class="alert alert-primary" role="alert">
                                <div class="form-group">
                                    <span class="badge badge-primary">
                                        <h6>Category:</h6>
                                    </span>
                                    <asp:Label ID="lblCategory" runat="server" class="control-label" Text=" "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="alert alert-primary" role="alert">
                                <div class="form-group">
                                    <span class="badge badge-primary">
                                        <h6>Exam Module :</h6>
                                    </span>
                                    <asp:Label ID="lblModule" runat="server" class="control-label" Text=" "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="alert alert-warning" role="alert">
                                <div class="form-group">
                                    <span class="badge badge-warning">
                                        <h6>Number of Questions:</h6>
                                    </span>
                                    <asp:Label ID="lblQuestionCount" runat="server" class="control-label" Text=" "></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="alert alert-danger" role="alert">

                                <div class="form-group">
                                    <span class="badge badge-danger">
                                        <h6>Duration</h6>
                                    </span>
                                    <asp:Label ID="lblDuration" runat="server" class="control-label" Text=" "></asp:Label>
                                </div>
                            </div>
                        </div>


                 <%--       <div class="col-sm-2">
                            <div class="alert alert-success" role="alert">
                                <div class="form-group">
                                    <span class="badge badge-success">
                                        <h6>Time Elapsed</h6>
                                    </span>
                                    <asp:Label ID="lbltime" runat="server" class="control-label" Text=" "></asp:Label>


                                </div>
                            </div>
                        </div>--%>



                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <div class="col-sm-12 col-md-12">
                <div class="alert tablebg1">
                    <div class="row" id="dvQuest" runat="server">


                        <div class="col-sm-12 col-lg-12 mar">
                            <span class="badge badge-primary">
                                <h6>Question</h6>
                            </span>
                        </div>
                        <div class="col-sm-12 col-lg-12 mar">
                            <p>
                                <asp:Label ID="lblQuestion" runat="server" Text=" "></asp:Label>
                            </p>
                        </div>



                        <div class="col-sm-6 col-lg-6 mar">

                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:RadioButton ID="rdBtn1" AutoPostBack="true" OnCheckedChanged="rdBtn1_CheckedChanged" runat="server" />
                                    <%-- <input type="radio" name="optradio" id="rdBtn1" runat="server">--%>
                                            &nbsp;&nbsp; A.
                                </div>

                                <div class="col-sm-10">
                                    <asp:Label ID="lblOption1" runat="server" Text=" "></asp:Label>

                                </div>
                            </div>


                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:RadioButton ID="rdBtn2" type="radio" AutoPostBack="true" OnCheckedChanged="rdBtn2_CheckedChanged" runat="server" />

                                    &nbsp;&nbsp; B.
                                </div>

                                <div class="col-sm-10">
                                    <asp:Label ID="lblOption2" runat="server" Text=" "></asp:Label>

                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:RadioButton ID="rdBtn3" type="radio" AutoPostBack="true" OnCheckedChanged="rdBtn3_CheckedChanged" runat="server" />

                                    &nbsp;&nbsp; C.
                                </div>

                                <div class="col-sm-10">
                                    <asp:Label ID="lblOption3" type="radio" runat="server" Text=" "></asp:Label>

                                </div>
                            </div>

                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <asp:RadioButton ID="rdBtn4" type="radio" AutoPostBack="true" OnCheckedChanged="rdBtn4_CheckedChanged" runat="server" />

                                    &nbsp;&nbsp; D.
                                </div>

                                <div class="col-sm-10">
                                    <asp:Label ID="lblOption4" runat="server" Text=" "></asp:Label>

                                </div>
                            </div>



                        </div>

                        <div class="col-sm-2 col-lg-1 mar"></div>
                        <div class="col-sm-4 col-lg-3 mar">
                        </div>




                        <div class="col-sm-2 col-lg-1 mar"></div>
                        <div class="col-sm-2 col-lg-1 mar"></div>
                    </div>
                    <br>

                    <asp:HiddenField Value="0" runat="server" ID="hdnQuestionID" />
                    <asp:HiddenField Value="All" runat="server" ID="hdnType" />
                    <div class="row">



                        <div class="col-2 mar imgcenter alert alert-success">
                            <p>
                                Question 
                                <asp:Label ID="lblFirst" runat="server" class="control-label" Text=" "></asp:Label>
                                of 
                                <asp:Label ID="lblLast" runat="server" class="control-label" Text=" "></asp:Label>
                            </p>
                        </div>

                        <div class="col-5 mar "></div>

                        <div class="col-4 mar text-right">


                            <asp:Button ID="btnViewIncomplete" class="btn btn-danger" runat="server" Text="View Incomplete" OnClick="btnViewIncomplete_Click" />
                            <asp:Button ID="btnViewAll" class="btn btn-warning" runat="server" Text="View All" OnClick="btnViewAll_Click" />
                            <asp:Button ID="btnPrevious" class="btn btn-warning" runat="server" Text="Previous" OnClick="btnPrevious_Click" />
                            <asp:Button ID="btnNext" class="btn btn-warning" runat="server" Text="Next" OnClick="btnNext_Click" />
                            <asp:Button ID="btnSubmit" class="btn btn-warning" runat="server" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return confirm('Are you sure you want to Submit the Exam?')" />


                        </div>


                    </div>
                </div>
            </div>

            </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br>

    <div class="container-fluid">
        <div class="row footer">
            <div class="col-sm ">Footer Text</div>
        </div>
    </div>

    <script type="text/javascript">
        function startCountdown(timeLeft) {
            var interval = setInterval(countdown, 1000);
            update();

            function countdown() {
                if (--timeLeft > 0) {
                    update();
                } else {
                    clearInterval(interval);
                    update();
                    completed();
                }
            }
            function update() {
                hours = Math.floor(timeLeft / 3600);
                minutes = Math.floor((timeLeft % 3600) / 60);
                seconds = timeLeft % 60;
                document.getElementById('<%=lblDuration.ClientID%>').innerHTML = '' + hours + ':' + minutes + ':' + seconds;
            }
            function completed() {
                alert('Time Over! Click Ok To See Result');
                window.open('UserDashBoard.aspx');
               /* ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Submited Successfully!');window.open('UserDashBoard.aspx');", true);*/
            }
        }
    </script>


   <%-- <script type="text/javascript">       
        var seconds = 3;
        function countdown() {
            seconds = seconds - 1;
            if (seconds < 0) {                   
                window.location = "test.aspx";
               

            } else {
               
                document.getElementById("countdown").innerHTML = seconds;
               
                window.setTimeout("countdown()", 1000);
            }
        }

        
        countdown();

    </script>--%>




  
</asp:Content>
