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
    public partial class StartPage : System.Web.UI.Page
    {
        MaterialModel MaterialModelObj = new MaterialModel();
        MaterialBLL MaterialBLLObj = new MaterialBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepartment.DataSource = MaterialBLLObj.GetDepartments();
                ddlDepartment.DataValueField = "Dept_Id";
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", "-1"));
            }
        }
       

        protected void btnRegisterdUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamLogin.aspx");
        }

        protected void btnAdministrator_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }

      


        protected void btnOk_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text != "" && ddlDepartment.SelectedValue != "-1")
            {
                Session["LoginUserName"] = txtUserName.Text;
                Session["Department"] = Convert.ToInt32(ddlDepartment.SelectedValue);
                MaterialModelObj.imode = 101;
                MaterialModelObj.UserId = txtUserName.Text;
                MaterialModelObj.INorOUT = 1;
                MaterialModelObj.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                int iRet = MaterialBLLObj.GetMaterialLoginDetails(MaterialModelObj);
                if(iRet > 0)
                {
                    Response.Redirect("StudyMaterial.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Please Enter valid credentials');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Enter or Select Necessary fields');", true);
            }
            //Response.Redirect("StudyMaterial.aspx");
        }
    }
}