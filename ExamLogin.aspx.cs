using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DAL;
using BLL;
using System.Web.Security;
using System.Data;
namespace L_T_Defence
{
    public partial class ExamLogin : System.Web.UI.Page
    {
        RegistrationBLL ObjRegistrationBLL = new RegistrationBLL();
        RegistrationModel ObjRegistrationModel = new RegistrationModel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Clear()
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            ObjRegistrationModel.UserName = txtUserName.Text.Trim();

            string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            ObjRegistrationModel.Password = sPassword;
            DataView dv = ObjRegistrationBLL.RegisterUserLogin(ObjRegistrationModel.UserName, ObjRegistrationModel.Password);
            if (dv.Count > 0)
            {
                Session["UserName"] = dv.Table.Rows[0]["UserName"];
                Session["Name"] = dv.Table.Rows[0]["Name"];
                Session["ID"] = dv.Table.Rows[0]["ID"];
                Session["DepartmentId"] = dv.Table.Rows[0]["DepartmentId"];
                Session["Department"] = dv.Table.Rows[0]["DepartmentName"];
                Response.Redirect("~/UserDashBoard.aspx");

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert1", "alert('Please enter Valid Credentials!'); ", true);
            }

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("LoginPage.aspx");
        }
    }
}