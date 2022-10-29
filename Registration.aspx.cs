using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
using System.IO;
using System.Net.Mail;

namespace L_T_Defence
{
    public partial class Registration : System.Web.UI.Page
    {

        RegistrationModel ObjRegistrationModel = new RegistrationModel();
        RegistrationBLL ObjRegistrationBLL = new RegistrationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepartment.DataSource = ObjRegistrationBLL.DropDownBind(2);
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", "-1"));

                ddlDepart.DataSource = ObjRegistrationBLL.DropDownBind(2);
                ddlDepart.DataTextField = "DepartmentName";
                ddlDepart.DataValueField = "ID";
                ddlDepart.DataBind();

                ddlDepart.Items.Insert(0, new ListItem("Show All", "-1"));
                Bind();

            }
        }

        private void Bind()
        {
            try
            {
                DataView dv = ObjRegistrationBLL.Grid();
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
        private void Clear()
        {
            btnSubmit.Text = "Save";
            txtName.Text = " ";
            ddlDepartment.SelectedValue = "-1";
            txtPersonalNumber.Text = "";
            txtEmailId.Text = "";
            txtUserName.Text = "";

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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {

                ObjRegistrationModel.imode = 101;
                ObjRegistrationModel.Name = txtName.Text.Trim();
                ObjRegistrationModel.UserName = txtUserName.Text.Trim();
                ObjRegistrationModel.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ObjRegistrationModel.PersonalNumber = txtPersonalNumber.Text.Trim();
                ObjRegistrationModel.EmailId = txtEmailId.Text.Trim();
                string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "SHA1");
                ObjRegistrationModel.Password = sPassword;
                //-------------------------------------------------
                string UserName = txtUserName.Text.Trim();

                System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
                string smtpUserName = "donot-reply@maple-software.com";
                string smtpPassword = "maple@123";

                string Receiver1 = txtEmailId.Text.Trim();   //mallesh.t@maple-software.com
                //string from = System.Configuration.ConfigurationManager.AppSettings["RegFromAddress"].ToString();
                string Sender1 = "donot-reply@maple-software.com";
                mail.Host = "secure214.sgcpanel.com";
                mail.Port = 2525;



                mail.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                MailAddress fromAddress = new MailAddress(Sender1, smtpUserName);
                MailMessage mailMessge = new MailMessage();
                mailMessge.From = fromAddress;
                mailMessge.To.Add(Receiver1);

                string EmailBody = "Your User Name : " + UserName + "  and  Your Default Password : admin";

                mailMessge.Body = EmailBody;
                mailMessge.IsBodyHtml = true;
                mailMessge.Subject = "Test Mail";

                int iret = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", " alert('Saved sucessfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Personal Number or UserName Already Exists!');", true);
                }
                mail.Send(mailMessge);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else
            {
                ObjRegistrationModel.imode = 102;
                ObjRegistrationModel.ID = Convert.ToInt32(hdn.Value);
                ObjRegistrationModel.Name = txtName.Text.Trim();
                ObjRegistrationModel.UserName = txtUserName.Text.Trim();
                ObjRegistrationModel.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                ObjRegistrationModel.PersonalNumber = txtPersonalNumber.Text.Trim();
                string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile("admin", "SHA1");
                ObjRegistrationModel.Password = sPassword;
                ObjRegistrationModel.EmailId = txtEmailId.Text.Trim();
                int iret = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated sucessfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Personal Number or UserName Already Exists!');", true);
                }
            }
            Bind();
            Clear();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Clear();
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                hdn.Value = ID.ToString();
                Button status = (Button)GridView1.Rows[index].FindControl("btnActivate");
                String state = status.CommandArgument.ToString();
                btnSubmit.Text = "Update";
                DataView dv = ObjRegistrationBLL.GridBindById(ID);
                if (dv.Table.Rows.Count > 0)
                {
                    if (dv.Table.Rows[0]["ActiveFlag"].ToString() != "0")
                    {
                        ddlDepartment.SelectedValue = dv.Table.Rows[0]["DepartmentId"].ToString();
                        txtEmailId.Text = dv.Table.Rows[0]["EmailId"].ToString();
                        txtName.Text = dv.Table.Rows[0]["Name"].ToString();
                        txtPersonalNumber.Text = dv.Table.Rows[0]["PersonalNumber"].ToString();
                        txtUserName.Text = dv.Table.Rows[0]["UserName"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Employee record has been deactivated.Activate to edit details');", true);
                    }

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
                DataView dv1 = ObjRegistrationBLL.GridBindById(ID);


                if (status.Text == "Activate")
                {


                    ObjRegistrationModel.imode = 106;
                    ObjRegistrationModel.ID = ID;
                    ObjRegistrationModel.ActiveFlag = 1;
                    ObjRegistrationModel.DepartmentId = Convert.ToInt32(dv1.Table.Rows[0]["DepartmentId"].ToString());
                    int ires = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
                    if (ires > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activated Successfully!');", true);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Department is no longer exists');", true);
                    }
                    Bind();
                    Clear();
                }
                else
                {

                    ObjRegistrationModel.imode = 107;
                    ObjRegistrationModel.ID = ID;
                    ObjRegistrationModel.ActiveFlag = 0;
                    int ires = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
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
            else if (e.CommandName == "Mail")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);

                string newPassword = "admin";
                string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
                DataView dv = ObjRegistrationBLL.GridBindById(ID);


                ObjRegistrationModel.DepartmentId = Convert.ToInt32(dv.Table.Rows[0]["DepartmentId"].ToString());
                ObjRegistrationModel.EmailId = dv.Table.Rows[0]["EmailId"].ToString();
                ObjRegistrationModel.Name = dv.Table.Rows[0]["Name"].ToString();
                ObjRegistrationModel.Password = sPassword;
                ObjRegistrationModel.PersonalNumber = dv.Table.Rows[0]["PersonalNumber"].ToString();
                ObjRegistrationModel.UserName = dv.Table.Rows[0]["UserName"].ToString();
                string UserName = dv.Table.Rows[0]["UserName"].ToString();
                //------------------------------------------------------------------------
                System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
                string smtpUserName = "donot-reply@maple-software.com";
                string smtpPassword = "maple@123";

                string Receiver1 = dv.Table.Rows[0]["EmailId"].ToString();     //mallesh.t@maple-software.com
                //string from = System.Configuration.ConfigurationManager.AppSettings["RegFromAddress"].ToString();
                string Sender1 = "donot-reply@maple-software.com";
                mail.Host = "secure214.sgcpanel.com";
                mail.Port = 2525;



                mail.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                MailAddress fromAddress = new MailAddress(Sender1, smtpUserName);
                MailMessage mailMessge = new MailMessage();
                mailMessge.From = fromAddress;
                mailMessge.To.Add(Receiver1);

                string EmailBody = "Your User Name :" + UserName + "  and  Your Default Password :" + newPassword;

                mailMessge.Body = EmailBody;
                mailMessge.IsBodyHtml = true;
                mailMessge.Subject = "Test Mail";

                ObjRegistrationModel.imode = 102;
                ObjRegistrationModel.ID = ID;

                int iRet = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
                if (iRet > 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Login Details sent Successfully!'); ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Login Details sent failed'); ", true);
                }

                mail.Send(mailMessge);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (e.CommandName == "SendMail")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);

                string newPassword = "mypassword";
                string sPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
                DataView dv = ObjRegistrationBLL.GridBindById(ID);


                ObjRegistrationModel.DepartmentId = Convert.ToInt32(dv.Table.Rows[0]["DepartmentId"].ToString());
                ObjRegistrationModel.EmailId = dv.Table.Rows[0]["EmailId"].ToString();
                ObjRegistrationModel.Name = dv.Table.Rows[0]["Name"].ToString();
                ObjRegistrationModel.Password = sPassword;
                ObjRegistrationModel.PersonalNumber = dv.Table.Rows[0]["PersonalNumber"].ToString();
                ObjRegistrationModel.UserName = dv.Table.Rows[0]["UserName"].ToString();
                string UserName = dv.Table.Rows[0]["UserName"].ToString();

                //------------------------------------------------------------------------
                System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
                string smtpUserName = "donot-reply@maple-software.com";
                string smtpPassword = "maple@123";

                string Receiver1 = dv.Table.Rows[0]["EmailId"].ToString();     //mallesh.t@maple-software.com
                //string from = System.Configuration.ConfigurationManager.AppSettings["RegFromAddress"].ToString();
                string Sender1 = "donot-reply@maple-software.com";
                mail.Host = "secure214.sgcpanel.com";
                mail.Port = 2525;



                mail.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                MailAddress fromAddress = new MailAddress(Sender1, smtpUserName);
                MailMessage mailMessge = new MailMessage();
                mailMessge.From = fromAddress;
                mailMessge.To.Add(Receiver1);
                string EmailBody = "Your User Name :" + UserName + "  and  Your new Password :" + newPassword;
                mailMessge.Body = EmailBody;
                mailMessge.IsBodyHtml = true;
                mailMessge.Subject = "Test Mail";

                ObjRegistrationModel.imode = 102;
                ObjRegistrationModel.ID = ID;

                int iRet = ObjRegistrationBLL.RegistrationUID(ObjRegistrationModel);
                if (iRet > 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('New password sent Successfully!'); ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('Password sent failed'); ", true);
                }

                mail.Send(mailMessge);




                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
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
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }


        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            if (ddlDepart.SelectedValue == "-1")
            {
                Bind();
            }
            else
            {
                GridView1.DataSource = ObjRegistrationBLL.GridBindByDepartID(Convert.ToInt32(ddlDepart.SelectedValue));
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
        }
    }
}

