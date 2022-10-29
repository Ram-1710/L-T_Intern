using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Configuration;
using System.Net;
using System.IO;
namespace L_T_Defence
{
    public partial class StudyMaterialUpload : System.Web.UI.Page
    {
        MaterialBLL ObjMaterialBLL = new MaterialBLL();
        MaterialModel ObjMaterialModel = new MaterialModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.DefaultButton = btnSubmit.UniqueID;
            btnSubmit.Focus();
            if (!IsPostBack)
            {
                btnSubmit.Focus();
                Bind();
                ddlDepartment.DataSource = ObjMaterialBLL.DropDownBind(2);
                ddlDepartment.DataTextField = "DepartmentName";
                ddlDepartment.DataValueField = "Dept_Id";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", "-1"));
            }
        }
        private void Bind()
        {
            
                DataView dv = ObjMaterialBLL.Grid();
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

        public void Clear()
        {
            btnSubmit.Text = "Save";
            ddlModule.SelectedValue = "-1";
            ddlCategory.SelectedValue = "-1";
            ddlDepartment.SelectedValue = "-1";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }
     
       
            

        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {
                ObjMaterialModel.imode = 101;
                int X = Convert.ToInt32(ddlDepartment.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModule.SelectedValue);

                DataView dv =  ObjMaterialBLL.GetCombinationId(X, y, z);
                ObjMaterialModel.CombinationId = Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString());
                              
                if (fupDocument.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(fupDocument.FileName);
                    if (fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".mp4" || fileExtension.ToLower() == ".docx"
                       || fileExtension.ToLower() == ".doc" || fileExtension.ToLower() == ".zip" || fileExtension.ToLower() == ".rar")
                    {
                        fupDocument.SaveAs(Server.MapPath("~/Uploads/" + fupDocument.FileName));
                        ObjMaterialModel.MaterialPDF = fupDocument.FileName.ToString();

                        int iret = ObjMaterialBLL.MaterialUID(ObjMaterialModel);
                        if (iret > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                            Clear();
                            Bind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Submission Failed!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Upload Only PDF Files!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Please Upload The Pdf File!');", true);
                }

               
            }
            else
            {
                ObjMaterialModel.imode = 102;
                ObjMaterialModel.ID = Convert.ToInt32(hdn.Value);
                int X = Convert.ToInt32(ddlDepartment.SelectedValue);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModule.SelectedValue);

                DataView dv = ObjMaterialBLL.GetCombinationId(X, y, z);
                ObjMaterialModel.CombinationId = Convert.ToInt32(dv.Table.Rows[0]["Id"].ToString());

                if (fupDocument.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(fupDocument.FileName);
                    if (fileExtension.ToLower() == ".pdf" || fileExtension.ToLower() == ".mp4" || fileExtension.ToLower() == ".docx"
                       || fileExtension.ToLower() == ".doc" || fileExtension.ToLower() == ".zip" || fileExtension.ToLower() == ".rar")
                    {
                        fupDocument.SaveAs(Server.MapPath("~/Uploads/" + fupDocument.FileName));
                        ObjMaterialModel.MaterialPDF = fupDocument.FileName.ToString();
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Only PDF or mp4 or rar or doc Files can be uploaded!');", true);
                    }
                }
                
                int iret = ObjMaterialBLL.MaterialUID(ObjMaterialModel);
                if (iret > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Updated Successfully!');", true);
                    Clear();
                    Bind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Update Failed!');", true);
                }
            }
            Bind();            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
         {
            if (e.CommandName == "Upd")
            {
                rffupDocument.Enabled = false;
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                hdn.Value = ID.ToString();
                btnSubmit.Text = "Update";
                DataView dv = ObjMaterialBLL.GridBindById(ID);
                if(dv.Count > 0)
                {
                    int x = Convert.ToInt32(dv.Table.Rows[0]["CombinationId"].ToString());
                    DataView dv1 = ObjMaterialBLL.GetDeptIdCatIdModId(x);
                    ddlDepartment.SelectedValue = dv1.Table.Rows[0]["Dept_Id"].ToString();


                    
                    ddlCategory.DataSource = ObjMaterialBLL.GetCategorybyDeptId(Convert.ToInt32(ddlDepartment.SelectedValue));                        
                    ddlCategory.DataTextField = "TypeName";
                    ddlCategory.DataValueField = "CategoryId";
                    ddlCategory.DataBind();
                    ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
                    ddlCategory.SelectedValue = dv1.Table.Rows[0]["TypeId"].ToString();


                    //ddlModule_SelectedIndexChanged(null, null);
                    int z = Convert.ToInt32(ddlDepartment.SelectedValue);
                    int y = Convert.ToInt32(ddlCategory.SelectedValue);
                    ddlModule.DataSource = ObjMaterialBLL.GetModbyDeptIdCatId(z, y);
                    ddlModule.DataTextField = "DepartmentModule";
                    ddlModule.DataValueField = "ModuleId";
                    ddlModule.DataBind();
                    ddlModule.Items.Insert(0, new ListItem("select Module", "-1"));
                    ddlModule.SelectedValue = dv1.Table.Rows[0]["ModuleId"].ToString();

                   
                }
                //ddlDepartment.SelectedValue = dv.Table.Rows[0]["DepartmentId"].ToString();
                //ddlDepartment_SelectedIndexChanged(null, null);
                //ddlCategory.SelectedValue = dv.Table.Rows[0]["CategoryId"].ToString();
                //ddlCategory_SelectedIndexChanged(null, null);
                //ddlModule.SelectedValue = dv.Table.Rows[0]["ModuleId"].ToString(); 
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (e.CommandName == "Del")
            {
                int ires = -1;
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int index = row.RowIndex;
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]);
                ObjMaterialModel.imode = 103;
                ObjMaterialModel.ID = ID;
                ires = ObjMaterialBLL.MaterialUID(ObjMaterialModel);
                if (ires > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deleted Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Deletion Failed!');", true);
                }
                Bind();
            }
            else if (e.CommandName == "ViewAttach")
            {
                string filename = e.CommandArgument.ToString();
                if (filename == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('File Not Found!'); ", true);
                }
                else
                {
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    byte[] data = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                    response.BinaryWrite(data);
                    response.End();
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = ObjMaterialBLL.Cascade(Convert.ToInt32(ddlDepartment.SelectedValue));
            ddlCategory.DataTextField = "TypeName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

     
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModule.DataSource = ObjMaterialBLL.Cascade1(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
            ddlModule.DataTextField = "DepartmentModule";
            ddlModule.DataValueField = "ModuleId";
            ddlModule.DataBind();
            ddlModule.Items.Insert(0, new ListItem("select Module", "-1"));
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }
    }
}