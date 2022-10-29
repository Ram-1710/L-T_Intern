using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL;
using DAL;
using Model;
using System.EnterpriseServices;

namespace L_T_Defence
{
    public partial class RegistrationAcceptance : System.Web.UI.Page
    {
        RegistrationAcceptanceBLL ObjRegistrationAcceptanceBLL = new RegistrationAcceptanceBLL();
        RegistrationAcceptanceModel ObjRegistrationAcceptanceModel = new RegistrationAcceptanceModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            
                DataView dv = ObjRegistrationAcceptanceBLL.Grid();
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





        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        Button status = (Button)GridView1.Rows[i].FindControl("btnActivate");
        //        string state = status.CommandArgument.ToString();

        //        if (state.Equals("False"))
        //        {
        //            status.Text = "Activate";
        //            status.Attributes.Add("class", "btn btn-primary");
        //        }
        //        else 
        //        {
        //            status.Text = "Decline";
        //            status.Attributes.Add("class", "btn btn-danger");
        //        }
        //    }
        //}

        protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            int index = row.RowIndex;
            int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
            hdn.Value = ID.ToString();

            Button status = (Button)GridView1.Rows[index].FindControl("btnActivate");
            String state = status.CommandArgument.ToString();

            if (e.CommandName == "Activate" && status.Text == "Activate")
            {
                ObjRegistrationAcceptanceModel.imode = 104;
                ObjRegistrationAcceptanceModel.ID = ID;
                int iRet = ObjRegistrationAcceptanceBLL.RegistrationAcceptanceUID(ObjRegistrationAcceptanceModel);
                if (iRet > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Activated Successfully!'); ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Activation Failed'); ", true);
                }
                Bind();
            }
          
            else
            {
                ObjRegistrationAcceptanceModel.imode = 105;
                ObjRegistrationAcceptanceModel.ID = ID;
                int iRet = ObjRegistrationAcceptanceBLL.RegistrationAcceptanceUID(ObjRegistrationAcceptanceModel);
                if (iRet > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Dectivated Successfully!'); ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Deactivation Failed'); ", true);
                }
                Bind();
            }
        }
    }
}