<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WorkNCInfoService.WebForm.Login" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="panelLogin" runat="server" DefaultButton="btnLogin">
        <asp:Label runat="server" ID="lblLoginFail" ForeColor="Red" Visible="false"></asp:Label>
        <div>
            <!--your content start-->

            <%--<p align="center"><a href="#" class="livebox">Click Here Trigger</a></p>--%>
            <table>
                <tr>
                    <td class="auto">
                        <asp:Label ID="lblUserName" runat="server" Text="UserName"></asp:Label>:</td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto">
                        <asp:Label ID="lblPassWord" runat="server"></asp:Label>:</td>
                    <td>
                        <asp:TextBox ID="txtPassWord" runat="server" Width="200px"
                            CssClass="textbox" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto">&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="cbRememberMe" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto">
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn"
                            OnClick="btnLogin_Click" />
                    </td>

                </tr>
            </table>
        </div> <!--your content end-->
        </asp:Panel>
</asp:Content>
