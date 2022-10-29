<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="L_T_Defence.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <link href="css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="js/jquery-3.3.1.js" rel="stylesheet"></script>
    <script src="js/jquery.dataTables.min.js" rel="stylesheet"></script>
    <script src="js/dataTables.bootstrap4.min.js" rel="stylesheet"></script>

    <link href="css/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <script src="js/dataTables.fixedHeader.min.js" type="text/javascript"></script>--%>



    <style>
        div.dt-top-container {
            display: grid;
            grid-template-columns: 200px 100px auto;
        }

        div.dt-center-in-div {
            margin: 0 10px;
        }

        div.dt-filter-spacer {
            margin: 10px 0;
        }
    </style>

    <link href="css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <script src="js/jquery-3.3.1.js" rel="stylesheet"></script>
    <script src="js/jquery.dataTables.min.js" rel="stylesheet"></script>
    <script src="js/dataTables.bootstrap4.min.js" rel="stylesheet"></script>

    <link href="css/fixedHeader.dataTables.min.css" rel="stylesheet" />
    <script src="js/dataTables.fixedHeader.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        function gvStyles() {
            var oTable = $('#' + '<%=GridView1.ClientID%>').dataTable({
                "sDOM": "<'H'lfr>t<'F'ip>",
                "bAutoWidth": true,
                "searching": true,
                "bPagination": true,
                "sPaginationType": "full_numbers",
                "bStateSave": true,
                "bPaginate": true,
                "bInfo": true,
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>',
                buttons: ['excel'],
                fixedHeader: {
                    header: true,
                    footer: false
                }
            });
            return true;
        }
    </script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>


    <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.3/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.print.min.js"></script>

    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>




    <div class="row ">
        <div class="col-sm">
            <h3><strong>Registration Page</strong></h3>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 ">
        <div class="alert tablebg1">
            <div class="row">
                <div class="col-sm-1 col-lg-1 mar">Department</div>
                <div class="col-sm-3 col-lg-3 mar">

                    <asp:DropDownList ID="ddlDepart" CssClass="form-control" runat="server"></asp:DropDownList>

                </div>
                <div class="col-sm-4 col-lg-4 mar">
                    <asp:Button ID="btnShowAll" CssClass="btn btn-primary" runat="server" CausesValidation="false" Text="GO" OnClick="btnShowAll_Click" />
                </div>

            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm">
            <div class="alert tablebg1 table-responsive">
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" EmptyDataText="No Matched Records Found" DataKeyNames="ID" runat="server"
                    CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="S.NO">
                            <ItemTemplate>
                                <span>
                                    <%# Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" FooterStyle-Width="100%" />
                      <%--  <asp:BoundField DataField="PersonalNumber" HeaderText="Personal Number" FooterStyle-Width="100%" />--%>
                        <asp:BoundField DataField="Name" HeaderText="Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="UserName" HeaderText="UID" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="EmailId" HeaderText="Email ID" FooterStyle-Width="100%" />


                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnEdit" CommandName="Upd" ImageUrl="~/images/edit.png" Width="30px" Height="30px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:Image runat="server" ID="btnActive" CommandName="Act"
                                    ImageUrl='<%# Eval("ActiveFlag").ToString() == "1" ? "~/images/tick.png" : "~/images/cross.png" %>' Width="30px"
                                    Height="30px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activation">
                            <ItemTemplate>
                                <asp:Button ID="btnActivate" runat="server" CssClass='<%# Eval("ActiveFlag").ToString() == "1" ? "btn btn-danger" : "btn btn-primary" %>' CausesValidation="false" CommandName="Activate"
                                    Text="Activate" CommandArgument='<%# Eval("ActiveFlag") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Send Mail">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" CommandName="Mail" ImageUrl="~/images/SendMail.PNG" runat="server" FooterStyle-Width="100%" Height="30px" Width="30px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reset Password">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" CommandName="SendMail" ImageUrl="~/images/reset.PNG" runat="server" FooterStyle-Width="100%" Height="30px" Width="30px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>
        <asp:HiddenField Value="0" runat="server" ID="hdn" />
    </div>

    <br />


    <div class="row">

        <div class="col-sm-12 col-md-12 ">
            <div class="alert tablebg1">
                <div class="row">

                    <div class="col-sm-1 col-lg-1 mar">Department</div>
                    <div class="col-sm-3 col-lg-3 mar">

                        <asp:DropDownList ID="ddlDepartment" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Select the Department">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-1 col-lg-1 mar">Personal Number</div>
                    <div class="col-sm-3 col-lg-3 mar">
                        <asp:TextBox ID="txtPersonalNumber" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPersonalNumber" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Enter the Personal Number">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-1 col-lg-1 mar">Name</div>
                    <div class="col-sm-3 col-lg-3 mar">

                        <asp:TextBox ID="txtName" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Enter the Name">
                        </asp:RequiredFieldValidator>
                    </div>


                    <div class="col-sm-1 col-lg-1 mar">User Name</div>
                    <div class="col-sm-3 col-lg-3 mar">

                        <asp:TextBox ID="txtUserName" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtUserName" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Enter the UserName">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-1 col-lg-1 mar">Email ID</div>
                    <div class="col-sm-3 col-lg-3 mar">
                        <asp:TextBox ID="txtEmailId" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEmailId" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please Enter the EmailId">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="Department" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailId" 
                            ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                    </div>

                    <div class="col-sm-4 col-lg-4 mar"></div>
                    <%--  <div class="col-sm-4 col-lg-4 mar">
                        <label>Confirm Password</label>

                        <asp:TextBox ID="txtConfirmPassword" ValidationGroup="Department" type="password" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtConfirmPassword" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please retype the password"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Type="String"
                            ControlToCompare="txtPassword" ValidationGroup="Department" ForeColor="Red" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Password does not match.">
                        </asp:CompareValidator>

                    </div>--%>



                    <div class="col-12 mar imgcenter">

                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="Department" runat="server" Text="Save" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>





</asp:Content>
