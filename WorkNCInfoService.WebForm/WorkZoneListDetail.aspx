<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkZoneListDetail.aspx.cs" Inherits="WorkNCInfoService.WebForm.WorkZoneListDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
     .hiddencol
       {
           display: none;
       }
    </style>
     <script type="text/javascript">
         function checkFileExtension(elem) {
             var filePath = elem.value;
             document.getElementById('<%=txtImgName.ClientID%>').value = document.getElementById('<%=fileUploadImage.ClientID%>').value;
             if (filePath.indexOf('.') == -1)
                 return false;

             var validExtensions = ["bmp", "gif", "png", "jpg", "jpeg"];
             var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

             for (var i = 0; i < validExtensions.length; i++) {
                 if (ext == validExtensions[i]) {
                     return true;
                     break;
                 }
             }
             var message = document.getElementById('<%=lblMessageCheckFileImg.ClientID%>').textContent;

            document.getElementById('<%=txtImgName.ClientID%>').value = "";
             document.getElementById('<%=fileUploadImage.ClientID%>').value = "";
             alert(String.format(message + ": .bmp, .gif, .png, .jpg, .jpeg", ext.toUpperCase()));

             return false;

         }

         function checkFileExtensionWZD(elem) {
             var filePath = elem.value;
             document.getElementById('<%=txtImgWZD.ClientID%>').value = document.getElementById('<%=fileUploadImageWZD.ClientID%>').value;
             if (filePath.indexOf('.') == -1)
                 return false;

             var validExtensions = ["bmp", "gif", "png", "jpg", "jpeg"];
             var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

             for (var i = 0; i < validExtensions.length; i++) {
                 if (ext == validExtensions[i]) {
                     return true;
                     break;
                 }
             }
             var message = document.getElementById('<%=lblMessageCheckFileImg.ClientID%>').textContent;

             document.getElementById('<%=txtImgWZD.ClientID%>').value = "";
             document.getElementById('<%=fileUploadImageWZD.ClientID%>').value = "";
             alert(String.format(message + ": .bmp, .gif, .png, .jpg, .jpeg", ext.toUpperCase()));

             return false;

         }

         function CallFileUpload() {
             document.getElementById('<%=fileUploadImage.ClientID%>').click();

         }
         function CallFileUploadWZ() {
             document.getElementById('<%=fileUploadImageWZD.ClientID%>').click();

           }
        function CheckFileAppear() {
            var message = document.getElementById('<%=lblMessageCheckFileImg.ClientID%>').textContent;

             if (document.getElementById('<%=txtImgName.ClientID%>').value != "" && document.getElementById('<%=fileUploadImage.ClientID%>').value == "") {

                 return true;
             }
             if (document.getElementById('<%=txtImgName.ClientID%>').value == "") {
                 document.getElementById('<%=fileUploadImage.ClientID%>').value = ""
                alert(message);
                return true;
            }

            return true;
        }

    </script>
    <script type="text/javascript">
        function ValidateTextDate(i)
        {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d\/]+/g, '');
            }
        }
        function CallEdit(rowindex)
        {
            var i, CellValue, Row;
            i = parseInt(rowindex) + 1;
         
            var table = document.getElementById("<%=grdWorkZoneDetail.ClientID %>");

            var myHidden = document.getElementById('<%= HiddenWorkZoneDetailId.ClientID %>');
            myHidden.value = table.rows[i].cells[16].innerText;

            var button = document.getElementById("<%= btnEdit.ClientID %>");
            button.click();
        }
    </script>
    <asp:UpdatePanel runat="server" ID="panelUpdate">
        <ContentTemplate>
            <asp:Label ID="lblMessageCheckFileImg" runat="server" Text="Label" ForeColor="Red" style="display:none"></asp:Label>
            <asp:Panel runat="server" ID="panelMain">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="checkvalidate" />
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblWorkZoneName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkZoneName" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredWorkZoneName" runat="server" ControlToValidate="txtWorkZoneName" Display="Dynamic" ForeColor="Red" ValidationGroup="checkvalidate">*</asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="9" style="width:80px"></td>
                        <td>
                            <asp:Label ID="lblPartName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 262px">
                            <asp:TextBox ID="txtPartName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblWorkZonePath" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkZonePath" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblMachiningTimeTotal" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 262px">
                            <asp:TextBox ID="txtMachiningTimeTotal" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblModelDataProgramer" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtModelDataProgramer" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblFactoryName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 262px">
                            <asp:DropDownList ID="cbxFactory" runat="server" Width="255px" AutoPostBack="True" OnSelectedIndexChanged="cbxFactory_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldFactory" runat="server" ControlToValidate="cbxFactory" Display="Dynamic" ForeColor="Red" ValidationGroup="checkvalidate">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNCDataProgramer" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNCDataProgramer" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblMachineName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 262px">
                            <asp:DropDownList ID="cbxMachine" runat="server" Width="255px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requiredMachine" runat="server" ControlToValidate="cbxMachine" Display="Dynamic" ForeColor="Red" ValidationGroup="checkvalidate">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblProgramDate" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtProgramDate" runat="server" Width="80px" onkeyup='ValidateTextDate(this)'></asp:TextBox>
                            <asp:HyperLink ID="hplCalendar" runat="server" ImageUrl="~/Images/calendar.png"></asp:HyperLink>
                            <asp:RequiredFieldValidator ID="RequiredFieldDate" runat="server" ControlToValidate="txtProgramDate" Display="Dynamic" ForeColor="Red" ValidationGroup="checkvalidate">*</asp:RequiredFieldValidator>

                            <asp:CompareValidator ID="CompareValidatorDate" runat="server"
                                ControlToValidate="txtProgramDate" Display="Dynamic"
                                ForeColor="Red" Operator="DataTypeCheck"
                                ValidationGroup="checkvalidate" Type="Date">*
                            </asp:CompareValidator>

                        </td>
                        <td style="height: 28px">
                            <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="height: 28px; width: 262px">
                            <asp:DropDownList ID="cbxStatus" runat="server" Width="255px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 26px">
                            <asp:Label ID="lblModelName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="height: 26px">
                            <asp:TextBox ID="txtModelName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td style="height: 26px">
                            <asp:Label ID="lblComment" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="height: 26px; width: 262px;">
                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblParts" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtParts" runat="server" Width="250px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblPicture" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="width: 262px">
                            <asp:TextBox ID="txtImgName" runat="server" Width="130px" ReadOnly="True"></asp:TextBox>
                            <input id="btnBrowse" type="button" value="..."  onclick="CallFileUpload();"/>
                            <asp:Button ID="btnLoadImage" runat="server" Text="Button" OnClick="btnLoadImage_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:FileUpload ID="fileUploadImage" runat="server" onchange="return checkFileExtension(this);" style="display:none" />
                            
                        </td>
                        <td>
                        </td>
                        <td style="width: 262px">
                            <asp:Image ID="imgWorkZone" runat="server" class="img" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            
                        </td>
                        <td></td>
                        <td style="width: 300px"></td>
                    </tr>
                </table>
                <br />
                <p>
                    <asp:Button ID="btnBack" runat="server" Text="Button" OnClick="btnBack_Click" CausesValidation="False" />
                    <asp:Button ID="btnSave" runat="server" Text="Button" OnClick="btnSave_Click" ValidationGroup="checkvalidate" />
                </p>
                
                <asp:GridView ID="grdWorkZoneDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="False"
                    CssClass="gridView" Font-Size="Small" OnRowCreated="grdWorkZoneDetail_RowCreated" OnRowDataBound="grdWorkZoneDetail_RowDataBound" Width="99%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cb" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="No">
                          <ItemStyle Font-Underline="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="PathType" />
                        <asp:BoundField DataField="StockAllowance" />
                         <asp:BoundField DataField="Tolerance" />

                        <asp:BoundField DataField="NCFileName" />
                        <asp:BoundField DataField="MachineTime" />
                        <asp:BoundField DataField="MachineDistance" />

                        <asp:BoundField DataField="ToolShape" />
                        <asp:BoundField DataField="ToolDia" />
                        <asp:BoundField DataField="ToolConerR" />
                        <asp:BoundField DataField="HolderName" />

                        <asp:BoundField DataField="Spindle" />
                        <asp:BoundField DataField="CuttingFeedRate" />
                        <asp:BoundField DataField="ApproachFeedRate" />
                  
                        
                        <asp:TemplateField>
                             <ItemTemplate>
                                  <%# GetResource(string.Format("STATUS_{0}", Eval("Status").ToString()))%>
                             </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" Font-Size="Small"></ItemStyle>
                        </asp:TemplateField>

                        <asp:BoundField DataField="WorkZoneDetailId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                        <asp:BoundField DataField="WorkZoneId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                        Mode="NumericFirstLast" PageButtonCount="5" />
                    <PagerStyle CssClass="pagination" BackColor="White" />
                    <RowStyle CssClass="tr_body" />
                  
                </asp:GridView>
                <asp:Button ID="btnEdit" runat="server" Text="Button" OnClick="btnEdit_Click" style="height: 21px;margin-top:5px" />
            </asp:Panel>
            <asp:Panel ID="panelEidtDetail" runat="server" Visible="False">

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNo" runat="server" ReadOnly="True" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNcFileName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNCfileName" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPathType" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPathType" runat="server" ReadOnly="True" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCommentWZD" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCommetWZD" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblImage" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtImgWZD" runat="server" ReadOnly="True" Width="130px"></asp:TextBox>
                            <input id="btnBrowseImgWZD" type="button" value="..." onclick="CallFileUploadWZ();"/>
                            <asp:Button ID="btnLoadImgWZD" runat="server" Text="Button" OnClick="btnLoadImgWZD_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Image ID="imgWZD" runat="server" class="img" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSaveWZD" runat="server" Text="Button" OnClick="btnSaveWZD_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Button" OnClick="btnCancel_Click" />
                            <asp:FileUpload ID="fileUploadImageWZD" runat="server" onchange="return checkFileExtensionWZD(this);" style="display:none"/>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoadImage"/>
            <asp:PostBackTrigger ControlID="btnLoadImgWZD" />
        </Triggers>
    </asp:UpdatePanel>
     <asp:HiddenField ID="HiddenWorkZoneId" runat="server" />
     <asp:HiddenField ID="HiddenWorkZoneDetailId" runat="server" Value="" />
</asp:Content>
