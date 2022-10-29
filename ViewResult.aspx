<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewResult.aspx.cs" Inherits="L_T_Defence.ViewResult" %>

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

    <div class="row ">
        <div class="col-sm">
            <h3><strong>View Result</strong></h3>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-sm">
            <div class="alert tablebg1 table-responsive">
                <div class="col-sm-12 col-md-12 ">

                    <div class="col-sm-4 col-lg-4 mar"></div>
                    <div class="col-sm-1 col-lg-1 mar">Department</div>
                    <div class="col-sm-3 col-lg-3 mar">
                        <asp:DropDownList ID="ddlDepart" CssClass="form-control" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" EmptyDataText="No Matched Records Found" ShowHeaderWhenEmpty="true" AutoPostBack="true" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlDepart" InitialValue="-1" ForeColor="Red"
                            runat="server" ErrorMessage="Please select the Department">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-4 col-lg-4 mar"></div>
                </div>

                <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"
                    CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static">
                    <Columns>
                        <asp:TemplateField HeaderText="S.NO">
                            <ItemTemplate>
                                <span>
                                    <%# Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="PersonalNumber" HeaderText="P.NO" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="DepartmentModule" HeaderText="Module" />
                        <asp:BoundField DataField="ExamDate" HeaderText="Exam Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="NoOfQuestions" HeaderText="Total Questions" />
                        <asp:BoundField DataField="TotalQuestionsAnswered" HeaderText="Total Answers" />
                        <asp:BoundField DataField="Correct" HeaderText="Correct" />
                        <asp:BoundField DataField="Wrong" HeaderText="Incorrect" />
                        <asp:BoundField DataField="Status" HeaderText="Final Result" />
                  
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <asp:HiddenField Value="0" runat="server" ID="hdn" />
     


    </div>

</asp:Content>
