<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WorkNCInfoService.WebForm.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function checkFileExtension(elem) {
            var filePath = elem.value;
            
            document.getElementById('<%=txtWorkZone.ClientID%>').value = document.getElementById('<%=fileUploadExcel.ClientID%>').value;
            if (filePath.indexOf('.') == -1)
                return false;


            var validExtensions = 'xls';
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();

            if (ext == validExtensions) {
                return true;
              }
            var message = document.getElementById('<%=lblMessageCheckFile.ClientID%>').textContent;
            
            document.getElementById('<%=txtWorkZone.ClientID%>').value = "";
            document.getElementById('<%=fileUploadExcel.ClientID%>').value = "";
            alert(String.format(message, ext.toUpperCase()));
           
               return false;
              
        }
        function checkImgExtension(elem) {
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
         function CallFileUpload() {
             document.getElementById('<%=fileUploadExcel.ClientID%>').click();

         }
        function CallFileUploadImg() {
            document.getElementById('<%=fileUploadImage.ClientID%>').click();

        }
        function CheckFileAppear() {
            if (document.getElementById('<%=txtWorkZone.ClientID%>').value != "") {
                return true;
            }
            if (document.getElementById('<%=txtWorkZone.ClientID%>').value == "") {
                document.getElementById('<%=fileUploadExcel.ClientID%>').value = ""
                var message = document.getElementById('<%=lblMessageCheckFileAppear.ClientID%>').textContent;
                alert(message);
                return true;
            }

            return true;
        }
        
        function CheckFileAppearImg() {
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
    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>
            <table style="vertical-align: top">

                <tr>
                    <td style="vertical-align: top;">
                        
                        <br />
                        <table>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblWorkZone" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:Label ID="lblChekFileFormat" runat="server" ForeColor="Red" Style="display: block" Text="Label" Visible="False"></asp:Label>
                                    <asp:TextBox ID="txtWorkZone" runat="server" Width="250px" ReadOnly="True"></asp:TextBox>
                                    <input type="button" id="btnFind" value="..." onclick="CallFileUpload();" />
                                    <asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" OnClientClick="return CheckFileAppear();" ValidationGroup="checkrequireFile" />
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblWorkZoneName" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:TextBox ID="txtWorkZoneName" runat="server" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredWorkZoneName" runat="server" ControlToValidate="txtWorkZoneName" ForeColor="Red" Display="Dynamic" ValidationGroup="checkrequire">*</asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblFactoryName" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:DropDownList ID="cbxFactory" runat="server" Width="255px" AutoPostBack="True" OnSelectedIndexChanged="cbxFactory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requiredFactory" runat="server" ControlToValidate="cbxFactory" ForeColor="Red" Display="Dynamic" ValidationGroup="checkrequire">*</asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblMachineName" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:DropDownList ID="cbxMachine" runat="server" Width="255px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requiredMachine" runat="server" ControlToValidate="cbxMachine" ForeColor="Red" Display="Dynamic" ValidationGroup="checkrequire">*</asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblComment" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:TextBox ID="txtComment" runat="server" Width="250px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto">
                                    <asp:Label ID="lblPicture" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td style="width: auto">
                                    <asp:TextBox ID="txtImgName" runat="server" Width="250px"></asp:TextBox>
                                    <input id="btnImage" type="button" value="..." onclick="CallFileUploadImg();"/>
                                    <asp:Button ID="btnLoadImage" runat="server" OnClick="btnLoadImage_Click" OnClientClick="return CheckFileAppearImg();" ValidationGroup="checkrequire" />
                                    </td>
                                
                            </tr>
                            <tr>
                                <td style="width: auto"></td>
                                <td style="width: auto">
                                    <asp:Label ID="lblImgFolder" runat="server" Text="Label" Visible="False"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td ></td>
                                <td style="width: auto">
                                    <asp:Image ID="imgWorkZone" runat="server" class="img" ImageUrl="~/Images/no-image.png" />
                                </td>
                                
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="checkrequire" />
                        <asp:Label ID="lblMessageCheckFile" runat="server" Style="display: none"></asp:Label>
                        <asp:Label ID="lblMessageCheckFileAppear" runat="server" Style="display: none"></asp:Label>
                        <br />
                        <p>
                            <asp:Button ID="btnUpload" runat="server" ValidationGroup="checkrequire" OnClick="btnUpload_Click" Enabled="False" />
                            <asp:Button ID="btnClear" runat="server" CausesValidation="False" OnClick="btnClear_Click" />

                            <asp:FileUpload ID="fileUploadExcel" runat="server" Style="display: none" onchange="return checkFileExtension(this);" accept="application/vnd.ms-excel"/>
                            <asp:FileUpload ID="fileUploadImage" runat="server" Style="display: none" onchange="checkImgExtension(this);" AllowMultiple="true" accept="image/*"/>
                            <asp:Label ID="lblMessageCheckFileImg" runat="server" Style="display: none" Text="Label"></asp:Label>
                        </p>
                    </td>

                    <td style="width: auto"></td>
                    <td style="vertical-align: top">
                        <asp:Panel ID="panelProcess" runat="server">
                            <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                CssClass="gridView" OnRowCreated="GridView1_RowCreated">
                                <Columns>
                                    <asp:BoundField DataField="No" />
                                    <asp:BoundField DataField="PathType" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <div class="showimgingrid">
                                                <asp:Image ID="imgProcess" runat="server" ImageUrl="~/Images/no-image.png" class="imginGrid" />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                                    Mode="NumericFirstLast" PageButtonCount="5" />
                                <PagerStyle CssClass="pagination" BackColor="White" />
                                <RowStyle CssClass="tr_body" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>

            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoad" />
            <asp:PostBackTrigger ControlID ="btnLoadImage"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
