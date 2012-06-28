<%@ page language="C#" autoeventwireup="true" inherits="zhechaxun, App_Web_lj1yodkq" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" DataKeyNames="Onum" DataSourceID="SqlDataSource2" 
            ForeColor="Black" GridLines="Vertical" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Onum" HeaderText="Onum" ReadOnly="True" 
                    SortExpression="Onum" />
                <asp:BoundField DataField="GN" HeaderText="GN" SortExpression="GN" />
                <asp:BoundField DataField="column1" HeaderText="column1" 
                    SortExpression="column1" />
                <asp:BoundField DataField="ST" HeaderText="ST" SortExpression="ST" />
                <asp:BoundField DataField="SN" HeaderText="SN" SortExpression="SN" />
                <asp:BoundField DataField="RT" HeaderText="RT" SortExpression="RT" />
                <asp:BoundField DataField="OW" HeaderText="OW" SortExpression="OW" />
                <asp:BoundField DataField="Flag" HeaderText="Flag" SortExpression="Flag" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:HuiErPuConnectionString %>" 
            SelectCommand="SELECT [Onum], [GN], [PO#] AS column1, [ST], [SN], [RT], [OW], [Flag] FROM [Garbage]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:HuiErPuConnectionString2 %>" 
            onselecting="SqlDataSource1_Selecting" 
            SelectCommand="SELECT [Onum], [GN], [PO#] AS column1, [SN], [ST], [RT], [OW], [Flag] FROM [Garbage]">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
