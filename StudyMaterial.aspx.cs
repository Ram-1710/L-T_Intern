using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;


namespace L_T_Defence
{
    public partial class StudyMaterial : System.Web.UI.Page
    {
        QuestionBankBLL ObjQuestionBankBLL = new QuestionBankBLL();
        QuestionBankModel ObjQuestionBankModel = new QuestionBankModel();
        TypeBLL ObjTypeBLL = new TypeBLL();
        DepartmentModuleBLL ObjDepartmentModuleBLL = new DepartmentModuleBLL();
        DepartmentBLL ObjDepartmentBLL = new DepartmentBLL();
        MaterialBLL ObjMaterialBLL = new MaterialBLL();
        MaterialModel ObjMaterialModel = new MaterialModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //ddlDepartment.DataSource = ObjQuestionBankBLL.DeptDropDownBind();
                //ddlDepartment.DataTextField = "DepartmentName";
                //ddlDepartment.DataValueField = "Dept_Id";
                //ddlDepartment.DataBind();
                //ddlDepartment.Items.Insert(0, new ListItem("select Department", "-1"));
                ddlDepartment.Visible = false;
                lbldept.Visible = false;
                ddlCategory.DataSource = ObjMaterialBLL.Cascade(Convert.ToInt32(Session["Department"]));
                ddlCategory.DataTextField = "TypeName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
                ddlModule.Enabled = false;

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

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = ObjMaterialBLL.GridBindByDepartment(Convert.ToInt32(ddlDepartment.SelectedValue));
            GridView1.DataSource = dv;
            GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewAttach")
            {
                string filename = e.CommandArgument.ToString();
               

                if (filename == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert5", "alert('File Not Found!'); ", true);
                }
                else
                {
                    ObjMaterialModel.imode = 102;
                    ObjMaterialModel.UserId = Session["LoginUserName"].ToString();
                    ObjMaterialModel.DepartmentId = Convert.ToInt32(Session["Department"]);
                    ObjMaterialModel.INorOUT = 1;
                    if (ddlCategory.SelectedValue == "-1" || ddlCategory.SelectedValue == "" || ddlCategory.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", " alert('Please Select the Category');", true);
                        int x = Convert.ToInt32(ddlCategory.SelectedValue);
                    }
                    else
                    {
                        ObjMaterialModel.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                    }


                    if (ddlModule.SelectedValue == "" || ddlModule.SelectedValue == "-1" || ddlModule.SelectedValue == "0")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", " alert('Please Select the Module');", true);
                        int y = Convert.ToInt32(ddlModule.SelectedValue);
                    }
                    else
                    {
                        ObjMaterialModel.ModuleId = Convert.ToInt32(ddlModule.SelectedValue);
                    }


                    ObjMaterialBLL.GetMaterialDownloadDetails(ObjMaterialModel);
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;

                    if (filename.EndsWith("pdf"))
                    {
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        byte[] data = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                        response.BinaryWrite(data);
                        //response.ContentType = "application/pdf";
                        //byte[] data1 = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                        //response.AddHeader("content-length", data1.Length.ToString());
                        //response.BinaryWrite(data1);
                        //response.AddHeader("content-disposition", "inline;filename=" + filename + ".pdf");


                        //response.Write(string.Format("<script>window.open('{0}','_blank');</script>", filename + "StudyMaterial.aspx"));

                    }
                    else if (filename.EndsWith("mp4"))
                    {
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        byte[] data = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                        response.BinaryWrite(data);
                        //response.ContentType = "video/mp4";
                        //byte[] data1 = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                        //response.AddHeader("content-length", data1.Length.ToString());
                        //response.BinaryWrite(data1);
                        //response.AddHeader("content-disposition", "inline;filename=" + filename + ".mp4");

                        //--------------------------------------------------------------------------

                        //StringBuilder sb = new StringBuilder();
                        //sb.Append("<object classid=clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95 ");  

                        //sb.Append("codebase=http://activex.microsoft.com/activex/controls / mplayer / en / nsmp2inf.cab#Version=  5, 1, 52, 701 Width = 200px  Height = 200px type=application/x-oleobject align = absmiddle");  

                        //sb.Append("standby='Loading Microsoft+reg; Windows+reg; MediaPlayer components...' id=mp1 /> ");  

                        //sb.Append("<param name=FileName value=" + filename.ToString()+ "> ");
                        //sb.Append("<param name=ShowStatusBar value=" + ShowStatusBar.ToString() + "> ");
                        //sb.Append("<param name=ShowPositionControls value=" +ShowPositionControls.ToString() + "> ");
                        //sb.Append("<param name=ShowTracker value=" + ShowTracker.ToString() + "> ");
                        //sb.Append("<param name=ShowControls value=" +ShowControls.ToString() + "> ");
                        //sb.Append("<embed src=" + filename.ToString() + " ");
                        //sb.Append("pluginspage=http://www.microsoft.com/Windows / MediaPlayer type = application / x - mplayer2 ");  

                        //sb.Append("Width = " + Width.Value.ToString() + " ");
                        //sb.Append("Height = " + Height.Value.ToString());
                        //sb.Append(" /></embed></object>");
                        //Response.RenderBeginTag(HtmlTextWriterTag.Div);
                        //Response.Write(sb.ToString());
                        //Response.RenderEndTag();



                        //----------------------------------------------------------------------------
                    }
                    else
                    {
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        byte[] data = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                        response.BinaryWrite(data);
                    }

                    //byte[] data = req.DownloadData(Server.MapPath("~/Uploads/") + filename);
                    //response.BinaryWrite(data);
                    response.End();
                }
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("LoginPage.aspx");
        }

        protected void ddlDepartment_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlCategory.DataSource = ObjMaterialBLL.Cascade(Convert.ToInt32(ddlDepartment.SelectedValue));
            ddlCategory.DataTextField = "TypeName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));
            ddlModule.Enabled = false;
            //DataView dv = ObjMaterialBLL.GridBindByDepartment(Convert.ToInt32(ddlDepartment.SelectedValue));
            //GridView1.DataSource = dv;
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModule.Enabled = true;
            ddlModule.DataSource = ObjMaterialBLL.Cascade1(Convert.ToInt32(Session["Department"]), Convert.ToInt32(ddlCategory.SelectedValue));
            ddlModule.DataTextField = "DepartmentModule";
            ddlModule.DataValueField = "ModuleId";
            ddlModule.DataBind();
            ddlModule.Items.Insert(0, new ListItem("select Module", "-1"));

            //DataView dv = ObjMaterialBLL.GridBindByDepartmentCategory(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
            //GridView1.DataSource = dv;
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataView dv = ObjMaterialBLL.GridBindByDepartmentCategoryModule(Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlModule.SelectedValue));
            //GridView1.DataSource = dv;
            //GridView1.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue == "-1")
            {
                ddlCategory.DataSource = ObjMaterialBLL.Cascade(Convert.ToInt32(Session["Department"]));
                ddlCategory.DataTextField = "TypeName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("select Category", "-1"));

                DataView dv = ObjMaterialBLL.GridBindByDepartment(Convert.ToInt32(Session["Department"]));
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (ddlDepartment.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModule.SelectedValue == "-1")
            {
                ddlModule.DataSource = ObjMaterialBLL.Cascade1(Convert.ToInt32(Session["Department"]), Convert.ToInt32(ddlCategory.SelectedValue));
                ddlModule.DataTextField = "DepartmentModule";
                ddlModule.DataValueField = "ModuleId";
                ddlModule.DataBind();
                ddlModule.Items.Insert(0, new ListItem("select Module", "-1"));

                DataView dv = ObjMaterialBLL.GridBindByDepartmentCategory(Convert.ToInt32(Session["Department"]), Convert.ToInt32(ddlCategory.SelectedValue));
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (ddlDepartment.SelectedValue != "-1" && ddlCategory.SelectedValue != "-1" && ddlModule.SelectedValue != "-1")
            {
                int x = Convert.ToInt32(Session["Department"]);
                int y = Convert.ToInt32(ddlCategory.SelectedValue);
                int z = Convert.ToInt32(ddlModule.SelectedValue);
                DataView dv = ObjMaterialBLL.GridBindByDepartmentCategoryModule(x, y, z);
                GridView1.DataSource = dv;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
           

        }
    }
}