<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DeptCatModuleCombinationPage.aspx.cs" Inherits="L_T_Defence.DeptCatModuleCombinationPage" %>

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
            <h3><strong>Combination Master</strong></h3>
        </div>
    </div>

    <div class="col-sm">
        <div class="alert tablebg1 table-responsive">
            <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound1" OnRowCommand="GridView1_RowCommand" runat="server"
                CssClass="table table-striped table-bordered table-hover"  EmptyDataText="No Matched Records Found" ClientIDMode="Static">
                <Columns>
                    <asp:TemplateField HeaderText="S.NO">
                        <ItemTemplate>
                            <span>
                                <%# Container.DataItemIndex + 1 %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" FooterStyle-Width="100%" />
                    <asp:BoundField DataField="TypeName" HeaderText="Category" FooterStyle-Width="100%" />
                    <asp:BoundField DataField="DepartmentModule" HeaderText="Module" FooterStyle-Width="100%" />

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="btnEdit" CommandName="Upd" ImageUrl="~/images/edit.png" Width="30px" Height="30px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IsActive">
                        <ItemTemplate>
                            <asp:Image runat="server" ID="imgActive" CommandName="Act"
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

                    <%--<asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnDelete" CommandName="Del" ImageUrl="~/images/delete.png" Width="40px" Height="40px" OnClientClick="return confirm('Are you sure you want to delete this record?')" />

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>

        </div>
    </div>

    <br />
    <div class="col-sm-12 col-md-12 ">
        <div class="alert tablebg1">
            <div class="row">

                <div class="col-sm-1 col-lg-1 mar">Department</div>
                <div class="col-sm-3 col-lg-3 mar">

                    <asp:DropDownList ID="ddlDepartment" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please select the Department">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-1 col-lg-1 mar">Category</div>
                <div class="col-sm-3 col-lg-3 mar">
                    <asp:DropDownList ID="ddlCategory" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlCategory" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please select the Category">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Module</div>
                <div class="col-sm-3 col-lg-3 mar">
                    <asp:DropDownList ID="ddlModule" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlModule" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please select the Module">
                    </asp:RequiredFieldValidator>
                </div>




                <div class="col-12 mar imgcenter">
                    <%--<button type="button" class="btn btn-primary">Submit</button>--%>
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="Department" runat="server" Text="Save" OnClick="btnSubmit_Click" />
                    <%-- <button type="button" class="btn btn-danger">Cancel</button>--%>
                    <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </div>


        </div>

    </div>

    <asp:HiddenField Value="0" runat="server" ID="hdn" />

</asp:Content>
