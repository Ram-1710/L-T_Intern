using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;

namespace L_T_Defence
{
    public partial class TypeMaster : System.Web.UI.Page
    {
        DepartmentBLL ObjdepartmentBLL = new DepartmentBLL();
        TypeModel ObjTypeModel = new TypeModel();
        TypeBLL ObjtypeBLL = new TypeBLL();
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
                DataView dv = ObjtypeBLL.GRIDBIND();
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

        public void Clear()
        {
            btnSubmit.Text = "Save";
            txtType.Text = " ";

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
                DataView dv = ObjtypeBLL.BindByID(ID);
                if (dv.Table.Rows.Count > 0)
                {

                    txtType.Text = dv.Table.Rows[0]["TypeName"].ToString();
                    //ddlActivation.SelectedValue = dv.Table.Rows[0]["ActiveFlag"].ToString();
                }
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
                    ObjTypeModel.imode = 106;
                    ObjTypeModel.TypeId = ID;
                    ObjTypeModel.ActiveFlag = 1;
                    int ires = ObjtypeBLL.TypeUID(ObjTypeModel);
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

                    ObjTypeModel.imode = 103;
                    ObjTypeModel.TypeId = ID;
                    ObjTypeModel.ActiveFlag = 0;
                    int ires = ObjtypeBLL.TypeUID(ObjTypeModel);
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
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {
                ObjTypeModel.imode = 101;
                ObjTypeModel.TypeName = txtType.Text.Trim();
                int iret = ObjtypeBLL.TypeUID(ObjTypeModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Category Already Exists!');", true);
                }
            }
            else
            {
                ObjTypeModel.imode = 102;
                ObjTypeModel.TypeId = Convert.ToInt32(hdn.Value);

                ObjTypeModel.TypeName = txtType.Text.Trim();
                //ObjTypeModel.ActiveFlag = Convert.ToInt32(ddlActivation.SelectedValue);
                int iret = ObjtypeBLL.TypeUID(ObjTypeModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Updated Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Category Already Exists !');", true);
                }
            }
            Bind();
            Clear();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}