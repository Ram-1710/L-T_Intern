<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="QuestionBank.aspx.cs" Inherits="L_T_Defence.QuestionBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                "bFilter": true,
                "bPagination": true,
                "sPaginationType": "full_numbers",
                "bStateSave": true,
                "bPaginate": true,
                "bInfo": true,
                fixedHeader: {
                    header: true,
                    footer: false
                },
            });
            return true;
        }

    </script>
    <script type="text/javascript">  
        function Validation() {
            var stralert = "";
            if (document.getElementById("<%=ddlDepart.ClientID %>").value == '-1') {
                stralert += 'Select Department';
            }
            if (document.getElementById("<%=ddlCategory.ClientID %>").value == '-1') {
                stralert += 'Select Category';
            }
            if (document.getElementById("<%=ddlModuleee.ClientID %>").value == '-1') {
                stralert += 'Select Module';
            }
            if (document.getElementById("<%=txtQuestion.ClientID %>").value == '') {
                stralert += 'Enter Question\n';
            }
            if (document.getElementById("<%=txtOption1.ClientID %>").value == '') {
                stralert += 'Enter Option 1 \n';
            }
            if (document.getElementById("<%=txtOption2.ClientID %>").value == '') {
                stralert += 'Enter Option 2';
            }
            if (document.getElementById("<%=txtOption3.ClientID %>").value == '') {
                stralert += 'Enter Option 3';
            }
            if (document.getElementById("<%=txtOption4.ClientID %>").value == '') {
                stralert += 'Enter Option 4';
            }

            if (stralert != "") {
                alert(stralert);
                return false;
            }
            else {
                return true;
            }
        }
    </script>


    <div class="row ">
        <div class="col-sm">
            <h3><strong>Question Bank</strong></h3>
        </div>
    </div>


    <br />

    <div class="col-sm">
        <div class="alert tablebg1 table-responsive">
            <div class="row">

                <div class="col-sm-1 col-lg-1 mar">Department</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlDepart" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlDepart" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Department">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Category</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlCategory" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlDepart" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Category">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Module</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlModuleee" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlModuleee" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Module">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-3 col-lg-3 mar">
                    <asp:Button ID="btnGo" CssClass="btn btn-primary" runat="server" Text="Go" OnClick="btnGo_Click" />
                </div>
            </div>
            <br />
            <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="GridView1_RowCommand" runat="server"
                CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static" EmptyDataText="No Questions in this combination"
                ShowHeaderWhenEmpty="True">
                <Columns>
                    <asp:TemplateField HeaderText="S.NO">
                        <ItemTemplate>
                            <span>
                                <%# Container.DataItemIndex + 1 %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:BoundField DataField="Question" HeaderText="Question" />

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="btnEdit" CommandName="Upd" ImageUrl="~/images/edit.png" Width="30px" Height="30px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="btnDelete" CommandName="Del" ImageUrl="~/images/delete.png" Width="30px" Height="30px" OnClientClick="return confirm('Are you sure you want to delete this record?')" />

                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <asp:HiddenField Value="0" runat="server" ID="hdn" />
        </div>
    </div>
    <div class="col">
        <div class="alert tablebg1">

            <%--  <div class="row">

                <div class="col-sm-1 col-lg-1 mar">Department</div>
                <div class="col-sm-3 col-lg-3 mar">

                    <asp:DropDownList ID="ddlDepartment" ValidationGroup="Question" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Department">
                    </asp:RequiredFieldValidator>
                </div>

                 <div class="col-sm-1 col-lg-1 mar">Category</div>
                <div class="col-sm-3 col-lg-3 mar">

                    <asp:DropDownList ID="ddlCategory2" ValidationGroup="Question" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory2_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Department">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Module</div>
                <div class="col-sm-3 col-lg-3 mar">

                    <asp:DropDownList ID="ddlModule" ValidationGroup="Question" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlModule" InitialValue="-1" ForeColor="Red" ValidationGroup="Question"
                        runat="server" ErrorMessage="Please select the Department">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2 col-lg-2 mar"></div>
            </div>--%>

            <div class="col-sm-12 col-lg-12 mar">
                <span class="badge badge-primary">
                    <h6>Question</h6>
                </span>
            </div>


            <div class="col-sm-6 col-lg-6 mar">
                <asp:TextBox ID="txtQuestion" TextMode="MultiLine" CssClass="form-control" Height="200px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtQuestion" ForeColor="Red" ValidationGroup="Question" runat="server" ErrorMessage="Please Enter Question"></asp:RequiredFieldValidator>
            </div>
            <br>

            <div class="col-sm-12 col-lg-12 mar">
                <span class="badge badge-warning">
                    <h6>Options</h6>
                </span>
            </div>
            <div class="col-sm-6 col-lg-6 mar">


                <div class="row">
                    <div class="col-sm-2">
                        <input type="radio" name="optradio" id="rdBtn1" runat="server">
                        &nbsp;&nbsp; A.
                    </div>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txtOption1" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOption1" ForeColor="Red" ValidationGroup="Question" runat="server" ErrorMessage="Please Enter Option1"></asp:RequiredFieldValidator>
                </div>
                <br>

                <div class="row">
                    <div class="col-sm-2">
                        <input type="radio" name="optradio" id="rdBtn2" runat="server">
                        &nbsp;&nbsp; B.
                    </div>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txtOption2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtOption2" ForeColor="Red" ValidationGroup="Question" runat="server" ErrorMessage="Please Enter Option2"></asp:RequiredFieldValidator>
                </div>
                <br>

                <div class="row">
                    <div class="col-sm-2">
                        <input type="radio" name="optradio" id="rdBtn3" runat="server">
                        &nbsp;&nbsp; C.
                    </div>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txtOption3" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtOption3" ForeColor="Red" ValidationGroup="Question" runat="server" ErrorMessage="Please Enter Option3"></asp:RequiredFieldValidator>
                </div>
                <br>

                <div class="row">
                    <div class="col-sm-2">
                        <input type="radio" name="optradio" id="rdBtn4" runat="server">
                        &nbsp;&nbsp; D.
                    </div>

                    <div class="col-sm-10">
                        <asp:TextBox ID="txtOption4" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtOption4" ForeColor="Red" ValidationGroup="Question" runat="server" ErrorMessage="Please Enter Option4"></asp:RequiredFieldValidator>
                </div>
                <br>
                <br>

                <div class="row">
                    <div class="col-12 mar imgcenter">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" ValidationGroup="Question" Text="Save" OnClientClick="return Validation();" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCancle" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancle_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
