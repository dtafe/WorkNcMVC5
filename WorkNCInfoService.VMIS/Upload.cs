using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkNCInfoService.VMIS.Properties;
using System.Web;
using System.Net;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.VMIS.VeroMachingInfoWS;
using System.Text.RegularExpressions;


namespace WorkNCInfoService.VMIS
{
    public partial class frmUpload : Form
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string loginUser;

        public string LoginUser
        {
            get { return loginUser; }
            set { loginUser = value; }
        }
        public string DIRECTORY_WORK_ZONE = "";

        public frmUpload()
        {
            InitializeComponent();            
        }
        private void Upload_Load(object sender, EventArgs e)
        {
            DIRECTORY_WORK_ZONE = Directory.GetCurrentDirectory();
            txtWorkZoneName.Text =  Path.GetFileName(DIRECTORY_WORK_ZONE);

            string pathConfigOperator  = Settings.Default.CONFIG_OPERATOR;
            string enviromentKey = System.Text.RegularExpressions.Regex.Match(pathConfigOperator, @"\%(\w+)\%").Groups[1].Value;
            if(!string.IsNullOrEmpty(enviromentKey))
            {
                string pathEnviroment = Common.GetEnviromentConfigPath(enviromentKey);
                pathConfigOperator = pathConfigOperator.Replace("%", "").Replace(enviromentKey, pathEnviroment);
            }
            List<string> list = GetListStringFromCfg(pathConfigOperator);
            foreach (string i in list)
            {
                if(!string.IsNullOrWhiteSpace(i))
                  cboOperatorNm.Items.Add(i);
            }
        }
        public void FillMachine(List<Machine> list)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();

            foreach (Machine i in list)
            {
                comboSource.Add(i.MachineId.ToString(), i.Name);
            }

            cboMachineNm.DataSource = new BindingSource(comboSource, null);
            cboMachineNm.DisplayMember = "Value";
            cboMachineNm.ValueMember = "Key";
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Info("Begin btnUpload_Click");
                WorkZone workZone = GetWorkZone(DIRECTORY_WORK_ZONE);
                List<WorkZoneDetail> listWorkZoneDetail = GetListWorkZoneDetail(DIRECTORY_WORK_ZONE);
                logger.Debug("Call UploadWorkZone WS");
                VMISCommon.WS.UploadWorkZone(workZone, listWorkZoneDetail.ToArray());

                MessageBox.Show(Common.GetResourceString("MSG_UPLOAD_SUCCESS"));
                this.Close();
            }
            catch(Exception ex)
            {
                logger.Error("Error btnUpload_Click ", ex);
                MessageBox.Show(ex.Message);
            }
        }

        #region Read CSV File
        private WorkZone GetWorkZone(string directoryWorkZone)
        {
            logger.Debug("BEGIN GetWorkZone Info directoryWorkZone = "+ directoryWorkZone);
            string pathHeaderInfo = Path.Combine(directoryWorkZone, "headinfo.csv");

            if (!File.Exists(pathHeaderInfo))
                throw new Exception(Common.GetResourceString("MSG_HEAD_INFO_CSV_NOT_FOUND"));

            WorkZone objWorkZone = new WorkZone();
            objWorkZone.Name = txtWorkZoneName.Text;
            objWorkZone.MachineId = int.Parse(cboMachineNm.SelectedValue.ToString());
           
            objWorkZone.Status = 0;
            objWorkZone.CreateAccount = objWorkZone.ModifiedAccount = LoginUser;
            objWorkZone.ProgramDate = dtpMachineDate.Value;
            objWorkZone.NCDataProgramer = cboOperatorNm.Text; 

            //Read CSV file 
            StreamReader sr = new StreamReader(pathHeaderInfo, Encoding.GetEncoding(932));
            string inputLine = "";
            String[] inputLineValues = null;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            while ((inputLine = sr.ReadLine()) != null)
            {
                inputLineValues = inputLine.Split('\n');
                if (inputLineValues[0] != null && inputLineValues[0].Trim() != "")
                {
                    string[] values = r.Split(inputLineValues[0]);
                    if (values.Length >= 2)
                    {
                        objWorkZone.WorkZonePath = values[0];

                        if (!string.IsNullOrEmpty(values[13]))   //N
                            objWorkZone.Comment = CorrectString(values[13]);

                        string pathFileImage = Path.Combine(directoryWorkZone, @"doc\images\" + Constant.WORKNC_WZ_IMAGE_NAME);
                        objWorkZone.Base64Data = null;
                        if (File.Exists(pathFileImage))
                        {
                            objWorkZone.ImageFile = Path.GetFileName(pathFileImage);

                            // Load file meta data with FileInfo
                            FileInfo fileInfo = new FileInfo(pathFileImage);

                            // The byte[] to save the data in
                            byte[] data = new byte[fileInfo.Length];
                            using (FileStream fs = fileInfo.OpenRead())
                            {
                                fs.Read(data, 0, data.Length);
                            }
                            objWorkZone.Base64Data = Convert.ToBase64String(data);
                        }
                        break;
                    }
                    else
                        return null;
                }
            }
            sr.Close();
            return objWorkZone;
        }
         private List<WorkZoneDetail> GetListWorkZoneDetail(string directoryWorkZone)
          {
            logger.Debug("BEGIN GetListWorkZoneDetail from directoryWorkZone = " + directoryWorkZone);
            string pathWorkZoneDetail = Path.Combine(directoryWorkZone, "zoneinfo.csv");
            if (!File.Exists(pathWorkZoneDetail))
                throw new Exception(Common.GetResourceString("MSG_ZONE_INFO_CSV_NOT_FOUND"));

            //Read CSV file  
            List<WorkZoneDetail> listWorkZoneDetail = new List<WorkZoneDetail>();
            StreamReader sr = new StreamReader(pathWorkZoneDetail, Encoding.GetEncoding(932));
            string inputLine = "";
            String[] inputLineValues = null;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            while ((inputLine = sr.ReadLine()) != null)
            {
                inputLineValues = inputLine.Split('\n');
                if (inputLineValues[0] != null && inputLineValues[0].Trim() != "")
                {
                    string[] values = r.Split(inputLineValues[0]);
                    if (values.Length >= 2)
                    {
                        #region Get WorkZone Detail
                        if (string.IsNullOrWhiteSpace(values[0]) || values[0] == "")
                        {
                            continue;
                        }
                        WorkZoneDetail objWorkZoneDetail = new WorkZoneDetail();
                        objWorkZoneDetail.No = values[0];//A  
                        objWorkZoneDetail.PathType = CorrectString(values[1]);//B
                        objWorkZoneDetail.Comment = CorrectString(values[2]);//C

                        if (!string.IsNullOrEmpty(values[34]) && values[34] != "-")//AI
                            objWorkZoneDetail.ToolLenth = double.Parse(values[34].Replace('-', '0'));

                        if (!string.IsNullOrEmpty(values[8]) && values[8] != "-")//I
                            objWorkZoneDetail.Tno = double.Parse(values[8]);

                        if (!string.IsNullOrEmpty(values[10]) && values[10] != "-")//K
                            objWorkZoneDetail.Tolerance = double.Parse(values[10]);

                        if (!string.IsNullOrEmpty(values[11]) && values[11] != "-")//L
                            objWorkZoneDetail.StockAllowance = double.Parse(values[11]);

                        //NC Data
                        objWorkZoneDetail.NCFileName = CorrectString(values[28]);//-> AC
                        objWorkZoneDetail.MachineTime = values[61];//BJ
                        if (!string.IsNullOrWhiteSpace(values[55]) && values[55] != "" && values[55] != "-")//BD
                            objWorkZoneDetail.MachineDistance = double.Parse(values[55]);

                        //Tool Infomation
                        objWorkZoneDetail.ToolShape = CorrectString(values[6]); //G

                        if (!string.IsNullOrEmpty(values[3]) && values[3] != "-")//D
                            objWorkZoneDetail.ToolDia = double.Parse(values[3].ToUpper().Replace("D", ""));

                        if (!string.IsNullOrEmpty(values[4]) && values[4] != "-")//E                  
                            objWorkZoneDetail.ToolConerR = double.Parse(values[4].ToUpper().Replace("R", ""));

                        objWorkZoneDetail.HolderName = CorrectString(values[25]); //Z

                        //Cut Condition
                        if (!string.IsNullOrEmpty(values[18]) && values[18] != "-")//S
                            objWorkZoneDetail.Spindle = double.Parse(values[18]);

                        if (!string.IsNullOrEmpty(values[20]) && values[20] != "-")//U
                            objWorkZoneDetail.CuttingFeedRate = double.Parse(values[20]);

                        if (!string.IsNullOrWhiteSpace(values[19]) && values[19] != "" && values[19] != "-")//T
                            objWorkZoneDetail.ApproachFeedRate = double.Parse(values[19]);


                        objWorkZoneDetail.Status = 0;


                        string pathFileImage = Path.Combine(directoryWorkZone, string.Format(Constant.WORKNC_WZ_DETAIL_IMAGE_NAME, objWorkZoneDetail.No));

                        objWorkZoneDetail.Base64Data = null;
                        if (File.Exists(pathFileImage))
                        {
                            objWorkZoneDetail.ImageFile = Path.GetFileName(pathFileImage);

                            // Load file meta data with FileInfo
                            FileInfo fileInfo = new FileInfo(pathFileImage);

                            // The byte[] to save the data in
                            byte[] data = new byte[fileInfo.Length];
                            using (FileStream fs = fileInfo.OpenRead())
                            {
                                fs.Read(data, 0, data.Length);
                            }
                            objWorkZoneDetail.Base64Data = Convert.ToBase64String(data);
                        }
                        listWorkZoneDetail.Add(objWorkZoneDetail);
                        #endregion 
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            sr.Close();
            return listWorkZoneDetail;
        }

        public static string CorrectString(string strValue)
        {
            string val = strValue;
            if (val == null || val.Length < 1)
                return string.Empty;
            if (val.StartsWith("\"") && val.EndsWith("\""))
            {
                val = val.Substring(1, val.Length - 2);
            }
            return val;
        }

        #endregion 

     
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        
        private List<string> GetListStringFromCfg(string pathFile)
        {
            logger.Debug("BEGIN GetListStringFromCfg from pathFile = " + pathFile);
            List<string> listItem = new List<string>();
            if (File.Exists(pathFile))
            {
                string[] lines = System.IO.File.ReadAllLines(pathFile);
                // Display the file contents by using a foreach loop.
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    listItem.Add(line);
                }
            }
            return listItem;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }   
}
