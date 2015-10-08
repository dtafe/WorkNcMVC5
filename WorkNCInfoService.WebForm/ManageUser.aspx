<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="WorkNCInfoService.WebForm.ManageUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="uppanel">
        
        <ContentTemplate>
            <asp:Panel runat="server" ID="panelMain">
                <table style="width: auto">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text="UserName:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:CheckBox ID="cbxOnlineUser" runat="server" AutoPostBack="True"
                    OnCheckedChanged="cbxOnlineUser_CheckedChanged" Text="Online User" />
                <br />
                <asp:GridView ID="grvUser" runat="server" AllowPaging="True" CssClass="gridView"
                    CellPadding="4" AutoGenerateColumns="False"
                    OnRowCreated="grvUser_RowCreated" ShowHeader="False"
                    EmptyDataText="No Data" OnPageIndexChanging="grvUser_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="UserName" />
                        <asp:BoundField DataField="Email" />

                        <asp:BoundField DataField="IsApproved">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="cbxPermission" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbxPermission_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Member">Member</asp:ListItem>
                                    <asp:ListItem Value="Chief">Chief</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cboAppPermission" runat="server" AutoPostBack="True" OnCheckedChanged="cboAppPermission_CheckChanged">
                                </asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lBtnLockUnLock" runat="server"
                                    OnClick="lBtnLockUnLock_Click">LinkButton</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lBtnResetPassword" runat="server"
                                    OnClick="lBtnResetPassword_Click">Reset PassWord</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                        Mode="NumericFirstLast" PageButtonCount="5" />
                    <PagerStyle CssClass="pagination" BackColor="White" />
                    <RowStyle CssClass="tr_body" />
                </asp:GridView>
                <asp:Label ID="lblNoRecord" runat="server" Text="No record found"
                    Visible="False"></asp:Label>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
