using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class MstCompany : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            InitLanguage();
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                try
                {                   
                    Initialize();
                    Search();
                }
                catch (Exception ex)
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                    logger.Error("Error in Page load:", ex);
                }
            }
        }
         private void Search()
        {
            try
            {
                lblNoRecord.Visible = false;
                List<Company> listCompany = new List<Company>();
                listCompany = Company.GetCompanySearch(txtCompanyName.Text);
                if (!cbShowDeleted.Checked)
                {
                    listCompany = listCompany.Where(l => l.isDeleted == false).ToList();
                }

                if (listCompany.Count == 0)
                {
                    Company objCompany = new Company();
                    objCompany.CompanyId = -1;
                    objCompany.CompanyName = "";
                    objCompany.isDeleted = true;
                    listCompany.Add(objCompany);
                    grdCompany.DataSource = listCompany;
                    grdCompany.DataBind();
                    if (listCompany.Count == 1 && listCompany[0].CompanyId == -1)
                        grdCompany.Rows[0].Visible = false;
                    lblNoRecord.Visible = true;
                    return;
                }
                else
                {
                   
                    grdCompany.DataSource = listCompany;
                    grdCompany.DataBind();
                    foreach (GridViewRow r in grdCompany.Rows)
                    {
                        r.Cells[7].Text = GetResource(string.Format("isDeleted_{0}", r.Cells[7].Text));
                    }                   
                }
               
            }
            catch (Exception e)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), e.Message) + "\");");
                logger.Error("Error in Search", e);
            }
        }

        private void Initialize()
        {
            btnDelete.Attributes["onclick"] = "javascript:return confirm(' " + GetResource("msDelete.Text") + " ');";
            lblCompanyName.Text = GetResource("lblCompanyName");
            cbShowDeleted.Text = GetResource("cbShowDeleted");
            lblNoRecord.Text = GetResource("lblNoRecord");
            SetButtonText(btnAddNew, btnEdit, btnClear, btnSearch,btnSave,btnCancel,btnDelete);
            lblCompanyID.Text = GetResource("header_Company_ID");
            lblName.Text = GetResource("header_Company_Name");
            requiredCompanyID.ErrorMessage = GetResource("requiredCompanyID");
            requiredCompanyName.ErrorMessage = GetResource("requiredCompanyName");
            compareValidatorCompanyID.ErrorMessage = GetResource("onlyInterger");
            lblAddress1.Text = GetResource("header_Address_1");
            lblAddress2.Text = GetResource("header_Address_2");
            lblTEL.Text = GetResource("header_TEL");
            lblFAX.Text = GetResource("header_FAX");
        }

        protected void grdCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCompany.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void grdCompany_RowCreated(object sender, GridViewRowEventArgs e)
        {
            InitLanguage();
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Build custom header.
                GridView oGridView = (GridView)sender;
                GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell oTableCell = new TableHeaderCell();

                //Add CheckBoxSelected
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(20);
                oGridViewRow.Cells.Add(oTableCell);

                //Add No
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Company_ID");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oGridViewRow.Cells.Add(oTableCell);


                //Add Name
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Company_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(180);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Address1
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Address_1");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(190);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Address2
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Address_2");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(190);
                oGridViewRow.Cells.Add(oTableCell);

                //Add TEL
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_TEL");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(100);
                oGridViewRow.Cells.Add(oTableCell);

                //Add TEL
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_FAX");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(100);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Status
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Company_Status");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(100);
                oGridViewRow.Cells.Add(oTableCell);

                //Add custom header            
                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtCompanyName.Text = string.Empty;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelEdit.Visible = true;
            btnDelete.Enabled = false;
            txtCompanyID.Enabled = true;
            txtCompanyID.Text = Company.GetNextCompanyId().ToString();
            txtCompanyName.Text = txtAddress1.Text = txtAddress2.Text = txtTEL.Text = txtFAX.Text = string.Empty;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                
                Company fac = new Company();
                btnDelete.Enabled = true;
                
                int countChecked = 0;
                CheckBox cb = new CheckBox();
                string ID = "";
                foreach (GridViewRow r in grdCompany.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("chk");
                    if (cb.Checked)
                    {
                        countChecked++;
                        if (countChecked == 2) break;
                        else
                        {                            
                            ID = Common.GetRowString(r.Cells[1].Text);       
                        }
                    }
                }
                if (countChecked == 0)
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_NONE_SELECTED_ITEM")) + "\");");
                    return;
                }
                else if (countChecked == 2)
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_MORE_ONE_SELECTED")) + "\");");
                    return;
                }
                else
                {
                    panelEdit.Visible = true;
                    panelMain.Visible = false;
                    txtCompanyID.Text = ID;
                    txtCompanyID.Enabled = false;

                    //show detail in here
                    Company comp = new Company();
                    comp.CompanyId = int.Parse(txtCompanyID.Text);
                    comp = comp.GetByPrimaryKey();

                    txtName.Text =  comp.CompanyName;
                    txtAddress1.Text = comp.Address1;
                    txtAddress2.Text = comp.Address2;
                    txtTEL.Text = comp.TEL;
                    txtFAX.Text = comp.FAX;

                }                

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Erorr btnEdit_click",ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelMain.Visible = true;
            panelEdit.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (btnDelete.Enabled == false)
                {
                    List<Company> listComp = new List<Company>();
                    listComp = Company.GetAll().Where(l => l.CompanyName.Trim().ToLower() == txtName.Text.Trim().ToLower()).ToList();
                    if (listComp.Count > 0)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblCompanyName.Text)) + "\");");
                        return;
                    }
                    Company comp = new Company();
                    comp.CompanyId = Convert.ToInt32(txtCompanyID.Text);
                    comp.CompanyName = txtName.Text;
                    if(txtAddress1.Text != "")
                       comp.Address1 = txtAddress1.Text;
                    if (txtAddress2.Text != "")
                      comp.Address2 = txtAddress2.Text;
                    if (txtTEL.Text != "")
                       comp.TEL = txtTEL.Text;
                    if (txtFAX.Text != "")
                       comp.FAX = txtFAX.Text;
                    comp.isDeleted = false;
                    comp.CreateAccount = comp.ModifiedAccount = this.User.Identity.Name;
                    comp.Insert();                    
                }
                else
                {

                    Company comp = new Company();
                    comp.CompanyId = int.Parse(txtCompanyID.Text);
                    comp = comp.GetByPrimaryKey();
                    if (comp.CompanyName.Trim() != txtName.Text.Trim())
                    {
                        List<Company> listComp = new List<Company>();
                        listComp = Company.GetAll().Where(l => l.CompanyName.Trim().ToLower() == txtName.Text.Trim().ToLower()).ToList();
                        if (listComp.Count > 0)
                        {
                            RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblCompanyName.Text)) + "\");");
                            return;
                        }
                    }
                    comp.ModifiedAccount = this.User.Identity.Name;
                    comp.CompanyId = int.Parse(txtCompanyID.Text);
                    comp.CompanyName = txtName.Text;
                    if (txtAddress1.Text != "")
                        comp.Address1 = txtAddress1.Text;
                    if (txtAddress2.Text != "")
                        comp.Address2 = txtAddress2.Text;
                    if (txtTEL.Text != "")
                        comp.TEL = txtTEL.Text;
                    if (txtFAX.Text != "")
                        comp.FAX = txtFAX.Text;
                    comp.Update();                     
                }                
                panelEdit.Visible = false;
                panelMain.Visible = true;
                Search();
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Erorr btnSave_click", ex);
            }
        }

        protected void cbShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Company comp = new Company();
                comp.CompanyId = int.Parse(txtCompanyID.Text);
                comp.isDeleted = true;
                comp.CompanyName = txtName.Text;                
                comp.ModifiedAccount = this.User.Identity.Name;
                comp.Update();
                Search();
                panelEdit.Visible = false;
                panelMain.Visible = true;
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Erorr btnDelete_click", ex);
            }
        } 
    }
}