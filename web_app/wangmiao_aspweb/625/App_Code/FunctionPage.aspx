<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunctionPage.aspx.cs" Inherits="FunctionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style2
        {
            width: 231px;
        }
        .style1
        {
            height: 20px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="查看" onclick="Button1_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="新增" onclick="Button2_Click" />
                </td>
                <td class="style2">
                    <asp:Button ID="Button3" runat="server" Text="修改" Width="40px" 
                        onclick="Button3_Click" />
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" Height="21px" Text="删除" 
                        onclick="Button4_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="2">
                    &nbsp;</td>
                <td class="style1" colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" DataKeyNames="Onum" 
                        DataSourceID="SqlDataSource1" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="Onum" HeaderText="Onum" ReadOnly="True" 
                                SortExpression="Onum" />
                            <asp:BoundField DataField="GN" HeaderText="GN" SortExpression="GN" />
                            <asp:BoundField DataField="PO#" HeaderText="PO#" SortExpression="PO#" />
                            <asp:BoundField DataField="SN" HeaderText="SN" SortExpression="SN" />
                            <asp:BoundField DataField="ST" HeaderText="ST" SortExpression="ST" />
                            <asp:BoundField DataField="RT" HeaderText="RT" SortExpression="RT" />
                            <asp:BoundField DataField="OW" HeaderText="OW" SortExpression="OW" />
                            <asp:BoundField DataField="Flag" HeaderText="Flag" SortExpression="Flag" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:HuiErPuConnectionString %>" 
                        SelectCommand="SELECT * FROM [Garbage]"></asp:SqlDataSource>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td class="style1" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
