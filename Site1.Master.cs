using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;using BLL;

namespace L_T_Defence
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        MaterialModel MaterialModelObj = new MaterialModel();
        MaterialBLL MaterialBLLObj = new MaterialBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] != null)
            {
                if (Session["Name"].ToString() != "admin")
                {
                    navbarText.Visible = false;
                    //btnSignOut.Visible = true;
                    btnSignOut.Text = "Exit";
                    //btnhome.Visible = false;
                }
                else
                {
                    //home.Visible = true;
                    navbarText.Visible = true;
                    //btnhome.Visible = false; 
                }
            }     
            else
            {
                navbarText.Visible = false;
                btnSignOut.Visible = true;
                btnSignOut.Text = "Exit";
                //btnhome.Visible = false;
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            if(btnSignOut.Text == "Log Out")
            {
                Session.Abandon();
                Session.Clear();
                Response.Redirect("~/LoginPage.aspx");
            }
            else
            {
                if (Session["LoginUserName"] != null)
                {
                    MaterialModelObj.imode = 101;
                    MaterialModelObj.UserId = Session["LoginUserName"].ToString();
                    MaterialModelObj.DepartmentId = Convert.ToInt32(Session["Department"]);
                    MaterialModelObj.INorOUT = 0;
                    MaterialBLLObj.GetMaterialLoginDetails(MaterialModelObj);
                }
                Session.Abandon();
                Session.Clear();
                Response.Redirect("~/LoginPage.aspx");
            }
           
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "confirm('Are you sure to go home page?');", true);

           // Response.Redirect("~/LoginPage.aspx");

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            
            if (Session["LoginUserName"] != null)
            {
                MaterialModelObj.imode = 101;
                MaterialModelObj.UserId = Session["LoginUserName"].ToString();
                MaterialModelObj.DepartmentId = Convert.ToInt32(Session["Department"]);
                MaterialModelObj.INorOUT = 0;
                MaterialBLLObj.GetMaterialLoginDetails(MaterialModelObj);
            }          

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "confirm('Are you sure to go home page?');", true);

            Response.Redirect("~/LoginPage.aspx");
        }
    }
}