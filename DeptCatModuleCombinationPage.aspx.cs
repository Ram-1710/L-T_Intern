using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
namespace L_T_Defence
{
    public partial class DeptCatModuleCombinationPage : System.Web.UI.Page
    {
        DepartmentBLL ObjDepartmentBLL = new DepartmentBLL();
        DepartmentModuleBLL ObjDepartmentModuleBLL = new DepartmentModuleBLL();
        TypeBLL ObjTypeBLL = new TypeBLL();
        DeptCatModuleBLL ObjDeptCatModuleBLL = new DeptCatModuleBLL();
        DeptCatModuleModel ObjDeptCatModuleModel = new DeptCatModuleModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = btnSubmit.UniqueID;
            btnSubmit.Focus();
            if (!IsPostBack)
            {
                btnSubmit.Focus();
                Bind();
                ddlDepartment.DataSource = ObjDepartmentBLL.Grid();
                ddlDepartment.DataValueField = "ID";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", "-1"));

                ddlModule.DataSource = ObjDepartmentModuleBLL.GetActiveModules();
                ddlModule.DataValueField = "ID";
                ddlModule.DataTextField = "DepartmentModule";
                ddlModule.DataBind();
                ddlModule.Items.Insert(0, new ListItem("Select Module", "-1"));

                ddlCategory.DataSource = ObjTypeBLL.GRID();
                ddlCategory.DataValueField = "TypeId";
                ddlCategory.DataTextField = "TypeName";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "-1"));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
        }

        public static void MakeAccessible(GridView grid)
        {
            if (grid.Rows.Count <= 0) return;
            grid.UseAccessibleHeader = true;
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            if (grid.ShowFooter)
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            MakeAccessible(GridView1);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {
                ObjDeptCatModuleModel.iMode = 101;
                ObjDeptCatModuleModel.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ObjDeptCatModuleModel.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                ObjDeptCatModuleModel.ModuelId = Convert.ToInt32(ddlModule.SelectedValue);

                int iret = ObjDeptCatModuleBLL.DeptCatModuleUID(ObjDeptCatModuleModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Combination Already Exists !');", true);
                }
            }
            else
            {
                ObjDeptCatModuleModel.iMode = 102;
                ObjDeptCatModuleModel.Id = Convert.ToInt32(hdn.Value);
                ObjDeptCatModuleModel.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ObjDeptCatModuleModel.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                ObjDeptCatModuleModel.ModuelId = Convert.ToInt32(ddlModule.SelectedValue);
                //ObjDeptCatModuleModel.ActiveFlag = Convert.ToInt32(ddlActivation.SelectedValue);

                int iret = ObjDeptCatModuleBLL.DeptCatModuleUID(ObjDeptCatModuleModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Updated Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Combination Already Exists !');", true);
                }
            }
            Bind();
            Clear();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);

        }
        private void Bind()
        {
            try
            {
                DataView dv = ObjDeptCatModuleBLL.GRID();
                if (dv.Count > 0)
                {
                    GridView1.DataSource = dv;
                }
                else
                {
                    GridView1.DataSource = null;
                }
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Clear()
        {
            btnSubmit.Text = "Save";
            ddlDepartment.SelectedValue = "-1";
            ddlModule.SelectedValue = "-1";
            ddlCategory.SelectedValue = "-1";
            //ddlActivation.SelectedValue = "-1";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Upd")
             {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int Id = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                hdn.Value = Id.ToString();
                btnSubmit.Text = "Update";
                DataView dv = ObjDeptCatModuleBLL.BindByID(Id);
                if (dv.Table.Rows.Count > 0)
                {
                    if (dv.Table.Rows[0]["ActiveFlag"].ToString() != "0")
                    {


                        if (ddlDepartment.Items.FindByValue(dv.Table.Rows[0]["DeptId"].ToString()) != null)
                        {
                            if (ddlCategory.Items.FindByValue(dv.Table.Rows[0]["TypeId"].ToString()) != null)
                            {
                                if (ddlModule.Items.FindByValue(dv.Table.Rows[0]["ModuleId"].ToString()) != null)
                                {
                                    ddlDepartment.SelectedValue = dv.Table.Rows[0]["DeptId"].ToString();
                                    ddlCategory.SelectedValue = dv.Table.Rows[0]["TypeId"].ToString();
                                    ddlModule.SelectedValue = dv.Table.Rows[0]["ModuleId"].ToString();
                                }
                                else
                                {
                                    Clear();
                                }

                            }
                            else
                            {
                                Clear();
                            }

                        }
                        else
                        {
                            Clear();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Combination is no longer active.Please Activate to Edit');", true);
                    }

                    //ddlActivation.SelectedValue = dv.Table.Rows[0]["ActiveFlag"].ToString();
                }

            }

            else if (e.CommandName == "Activate")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                Button status = (Button)GridView1.Rows[index].FindControl("btnActivate");
                String state = status.CommandArgument.ToString();
                if (status.Text == "Activate")
                {
                    ObjDeptCatModuleModel.iMode = 106;
                    ObjDeptCatModuleModel.Id = ID;
                    DataView dv = ObjDeptCatModuleBLL.GetDeptCatModule(ID);
                    ObjDeptCatModuleModel.DeptId = Convert.ToInt32(dv.Table.Rows[0]["Dept_Id"]);
                    ObjDeptCatModuleModel.CategoryId = Convert.ToInt32(dv.Table.Rows[0]["TypeId"]);
                    ObjDeptCatModuleModel.ModuelId = Convert.ToInt32(dv.Table.Rows[0]["ModuleId"]);

                    ObjDeptCatModuleModel.ActiveFlag = 1;
                    int ires = ObjDeptCatModuleBLL.DeptCatModuleUID(ObjDeptCatModuleModel);
                    if (ires == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activated Successfully!');", true);
                    }
                    else if (ires == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Department is no longer active');", true);
                    }
                    else if (ires == 3)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Category is no longer active');", true);
                    }
                    else if (ires == 4)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Module is no longer active');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activation Failed!');", true);
                    }
                    Bind();
                    Clear();
                }
                else
                {

                    ObjDeptCatModuleModel.iMode = 103;
                    ObjDeptCatModuleModel.Id = ID;
                    ObjDeptCatModuleModel.ActiveFlag = 0;
                    int ires = ObjDeptCatModuleBLL.DeptCatModuleUID(ObjDeptCatModuleModel);
                    if (ires > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Dectivated Successfully!'); ", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Deactivation Failed'); ", true);
                    }
                    Bind();
                    Clear();
                }

            }

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                Button status = (Button)GridView1.Rows[i].FindControl("btnActivate");
                string state = status.CommandArgument.ToString();

                if (state.Equals("0"))
                {
                    status.Text = "Activate";
                }
                else
                {
                    status.Text = "Deactivate";
                }
            }
        }
    }
}