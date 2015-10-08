using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.Domain;
using log4net;
using System.IO;
using System.Drawing.Imaging;

namespace WorkNCInfoService.WebForm
{
    public partial class MstFactory : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        protected void Page_Load(object sender, EventArgs e)
        {
            InitLanguage();
            
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
           
            if (!IsPostBack)
            {
                try
                {
                    ViewState.Remove("img");
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
                grdFactiry.Columns[4].Visible = true;
                grdFactiry.Columns[5].Visible = true;
                lblNoRecord.Visible = false;
                List<Factory> listFactory = new List<Factory>();
             
                listFactory = Factory.GetFactorySearch( int.Parse(GetCompany()), txtFactoryName.Text);
                if (!cbShowDeleted.Checked)
                {
                    listFactory = listFactory.Where(l => l.isDeleted == false).ToList();
                }
                if (listFactory.Count == 0)
                {
                    Factory objFt = new Factory();
                    objFt.FactoryId = -1;
                    objFt.No = "";
                    objFt.Name = "";
                    objFt.isDeleted = true;
                    listFactory.Add(objFt);
                    grdFactiry.DataSource = listFactory;
                    grdFactiry.DataBind();
                    if (listFactory.Count == 1 && listFactory[0].FactoryId == -1)
                        grdFactiry.Rows[0].Visible = false;
                    lblNoRecord.Visible = true;
                    return;
                }
                else
                {
                   
                    grdFactiry.DataSource = listFactory;
                    grdFactiry.DataBind();
                    foreach (GridViewRow r in grdFactiry.Rows)
                    {
                        r.Cells[3].Text = GetResource(string.Format("isDeleted_{0}", r.Cells[3].Text));
                    }
                    grdFactiry.Columns[4].Visible = false;
                    grdFactiry.Columns[5].Visible = false;
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
            lblFactoryName.Text = GetResource("lblFactoryName");
            cbShowDeleted.Text = GetResource("cbShowDeleted");
            lblNoRecord.Text = GetResource("lblNoRecord");
            SetButtonText(btnAddNew, btnEdit, btnClear, btnSearch,btnSave,btnCancel,btnDelete,btnLoadImage);
            lblNo.Text = GetResource("header_Factory_No");
            lblName.Text = GetResource("header_Factory_Name");
            lblPicture.Text = GetResource("lblPicture");
            lblMessageCheckFileImg.Text = GetResource("lblMessageCheckFileImg");
            requiredFactoryName.ErrorMessage = GetResource("requiredFactoryName");
        }

        protected void grdFactiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFactiry.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void grdFactiry_RowCreated(object sender, GridViewRowEventArgs e)
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
                oTableCell.Text = GetResource("header_Factory_No");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(150);
                oGridViewRow.Cells.Add(oTableCell);


                //Add Name
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Factory_Name");
                oTableCell.CssClass = "td_header";
                oTableCell.Width = Unit.Pixel(200);
                oGridViewRow.Cells.Add(oTableCell);

                //Add Status
                oTableCell = new TableHeaderCell();
                oTableCell.Text = GetResource("header_Factory_Status");
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
            txtFactoryName.Text = string.Empty;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelEdit.Visible = true;
            btnDelete.Enabled = false;
            txtName.Text = txtNo.Text = txtImgName.Text= string.Empty;

            imgFactory.ImageUrl = "~/Images/no-image.png";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {            
            try
            {                
                Factory fac = new Factory();
                btnDelete.Enabled = true;
                
                int countChecked = 0;
                CheckBox cb = new CheckBox();
                string No = "", Name="",Img="";
                foreach (GridViewRow r in grdFactiry.Rows)
                {
                    cb = (CheckBox)r.Cells[0].FindControl("cb");
                    if (cb.Checked)
                    {
                        countChecked++;
                        if (countChecked == 2) break;
                        else
                        {
                            hiddenFacID.Value = Common.GetRowString(r.Cells[4].Text);
                            No = Common.GetRowString(r.Cells[1].Text);
                            Name = Common.GetRowString(r.Cells[2].Text);
                            Img = Common.GetRowString(r.Cells[5].Text);
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
                    txtName.Text = Name;
                    txtImgName.Text = Img;

                    int companyId = int.Parse(GetCompany());
                    string pathFactory =  Common.GetFolderFactory(companyId , Company.GetCompanyName(companyId) ,  int.Parse(hiddenFacID.Value), txtName.Text);

                    if (!File.Exists(Server.MapPath("~/" + pathFactory + "/" + txtImgName.Text)))
                        imgFactory.ImageUrl = "~/Images/no-image.png";

                    else
                    {
                        Random rd = new Random();
                        int i = rd.Next(int.MinValue, int.MaxValue);

                        imgFactory.ImageUrl = "~/" + pathFactory + "/" + txtImgName.Text + "?" + i;
                    }            
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
            grdFactiry.Columns[4].Visible = false;
            if (btnDelete.Enabled == false)
            {
                string pathNew = Constant.PORTAL + GetCompanyPath() + string.Format(Constant.PATH_FACTORY, hiddenFacID.Value, txtFactoryName.Text);
                string fileNm = Server.MapPath(pathNew + "/" + txtImgName.Text); //("~/Portal\\Images\\Factory\\" + txtImgName.Text);
                if(File.Exists(fileNm))
                {
                    File.Delete(fileNm);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                if (btnDelete.Enabled == false)
                {                   
                    List<Factory> listFac = new List<Factory>();
                    listFac = Factory.GetAll().Where(l => l.Name.Trim().ToLower() == txtName.Text.Trim().ToLower()).ToList();
                    if (listFac.Count > 0)
                    {
                        RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblFactoryName.Text)) + "\");");
                        return;
                    }
                    Factory fac = new Factory();
                    fac.No = txtNo.Text;
                    fac.CompanyId = int.Parse(GetCompany());
                    fac.Name = txtName.Text;
                    fac.isDeleted = false;
                    //fac.ImageFile = txtImgName.Text;
                    fac.CreateAccount = fac.ModifiedAccount = this.User.Identity.Name;
                    int id = Factory.InsertWithIndentity(fac);
                    fac.ImageFile = id.ToString()+".png";
                    fac.Update();

                    string pathNew = Constant.PORTAL + GetCompanyPath() + string.Format(Constant.PATH_FACTORY, id, txtName.Text);
                    if (!Directory.Exists(Server.MapPath(pathNew))) //(!Directory.Exists(Server.MapPath("~/Portal\\Images\\Factory\\")))
                    {
                        Directory.CreateDirectory(Server.MapPath(pathNew)); //("~/Portal\\Images\\Factory\\)"));
                    }
                    string path = Server.MapPath(pathNew);//"~/Portal\\Images\\Factory\\");
                    //string filename = imgFactory.ImageUrl;
                    string base64 = (string)ViewState["img"];
                    byte[] b = Convert.FromBase64String(base64);
                    System.IO.File.WriteAllBytes(path + "/" + id +".png", b);
                }
                else
                {

                    Factory fac = new Factory();
                    fac.FactoryId = int.Parse(hiddenFacID.Value);
                    fac.CompanyId = int.Parse(GetCompany());
                    fac = fac.GetByPrimaryKey();
                    if (fac.Name.Trim() != txtName.Text.Trim())
                    {
                        List<Factory> listFac = new List<Factory>();
                        listFac = Factory.GetAll().Where(l => l.Name.Trim().ToLower() == txtName.Text.Trim().ToLower()).ToList();
                        if (listFac.Count > 0)
                        {
                            RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), string.Format(GetResource("CheckName"), lblFactoryName.Text)) + "\");");
                            return;
                        }
                    }
                    fac.ModifiedAccount = this.User.Identity.Name;
                    fac.No = txtNo.Text;
                    fac.Name = txtName.Text;
                    fac.ImageFile = hiddenFacID.Value.ToString()+".png";
                    fac.Update();
                    string pathNew = Constant.PORTAL + GetCompanyPath() + string.Format(Constant.PATH_FACTORY, hiddenFacID.Value, txtName.Text);
                    if (!Directory.Exists(Server.MapPath(pathNew))) //("~/Portal\\Images\\Factory\\")))
                    {
                        Directory.CreateDirectory(Server.MapPath(pathNew)); //("~/Portal\\Images\\Factory\\)"));
                    }
                    string path = Server.MapPath(pathNew); //("~/Portal\\Images\\Factory\\");
                    if (ViewState["img"] != null)
                    {
                        string base64 = (string)ViewState["img"];
                        byte[] b = Convert.FromBase64String(base64);
                        System.IO.File.WriteAllBytes(path + "/" + int.Parse(hiddenFacID.Value) + ".png", b);
                        
                    }
                }
                ViewState.Remove("img");
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
                Factory fac = new Factory();
                fac.FactoryId = int.Parse(hiddenFacID.Value);
                fac.isDeleted = true;
                fac.No = txtNo.Text;
                fac.Name = txtName.Text;
                fac.ImageFile = txtImgName.Text;
                fac.ModifiedAccount = this.User.Identity.Name;
                fac.Update();
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

        protected void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUploadImage.HasFile)
                {
                    imgFactory.ImageUrl = ShowImage();
                    if (btnDelete.Enabled == true)
                    {
                        txtImgName.Text = hiddenFacID.Value+".png";
                    }
                    else
                    {
                        txtImgName.Text = "Untitled.png";
                    }
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");");
                logger.Error("Erorr Load image", ex);
            }
        }

        private string ShowImage()
        {
            ViewState.Remove("img");
            Stream s = fileUploadImage.PostedFile.InputStream;
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
                    ViewState["img"] = base64String;
                    return "data:image/png;base64," + base64String;
                }
            }          
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
    }
}