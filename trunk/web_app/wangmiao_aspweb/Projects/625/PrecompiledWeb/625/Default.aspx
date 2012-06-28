<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_gvbitfgp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Onum" DataSourceID="SqlDataSource1" 
        >
        <Columns>
            <asp:BoundField DataField="Onum" HeaderText="流水号" ReadOnly="True" 
                SortExpression="Onum" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:HuiErPuConnectionString %>" 
        SelectCommand="SELECT [Onum] FROM [Garbage]"></asp:SqlDataSource>
    <div>
    
    </div>
    </form>
</body>
</html>
