using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class WorkZoneList : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            hplDateFrom.NavigateUrl = Calendar.InvokePopupCal(txtDateFrom);
            hplDateTo.NavigateUrl = Calendar.InvokePopupCal(txtDateTo);
            InitLanguage();
            if (!IsPostBack)
            {
                try
                {
                    Initialize();
                    FillFactory();
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
                //grdWorkZoneList.Columns[12].Visible = true;
               // grdWorkZoneList.Columns[13].Visible = true;
               // grdWorkZoneList.Columns[14].Visible = true;
                string MachineName = string.Empty;
                if (cbxMachine.Items.Count == 0 || cbxMachine.SelectedItem.Text == string.Empty)
                    MachineName = string.Empty;
                else
                    MachineName = cbxMachine.SelectedItem.Text;
                List<WorkZoneListInfo> listWNCInfo = new List<WorkZoneListInfo>();
                listWNCInfo = WorkZoneListInfo.GetWorkZoneListSearch(int.Parse(GetCompany()), txtWorkZoneName.Text, txtDateFrom.Text, txtDateTo.Text, cbxFactory.SelectedItem.Text, MachineName);
                if (listWNCInfo.Count == 0)
                {
                    WorkZoneListInfo WZinfo = new WorkZoneListInfo();
                    WZinfo.WorkZoneId = -1;
                    WZinfo.ProgramDate = Convert.ToDateTime("1/1/1973");
                    listWNCInfo.Add(WZinfo);
                    grdWorkZoneList.DataSource = listWNCInfo;
                    grdWorkZoneList.DataBind();
                    if (grdWorkZoneList.Rows[0].Cells[12].Text == "-1")
                        grdWorkZoneList.Rows[0].Visible = false;
                    return;
                }
                grdWorkZoneList.DataSource = listWNCInfo;
                grdWorkZoneList.DataBind();
                foreach (GridViewRow r in grdWorkZoneList.Rows)
                {
                    r.Cells[4].Text = SetDateFormat(r.Cells[4].Text);
                }
               // grdWorkZoneList.Columns[12].Visible = false;
               // grdWorkZoneList.Columns[13].Visible = false;
            //    grdWorkZoneList.Columns[14].Visible = false;
            }
            catch (Exception ex)
            {

                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Search:", ex);
            }
        }

        private void FillFactory()
        {
            List<Factory> listFac = Factory.GetAllFactory(int.Parse(GetCompany()));
            cbxFactory.Items.Add("");
            foreach(Factory f in listFac)
            {
                cbxFactory.Items.Add(new ListItem(f.Name,f.FactoryId.ToString()));
            }
        }

        private void Initialize()
        {
            btnDelete.Attributes["onclick"] = "javascript:return confirm(' " + GetResource("msDelete.Text") + " ');";

            SetButtonText(btnSearch, btnClear, btnEdit,btnDelete);
            SetLabelText(lblWorkZoneName, lblProgramDate,lblFactoryName,lblMachineName);
        }

        protected void cbxFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMachine();
        }

        private void FillMachine()
        {
            cbxMachine.Items.Clear();
            if (cbxFactory.SelectedItem.Value == string.Empty)
            {
                cbxMachine.Items.Clear();
                return;
            }
            List<Machine> listMachine = Machine.GetListMachine(int.Parse(GetCompany()), cbxFactory.SelectedValue);

            cbxMachine.Items.Add("");
            foreach (Machine m in listMachine)
            {
                cbxMachine.Items.Add(new ListItem(m.Name,m.MachineId.ToString()));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void grdWorkZoneList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                InitLanguage();

                //Build custom header.
                GridView oGridView = (GridView)sender;
                GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell oTableCell = new TableHeaderCell();

                #region "Make Headers"
                //Add CheckBoxSelected
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(20);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);
                
                //Add header_workzonename
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_workzonename");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(90);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);


                //Add header_ModelDataProgramer
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_ModelDataProgramer");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(80);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_NCDataProgramer
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_NCDataProgramer");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(80);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_ProgramDate
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_ProgramDate");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(70);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_ModelName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_ModelName");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(90);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_Parts
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Parts");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(80);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_PartName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_PartName");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(80);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_MachiningTimeTotal
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_MachiningTimeTotal");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_FactoryName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_FactoryName");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_MachineName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_MachineName");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                //Add header_Status
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Status");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Small;
                oGridViewRow.Cells.Add(oTableCell);

                ////Add header_Comment
                //oTableCell = new TableHeaderCell();
                //oTableCell.Text = GetResource("header_Comment");
                //oTableCell.CssClass = "td_header";
                //oTableCell.Width = Unit.Pixel(100);
                //oGridViewRow.Cells.Add(oTableCell);
                #endregion
                //Add custom header            
                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Upload.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int countcheck = 0;
                string id = "";
                CheckBox cb = new CheckBox();
                foreach (GridViewRow r in grdWorkZoneList.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("cb");
                    if (cb.Checked)
                    {
                        countcheck++;
                        if (countcheck == 2) break;
                        else id = r.Cells[12].Text;
                    }
                }
                if (countcheck == 0)
                {

                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_NONE_SELECTED_ITEM")) + "\");");
                    return;
                }
                else if (countcheck == 2)
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_MORE_ONE_SELECTED")) + "\");");
                    return;
                }
                else
                    Response.Redirect("~/WorkZoneListDetail.aspx?Id="+id+"",false);
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Edit:", ex);
            }
            
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDateFrom.Text = txtDateTo.Text = txtWorkZoneName.Text = cbxFactory.SelectedValue = cbxMachine.SelectedValue = string.Empty;
        }

        protected void grdWorkZoneList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWorkZoneList.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int countcheck = 0;
                string id = "";
                string name = "";
                CheckBox cb = new CheckBox();
                foreach (GridViewRow r in grdWorkZoneList.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("cb");
                    if (cb.Checked)
                    {
                        countcheck++;
                        if (countcheck == 2) break;
                        else
                        {
                            id = r.Cells[12].Text;
                            name = r.Cells[1].Text;
                        }
                    }
                }
                if (countcheck == 0)
                {

                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_NONE_SELECTED_ITEM")) + "\");");
                    return;
                }
                else if (countcheck == 2)
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("MSG_MORE_ONE_SELECTED")) + "\");");
                    return;
                }
                else
                {
                    WorkZone wz = WorkZone.GetWorkZone(int.Parse(id));
                    WorkZone.DeleteWorkZone(int.Parse(id));

                    string directoryPath = Server.MapPath("~" +  Common.GetFolderWorkZone( wz.CompanyId , wz.CompanyName , wz.FactoryId , wz.FactoryName , wz.WorkZoneId , wz.Name)); 
                    if (Directory.Exists(directoryPath))
                    {
                        Directory.Delete(directoryPath,true);
                    }
                    Search();
                }
                
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Delete:", ex);
            }
        }

        protected void grdWorkZoneList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='underline';";
                e.Row.Cells[1].ToolTip = "Click to view details";
                e.Row.Cells[1].Attributes["onclick"] = "rowno(" + e.Row.RowIndex + ")";
                
            }
        }
    }
}