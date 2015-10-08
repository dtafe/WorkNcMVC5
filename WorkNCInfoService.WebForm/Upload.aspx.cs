using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class Upload : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["FileName"] == null && fileUploadExcel.HasFile)
                {

                    Session["FileName"] = fileUploadExcel;

                }

                if (fileUploadExcel.HasFile)
                {

                    Session["FileName"] = fileUploadExcel;

                }
                
                InitLanguage();
                if (!IsPostBack)
                {
                    ViewState.Remove("dtDatas");
                    ViewState.Remove("dtDataWZ");
                    Session["img"] = null;
                    btnLoadImage.Enabled = false;
                    Initialize();
                    AddFakeDataToGrid();
                    FillFactory();
                  
                }
            }


            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Page load:", ex);
            }
        }

        private void FillFactory()
        {
            List<Factory> listFac = Factory.GetAllFactory(int.Parse(GetCompany()));
            cbxFactory.Items.Add("");
            foreach (Factory f in listFac)
            {
                cbxFactory.Items.Add(new ListItem(f.Name, f.FactoryId.ToString()));
            }
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
                cbxMachine.Items.Add(new ListItem(m.Name, m.MachineId.ToString()));
            }
        }

        private void Initialize()
        {
            btnUpload.Attributes["onclick"] = "javascript:return confirm(' " + GetResource("msUpload") + " ');";
            fileUploadExcel.Attributes.Add("onchange", "return checkFileExtension(this);");
            SetLabelText(lblWorkZone, lblWorkZoneName, lblFactoryName, lblMachineName, lblTitle, lblComment, lblMessageCheckFileAppear, lblPicture,lblChekFileFormat,lblMessageCheckFileImg);
            SetButtonText(btnUpload, btnClear, btnLoad,btnLoadImage);
            requiredFactory.ErrorMessage = GetResource("RequiredFieldFactory");
            requiredMachine.ErrorMessage = GetResource("requiredMachine");
            //requiredWorkZone.ErrorMessage = GetResource("requiredWorkZone");
            requiredWorkZoneName.ErrorMessage = GetResource("requiredWorkZoneName");
            lblMessageCheckFile.Text = GetResource("lblMessageCheckFile");
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                txtWorkZone.Text = string.Empty;
                if (Session["FileName"] != null)
                {
                    FileUpload fup = new FileUpload();
                    fup = (FileUpload)Session["FileName"];
                    if (Path.GetExtension(fup.PostedFile.FileName) == ".xls")
                    {
                        txtWorkZone.Text = fup.PostedFile.FileName;
                        Session["FileName"] = null;
                    }
                    else
                    {
                        txtWorkZone.Text = string.Empty;
                        AddFakeDataToGrid();
                    }

                }
                #region Load Excel file
                if (fileUploadExcel.HasFile == false)
                {
                    ViewState.Remove("dtDatas");
                    ViewState.Remove("dtDataWZ");
                    AddFakeDataToGrid();
                    return;
                }
                Stream s = fileUploadExcel.PostedFile.InputStream;
                HSSFWorkbook wb;
                List<WorkZoneDetail> ListwncInfo = new List<WorkZoneDetail>();
                List<WorkZone> ListwzInfo = new List<WorkZone>();
                wb = new HSSFWorkbook(s);
                ISheet sheet = wb.GetSheet(Constant.WORKNC_DEFAULT_SHEET);
                if (sheet==null || sheet.GetRow(5).GetCell(2).StringCellValue == null || sheet.GetRow(5).GetCell(2).ToString()==string.Empty)
                {
                    lblChekFileFormat.Visible = true;
                    return;
                }
                DatastructureWZDT();
                DatastructureWZ();

                #region add workzonedetail list
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["dtDatas"];
                for (int row = 9; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row) != null)
                    {
                        dt.Rows.Add(
                            sheet.GetRow(row).GetCell(0).ToString().Trim(),
                            sheet.GetRow(row).GetCell(1).ToString().Trim(),
                            sheet.GetRow(row).GetCell(2).ToString().Trim(),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(3).ToString().Trim().TrimStart('φ')),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(4).ToString().Trim().TrimStart('r', '\0')),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(5).ToString().Trim().TrimStart('-')),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(6).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(7).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(8).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(9).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(10).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(11).ToString().Trim()),
                            Common.GetNullableDouble(sheet.GetRow(row).GetCell(12).ToString().Trim()),
                            sheet.GetRow(row).GetCell(13).ToString().Trim(),
                            string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME, sheet.GetRow(row).GetCell(0).ToString().Trim())
                       );
                    }
                }
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i][0].ToString() == string.Empty)
                        dt.Rows.RemoveAt(i);
                }
                ViewState["dtDatas"] = dt;
                #endregion

                #region add workzone list
                DataTable dtwz = new DataTable();
                dtwz = (DataTable)ViewState["dtDataWZ"];
                dtwz.Rows.Add(
                        txtWorkZoneName.Text,
                        sheet.GetRow(5).GetCell(2).ToString(),
                        sheet.GetRow(3).GetCell(5).ToString(),
                        sheet.GetRow(5).GetCell(5).ToString(),
                        Common.GetNullableDateTime(sheet.GetRow(7).GetCell(5).ToString()),
                        sheet.GetRow(2).GetCell(2).ToString(),
                        sheet.GetRow(3).GetCell(2).ToString(),
                        sheet.GetRow(4).GetCell(2).ToString(),
                        sheet.GetRow(8 + ListwncInfo.Count + 3).GetCell(13).ToString(),
                        //int.Parse(cbxFactory.SelectedItem.Value),
                        //int.Parse(cbxMachine.SelectedItem.Value),
                        txtComment.Text,
                        0,
                        txtImgName.Text
                    );

                ViewState["dtDataWZ"] = dtwz;
                #endregion

                GridView1.DataSource = dt;
                GridView1.DataBind();
                btnLoadImage.Enabled = true;
                lblImgFolder.Visible = true;
                lblImgFolder.Text = GetResource("lblImgFolder") + dtwz.Rows[0][1].ToString();
                lblChekFileFormat.Visible = false;
                #endregion

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Load excel:", ex);
            }
        }

        private void DatastructureWZ()
        {
            DataTable dtDatas = new DataTable();
            //add columns to the datatable
            dtDatas.Columns.Add("Name");
            dtDatas.Columns.Add("WorkZonePath");
            dtDatas.Columns.Add("ModelDataProgramer");
            dtDatas.Columns.Add("NCDataProgramer");
            dtDatas.Columns.Add("ProgramDate");
            dtDatas.Columns.Add("ModelName");
            dtDatas.Columns.Add("Parts");
            dtDatas.Columns.Add("PartName");
            dtDatas.Columns.Add("MachiningTimeTotal");
            dtDatas.Columns.Add("FactoryId");
            dtDatas.Columns.Add("MachineId");
            dtDatas.Columns.Add("Comment");
            dtDatas.Columns.Add("Status");
            dtDatas.Columns.Add("ImageFile");
            //store the state of the datatable into a ViewState object
            ViewState["dtDataWZ"] = dtDatas;
        }

        private void DatastructureWZDT()
        {
            DataTable dtDatas = new DataTable();
            //add columns to the datatable
            dtDatas.Columns.Add("No");
            dtDatas.Columns.Add("NCFileName");
            dtDatas.Columns.Add("PathType");
            dtDatas.Columns.Add("ToolDia");
            dtDatas.Columns.Add("ToolConerR");
            dtDatas.Columns.Add("ToolLenth");
            dtDatas.Columns.Add("Tno");
            dtDatas.Columns.Add("StockAllowance");
            dtDatas.Columns.Add("Tolerance");
            dtDatas.Columns.Add("Spindll");
            dtDatas.Columns.Add("FeedRate");
            dtDatas.Columns.Add("Zmin");
            dtDatas.Columns.Add("CutDistance");
            dtDatas.Columns.Add("MachineTime");
            dtDatas.Columns.Add("ImageFile");
            //store the state of the datatable into a ViewState object
            ViewState["dtDatas"] = dtDatas;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["dtDatas"] != null && ViewState["dtDataWZ"]!=null)
                {
                    if (Session["img"] == null)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), lblMessageCheckFileImg.Text) + "\");");
                        return;
                    }
                    if (cbxFactory.SelectedItem.Value == string.Empty || cbxMachine.SelectedItem.Value == string.Empty)
                    {
                        return;
                    }
                    List<WorkZone> WZName = new List<WorkZone>();
                    WZName = WorkZone.GetAll().Where(l => l.Name == txtWorkZoneName.Text.Trim()).ToList();

                    if (WZName.Count > 0)
                    {
                        lblChekFileFormat.Visible = true;
                        lblChekFileFormat.Text = string.Format(GetResource("CheckWZName"), txtWorkZoneName.Text);
                        //txtImgName.Text = string.Empty;
                        return;
                    }
                    //get data on ViewSate
                    DataTable dtWZD = new DataTable();
                    dtWZD = (DataTable)ViewState["dtDatas"];
                    DataTable dtWZ = new DataTable();
                    dtWZ = (DataTable)ViewState["dtDataWZ"];

                    //Convert Table WorkZone to Object
                    WorkZone wz = new WorkZone();
                    wz.Name = txtWorkZoneName.Text;
                    wz.WorkZonePath = dtWZ.Rows[0]["WorkZonePath"].ToString();
                    wz.ModelDataProgramer = dtWZ.Rows[0]["ModelDataProgramer"].ToString();
                    wz.NCDataProgramer = dtWZ.Rows[0]["NCDataProgramer"].ToString();
                    wz.ProgramDate = Common.GetNullableDateTime(dtWZ.Rows[0]["ProgramDate"].ToString());
                    wz.ModelName = dtWZ.Rows[0]["ModelName"].ToString();
                    wz.Parts = dtWZ.Rows[0]["Parts"].ToString();
                    wz.PartName = dtWZ.Rows[0]["PartName"].ToString();
                    wz.MachiningTimeTotal = dtWZ.Rows[0]["MachiningTimeTotal"].ToString();
                    wz.CompanyId = int.Parse(GetCompany());
                    wz.CompanyName = Company.GetCompanyName(wz.CompanyId);
                    wz.FactoryId = int.Parse(cbxFactory.SelectedItem.Value);
                    wz.FactoryName = Company.GetCompanyName(wz.FactoryId);
                    wz.MachineId = int.Parse(cbxMachine.SelectedItem.Value);
                    wz.Comment = txtComment.Text;
                    wz.Status = 0;
                    wz.ImageFile = Constant.WORKNC_WZ_IMAGE_NAME;
                    wz.CreateAccount = this.User.Identity.Name;

                    //Convert DataTable WorkZoneDetail to List
                    List<WorkZoneDetail> listWZD = new List<WorkZoneDetail>();
                    foreach (DataRow r in dtWZD.Rows)
                    {
                        listWZD.Add(new WorkZoneDetail()
                        {
                            No = r["No"].ToString(),
                            NCFileName = r["NCFileName"].ToString(),
                            PathType = r["PathType"].ToString(),
                            ToolDia =Common.GetNullableDouble(r["ToolDia"].ToString()),
                            ToolConerR = Common.GetNullableDouble(r["ToolConerR"].ToString()),
                            ToolLenth = Common.GetNullableDouble(r["ToolLenth"].ToString()),
                            Tno = Common.GetNullableDouble(r["Tno"].ToString()),
                            StockAllowance = Common.GetNullableDouble(r["StockAllowance"].ToString()),
                            Tolerance = Common.GetNullableDouble(r["Tolerance"].ToString()),
                            Spindle = Common.GetNullableDouble(r["Spindlle"].ToString()),
                            CuttingFeedRate = Common.GetNullableDouble(r["CuttingFeedRate"].ToString()),
                            ApproachFeedRate = Common.GetNullableDouble(r["ApproachFeedRate"].ToString()),
                            MachineDistance = Common.GetNullableDouble(r["MachineDistance"].ToString()),
                            MachineTime = r["MachineTime"].ToString(),
                            Status = 0,
                            ImageFile = string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME, r["No"].ToString()),
                            CreateAccount = this.User.Identity.Name
                        });
                    }

                    //Transaction upload workZone
                    int idWZ = WorkZone.TransactionUpLoad(wz, listWZD);

                    //Rename  0_workZoneName -> 1_workzoneName 
                    string workZoneTemp = Server.MapPath("~/" + Common.GetFolderWorkZone(wz.CompanyId, wz.CompanyName, wz.FactoryId, wz.FactoryName, 0, Session["previousName"].ToString()));
                    string workZoneFolder = Server.MapPath("~/" + Common.GetFolderWorkZone(wz.CompanyId, wz.CompanyName, wz.FactoryId, wz.FactoryName, idWZ, wz.Name)); 

                    if (Directory.Exists(workZoneTemp))
                    {
                        Directory.Move(workZoneTemp , workZoneFolder);
                    }
                    else
                    {
                        Directory.CreateDirectory(workZoneFolder);
                    }

                    RegisterStartupScript("alert(\"" + GetJSMessage("", GetResource("MSGUPLOAD")) + "\");");
                    txtImgName.Text = string.Empty;
                    lblImgFolder.Visible = false;
                    imgWorkZone.ImageUrl = "~/Images/no-image.png";
                    AddFakeDataToGrid();

                    ViewState.Remove("dtDatas");
                    ViewState.Remove("dtDataWZ");
                    Session.RemoveAll();
                    btnLoadImage.Enabled = false;
                    lblChekFileFormat.Visible = false;
                }
                else
                {
                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("requiredWorkZoneLoad")) + "\");");
                    return;
                }
                txtWorkZone.Text = txtComment.Text = txtWorkZoneName.Text = cbxFactory.SelectedValue = cbxMachine.SelectedValue = string.Empty;
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Upload:", ex);
            }
        }

        private void AddFakeDataToGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No");
            dt.Columns.Add("PathType");
            dt.Rows.Add("", "");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (Common.GetRowString(GridView1.Rows[0].Cells[0].Text) == "")
                GridView1.Rows[0].Visible = false;
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            InitLanguage();
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Build custom header.
                GridView oGridView = (GridView)sender;
                GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell oTableCell = new TableHeaderCell();

                //Add No
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Factory_No");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oGridViewRow.Cells.Add(oTableCell);


                //Add PathType
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_PathType_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(150);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Comment
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_image");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(150);
                oGridViewRow.Cells.Add(oTableCell);

                //Add custom header            
                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            }
        }

        protected void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["dtDatas"] == null && ViewState["dtDataWZ"] == null)
                {
                    return;
                }
                if (txtWorkZoneName.Text == string.Empty)
                {
                    txtImgName.Text = string.Empty;
                    return;
                }
                if (fileUploadImage.HasFile)
                {
                    if (WorkZone.GetCountWorkZoneName(int.Parse(cbxMachine.SelectedValue), txtWorkZoneName.Text) > 0)
                    {
                        lblChekFileFormat.Visible = true;
                        lblChekFileFormat.Text = string.Format(GetResource("CheckWZName"),txtWorkZoneName.Text);
                        txtImgName.Text = string.Empty;
                        return;
                    }
                    int companyId = int.Parse(GetCompany()); 
                    string companyName = Company.GetCompanyName(companyId);

                    //set tempPath in Factory
                    
                    string pathTemp = "/" + Common.GetFolderWorkZone(companyId, companyName, int.Parse(cbxFactory.SelectedValue), cbxFactory.SelectedItem.Text , 0 , txtWorkZoneName.Text); 
                    string imgUrl = "~" +  pathTemp +  "/";
                    if (!Directory.Exists(Server.MapPath(pathTemp)))
                    {
                        Directory.CreateDirectory(Server.MapPath(pathTemp));
                    }
                    
                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            string ext = Path.GetExtension(hpf.FileName);
                            if (ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                                hpf.SaveAs(Server.MapPath(pathTemp) + "\\" + Path.GetFileName(hpf.FileName));
 
                        }
                    }
                    Session["img"] = 1;
                    lblChekFileFormat.Visible = false;
                    imgWorkZone.ImageUrl = imgUrl + Constant.WORKNC_WZ_IMAGE_NAME;
                    Image img = new Image();
                    string pathWZDetailTemp = "~/" + Common.GetFolderWorkZoneDetail(companyId, companyName, int.Parse(cbxFactory.SelectedValue), cbxFactory.SelectedItem.Text, 0, txtWorkZoneName.Text); 
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        img = (Image)r.Cells[1].FindControl("imgProcess");
                        img.ImageUrl = pathWZDetailTemp + "/" +  string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME, r.Cells[0].Text);
                    }
                    Session["previousName"] = txtWorkZoneName.Text;
                }
                
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Load image:", ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ViewState.Remove("dtDatas");
            ViewState.Remove("dtDataWZ");
            Session["img"] = null;
            btnLoadImage.Enabled = false;
            AddFakeDataToGrid();
            txtComment.Text = txtImgName.Text = txtWorkZone.Text = txtWorkZoneName.Text = string.Empty;
        }

        protected void cbxFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxMachine.Items.Clear();
            FillMachine();
        }
   
    }
}