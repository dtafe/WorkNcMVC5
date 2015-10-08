using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class WorkZoneListDetail : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitLanguage();
                if (!IsPostBack)
                {
                    ViewState.Remove("imgWZ");
                    ViewState.Remove("imgWZD");
                    Initialize();
                    FillFactory();
                    FillStatus();
                    FillWorkZone();
                    FillWorkZoneDetail();
                   
                }
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Page load:", ex);
            }
        }

        private void FillStatus()
        {
            cbxStatus.Items.Clear();
            cbxStatus.Items.Add(new ListItem(GetResource("STATUS_0"),"0"));
            cbxStatus.Items.Add(new ListItem(GetResource("STATUS_1"),"1"));
            cbxStatus.Items.Add(new ListItem(GetResource("STATUS_2"),"2"));
        }

        private void FillFactory()
        {
            List<Factory> listFactory =  Factory.GetAllFactory(int.Parse(GetCompany()));
            cbxFactory.Items.Add("");
            foreach (Factory f in listFactory)
            {
                cbxFactory.Items.Add(new ListItem(f.Name,f.FactoryId.ToString()));
            }
        }

        private void FillWorkZoneDetail()
        {
           // grdWorkZoneDetail.Columns[16].Visible = grdWorkZoneDetail.Columns[17].Visible = true;
            string id = Request.QueryString["Id"];
            if (id == string.Empty)
                return;


            List<WorkZoneDetail> listWZD = new List<WorkZoneDetail>();
            listWZD = WorkZoneDetail.GetWorkZoneDetailByWorkZoneID(int.Parse(id));
            if (listWZD.Count == 0)
            {
                WorkZoneDetail WZD = new WorkZoneDetail();
                WZD.WorkZoneId = -1;
                WZD.WorkZoneDetailId = -1;
                listWZD.Add(WZD);
                grdWorkZoneDetail.DataSource = listWZD;
                grdWorkZoneDetail.DataBind();
                if (grdWorkZoneDetail.Rows[0].Cells[17].Text == "-1")
                {
                    grdWorkZoneDetail.Rows[0].Visible = false;
                    return;
                }
            }
            grdWorkZoneDetail.DataSource = listWZD;
            grdWorkZoneDetail.DataBind();
            //grdWorkZoneDetail.Columns[16].Visible = grdWorkZoneDetail.Columns[17].Visible = false;
        }

        private void FillWorkZone()
        {
            string id = Request.QueryString["Id"];
            if (id == string.Empty)
                return;
            WorkZone w = new WorkZone();
            w.WorkZoneId = int.Parse(id);
            HiddenWorkZoneId.Value = id;
            w = w.GetByPrimaryKey();
            txtWorkZoneName.Text = w.Name;
            txtWorkZonePath.Text = w.WorkZonePath;
            txtModelDataProgramer.Text = w.ModelDataProgramer;
            txtNCDataProgramer.Text = w.NCDataProgramer;
            txtProgramDate.Text = SetDateFormat(w.ProgramDate.ToString());
            txtModelName.Text = w.ModelName;
            txtParts.Text = w.Parts;
            txtPartName.Text = w.PartName;
            txtMachiningTimeTotal.Text = w.MachiningTimeTotal;
          
            string pathWZFile = Server.MapPath("~/" + Common.GetFolderWorkZone(w.CompanyId, w.CompanyName, w.FactoryId, w.FactoryName, w.WorkZoneId, w.Name)) + "/" + w.ImageFile;

            if (File.Exists(pathWZFile))
                imgWorkZone.ImageUrl = "~/" + Common.GetFolderWorkZone(w.CompanyId, w.CompanyName, w.FactoryId, w.FactoryName, w.WorkZoneId, w.Name) + "/" + w.ImageFile  + "?" + (new Random()).Next(int.MinValue, int.MaxValue) + "";
            else
                imgWorkZone.ImageUrl = "~/Images/no-image.png";
            cbxFactory.SelectedValue = w.FactoryId.ToString();
            FillMachine();
            cbxMachine.SelectedValue = w.MachineId.ToString();
            cbxStatus.SelectedValue = w.Status.ToString();
            txtComment.Text = w.Comment;
        }

        private void Initialize()
        {
            hplCalendar.NavigateUrl = Calendar.InvokePopupCal(txtProgramDate);
            lblWorkZoneName.Text = GetResource("header_workzonename");
            lblWorkZonePath.Text = GetResource("header_WorkZonePath");
            lblModelDataProgramer.Text = GetResource("header_ModelDataProgramer");
            lblNCDataProgramer.Text = GetResource("header_NCDataProgramer");
            lblProgramDate.Text = GetResource("header_ProgramDate");
            lblModelName.Text = GetResource("header_ModelName");
            lblParts.Text = GetResource("header_Parts");
            lblPartName.Text = GetResource("header_PartName");
            lblMachiningTimeTotal.Text = GetResource("header_MachiningTimeTotal");
            lblFactoryName.Text = GetResource("header_Factory_Name");
            lblMachineName.Text = GetResource("header_Machine_Name");
            lblStatus.Text = GetResource("header_Status");
            lblComment.Text = GetResource("lblComment");
            lblPicture.Text = GetResource("lblPicture");
            lblMessageCheckFileImg.Text = GetResource("lblMessageCheckFileImg");
            SetButtonText(btnSave, btnBack,btnEdit,btnLoadImage,btnCancel);
            lblNo.Text = "No";
            lblNcFileName.Text = "NCFileName";
            lblPathType.Text = "PathType";
            lblCommentWZD.Text = GetResource("lblComment");
            lblImage.Text = GetResource("lblPicture");
            btnSaveWZD.Text = GetResource("btnSaveWZD");
            btnLoadImgWZD.Text = GetResource("btnLoadImage");
            
            //add valiadation message
            requiredWorkZoneName.ErrorMessage = GetResource("requiredWorkZoneName");
            RequiredFieldFactory.ErrorMessage = GetResource("RequiredFieldFactory");
            requiredMachine.ErrorMessage = GetResource("requiredMachine");
            RequiredFieldDate.ErrorMessage = GetResource("RequiredFieldDate");
            CompareValidatorDate.ErrorMessage = GetResource("CompareValidatorDate");
        }

        protected void grdWorkZoneDetail_RowCreated(object sender, GridViewRowEventArgs e)
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
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add NO
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_PathNo");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(20);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add PathType
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_PathType");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(100);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);


                //Add StockAllowance
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_StockAllowance");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add Tolerance
                oTableCell = new TableHeaderCell();
                oTableCell.Text =  GetResource("header_StockTolerance");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);


                //Add NCFileName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_NCData_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add MachineTime
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Machine_Time");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add MachineDistance
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Machine_Distance");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add ToolShape
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Tool_Shape");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);


                //Add ToolDia
                oTableCell = new TableHeaderCell();
                oTableCell.Text =  GetResource("header_Tool_Diameter");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add ToolConerR
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Tool_Corner");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add HolderName
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Holder_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add Spindle
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Spindle");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add FeedRate
                oTableCell = new TableHeaderCell();
                oTableCell.Text =  GetResource("header_Cutting_FeedRate");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(50);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add Zmin
                oTableCell = new TableHeaderCell();
                oTableCell.Text =  GetResource("header_Approach_FeedRate");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);

                //Add Status
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Status");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(60);
                oTableCell.Font.Size = FontUnit.Smaller;
                oGridViewRow.Cells.Add(oTableCell);


                #endregion
                //Add custom header            
                oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
            }
        }

        protected void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWorkZoneName.Text == string.Empty)
                {
                    txtImgName.Text = string.Empty;
                    return;
                }
                if (fileUploadImage.HasFile)
                {
                    ViewState["imgWZ"] = ShowImage(fileUploadImage);
                    imgWorkZone.ImageUrl = "data:image/png;base64," + ViewState["imgWZ"].ToString();
                    txtImgName.Text = "Untitled";
                }

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Load image:", ex);
            }
        }
        private string ShowImage(FileUpload fileUpload)
        {
            
            Stream s = fileUpload.PostedFile.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            s.Close();
            MemoryStream ss = new MemoryStream(b);

            System.Drawing.Image image = System.Drawing.Image.FromStream(ss);
            using (System.Drawing.Image thumbnail = image.GetThumbnailImage(200, 200, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    thumbnail.Save(memoryStream, ImageFormat.Png);
                    Byte[] bytes = new Byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    return base64String;
                }
            }

        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id"].ToString() == string.Empty)
                    return;
                WorkZone wz = WorkZone.GetWorkZone(int.Parse(Request.QueryString["Id"].ToString()));
              
                int countWorKZoneName = WorkZone.GetCountWorkZoneName( int.Parse(cbxMachine.SelectedItem.Value),txtWorkZoneName.Text);
                if (wz == null)
                {
                    if(countWorKZoneName > 0)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckWZName"), txtWorkZoneName.Text)) + "\");");
                        return;
                    }
                    wz = new WorkZone();
                    wz.WorkZoneId = int.Parse(Request.QueryString["Id"].ToString());
                    wz.CreateAccount = this.User.Identity.Name;
                }
                else
                {
                    if (countWorKZoneName > 0 && wz.Name != txtWorkZoneName.Text)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckWZName"), txtWorkZoneName.Text)) + "\");");
                        return;
                    }
                }
                wz.Name = txtWorkZoneName.Text;
                wz.CompanyId = int.Parse(GetCompany());
                wz.CompanyName = Company.GetCompanyName(wz.CompanyId);
                wz.FactoryId = int.Parse(cbxFactory.SelectedItem.Value);
                wz.FactoryName = Factory.GetFactoryName(wz.FactoryId);
                wz.MachineId = int.Parse(cbxMachine.SelectedItem.Value);

                wz.WorkZonePath = txtWorkZonePath.Text;
                wz.ModelDataProgramer = txtModelDataProgramer.Text;
                wz.NCDataProgramer = txtNCDataProgramer.Text;
                wz.ProgramDate = Convert.ToDateTime(txtProgramDate.Text);
                wz.ModelName = txtModelName.Text;
                wz.Parts = txtParts.Text;
                wz.PartName = txtPartName.Text;
                wz.MachiningTimeTotal = txtMachiningTimeTotal.Text;
              
                wz.Comment = txtComment.Text;
                wz.Status = int.Parse(cbxStatus.SelectedItem.Value);
                wz.ImageFile = Constant.WORKNC_WZ_IMAGE_NAME;
                wz.ModifiedAccount = this.User.Identity.Name;
                wz.Update();

                //if(previousName != wz.Name)
                //    Directory.Move(Server.MapPath("~/Portal\\Images\\WorkZone\\" + previousName + ""), Server.MapPath("~/Portal\\Images\\WorkZone\\" + txtWorkZoneName.Text + ""));
               
                if (txtImgName.Text != string.Empty)
                {
                    string path = "~/" + Common.GetFolderWorkZone(wz.CompanyId , wz.CompanyName , wz.FactoryId, wz.FactoryName , wz.WorkZoneId , wz.Name);
                    SaveImage("imgWZ", path, wz.ImageFile);
                }
                ViewState.Remove("imgWZ");
                RegisterStartupScript("alert(\"" + GetJSMessage("", GetResource("MSGUPLOAD")) + "\");");
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Save WorkZone:", ex);
            }
        }

        private void SaveImage(string ViewStateName,string Path,string ImageName)
        {
            try
            {
                if (ViewState["" + ViewStateName + ""] == null)
                    return;
                string base64string = ViewState["" + ViewStateName + ""].ToString();
                if (!Directory.Exists(Server.MapPath(Path)))
                {
                    Directory.CreateDirectory(Server.MapPath(Path));
                }
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(base64string)))
                {
                    using (Bitmap bm2 = new Bitmap(ms))
                    {
                        bm2.Save(Server.MapPath(Path) + "\\" + ImageName + "");
                    }
                }
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Save Image:", ex);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WorkZoneList.aspx");
        }

        protected void cbxFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMachine();
        }

        private void FillMachine()
        {
            if (cbxFactory.SelectedItem.Value == string.Empty)
                return;
            cbxMachine.Items.Clear();
            List<Machine> listMaChine = new List<Machine>();
            listMaChine = Machine.GetAll().Where(m => m.FactoryId == int.Parse(cbxFactory.SelectedItem.Value)).ToList();
            cbxMachine.Items.Add("");
            foreach (Machine m in listMaChine)
            {
                cbxMachine.Items.Add(new ListItem(m.Name, m.MachineId.ToString()));
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (HiddenWorkZoneDetailId.Value != "")
                {
                    ShowWorkZOneDetail();
                    return;
                }
                int countcheck = 0;
                string id = "";
                CheckBox cb = new CheckBox();
                foreach (GridViewRow r in grdWorkZoneDetail.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("cb");
                    if (cb.Checked)
                    {
                        countcheck++;
                        if (countcheck == 2) break;
                        else
                        {
                            id = r.Cells[16].Text;
                            HiddenWorkZoneDetailId.Value = r.Cells[16].Text;
                            HiddenWorkZoneId.Value = r.Cells[17].Text;
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
                    ShowWorkZOneDetail();
                }
            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Edit WorkZoneDetail:", ex);
            }
        }
        private void ShowWorkZOneDetail()
        {
            WorkZoneDetail wzd = new WorkZoneDetail();

            wzd = WorkZoneDetail.GetWorkZoneDetail(int.Parse(HiddenWorkZoneId.Value), int.Parse(HiddenWorkZoneDetailId.Value));
            txtNo.Text = wzd.No;
            txtNCfileName.Text = wzd.NCFileName;
            txtPathType.Text = wzd.PathType;
            txtCommetWZD.Text = wzd.Comment;
            txtImgWZD.Text = wzd.ImageFile;

            WorkZone wz = WorkZone.GetWorkZone(int.Parse(HiddenWorkZoneId.Value));
            string pathFileDetail = Server.MapPath("~/" + Common.GetFolderWorkZoneDetail(wz.CompanyId, wz.CompanyName, wz.FactoryId, wz.FactoryName, wz.WorkZoneId, wz.Name)) + "/" + wzd.ImageFile;

            if (File.Exists(pathFileDetail))
                imgWZD.ImageUrl = "~/" + Common.GetFolderWorkZoneDetail(wz.CompanyId, wz.CompanyName, wz.FactoryId, wz.FactoryName, wz.WorkZoneId, wz.Name) + "/" + txtImgWZD.Text + "?" + (new Random()).Next(int.MinValue, int.MaxValue) + "";
            else
                imgWZD.ImageUrl = "~/Images/no-image.png";

            panelMain.Visible = false;
            panelEidtDetail.Visible = true;
        }

        protected void btnLoadImgWZD_Click(object sender, EventArgs e)
        {
            if (fileUploadImageWZD.HasFile)
            {
                ViewState["imgWZD"] = ShowImage(fileUploadImageWZD);
                imgWZD.ImageUrl = "data:image/png;base64," + ViewState["imgWZD"].ToString();
                txtImgWZD.Text = string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME,txtNo.Text);
            }
        }

        protected void btnSaveWZD_Click(object sender, EventArgs e)
        {
            try
            {
                WorkZoneDetail wzd = new WorkZoneDetail();
                wzd = WorkZoneDetail.GetWorkZoneDetail(int.Parse(HiddenWorkZoneId.Value) , int.Parse(HiddenWorkZoneDetailId.Value));
                wzd.Comment = txtCommetWZD.Text;
                wzd.ImageFile = string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME, wzd.WorkZoneDetailId);
                wzd.Update();
                if (ViewState["imgWZD"] == null)
                    return;

                WorkZone wz = WorkZone.GetWorkZone(int.Parse(HiddenWorkZoneId.Value));

                string path = "~/" + Common.GetFolderWorkZoneDetail(wz.CompanyId , wz.CompanyName , wz.FactoryId  ,wz.FactoryName , wz.WorkZoneId , wz.Name);

                SaveImage("imgWZD", path, wzd.ImageFile);
                ViewState.Remove("imgWZD");
                panelMain.Visible = true;
                panelEidtDetail.Visible = false;

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Error in Save WorkZoneDetail:", ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HiddenWorkZoneDetailId.Value = "";
            panelMain.Visible = true;
            panelEidtDetail.Visible = false;
        }

        protected void grdWorkZoneDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='underline';";
                e.Row.Cells[1].ToolTip = "Click to view details";
                e.Row.Cells[1].Attributes["Onclick"] =  "CallEdit(" + e.Row.RowIndex + ")";
              
            }
        }

    }
}