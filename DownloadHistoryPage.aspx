<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DownloadHistoryPage.aspx.cs" Inherits="L_T_Defence.DownloadHistoryPage" %>

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
            <h3><strong>Users' Downloads' History</strong></h3>
        </div>
    </div>
    <div class="col-sm">
        <div class="alert tablebg1 table-responsive">
            <asp:GridView ID="GridView1" AutoGenerateColumns="false"  EmptyDataText="No Matched Records Found" DataKeyNames="UserId" runat="server"
                CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static">
                <Columns>
                    <asp:TemplateField HeaderText="S.NO">
                        <ItemTemplate>
                            <span>
                                <%# Container.DataItemIndex + 1 %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserId" HeaderText="UID" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                    <asp:BoundField DataField="TypeName" HeaderText="Category" />
                    <asp:BoundField DataField="DepartmentModule" HeaderText="Module" />
                    <asp:BoundField DataField="DownloadedDate" HeaderText="Date and Time" />
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
