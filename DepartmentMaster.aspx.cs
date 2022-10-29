using BLL; //including the namespace BLL
using Model;//including the namespace Model
using System;//including the namespace System
using System.Collections.Generic;
using System.Data;//including the namespace System.Data
using System.IO;//including the namespace System.IO
using System.Linq;
using System.Web;//including the namespace System.Web
using System.Web.UI;//including the namespace System.Web.UI
using System.Web.UI.WebControls;//including the namespace System.Web.UI.WebControls

namespace L_T_Defence
{
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        DepartmentModel ObjDepartmentModel = new DepartmentModel(); //creating the departmentModel object
        DepartmentBLL ObjDepartmentBLL = new DepartmentBLL(); //creating the DepartmentBLL object
        protected void Page_Load(object sender, EventArgs e) 
        {
            //declaring the submit button is the default option when pressing the enter key
            Page.Form.DefaultButton = btnSubmit.UniqueID; 
            btnSubmit.Focus();
            if (!IsPostBack) //condition check whether http request is post back or not
            {
                Bind(); //calling the Bind() method to bind the data with the grid
                Clear();//calling the clear method to clear all controls 
                btnSubmit.Focus();
            }
        }
        private void Bind() //creating the Bind() method 
        {       
                // method definition  
                DataView dv = ObjDepartmentBLL.GRIDBIND(); //creating and filling the DataView with data
                if (dv.Count > 0) //checking whether the DataView has data or not 
                {
                    GridView1.DataSource = dv; //giving the DataView as datasource to the GridView
                }
                else
                {
                    GridView1.DataSource = null;
                }
                GridView1.DataBind(); //binding the data with gridview
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);            
        }

        private void ExportGridToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = ObjDepartmentBLL.Grid();
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Nagasai" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gv.GridLines = GridLines.Both;
            gv.HeaderStyle.Font.Bold = true;

            gv.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();

        }
        public void Clear() //creating method Clear()
        {
            btnSubmit.Text = "Save";
            txtDepartment.Text = " ";
           
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            /*The RowDataBound event is raised when a data row (represented by a GridViewRow object) is
            bound to data in the GridView control. This enables you to provide an event-handling method
            that performs a custom routine, such as modifying the values of the data bound to the row,
            whenever this event occurs.*/
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
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            /*The RowCommand event is raised when a button is clicked in the GridView control.
              This enables you to provide an event-handling method that performs a custom routine
              whenever this event occurs.*/

            if (e.CommandName == "Upd") //checking the command name.
            {
                GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer; //rendering the GridViewRow 
                int index = row.RowIndex; //obtaining the index of the row
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]); //obtaining the respective ID value of that row from the database.
                hdn.Value = ID.ToString(); //setting the id value to the hidden value
                Button status = (Button)GridView1.Rows[index].FindControl("btnActivate"); //finding the button control on the grid view
                String state = status.CommandArgument.ToString(); //getting the status of the button

                btnSubmit.Text = "Update"; //changing the submit button text to "update"
                DataView dv = ObjDepartmentBLL.GetDataById(ID); //creating dataview to get the respective ro details
                if (dv.Table.Rows.Count > 0) //checkinng whether dataview has a data or not
                {
                    //Assigning the value to the text box
                    txtDepartment.Text = dv.Table.Rows[0]["DepartmentName"].ToString();
                    
                }
                btnSubmit.Focus();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "gvStyles(); ", true);
            }
            else if (e.CommandName == "Activate")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer; //rendering the GridViewRow
                int index = row.RowIndex;//obtaining the index of the row
                int ID = Convert.ToInt32(GridView1.DataKeys[index].Values[0]); //obtaining the respective ID value of that row from the database.              
                Button status = (Button)GridView1.Rows[index].FindControl("btnActivate"); //finding the button control on the grid view
                String state = status.CommandArgument.ToString(); //getting the status of the button
                if (status.Text == "Activate") //checking the status of the button
                {                    
                    ObjDepartmentModel.imode = 106; //setting the imode
                    ObjDepartmentModel.ID = ID; // setting the ID
                    ObjDepartmentModel.ActiveFlag = 1; //setting the ActiveFlag
                    int ires = ObjDepartmentBLL.DepartmentUID(ObjDepartmentModel);
                    if (ires > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activated Successfully!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Activation Failed!');", true);
                    }
                    Bind();
                    Clear();
                }
                else
                {
                    ObjDepartmentModel.imode = 103;// setting the imode
                    ObjDepartmentModel.ID = ID;//setting the ID
                    ObjDepartmentModel.ActiveFlag = 0;//setting the ActiveFlag
                    int ires = ObjDepartmentBLL.DepartmentUID(ObjDepartmentModel); 
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
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Save")
            {
                ObjDepartmentModel.imode = 101; //insert 
                ObjDepartmentModel.DepartmentName = txtDepartment.Text.Trim(); //to remove empty spaces before and after the name we use Trim()
                int iret = ObjDepartmentBLL.DepartmentUID(ObjDepartmentModel);//obtaining the output value from the database and assigning to variable
                if (iret > 0) //checking the iret value to show whether insert is successfull or not
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Saved Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Department Already Exists!');", true);
                }
                btnSubmit.Focus();
            }
            else
            {
                ObjDepartmentModel.imode = 102; //insert 
                ObjDepartmentModel.ID = Convert.ToInt32(hdn.Value); //assigning the ID value obtained from the edit functionality 
               
                ObjDepartmentModel.DepartmentName = txtDepartment.Text.Trim(); //to remove empty spaces before and after the name we use Trim()
                int iret = ObjDepartmentBLL.DepartmentUID(ObjDepartmentModel);//obtaining the output value from the database and assigning to variable
                if (iret > 0)//checking the iret value to show whether insert is successfull or not
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Updated Successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert(5)", "alert('Department Already Exists!');", true);
                }
                btnSubmit.Focus();
            }
         
            Bind(); //calling the Bind() method to freshly load data to GridView. 
            Clear(); //calling the Clear() method to clear all the controls
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear(); //calling the Clear() method to clear all the controls
        }        
    }
}