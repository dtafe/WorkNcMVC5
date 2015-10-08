<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MstCompany.aspx.cs" Inherits="WorkNCInfoService.WebForm.MstCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelMain" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelMain" runat="server" DefaultButton="btnSearch">
                <table style="width: auto">
                    <tr>
                        <td>
                            <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" Width="250px"></asp:TextBox>
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
                <asp:GridView ID="grdCompany" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                    CssClass="gridView" AllowPaging="True" OnPageIndexChanging="grdCompany_PageIndexChanging" OnRowCreated="grdCompany_RowCreated" Width="98%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>        
                        <asp:BoundField DataField="CompanyId" />                
                        <asp:BoundField DataField="CompanyName" />
                        <asp:BoundField DataField="Address1" />
                        <asp:BoundField DataField="Address2" />
                        <asp:BoundField DataField="TEL" />
                        <asp:BoundField DataField="FAX" />
                        <asp:BoundField DataField="isDeleted">
                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:BoundField>
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

            <asp:Panel ID="panelEdit" runat="server" Visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCompanyID" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyID" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredCompanyID" runat="server" ControlToValidate="txtCompanyID" ForeColor="Red" ValidationGroup="checkCompany" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="compareValidatorCompanyID" runat="server" ForeColor="Red" Type="Integer" ValidationGroup="checkCompany" ControlToValidate="txtCompanyID" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtName" runat="server" Width="250px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredCompanyName" runat="server" ControlToValidate="txtName" ForeColor="Red" ValidationGroup="checkCompany"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtAddress1" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtAddress2" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblTEL" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtTEL" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <td style="height: 28px">
                            <asp:Label ID="lblFAX" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 28px">
                            <asp:TextBox ID="txtFAX" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <p>
                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" ValidationGroup="checkCompany" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CausesValidation="False" />
                    <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CausesValidation="False" />
                </p>
            </asp:Panel>
        </ContentTemplate>      
    </asp:UpdatePanel>
</asp:Content>
