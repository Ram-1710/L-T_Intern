using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace L_T_Defence
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert1", "alert('Please enter  User!'); ", true);
                txtUserName.Focus();


            }
            else if (txtPassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert1", "alert('Please enter  password!'); ", true);
                txtPassword.Focus();
            }
            else
            {
                if (txtUserName.Text == "admin" && txtPassword.Text == "admin")
                {
                    Session["UserId"] = 99999;
                    Session["Name"] = "admin";
                    Session["PassNo"] = "weqruiop";
                    Response.Redirect("~/DashBoard.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert1", "alert('Please enter  Valid Credentials!'); ", true);
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
    }
}