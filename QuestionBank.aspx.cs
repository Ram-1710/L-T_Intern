using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Model;
using BLL;
using System.Data;

namespace L_T_Defence
{
    public partial class QuestionBank : System.Web.UI.Page
    {
        QuestionBankBLL ObjQuestionBankBLL = new QuestionBankBLL();
        QuestionBankModel ObjQuestionBankModel = new QuestionBankModel();
        DepartmentModel DepartmentModelObj = new DepartmentModel();
        DepartmentBLL DepartmentBLLObj = new DepartmentBLL();
        DepartmentModuleModel DepartmentModuleModelObj = new DepartmentModuleModel();
        DepartmentModuleBLL DepartmentModuleBLLObj = new DepartmentModuleBLL();
        TypeModel TypeModelObj = new TypeModel();
        TypeBLL TypeBLLObj = new TypeBLL();
        MaterialBLL MaterialBLLObj = new MaterialBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = btnSubmit.UniqueID;
            btnSubmit.Focus();
            if (!IsPostBack)
            {
                //Bind();
                btnSubmit.Focus();
                ddlDepart.DataSource = ObjQuestionBankBLL.DeptDropDownBind();
                ddlDepart.DataTextField = "DepartmentName";
                ddlDepart.DataValueField = "Dept_Id";
                ddlDepart.DataBind();
                ddlDepart.Items.Insert(0, new ListItem("select Department", "-1"));

                //ddlDepart.DataSource = ObjQuestionBankBLL.DeptDropDownBind();
                //ddlDepart.DataTextField = "DepartmentName";
                //ddlDepart.DataValueField = "Dept_Id";
                //ddlDepart.DataBind();
                //ddlDepart.Items.Insert(0, new ListItem("select Department", "-1"));
               
            }
        }

        private void Bind()
        {
            try
            {
                DataView dv = ObjQuestionBankBLL.Grid();
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

        public void Clear()
        {
            btnSubmit.Text = "Save";
         
            //ddlModuleee.SelectedValue = "-1";
            //ddlCategory.SelectedValue = "-1";
            //ddlDepart.SelectedValue = "-1";
            
            
            txtQuestion.Text = "";
            txtOption1.Text = "";
            txtOption2.Text = "";
            txtOption3.Text = "";
            txtOption4.Text = "";
            rdBtn1.Checked = false;
            rdBtn2.Checked = false;
            rdBtn3.Checked = false;
            rdBtn4.Checked = false;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Upd")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                hdn.Value = ID.ToString();
                btnSubmit.Text = "Update";
                DataView dv = ObjQuestionBankBLL.GridBindById(ID);
                if (dv.Table.Rows.Count > 0)
                {
                    int x = Convert.ToInt32(dv.Table.Rows[0]["CombinationId"].ToString());
                    DataView dv1 = ObjQuestionBankBLL.GetDeptIdCatIdModId(x);
                    ddlDepart.SelectedValue = dv1.Table.Rows[0]["Dept_Id"].ToString();


                    //ddlCategory2_SelectedIndexChanged(null, null);
                    ddlCategory.DataSource = ObjQuestionBankBLL.GetCategoryByDeptId2(Convert.ToInt32(ddlDepart.SelectedValue));
                    ddlCategory.DataTextField = "TypeName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
                    ddlCategory.SelectedValue = dv1.Table.Rows[0]["TypeId"].ToString();


                    //ddlModule_SelectedIndexChanged(null, null);
                    int z = Convert.ToInt32(ddlDepart.SelectedValue);
                    int y = Convert.ToInt32(ddlCategory.SelectedValue);
                    ddlModuleee.DataSource = ObjQuestionBankBLL.GetModulebyDeptandCat2(z, y);
                    ddlModuleee.DataTextField = "DepartmentModule";
                    ddlModuleee.DataValueField = "ModuleId";
                    ddlModuleee.DataBind();
                    ddlModuleee.Items.Insert(0, new ListItem("select Module", "-1"));
                    ddlModuleee.SelectedValue = dv1.Table.Rows[0]["ModuleId"].ToString();

                    txtQuestion.Text = dv.Table.Rows[0]["Question"].ToString();
                    txtOption1.Text = dv.Table.Rows[0]["Option1"].ToString();
                    txtOption2.Text = dv.Table.Rows[0]["Option2"].ToString();
                    txtOption3.Text = dv.Table.Rows[0]["Option3"].ToString();
                    txtOption4.Text = dv.Table.Rows[0]["Option4"].ToString();

                    if (Convert.ToString(dv.Table.Rows[0]["Answer"]) == "1")
                        rdBtn1.Checked = true;
                    else if (Convert.ToString(dv.Table.Rows[0]["Answer"]) == "2")
                        rdBtn2.Checked = true;
                    else if (Convert.ToString(dv.Table.Rows[0]["Answer"]) == "3")
                        rdBtn3.Checked = true;
                    else if (Convert.ToString(dv.Table.Rows[0]["Answer"]) == "4")
                        rdBtn4.Checked = true;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
                }
            }
            else if (e.CommandName == "Del")
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                int ires = -1;
                ObjQuestionBankModel.imode = 103;
                ObjQuestionBankModel.ID = ID;
                ires = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                if (ires > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deleted Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deletion Failed!');", true);
                }
                int x = Convert.ToInt32(ddlDepart.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModuleee.SelectedValue);
                DataView dv = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatIdModId(x, y, z);
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
                Clear();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {
                ObjQuestionBankModel.imode = 101;
               
                int x = Convert.ToInt32(ddlDepart.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModuleee.SelectedValue);
                DataView dv = ObjQuestionBankBLL.GetCombinationId(x, y, z);

                ObjQuestionBankModel.CombinationId = Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString());

                ObjQuestionBankModel.Question = txtQuestion.Text;
                ObjQuestionBankModel.Options1 = txtOption1.Text;
                ObjQuestionBankModel.Options2 = txtOption2.Text;
                ObjQuestionBankModel.Options3 = txtOption3.Text;
                ObjQuestionBankModel.Options4 = txtOption4.Text;
                if (rdBtn1.Checked)
                {
                    ObjQuestionBankModel.Answers = "1";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }
                    
                else if (rdBtn2.Checked)
                {
                    ObjQuestionBankModel.Answers = "2";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }
                    
                else if (rdBtn3.Checked)
                {
                    ObjQuestionBankModel.Answers = "3";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }
                    
                else if (rdBtn4.Checked)
                {
                    ObjQuestionBankModel.Answers = "4";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }
                   
                else if(!rdBtn1.Checked && !rdBtn2.Checked && !rdBtn3.Checked && !rdBtn4.Checked) 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Please select the correct answer to save!');", true);                   
                }
                
                   
               
               
               
            }
            else
            {
                ObjQuestionBankModel.imode = 102;
                ObjQuestionBankModel.ID = Convert.ToInt32(hdn.Value);
                int x =Convert.ToInt32(ddlDepart.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModuleee.SelectedValue);
                DataView dv = ObjQuestionBankBLL.GetCombinationId(x, y, z);

                ObjQuestionBankModel.CombinationId = Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString());

                ObjQuestionBankModel.Question = txtQuestion.Text;
                ObjQuestionBankModel.Options1 = txtOption1.Text;
                ObjQuestionBankModel.Options2 = txtOption2.Text;
                ObjQuestionBankModel.Options3 = txtOption3.Text;
                ObjQuestionBankModel.Options4 = txtOption4.Text;
                if (rdBtn1.Checked)
                {
                    ObjQuestionBankModel.Answers = "1";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }

                else if (rdBtn2.Checked)
                {
                    ObjQuestionBankModel.Answers = "2";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }

                else if (rdBtn3.Checked)
                {
                    ObjQuestionBankModel.Answers = "3";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }

                else if (rdBtn4.Checked)
                {
                    ObjQuestionBankModel.Answers = "4";
                    int iret = ObjQuestionBankBLL.QuestionBankUID(ObjQuestionBankModel);
                    if (iret > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                    }
                    Clear();
                }

                else if (!rdBtn1.Checked && !rdBtn2.Checked && !rdBtn3.Checked && !rdBtn4.Checked)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Please select the correct answer to save!');", true);
                }

                Clear();
            }
            int x1 = Convert.ToInt32(ddlDepart.SelectedValue);
            int y1 = Convert.ToInt32(ddlCategory.SelectedValue);
            int z1 = Convert.ToInt32(ddlModuleee.SelectedValue);
            DataView dv1 = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatIdModId(x1, y1, z1);
            GridView1.DataSource = dv1;
            GridView1.DataBind();
            
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Clear();
        }

       

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
                  
            //ddlCategory2.DataSource = MaterialBLLObj.GridBindByDepartment(Convert.ToInt32(ddlDepartment.SelectedValue));
            //ddlCategory2.DataTextField = "TypeName";
            //ddlCategory2.DataValueField = "CategoryId";
            //ddlCategory2.DataBind();
            //ddlCategory2.Items.Insert(0, new ListItem("select Category", "-1"));
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = ObjQuestionBankBLL.GetCategorybyDeptId(Convert.ToInt32(ddlDepart.SelectedValue));               
            ddlCategory.DataTextField = "TypeName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("select Category", "-1")); 
            ddlCategory_SelectedIndexChanged(null, null);
           // ddlModuleee.Enabled = false;
        
            //ddlModule.DataSource = null;
            //ddlModule.DataBind();
            //GridView1.DataSource = ObjQuestionBankBLL.GetQuestionsbyDeptId(Convert.ToInt32(ddlDepart.SelectedValue));
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlModuleee_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlModuleee.DataSource = ObjQuestionBankBLL.GetModulebyDeptandCat(Convert.ToInt32(ddlDepart.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
            ddlModuleee.DataBind();
            //int x = Convert.ToInt32(ddlDepart.SelectedValue);
            //int y = Convert.ToInt32(ddlCategory.SelectedValue);
            //int z = Convert.ToInt32(ddlModuleee.SelectedValue);            
            //DataView dv = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatIdModId(x, y, z);
            //GridView1.DataSource = dv;
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {            
            //DataView dv = ObjQuestionBankBLL.GridBySearch(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlModule.SelectedValue));
            //GridView1.DataSource = dv;
            //GridView1.DataBind();
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  ddlModuleee.Enabled = true;
            int x = Convert.ToInt32(ddlDepart.SelectedValue);
            int y = Convert.ToInt32(ddlCategory.SelectedValue);
            ddlModuleee.DataSource = ObjQuestionBankBLL.GetModulebyDeptandCat(x,y);
            ddlModuleee.DataTextField = "DepartmentModule";
            ddlModuleee.DataValueField = "ModuleId";
            ddlModuleee.DataBind();
            ddlModuleee.Items.Insert(0, new ListItem("select Module", "-1"));
            

            //GridView1.DataSource = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatId(x, y);
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);

        }

        protected void ddlCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int x = Convert.ToInt32(ddlDepartment.SelectedValue);
            //int y = Convert.ToInt32(ddlCategory2.SelectedValue);
            //ddlModule.DataSource = MaterialBLLObj.GridBindByDepartmentCategory(x, y);
            //ddlModule.DataTextField = "DepartmentModule";
            //ddlModule.DataValueField = "ModuleId";
            //ddlModule.DataBind();
            //ddlModule.Items.Insert(0, new ListItem("select Module", "-1"));
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue == "-1")
            {
                ddlCategory.DataSource = MaterialBLLObj.Cascade(Convert.ToInt32(ddlDepart.SelectedValue));
                ddlCategory.DataTextField = "TypeName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));

                DataView dv = ObjQuestionBankBLL.GetQuestionsbyDeptId(Convert.ToInt32(ddlDepart.SelectedValue));
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModuleee.SelectedValue == "-1")
            {
                ddlModuleee.DataSource = MaterialBLLObj.Cascade1(Convert.ToInt32(ddlDepart.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
                ddlModuleee.DataTextField = "DepartmentModule";
                ddlModuleee.DataValueField = "ModuleId";
                ddlModuleee.DataBind();
                ddlModuleee.Items.Insert(0, new ListItem("select Module", "-1"));

                DataView dv = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatId(Convert.ToInt32(ddlDepart.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (ddlDepart.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModuleee.SelectedValue != "-1")
            {
                int x = Convert.ToInt32(ddlDepart.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModuleee.SelectedValue);
                DataView dv = ObjQuestionBankBLL.GetQuestionsbyDeptIdCatIdModId(x, y, z);
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }
    }
}