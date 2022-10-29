<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RegistrationAcceptance.aspx.cs" Inherits="L_T_Defence.RegistrationAcceptance" %>

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
                }
            });
            return true;
        }
    </script>

    <div class="row ">
        <div class="col-sm">
            <h3><strong>Registered Users</strong></h3>
        </div>
    </div>

    <div class="row">
        <div class="col-sm">
            <div class="alert tablebg1 table-responsive">
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataKeyNames="ID"  runat="server" 
                    CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static" OnRowCommand="GridView1_RowCommand1" >
                    <columns>
                        <asp:TemplateField HeaderText="S.NO">
                            <itemtemplate>
                                <span>
                                    <%# Container.DataItemIndex + 1 %>
                                </span>
                            </itemtemplate>
                        </asp:TemplateField>
                                                
                        <asp:BoundField DataField="PersonalNumber" HeaderText="Personal Number" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="Name" HeaderText="Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="EmailId" HeaderText="Email ID" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Date Of Registration" DataFormatString="{0:dd/MM/yyyy}" FooterStyle-Width="100%" />                     

             
                       
                    </columns>
                </asp:GridView>

            </div>
        </div>
         <asp:HiddenField Value="0" runat="server" ID="hdn" />
    </div>
</asp:Content>
