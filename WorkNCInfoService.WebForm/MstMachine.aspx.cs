using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;
using log4net;
namespace WorkNCInfoService.WebForm
{
    public partial class MstMachine : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitLanguage();
                if (!IsPostBack)
                {
                    Initialize();
                    InitFactoryDropdownList();
                    Search();
                }
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Page load:", ex);
            }
        }

        private void Search()
        {
            try
            {
                lblNoRecord.Visible = false;
                grdMachine.Columns[4].Visible = true;
                grdMachine.Columns[5].Visible = true;
                List<Machine> listMachine = new List<Machine>();
                listMachine = Machine.GetMachineSearch(int.Parse(GetCompany()) ,  txtMachineNameSearch.Text, cbxFactorySearch.SelectedItem.Value);
                if (!cbShowDeleted.Checked)
                {
                    listMachine = listMachine.Where(l => l.isDeleted == false).ToList();
                }
                if (listMachine.Count == 0)
                {
                    Machine objMachine = new Machine();
                    objMachine.MachineId = -1;
                    objMachine.Name = "";
                    objMachine.No = "";
                    listMachine.Add(objMachine);
                    grdMachine.DataSource = listMachine;
                    grdMachine.DataBind();
                    if (grdMachine.Rows[0].Cells[4].Text == "-1")
                    {
                        grdMachine.Rows[0].Visible = false;
                        lblNoRecord.Visible = true;
                    }
                    return;
                }
                else
                {
                    grdMachine.DataSource = listMachine;
                    grdMachine.DataBind();
                    foreach (GridViewRow r in grdMachine.Rows)
                    {
                        r.Cells[3].Text = GetResource(string.Format("isDeleted_{0}", r.Cells[3].Text));
                    }
                    grdMachine.Columns[4].Visible = false;
                    grdMachine.Columns[5].Visible = false;
                }
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Search:", ex);
            }
        }

        private void InitFactoryDropdownList()
        {
            List<Factory> listFac = new List<Factory>();
            listFac = Factory.GetFactorySearch(int.Parse(GetCompany()) , "").Where(l=>l.isDeleted == false).ToList();
            cbxFactorySearch.Items.Add("");
            cbxfactory.Items.Add("");
            foreach (Factory f in listFac)
            {
                cbxFactorySearch.Items.Add(new ListItem(f.Name,f.FactoryId.ToString()));
                cbxfactory.Items.Add(new ListItem(f.Name, f.FactoryId.ToString()));
            }
            
        }

        private void Initialize()
        {
            btnDelete.Attributes["onclick"] = "javascript:return confirm(' " + GetResource("msDelete.Text") + " ');";
            SetButtonText(btnClear, btnSearch,btnAddNew,btnEdit,btnSave,btnDelete,btnCancel);
            lblfactoryName.Text =  lblFactoryNameSearch.Text = GetResource("lblFactoryName");
            lblMachineName.Text =  lblMachineNameSearch.Text = GetResource("lblMachineName");
            lblNo.Text = GetResource("header_Machine_No");
            cbShowDeleted.Text = GetResource("cbShowDeleted");
            RequiredFieldFactory.ErrorMessage = GetResource("RequiredFieldFactory");
            lblNoRecord.Text = GetResource("lblNoRecord");
        }

        protected void grdMachine_RowCreated(object sender, GridViewRowEventArgs e)
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
                oTableCell.Text = GetResource("header_Machine_No");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(150);
                oGridViewRow.Cells.Add(oTableCell);


                //Add Name
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Machine_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(200);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Status
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Machine_Status");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(100);
                oGridViewRow.Cells.Add(oTableCell);

                //Add custom header            
                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            }
        }

        protected void grdMachine_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMachine.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void cbxFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            panelEdit.Visible = true;
            panelMain.Visible = false;
            btnDelete.Enabled = false;
            txtMachineName.Text = txtNo.Text = string.Empty;
            cbxfactory.SelectedValue = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDelete.Enabled == false)
                {
                    List<Machine> listMac = new List<Machine>();
                    listMac = Machine.GetAll().Where(l => l.Name.Trim().ToLower() == txtMachineName.Text.Trim().ToLower()).ToList();
                    if (listMac.Count > 0)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblMachineName.Text)) + "\");");
                        return;
                    }
                    Machine mac = new Machine();
                    mac.No = txtNo.Text;
                    mac.Name = txtMachineName.Text;
                    mac.CompanyId = int.Parse(GetCompany());
                    mac.FactoryId = int.Parse(cbxfactory.SelectedItem.Value);
                    mac.isDeleted = false;
                    mac.CreateAccount = mac.ModifiedAccount = this.User.Identity.Name;
                    mac.Insert();
                }
                else
                {
                    Machine mac = new Machine();
                    mac.MachineId = int.Parse(hiddenMacID.Value);
                    mac = mac.GetByPrimaryKey();
                    if (mac.Name.Trim() != txtMachineName.Text.Trim())
                    {
                        List<Machine> listMac = new List<Machine>();
                        listMac = Machine.GetAll().Where(l => l.Name.Trim().ToLower() == txtMachineName.Text.Trim().ToLower()).ToList();
                        if (listMac.Count > 0)
                        {
                            RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblMachineName.Text)) + "\");");
                            return;
                        }
                    }
                    mac.ModifiedAccount = this.User.Identity.Name;
                    mac.No = txtNo.Text;
                    mac.Name = txtMachineName.Text;
                    mac.FactoryId = int.Parse(cbxfactory.SelectedItem.Value);
                    mac.CompanyId = int.Parse(GetCompany());
                    mac.Update();
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {

                Factory fac = new Factory();
                btnDelete.Enabled = true;
                int countChecked = 0;
                CheckBox cb = new CheckBox();
                string No = "", Name = "";
                foreach (GridViewRow r in grdMachine.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("cb");
                    if (cb.Checked)
                    {
                        countChecked++;
                        if (countChecked == 2) break;
                        else
                        {
                            hiddenMacID.Value = Common.GetRowString(r.Cells[4].Text);
                            HiddenFacID.Value = Common.GetRowString(r.Cells[5].Text);
                            No = Common.GetRowString(r.Cells[1].Text);
                            Name = Common.GetRowString(r.Cells[2].Text);
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
                    txtNo.Text = No;
                    txtMachineName.Text = Name;
                    cbxfactory.SelectedValue = HiddenFacID.Value;
                }

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Erorr btnEdit_click", ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Machine mac = new Machine();
                mac.MachineId = int.Parse(hiddenMacID.Value);
                mac.isDeleted = true;
                mac.No = txtNo.Text;
                mac.Name = txtMachineName.Text;
                mac.FactoryId = int.Parse(cbxfactory.SelectedItem.Value);
                mac.ModifiedAccount = this.User.Identity.Name;
                mac.Update();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelEdit.Visible = false;
            panelMain.Visible = true;
            txtMachineName.Text = txtNo.Text = string.Empty;
            cbxfactory.SelectedValue = "";
        }

        protected void cbShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtMachineNameSearch.Text = cbxFactorySearch.SelectedValue = string.Empty;
        }
    }
}