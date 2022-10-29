<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="L_T_Defence.DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                "bFilter": true,
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
            <h3><strong>Dash Board</strong></h3>
        </div>
    </div>

 
    <div class="col-sm-12 col-md-12 col-lg-12 ">
        <div class="alert tablebg1">
            <div class="row">

                <div class="col-sm-1 col-lg-1 mar">Department</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlDepart" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" runat="server">
                        
 <asp:ListItem Text="--select--" Value="-1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlDepart" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Select the Department">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Category</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlCategory" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server">
                        
 <asp:ListItem Text="--select--" Value="-1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Select the Category">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Module</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlModuleee" ValidationGroup="Department" CssClass="form-control" runat="server">
 <asp:ListItem Text="--select--" Value="-1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlModuleee" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Select the Module">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-3 col-lg-3 mar">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="Go" />
                </div>
            </div>
        </div>
    </div>

     <div class="row ">
        <div class="col-sm">
            <h3><strong>Results</strong></h3>
        </div>
    </div>


    <div class="row">
        <div class="col-sm-9 col-md-9 col-lg-9 ">
            <div class="alert tablebg1 table-responsive">

                <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server"
                    CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Matched Records Found" ClientIDMode="Static">
                    <Columns>
                        <asp:TemplateField HeaderText="S.NO">
                            <ItemTemplate>
                                <span>
                                    <%# Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Category" HeaderText="Category Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="Module" HeaderText="Module Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="Name" HeaderText=" Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="ExamDate" HeaderText="ExamDate" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="Result" HeaderText="Result" FooterStyle-Width="100%" />

                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-sm-3 col-md-3 col-lg-3 ">
            <div class="alert tablebg1" style="background-color: #ffbb33">
                <div class="col-sm-3 col-lg-3 mar">
                    <span><b>Departments&nbsp:</b>&nbsp<asp:Label ID="lblDepartment" runat="server" Text=" "></asp:Label></span>
                </div>
                <div class="col-sm-3 col-lg-3 mar">
                    <span><b>Categories&nbsp:</b>&nbsp<asp:Label ID="lblCategory" runat="server" Text=" "></asp:Label></span>
                </div>
                <div class="col-sm-3 col-lg-3 mar">
                    <span><b>Modules&nbsp:</b>&nbsp<asp:Label ID="lblModule" runat="server" Text=" "></asp:Label></span>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
