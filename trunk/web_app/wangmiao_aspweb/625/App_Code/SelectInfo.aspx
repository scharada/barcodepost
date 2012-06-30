<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectInfo.aspx.cs" Inherits="SelectInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 244px;
        }
        .style2
        {
            width: 428px;
        }
        .style3
        {
            width: 86px;
        }
    </style>
</head>
<body style="height: 540px">
    <form id="form2" runat="server">
    <div>
    
        <table style="width:100%; height: 348px;">
            <tr>
                <td class="style1">
                    查询主页</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    单一查询</td>
                <td class="style3">
                    流水号</td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    仓库代码</td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    订单号</td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="查询" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    序列号</td>
                <td class="style2" colspan="2">
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="查询" 
                        style="height: 21px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    组合查询(待完成) </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    序列号<asp:TextBox 
                        ID="txtONUM" runat="server"></asp:TextBox>
                </td>
                <td class="style2">
                    仓库号<asp:TextBox ID="txtWarehouseNo" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="查询" onclick="Button5_Click" 
                        style="height: 21px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    订单号<asp:TextBox ID="txtPO" runat="server"></asp:TextBox>
                </td>
                <td class="style2">
                    序列号<asp:TextBox ID="txtSN" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2">
                    <asp:Button ID="Button6" runat="server" Text="导出" />
                </td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style2" colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
