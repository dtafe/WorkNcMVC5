<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkZoneList.aspx.cs" Inherits="WorkNCInfoService.WebForm.WorkZoneList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
 .hiddencol
   {
       display: none;
   }
</style>

 <script type="text/javascript">
            function rowno(rowindex)
            {
                var i, CellValue, Row;
                i = parseInt(rowindex) + 1;
                
                var table = document.getElementById("<%=grdWorkZoneList.ClientID %>");
                Row = table.rows[i];
               
                CellValue = Row.cells[12].innerText;
                window.location.replace("/WorkZoneListDetail.aspx?Id=" + CellValue + "");
                return false;
            }
 </script>

    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <table style="width: auto">
                <tr>
                    <td>
                        <asp:Label ID="lblWorkZoneName" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWorkZoneName" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="lblFactoryName" runat="server" Text="Label"></asp:Label>

                    </td>
                    <td>

                        <asp:DropDownList ID="cbxFactory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbxFactory_SelectedIndexChanged" Width="250px">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMachineName" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbxMachine" runat="server" Width="250px">
                        </asp:DropDownList>
                    </td>
                    </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProgramDate" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateFrom" runat="server" Width="80px"></asp:TextBox>
                        <asp:HyperLink ID="hplDateFrom" runat="server" ImageUrl="~/Images/calendar.png"></asp:HyperLink>
                        <strong><span style="font-size: medium">~</span></strong>
                        <asp:TextBox ID="txtDateTo" runat="server" Width="80px"></asp:TextBox>
                        <asp:HyperLink ID="hplDateTo" runat="server" ImageUrl="~/Images/calendar.png"></asp:HyperLink>
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
            <p>
                <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                 &nbsp;&nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Button" OnClick="btnDelete_Click" />
            </p>
            <asp:GridView ID="grdWorkZoneList" runat="server"
                AutoGenerateColumns="False" ShowHeader="False"
                CssClass="gridView" AllowPaging="True" OnRowCreated="grdWorkZoneList_RowCreated" Font-Size="Small" OnPageIndexChanging="grdWorkZoneList_PageIndexChanging" OnRowDataBound="grdWorkZoneList_RowDataBound" Width="99%">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cb" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Name">
                      <ItemStyle Font-Underline="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField >

                    <asp:BoundField DataField="ModelDataProgramer" />
                    <asp:BoundField DataField="NCDataProgramer" />
                    <asp:BoundField DataField="ProgramDate" />
                    <asp:BoundField DataField="ModelName" />
                    <asp:BoundField DataField="Parts" />
                    <asp:BoundField DataField="PartName" />
                    <asp:BoundField DataField="MachiningTimeTotal" />
                    <asp:BoundField DataField="FactoryName" />
                    <asp:BoundField DataField="MachineName" />
                     <asp:TemplateField>
                        <ItemTemplate>
                            <%# GetResource(string.Format("STATUS_{0}", Eval("Status").ToString()))%>
                        </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Font-Size="Small"></ItemStyle>
                     </asp:TemplateField>


                    <asp:BoundField DataField="WorkZoneId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    <asp:BoundField DataField="FactoryId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                    <asp:BoundField DataField="MachineId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" />
                </Columns>

                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"
                    Mode="NumericFirstLast" PageButtonCount="5" />
                <PagerStyle CssClass="pagination" BackColor="White" />
                <RowStyle CssClass="tr_body" />
            </asp:GridView>
            <br />

        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
