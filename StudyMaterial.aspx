<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StudyMaterial.aspx.cs" Inherits="L_T_Defence.StudyMaterial" %>

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
            <h3><strong>Study Material</strong></h3>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 ">
        <div class="alert tablebg1">
            <div class="row">
              
              
                <div class="col-sm-1 col-lg-1 mar">Category</div>
                <div class="col-sm-2 col-lg-2 mar">
                    <asp:DropDownList ID="ddlCategory" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlCategory" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Enter the Category">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-1 col-lg-1 mar">Module</div>
                <div class="col-sm-2 col-lg-2 mar">
                    <asp:DropDownList ID="ddlModule" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlModule" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Enter the Module">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-3 col-lg-3 mar">
                    <asp:Button ID="btnGo" CssClass="btn btn-primary" runat="server" Text="Go" OnClick="btnGo_Click"/>
                </div>

                  <div class="col-sm-1 col-lg-1 mar" runat="server" id="lbldept">Department</div>
                <div class="col-sm-2 col-lg-2 mar">

                    <asp:DropDownList ID="ddlDepartment" ValidationGroup="Department" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged1"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlDepartment" InitialValue="-1" ForeColor="Red" ValidationGroup="Department"
                        runat="server" ErrorMessage="Please Enter the Department">
                    </asp:RequiredFieldValidator>
                </div>


            </div>
        </div>
    </div>
    <br />
    <div class="col-sm">
        <div class="alert tablebg1 table-responsive">
            <asp:GridView ID="GridView1" AutoGenerateColumns="false" DataKeyNames="ID" EmptyDataText="No Matched Records Found" OnRowCommand="GridView1_RowCommand" runat="server"
                CssClass="table table-striped table-bordered table-hover" ClientIDMode="Static">
                <Columns>
                    <asp:TemplateField HeaderText="S.NO">
                        <ItemTemplate>
                            <span>
                                <%# Container.DataItemIndex + 1 %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--<asp:BoundField DataField="DepartmentName" HeaderText="Department Name" FooterStyle-Width="100%" />--%>
                    <asp:BoundField DataField="TypeName" HeaderText="Category" FooterStyle-Width="100%" />
                    <asp:BoundField DataField="DepartmentModule" HeaderText="Module" FooterStyle-Width="100%" />
                    <asp:TemplateField HeaderText="File Download">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" ID="lnkAttachment" Width="20px" Height="20px"
                                CommandName="ViewAttach" ImageUrl='<%# Eval("Extension").ToString() == "pdf" ? "~/images/PDF.png" : Eval("Extension").ToString() == "mp4" ? "~/images/video.png":Eval("Extension").ToString() == "doc" ? "~/images/word.png":Eval("Extension").ToString() == "ocx" ? "~/images/word.png":"~/images/rar.png"   %>'
                                CommandArgument='<%# Eval("MaterialPDF") %>' Text='<%# Eval("MaterialPDF") %>' />   <%-- OnClientClick="window.open('studymaterial.aspx?id=','openPDF','target=_blank')"--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>

