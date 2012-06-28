<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorLogIn.aspx.cs" Inherits="ErrorLogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 144px;
        }
        .style2
        {
            width: 144px;
            height: 25px;
        }
        .style3
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    登录首页</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    账号</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" style="color: #FF0000; font-size: medium" 
                        Text="账号或密码错误，请重新输入"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    密码</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" 
                        TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    </td>
                <td class="style3">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="登录" />
                </td>
                <td class="style3">
                    </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
