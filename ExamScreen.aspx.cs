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
using System.Drawing.Drawing2D;

namespace L_T_Defence
{
    public partial class ExamScreen : System.Web.UI.Page
    {
        QuestionBankBLL ObjQuestionBankBLL = new QuestionBankBLL();
        ExamBLL ObjExamBLL = new ExamBLL();
        ExamModel ObjExamModel = new ExamModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lbltime.Text = "120 sec";
                btnSubmit.Visible = false;
                btnNext.Visible = false;
                btnPrevious.Visible = false;

                if (Session["Name"] != null)
                {
                    lblName.Text = Session["Name"].ToString();
                    lblDepartment.Text = Session["Department"].ToString();
                }
                ddlcategory.DataSource = ObjQuestionBankBLL.Cascade(Convert.ToInt32(Session["DepartmentId"]));
                ddlcategory.DataTextField = "TypeName";
                ddlcategory.DataValueField = "CategoryId";
                ddlcategory.DataBind();
                ddlcategory.Items.Insert(0, new ListItem("Select Category", "-1"));





                Session["SNo"] = 0;
                Session["SNOIncomplete"] = 0;
                lblQuestionCount.Text = "1";
                panel.Visible = false;
                UpdatePanel1.Visible = false;

               

            }
        }

        private void Save()
        {

          


            int myvar = Convert.ToInt32(Session["CombinationId"]);

            ObjExamModel.imode = 101;
            ObjExamModel.EID = Convert.ToInt32(Session["EID"]);
            ObjExamModel.QuestionId = Convert.ToInt32(hdnQuestionID.Value);
            ObjExamModel.CombinationId = Convert.ToInt32(Session["CombinationId"]);
            if (rdBtn1.Checked)
            {
                ObjExamModel.Answer = "1";
            }
            else if (rdBtn2.Checked)
            {
                ObjExamModel.Answer = "2";
            }
            else if (rdBtn3.Checked)
            {
                ObjExamModel.Answer = "3";
            }
            else if (rdBtn4.Checked)
            {
                ObjExamModel.Answer = "4";
            }

            int iret = ObjExamBLL.ExamUID(ObjExamModel);
            if (iret > 0)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
            }
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (hdnType.Value == "All")
            {
                Session["SNo"] = Convert.ToInt32(Session["SNo"].ToString()) + 1;
                lblFirst.Text = Session["SNo"].ToString();
            }
            else
            {
                DataView dv1 = ObjExamBLL.ViewIncomplete(Convert.ToInt32(Session["CombinationId"]), Convert.ToInt32(Session["EID"]));

                lblLast.Text = dv1.Table.Rows.Count.ToString();
                Session["SNOIncomplete"] = Convert.ToInt32(Session["SNOIncomplete"].ToString()) + 1;
                lblFirst.Text = Session["SNOIncomplete"].ToString();
            }
            GetDetails();
        }
        private void GetDetails()
        {
            rdBtn1.Checked = false;
            rdBtn2.Checked = false;
            rdBtn3.Checked = false;
            rdBtn4.Checked = false;


            int SNo = (hdnType.Value == "All") ? Convert.ToInt32(Session["SNo"]) : Convert.ToInt32(Session["SNOIncomplete"]);
            int imode = (hdnType.Value == "All") ? 103 : 106;

            int X = Convert.ToInt32(Session["EID"]);
            DataView dv = ObjExamBLL.Next(imode, Convert.ToInt32(Session["CombinationId"]), SNo, X);
            if (dv.Table.Rows.Count > 0)
            {
                hdnQuestionID.Value = dv.Table.Rows[0]["ID"].ToString();
                lblQuestion.Text = dv.Table.Rows[0]["Question"].ToString();
                lblOption1.Text = dv.Table.Rows[0]["Option1"].ToString();
                lblOption2.Text = dv.Table.Rows[0]["Option2"].ToString();
                lblOption3.Text = dv.Table.Rows[0]["Option3"].ToString();
                lblOption4.Text = dv.Table.Rows[0]["Option4"].ToString();
                rdBtn1.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "1") ? true : false;
                rdBtn2.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "2") ? true : false;
                rdBtn3.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "3") ? true : false;
                rdBtn4.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "4") ? true : false;

            }
            else
            {
                hdnQuestionID.Value ="0";
                lblQuestion.Text = "";
                lblOption1.Text = "";
                lblOption2.Text = "";
                lblOption3.Text = "";
                lblOption4.Text = "";
                rdBtn1.Checked =  false;
                rdBtn2.Checked =  false;
                rdBtn3.Checked = false;
                rdBtn4.Checked =  false;
            }
            btnSubmit.Visible = false;
            btnNext.Visible = true;
            btnPrevious.Visible = true;

            if (Convert.ToInt32(lblFirst.Text) == 1)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = false;
                btnSubmit.Visible = false;
            }
            if (Convert.ToInt32(lblFirst.Text) == Convert.ToInt32(lblLast.Text))
            {
                btnNext.Visible = false;
                btnPrevious.Visible = true;
                btnSubmit.Visible = true;
            }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (hdnType.Value == "All")
            {
                Session["SNo"] = Convert.ToInt32(Session["SNo"].ToString()) - 1;
                lblFirst.Text = Session["SNo"].ToString();
            }
            else
            {
                DataView dv1 = ObjExamBLL.ViewIncomplete(Convert.ToInt32(Session["CombinationId"]), Convert.ToInt32(Session["EID"]));

                lblLast.Text = dv1.Table.Rows.Count.ToString();
                Session["SNOIncomplete"] = Convert.ToInt32(Session["SNOIncomplete"].ToString()) - 1;
                lblFirst.Text = Session["SNOIncomplete"].ToString();
            }
            GetDetails();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ObjExamModel.imode = 107;
            ObjExamModel.EID = Convert.ToInt32(Session["EID"]);

            int iret = ObjExamBLL.ExamUID(ObjExamModel);
            if (iret > 0)
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Submited Successfully!');window.open('UserDashBoard.aspx');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Submited Successfully!');", true);
                Response.Redirect("UserDashBoard.aspx");
            }
        }
        protected void rdBtn1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn1.Checked)
            {
                rdBtn2.Checked = false;
                rdBtn3.Checked = false;
                rdBtn4.Checked = false;
            }
            Save();
        }
        protected void rdBtn2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn2.Checked)
            {
                rdBtn1.Checked = false;
                rdBtn3.Checked = false;
                rdBtn4.Checked = false;
            }
            Save();
        }
        protected void rdBtn3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn3.Checked)
            {
                rdBtn1.Checked = false;
                rdBtn2.Checked = false;
                rdBtn4.Checked = false;
            }
            Save();
        }
        protected void rdBtn4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtn4.Checked)
            {
                rdBtn1.Checked = false;
                rdBtn2.Checked = false;
                rdBtn3.Checked = false;
            }
            Save();
        }
        protected void btnViewIncomplete_Click(object sender, EventArgs e)
        {
            rdBtn1.Checked = false;
            rdBtn2.Checked = false;
            rdBtn3.Checked = false;
            rdBtn4.Checked = false;
            hdnType.Value = "Incomplete";
            Session["SNOIncomplete"] = 1;

          

            DataView dv = ObjExamBLL.ViewIncomplete(Convert.ToInt32(Session["CombinationId"]), Convert.ToInt32(Session["EID"]));

            if (dv.Table.Rows.Count > 0)
            {
                dvQuest.Visible = true;
                hdnQuestionID.Value = dv.Table.Rows[0]["ID"].ToString();
                lblQuestion.Text = dv.Table.Rows[0]["Question"].ToString();
                lblOption1.Text = dv.Table.Rows[0]["Option1"].ToString();
                lblOption2.Text = dv.Table.Rows[0]["Option2"].ToString();
                lblOption3.Text = dv.Table.Rows[0]["Option3"].ToString();
                lblOption4.Text = dv.Table.Rows[0]["Option4"].ToString();
                btnSubmit.Visible = false;
                btnNext.Visible = true;
                btnPrevious.Visible = true;

                lblLast.Text = dv.Table.Rows.Count.ToString();
                lblFirst.Text = Session["SNOIncomplete"].ToString();
                if (Convert.ToInt32(lblFirst.Text) == 1)
                {
                    btnNext.Visible = true;
                    btnPrevious.Visible = false;
                    btnSubmit.Visible = false;
                }


                if (Convert.ToInt32(lblFirst.Text) == Convert.ToInt32(lblLast.Text))
                {
                    if (Convert.ToInt32(lblFirst.Text) == 1)
                    {
                        btnNext.Visible = false;
                        btnPrevious.Visible = false;
                    }
                    else
                    {
                        btnNext.Visible = false;
                        btnPrevious.Visible = true;
                    }

                    btnSubmit.Visible = true;
                }
            }
            else
            {
                dvQuest.Visible = false;
                btnNext.Visible = false;
                btnPrevious.Visible = false;
                btnSubmit.Visible = false;
                hdnQuestionID.Value = lblQuestion.Text = lblOption1.Text = lblOption2.Text = lblOption3.Text = lblOption4.Text = "";
                lblLast.Text = lblFirst.Text = "0";
            }

        }

        protected void btnExamStart_Click1(object sender, EventArgs e)
        {
           

           

            Session["EID"] = ObjExamBLL.GetEID(Convert.ToInt32(Session["ID"]));


            DataView dv2 = ObjExamBLL.GetCombinationId(Convert.ToInt32(Session["DepartmentId"]), Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(ddlmodule.SelectedValue));
            int CombinationId = Convert.ToInt32(dv2.Table.Rows[0]["Id"].ToString());
            Session["CombinationId"] = CombinationId;
            DataView dv1 = ObjExamBLL.NoOfQuestions(CombinationId);   //questions Count
            lblQuestionCount.Text = lblLast.Text = dv1.Table.Rows[0]["NoOfQuestions"].ToString();
            int ttt = Convert.ToInt32(dv1.Table.Rows[0]["NoOfQuestions"].ToString());
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "startCountdown(" + 120 * ttt + "); ", true);     //total duration

            ViewAllMethod();
        }

        private void ViewAllMethod()
        {          


            DataView dv1 = ObjExamBLL.NoOfQuestions(Convert.ToInt32(Session["CombinationId"]));   //questions Count
            lblQuestionCount.Text = lblLast.Text = dv1.Table.Rows[0]["NoOfQuestions"].ToString();

            hdnType.Value = "All";


            Session["SNo"] = "1";

            ddlmodule.Visible = false;
            Module.Visible = false;
            ddlcategory.Visible = false;
            category.Visible = false;

            lblModule.Text = ddlmodule.SelectedItem.Text;
            lblCategory.Text = ddlcategory.SelectedItem.Text;
            int ttt = Convert.ToInt32(dv1.Table.Rows[0]["NoOfQuestions"].ToString());
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "startCountdown(" + 120 * ttt + "); ", true);     //total duration

            DataView dv = ObjExamBLL.Question(Convert.ToInt32(Session["CombinationId"]), Convert.ToInt32(Session["EID"]));  //first question Bind
            Session["Questions"] = dv;
            lblFirst.Text = Session["SNo"].ToString();
     

            if (dv.Table.Rows.Count > 0)
            {
                dvQuest.Visible = true;
                hdnQuestionID.Value = dv.Table.Rows[0]["ID"].ToString();
                lblQuestion.Text = dv.Table.Rows[0]["Question"].ToString();
                lblOption1.Text = dv.Table.Rows[0]["Option1"].ToString();
                lblOption2.Text = dv.Table.Rows[0]["Option2"].ToString();
                lblOption3.Text = dv.Table.Rows[0]["Option3"].ToString();
                lblOption4.Text = dv.Table.Rows[0]["Option4"].ToString();

                if (lblFirst.Text == "1" && lblLast.Text == "1")
                {
                    btnSubmit.Visible = true;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                }
                else
                {
                    btnNext.Visible = true;
                }

                btnPrevious.Visible = false;
                btnExamStart.Visible = false;
                //rdBtn1.Checked = (lblOption1.Text == dv.Table.Rows[0]["Answers"].ToString()) ? true : false;
                //rdBtn2.Checked = (lblOption2.Text == dv.Table.Rows[0]["Answers"].ToString()) ? true : false;
                //rdBtn3.Checked = (lblOption3.Text == dv.Table.Rows[0]["Answers"].ToString()) ? true : false;
                //rdBtn4.Checked = (lblOption4.Text == dv.Table.Rows[0]["Answers"].ToString()) ? true : false;
                rdBtn1.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "1") ? true : false;
                rdBtn2.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "2") ? true : false;
                rdBtn3.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "3") ? true : false;
                rdBtn4.Checked = (dv.Table.Rows[0]["Answers"].ToString() == "4") ? true : false;
            }
            else
            {
                dvQuest.Visible = false;
                btnNext.Visible = false;
                btnPrevious.Visible = false;
                btnSubmit.Visible = false;
                hdnQuestionID.Value = lblQuestion.Text = lblOption1.Text = lblOption2.Text = lblOption3.Text = lblOption4.Text = "";
                lblLast.Text = lblFirst.Text = "0";
            }
            panel.Visible = true;
            UpdatePanel1.Visible = true;
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlmodule.DataSource = ObjQuestionBankBLL.Cascade1(Convert.ToInt32(Session["DepartmentId"]), Convert.ToInt32(ddlcategory.SelectedValue));
            ddlmodule.DataTextField = "DepartmentModule";
            ddlmodule.DataValueField = "ModuleId";
            ddlmodule.DataBind();
            ddlmodule.Items.Insert(0, new ListItem("Select Module", "-1"));
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            ViewAllMethod();
        }


    }
}


