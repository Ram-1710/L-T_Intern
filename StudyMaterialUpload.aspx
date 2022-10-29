<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StudyMaterialUpload.aspx.cs" Inherits="L_T_Defence.StudyMaterialUpload" %>

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
                dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>',
                buttons: ['excel']
            });
            return true;
        }

    </script>

    <div class="row ">
        <div class="col-sm">
            <h3><strong>Study Material Upload</strong></h3>
        </div>
    </div>

    <div class="row">
        <div class="col-sm">
            <div class="alert tablebg1 table-responsive">
                <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="GridView1_RowCommand" runat="server"
                    CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Matched Records Found" ClientIDMode="Static">
                    <Columns>
                        <asp:TemplateField HeaderText="S.NO">
                            <ItemTemplate>
                                <span>
                                    <%# Container.DataItemIndex + 1 %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="TypeName" HeaderText="Category" FooterStyle-Width="100%" />
                        <asp:BoundField DataField="DepartmentModule" HeaderText="Module" FooterStyle-Width="100%" />

                        <asp:TemplateField HeaderText="File Download">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="lnkAttachment" Width="30px" Height="30px"
                                    CommandName="ViewAttach" ImageUrl='<%# Eval("Extension").ToString() == "pdf" ? "~/images/PDF.png" : Eval("Extension").ToString() == "mp4" ? "~/images/video.png":Eval("Extension").ToString() == "doc" ? "~/images/word.png":Eval("Extension").ToString() == "ocx" ? "~/images/word.png":"~/images/rar.png"   %>'
                                    CommandArgument='<%# Eval("MaterialPDF") %>' Text='<%# Eval("MaterialPDF") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>

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

            </div>
        </div>
        <asp:HiddenField Value="0" runat="server" ID="hdn" />
        <!-- Table Grid Ends Here-->


        <!-- Form Starts Here-->

        <div class="col-sm-12 col-md-12 ">
            <div class="alert tablebg1">
                <div class="row">

                    <div class="col-sm-1 col-lg-1 mar">Department</div>
                    <div class="col-sm-2 col-lg-2 mar">

                        <asp:DropDownList ID="ddlDepartment" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please select the Department">
                        </asp:RequiredFieldValidator>
                    </div>


                    <div class="col-sm-1 col-lg-1 mar">Category</div>
                    <div class="col-sm-2 col-lg-2 mar">
                        <asp:DropDownList ID="ddlCategory" ValidationGroup="Department" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="-1" ControlToValidate="ddlCategory" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please select the Category">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-1 col-lg-1 mar">Module</div>
                    <div class="col-sm-2 col-lg-2 mar">
                        <asp:DropDownList ID="ddlModule" ValidationGroup="Department" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="-1" ControlToValidate="ddlModule" ForeColor="Red" ValidationGroup="Department"
                            runat="server" ErrorMessage="Please select the Module">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-1 col-lg-1 mar">File Upload</div>
                    <div class="col-sm-2 col-lg-2 mar">
                        <div class="custom-file">
                            <asp:FileUpload ID="fupDocument" CssClass="custom-file-input" runat="server" />
                            <label class="custom-file-label" for="FileUpload1">Choose file</label>
                            <asp:RequiredFieldValidator ID="rffupDocument" ForeColor="Red" runat="server" ControlToValidate="fupDocument" ValidationGroup="Department" ErrorMessage="Please select a file"></asp:RequiredFieldValidator>
                        </div>

                    </div>


                    <div class="col-sm-2 col-lg-1 mar"></div>
                    <div class="col-sm-4 col-lg-3 mar"></div>



                    <div class="col-12 mar imgcenter">

                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="Department" runat="server" Text="Save" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
