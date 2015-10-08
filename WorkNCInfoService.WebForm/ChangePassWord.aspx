<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassWord.aspx.cs" Inherits="WorkNCInfoService.WebForm.ChangePassWord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat ="server" ID="updatepanel">
<ContentTemplate>
<asp:Panel runat ="server" ID="panel" DefaultButton="btnSave">
    
    <table>
        <tr>
            <td>
                <asp:Label ID="lblOldPassWord" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOldPassWord" runat="server" CssClass="textbox" 
                    TextMode="Password" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqValidatorPassWord" runat="server" 
                    ControlToValidate="txtOldPassWord" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="checkValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegValidatorPassWordLength" runat="server" 
                    ControlToValidate="txtOldPassWord" Display="Dynamic" ForeColor="Red" 
                    ValidationExpression="^[\s\S]{7,}$" ValidationGroup="checkValidation"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNewPassWord" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNewPassWord" runat="server" CssClass="textbox" 
                    TextMode="Password" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqValidatorNewPassWord" runat="server" 
                    ControlToValidate="txtNewPassWord" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="checkValidation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegValidatorPassNewWordLength" 
                    runat="server" ControlToValidate="txtNewPassWord" Display="Dynamic" 
                    ForeColor="Red" ValidationExpression="^[\s\S]{7,}$" 
                    ValidationGroup="checkValidation"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblConfirmPassWord" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassWord" runat="server" CssClass="textbox" 
                    TextMode="Password" Width="300px"></asp:TextBox>
                <asp:CompareValidator ID="CompValidatorPassWord" runat="server" 
                    ControlToCompare="txtNewPassWord" ControlToValidate="txtConfirmPassWord" 
                    Display="Dynamic" ForeColor="Red" ValidationGroup="checkValidation"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="ReqValidatorPassWordConfirm" runat="server" 
                    ControlToValidate="txtConfirmPassWord" Display="Dynamic" ForeColor="Red" 
                    ValidationGroup="checkValidation"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Button" CssClass="btn" 
                    onclick="btnSave_Click" ValidationGroup="checkValidation" />
            </td>
        </tr>
    </table>
    
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
