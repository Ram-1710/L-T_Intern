using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;

namespace L_T_Defence
{
    public partial class DownloadHistoryPage : System.Web.UI.Page
    {
        MaterialModel MaterialModelObj = new MaterialModel();
        MaterialBLL MaterialBLLObj = new MaterialBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = MaterialBLLObj.DownloadsDetailsGrid();
                GridView1.DataBind();
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
    }
}