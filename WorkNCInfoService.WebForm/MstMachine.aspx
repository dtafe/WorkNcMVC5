<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MstMachine.aspx.cs" Inherits="WorkNCInfoService.WebForm.MstMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <asp:Panel ID="panelMain" runat="server" DefaultButton="btnSearch">

                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMachineNameSearch" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineNameSearch" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFactoryNameSearch" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbxFactorySearch" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="cbxFactory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:CheckBox ID="cbShowDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="cbShowDeleted_CheckedChanged" />
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
                <asp:GridView ID="grdMachine" runat="server"
                    AutoGenerateColumns="False" ShowHeader="False"
                    CssClass="gridView" AllowPaging="True" OnPageIndexChanging="grdMachine_PageIndexChanging" OnRowCreated="grdMachine_RowCreated">

                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cb" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="No" />
                        <asp:BoundField DataField="Name" />
                        <asp:BoundField DataField="isDeleted" />
                        <asp:BoundField DataField="MachineID" />
                        <asp:BoundField DataField="FactoryID" />
                    </Columns>
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                        Mode="NumericFirstLast" PageButtonCount="5" />
                    <PagerStyle CssClass="pagination" BackColor="White" />
                    <RowStyle CssClass="tr_body" />
                </asp:GridView>
                <asp:Label ID="lblNoRecord" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <p>
                    <asp:Button ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                </p>
            </asp:Panel>
            <asp:Panel ID="panelEdit" runat ="server" Visible="false">

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
                            <asp:Label ID="lblMachineName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMachineName" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblfactoryName" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbxfactory" runat="server" Width="250px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldFactory" runat="server" ControlToValidate="cbxfactory" Display="Dynamic" ForeColor="Red" ValidationGroup="checkerror"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <p>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="checkerror" />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click" />
                    <asp:HiddenField ID="hiddenMacID" runat="server" />
                    <asp:HiddenField ID="HiddenFacID" runat="server" />
                </p>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
