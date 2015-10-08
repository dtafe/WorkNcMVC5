<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MstFactory.aspx.cs" Inherits="WorkNCInfoService.WebForm.MstFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        function CallFileUpload() {
            document.getElementById('<%=fileUploadImage.ClientID%>').click();

         }
         function CheckFileAppear() {
             var message = document.getElementById('<%=lblMessageCheckFileImg.ClientID%>').textContent;
             
             if (document.getElementById('<%=txtImgName.ClientID%>').value != "" &&  document.getElementById('<%=fileUploadImage.ClientID%>').value == "") {
                
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
    <asp:UpdatePanel ID="updatePanelMain" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelMain" runat="server" DefaultButton="btnSearch">
                <table style="width: auto">
                    <tr>
                        <td>
                            <asp:Label ID="lblFactoryName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFactoryName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="cbShowDeleted" runat="server" Style="text-align: left" AutoPostBack="True" OnCheckedChanged="cbShowDeleted_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:GridView ID="grdFactiry" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                    CssClass="gridView" AllowPaging="True" OnPageIndexChanging="grdFactiry_PageIndexChanging" OnRowCreated="grdFactiry_RowCreated">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cb" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="No" />
                        <asp:BoundField DataField="Name" />
                        <asp:BoundField DataField="isDeleted">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:BoundField> 
                        
                        <asp:BoundField DataField="FactoryID" />
                        
                        <asp:BoundField DataField="ImageFile" />
                        
                    </Columns>

                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                        Mode="NumericFirstLast" PageButtonCount="5" />
                    <PagerStyle CssClass="pagination" BackColor="White" />
                    <RowStyle CssClass="tr_body" />

                </asp:GridView>

                <asp:Label ID="lblNoRecord" runat="server" Text="Label"></asp:Label>
                <br />
                <p>
                    <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                </p>
            </asp:Panel>

            <asp:Panel ID="panelEdit" runat="server" Visible="False">


                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNo" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNo" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredFactoryName" runat="server" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="checkFactory"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPicture" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtImgName" runat="server" ReadOnly="True" Width="250px"></asp:TextBox>
                            <input id="btnBrowse" type="button" value="..." onclick="CallFileUpload();"/>
                            <asp:Button ID="btnLoadImage" runat="server" OnClick="btnLoadImage_Click" OnClientClick="return CheckFileAppear();" ValidationGroup="checkFactory" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <div class="showimg" style="background-repeat: repeat">
                                <asp:Image ID="imgFactory" runat="server" class="img"/>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <p>
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="checkFactory" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CausesValidation="False" />
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CausesValidation="False" />
                    <asp:HiddenField ID="hiddenFacID" runat="server" />
                    
                    <asp:FileUpload ID="fileUploadImage" runat="server" onchange="return checkFileExtension(this);" style="display:none" accept="Image/*"/>
                   
                    <asp:Label ID="lblMessageCheckFileImg" runat="server" Text="Label" style="display:none"></asp:Label>
                </p>

            </asp:Panel>
        </ContentTemplate>
       <Triggers>
           <asp:PostBackTrigger ControlID="btnLoadImage"/>
       </Triggers>
    </asp:UpdatePanel>
</asp:Content>
