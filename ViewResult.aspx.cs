using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BLL;
using Model;
using System.Data;
namespace L_T_Defence
{
    public partial class ViewResult : System.Web.UI.Page
    {
        DepartmentModuleBLL ObjDepartmentModuleBLL = new DepartmentModuleBLL();
        ExamBLL ObjExamBLL = new ExamBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepart.DataSource = ObjDepartmentModuleBLL.DropDownBind(1);
                ddlDepart.DataTextField = "DepartmentName";
                ddlDepart.DataValueField = "ID";
                ddlDepart.DataBind();
                ddlDepart.Items.Insert(0, new ListItem("select Department", "-1"));
                
            }
        }
        //private void Bind()
        //{
        //    try
        //    {
        //        DataView dv = ObjExamBLL.ViewResult();
        //        if (dv.Count > 0)
        //        {
        //            GridView1.DataSource = dv;
        //        }
        //        else
        //        {
        //            GridView1.DataSource = null;
        //        }
        //        GridView1.DataBind();
        //        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);

        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
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
            DataView dv = ObjExamBLL.GridBindByDepartment(Convert.ToInt32(ddlDepart.SelectedValue));
            GridView1.DataSource = dv;
            GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }
    }
}