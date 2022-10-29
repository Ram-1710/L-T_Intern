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
    public partial class DashBoard : System.Web.UI.Page
    {
        ResultsModel ResultsModelObj = new ResultsModel();
        ResulstsBLL ResulstsBLLObj = new ResulstsBLL();
        QuestionBankBLL QuestionBankBLLObj = new QuestionBankBLL();
        MaterialBLL MaterialBLLObj = new MaterialBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int x = Convert.ToInt32(ddlDepart.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModuleee.SelectedValue);
                GridView1.DataSource = ResulstsBLLObj.ResultById(-1,x,y,z);
                GridView1.DataBind();

                ddlDepart.DataSource = QuestionBankBLLObj.GetActiveDepartments();
                ddlDepart.DataTextField = "DepartmentName";
                ddlDepart.DataValueField = "ID";
                ddlDepart.DataBind();
                ddlDepart.Items.Insert(0, new ListItem("select Department", "-1"));

                DataView dv = ResulstsBLLObj.GetCount(0);
                lblDepartment.Text = dv.Table.Rows[0]["NoofDepartments"].ToString();

                DataView dv1 = ResulstsBLLObj.GetCount(1);
                lblCategory.Text = dv1.Table.Rows[0]["NoofCategories"].ToString();

                DataView dv2 = ResulstsBLLObj.GetCount(2);
                lblModule.Text = dv2.Table.Rows[0]["NoofModules"].ToString();
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
        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = MaterialBLLObj.GridBindByDepartment(Convert.ToInt32(ddlDepart.SelectedValue));
            ddlCategory.DataTextField = "TypeName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
            ddlModuleee.Enabled = false;
            ddlCategory_SelectedIndexChanged(null, null);
            //GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptId(Convert.ToInt32(ddlDepart.SelectedValue));
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModuleee.Enabled = true;
            int x = Convert.ToInt32(ddlDepart.SelectedValue);
            int y = Convert.ToInt32(ddlCategory.SelectedValue);
            ddlModuleee.DataSource = MaterialBLLObj.GridBindByDepartmentCategory(x, y);
            ddlModuleee.DataTextField = "DepartmentModule";
            ddlModuleee.DataValueField = "ModuleId";
            ddlModuleee.DataBind();
            ddlModuleee.Items.Insert(0, new ListItem("select Module", "-1"));

            //GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptandCategory(x, y);
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(ddlDepart.SelectedValue);
            int y = Convert.ToInt32(ddlCategory.SelectedValue);
            int z = Convert.ToInt32(ddlModuleee.SelectedValue);
            GridView1.DataSource = ResulstsBLLObj.ResultById(-1, x, y, z);
            GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //if(ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue == "" || ddlCategory.SelectedValue == "-1")
            //{
            //    ddlCategory.DataSource = MaterialBLLObj.GridBindByDepartment(Convert.ToInt32(ddlDepart.SelectedValue));
            //    ddlCategory.DataTextField = "TypeName";
            //    ddlCategory.DataValueField = "CategoryId";
            //    ddlCategory.DataBind();
            //    ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));

            //    GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptId(Convert.ToInt32(ddlDepart.SelectedValue));
            //    GridView1.DataBind();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //}
            //else if(ddlDepart.SelectedValue != "-1" &&  ddlCategory.SelectedValue != "-1" && ddlCategory.SelectedValue == "")
            //{
            //    int x = Convert.ToInt32(ddlDepart.SelectedValue);
            //    int y = Convert.ToInt32(ddlCategory.SelectedValue);
            //    ddlModuleee.DataSource = MaterialBLLObj.GridBindByDepartmentCategory(x, y);
            //    ddlModuleee.DataTextField = "DepartmentModule";
            //    ddlModuleee.DataValueField = "ModuleId";
            //    ddlModuleee.DataBind();
            //    ddlModuleee.Items.Insert(0, new ListItem("select Module", "-1"));

            //    GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptandCategory(x, y);
            //    GridView1.DataBind();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //}
            //else if(ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModuleee.SelectedValue == "")
            //{
            //    int x = Convert.ToInt32(ddlDepart.SelectedValue);
            //    int y = Convert.ToInt32(ddlCategory.SelectedValue);
            //    GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptandCategory(x, y);
            //    GridView1.DataBind();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //}
            //else if (ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModuleee.SelectedValue == "-1")
            //{
            //    int x = Convert.ToInt32(ddlDepart.SelectedValue);
            //    int y = Convert.ToInt32(ddlCategory.SelectedValue);
            //    GridView1.DataSource = ResulstsBLLObj.GetResultsByDeptandCategory(x, y);
            //    GridView1.DataBind();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //}

            //else if (ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" || ddlModuleee.SelectedValue != "-1")
            //{
            //    int x = Convert.ToInt32(ddlDepart.SelectedValue);
            //    int y = Convert.ToInt32(ddlCategory.SelectedValue);
            //    int z = Convert.ToInt32(ddlModuleee.SelectedValue);
            //    GridView1.DataSource = ResulstsBLLObj.GetResultsbyDeptCatModuleId(x, y, z);
            //    GridView1.DataBind();
            //    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            //}

        }

       
    }
}