using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace L_T_Defence
{
    public partial class UserDashBoard : System.Web.UI.Page
    {
        ResultsModel ResultsModelObj = new ResultsModel();
        ResulstsBLL ResulstsBLLObj = new ResulstsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
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
        private void Bind()
        {
            int X = Convert.ToInt32(Session["ID"]);
          
            DataView dv = ResulstsBLLObj.ResultById(X,-1,-1,-1);
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

        protected void btnExam_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ExamScreen.aspx");
        }
    }
}