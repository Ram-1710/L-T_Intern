using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.IO;

namespace L_T_Defence
{
    public partial class DepartmentModuleMaster : System.Web.UI.Page
    {
        DepartmentModuleBLL ObjDepartmentModuleBLL = new DepartmentModuleBLL();
        DepartmentModuleModel objDepartmentModuleModel = new DepartmentModuleModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = btnSubmit.UniqueID;
            btnSubmit.Focus();
            if (!IsPostBack)
            {
                btnSubmit.Focus();
                Bind();

               
            }
        }
        private void Bind()
        {
            try
            {
                DataView dv = ObjDepartmentModuleBLL.Grid();
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
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles2(); ", true);

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Clear()
        {
            btnSubmit.Text = "Save";
            txtModule.Text = " ";
            
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
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
                objDepartmentModuleModel.imode = 101;
                //objDepartmentModuleModel.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                objDepartmentModuleModel.DepartmentModule = txtModule.Text.Trim();
                int iret = ObjDepartmentModuleBLL.DepartmentModuleUID(objDepartmentModuleModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Module Already Exists !');", true);
                }
            }
            else
            {
                objDepartmentModuleModel.imode = 102;
                objDepartmentModuleModel.ID = Convert.ToInt32(hdn.Value);
              
                objDepartmentModuleModel.DepartmentModule = txtModule.Text.Trim();
                
                int iret = ObjDepartmentModuleBLL.DepartmentModuleUID(objDepartmentModuleModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Updated Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Module Already Exists !');", true);
                }
            }
            Bind();
            Clear();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Upd")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                hdn.Value = ID.ToString();
                btnSubmit.Text = "Update";
                DataView dv = ObjDepartmentModuleBLL.BindByID(ID);
                if (dv.Table.Rows.Count > 0)
                {                    
                    txtModule.Text = dv.Table.Rows[0]["DepartmentModule"].ToString();
                }
                Bind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);

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
                    objDepartmentModuleModel.imode = 106;
                    objDepartmentModuleModel.ID = ID;
                    objDepartmentModuleModel.ActiveFlag = 1;
                    int ires = ObjDepartmentModuleBLL.DepartmentModuleUID(objDepartmentModuleModel);
                    if (ires > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activated Successfully!');", true);
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

                    objDepartmentModuleModel.imode = 103;
                    objDepartmentModuleModel.ID = ID;
                    objDepartmentModuleModel.ActiveFlag = 0;
                    int ires = ObjDepartmentModuleBLL.DepartmentModuleUID(objDepartmentModuleModel);
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
            //else if (e.CommandName == "Del")
            //{
            //    GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            //    int index = row.RowIndex;
            //    int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
            //    int ires = -1;
            //    objDepartmentModuleModel.imode = 103;
            //    objDepartmentModuleModel.ID = ID;
            //    ires = ObjDepartmentModuleBLL.DepartmentModuleUID(objDepartmentModuleModel);
            //    if (ires > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deleted Successfully!');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deletion Failed!');", true);
            //    }
            //    Bind();
            //    Clear();
            //}
        }

        //private void ExportGridToExcel()
        //{

        //    GridView gv = new GridView();
        //    gv.DataSource = ObjDepartmentModuleBLL.Grid();
        //    gv.DataBind();
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.Charset = "";
        //    string FileName = "Module" + DateTime.Now + ".xls";
        //    StringWriter strwritter = new StringWriter();
        //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //    gv.GridLines = GridLines.Both;
        //    gv.HeaderStyle.Font.Bold = true;

        //    gv.RenderControl(htmltextwrtter);
        //    Response.Write(strwritter.ToString());
        //    Response.End();

        //}
       
        //protected void ExportToXL_Click(object sender, EventArgs e)
        //{
        //    //ExportGridToExcel();
        //}
    }
}